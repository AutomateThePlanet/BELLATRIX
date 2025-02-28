using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Diagnostics;
using Bellatrix.Web.Tests.MetaheuristicVersion2.Core;
using Bellatrix.Web.Tests.MetaheuristicVersion2.TestValueProviders;
using Bellatrix.Web;
using Bellatrix.DataGeneration.Core.Parameters;

namespace Bellatrix.DataGeneration.Tests.Tests
{
    [TestFixture]
    public class ABCOptimizationBenchmarkTests
    {
        private const int Iterations = 10;
        private List<IInputParameter> _parameters;
        private List<ABCParameterSet> _parameterSets;
        private Dictionary<ABCParameterSet, List<double>> _abcScores = new();
        private Dictionary<ABCParameterSet, List<double>> _pairwiseScores = new();
        private List<KeyValuePair<string[], double>> _sortedPairwiseScores = new();

        [SetUp]
        public void SetUp()
        {
            RegisterTestValueProviders();
            InitializeParameters();
            InitializeParameterSets();
            PrecomputePairwiseScores();
        }

        [Test]
        public void RunOptimizationBenchmark()
        {
            Console.WriteLine("\n========== Running ABC Parameter Optimization Benchmark ==========");
            Debug.WriteLine("\n========== Running ABC Parameter Optimization Benchmark ==========");

            foreach (var paramSet in _parameterSets)
            {
                RunBenchmarkForParameterSet(paramSet);
            }

            PrintBestABCParameters();
            PrintBestPairwisePerformance();
        }

        // 🔹 Register test value providers for different input types
        private void RegisterTestValueProviders()
        {
            ServicesCollection.Current.RegisterType<IComponentTestValuesProviderStrategy<Email>, EmailTestValueProviderStrategy>();
            ServicesCollection.Current.RegisterType<IComponentTestValuesProviderStrategy<Phone>, PhoneTestValueProviderStrategy>();
            ServicesCollection.Current.RegisterType<IComponentTestValuesProviderStrategy<TextField>, TextFieldTestValueProviderStrategy>();
        }

        // 🔹 Initialize input parameters for testing different fields
        private void InitializeParameters()
        {
            _parameters = new List<IInputParameter>
            {
                new ComponentInputParameter<TextField>(isManualMode: true, customValues: new[]
                {
                    Tuple.Create("Normal1", TestValueCategory.Valid),
                    Tuple.Create("BoundaryMin-1", TestValueCategory.BoundaryInvalid),
                    Tuple.Create("BoundaryMin", TestValueCategory.BoundaryValid),
                    Tuple.Create("BoundaryMax", TestValueCategory.BoundaryValid),
                    Tuple.Create("BoundaryMax+1", TestValueCategory.BoundaryInvalid),
                    Tuple.Create("Invalid1", TestValueCategory.Invalid)
                }),
                new ComponentInputParameter<Email>(isManualMode: true, customValues: new[]
                {
                    Tuple.Create("test@mail.comMIN-1", TestValueCategory.BoundaryInvalid),
                    Tuple.Create("test@mail.comMIN", TestValueCategory.BoundaryValid),
                    Tuple.Create("test@mail.comMAX", TestValueCategory.BoundaryValid),
                    Tuple.Create("test@mail.comMAX+1", TestValueCategory.BoundaryInvalid),
                    Tuple.Create("test@mail.com", TestValueCategory.Valid),
                    Tuple.Create("invalid@mail", TestValueCategory.Invalid)
                }),
                new ComponentInputParameter<Phone>(isManualMode: true, customValues: new[]
                {
                    Tuple.Create("+359888888888", TestValueCategory.Valid),
                    Tuple.Create("000000", TestValueCategory.Invalid)
                }),
                new ComponentInputParameter<TextField>(isManualMode: true, customValues: new[]
                {
                    Tuple.Create("NormalX", TestValueCategory.Valid)
                }),
            };
        }

        // 🔹 Define different ABC parameter sets for benchmarking
        public void InitializeParameterSets()
        {
            _parameterSets = new List<ABCParameterSet>()
            {
                // 🔹 Best general configuration: Balanced selection & mutation
                new ABCParameterSet(
                    selectionRatio: 0.6,         // Selects 60% of test cases for the next generation
                    eliteSelectionRatio: 0.5,    // Keeps 50% of the best test cases unchanged
                    maxIterations: 70,           // Runs for 70 generations
                    mutationRate: 0.2),          // Introduces minimal mutation (only 20% chance)
                
                // 🔹 Stronger selection & refinement: Ideal when test cases must be stable
                new ABCParameterSet(
                    selectionRatio: 0.6,
                    eliteSelectionRatio: 0.6,    // Retains more top test cases (60%)
                    maxIterations: 100,         // More iterations (100) for fine-tuning
                    mutationRate: 0.3),         // Moderate mutation for controlled variation
                
                // 🔹 Higher mutation rate: Ensures wider test coverage
                new ABCParameterSet(
                    selectionRatio: 0.6,
                    eliteSelectionRatio: 0.6,
                    maxIterations: 100,
                    mutationRate: 0.4),         // Higher mutation (40%) for increased diversity
                
                // 🔹 Balanced exploitation & diversity: Great for complex test scenarios
                new ABCParameterSet(
                    selectionRatio: 0.5,
                    eliteSelectionRatio: 0.6,
                    maxIterations: 100,
                    mutationRate: 0.4),         

                // 🔹 More diverse test cases: Prevents overfitting to high-scoring cases
                new ABCParameterSet(
                    selectionRatio: 0.4,
                    eliteSelectionRatio: 0.6,
                    maxIterations: 100,
                    mutationRate: 0.4),         

                // 🔹 Balanced mutation & selection: Useful when both exploration and exploitation are needed
                new ABCParameterSet(
                    selectionRatio: 0.5,
                    eliteSelectionRatio: 0.5,
                    maxIterations: 100,
                    mutationRate: 0.4),         

                // 🔹 Maximum exploration: Ensures high diversity, best for finding edge cases
                new ABCParameterSet(
                    selectionRatio: 0.4,
                    eliteSelectionRatio: 0.5,
                    maxIterations: 100,
                    mutationRate: 0.4),
            };
        }

        // 🔹 Precompute pairwise scores for baseline comparison
        private void PrecomputePairwiseScores()
        {
            var pairwiseTestCases = ImprovedPairwiseTestCaseGenerator.GenerateTestCases(_parameters);
            var testCaseEvaluator = new TestCaseEvaluator();
            var pairwiseScores = testCaseEvaluator.EvaluatePopulationToDictionary(pairwiseTestCases, _parameters);

            _sortedPairwiseScores = pairwiseScores
                .OrderByDescending(x => x.Value)
                .ThenBy(x => string.Join(",", x.Key))
                .ToList();
        }

        // 🔹 Run benchmarking for a given ABC parameter set
        private void RunBenchmarkForParameterSet(ABCParameterSet paramSet)
        {
            Console.WriteLine($"\n========== Testing ABC with Parameters: {paramSet} ==========");
            _abcScores[paramSet] = new List<double>();
            _pairwiseScores[paramSet] = new List<double>();

            for (var i = 0; i < Iterations; i++)
            {
                var abcTotalScore = RunSingleIteration(paramSet);
                _abcScores[paramSet].Add(abcTotalScore);
            }

            PrintResultsForParameterSet(paramSet);
        }

        // 🔹 Run a single iteration of ABC optimization
        private double RunSingleIteration(ABCParameterSet paramSet)
        {
            var abcGenerator = new HybridArtificialBeeColonyTestCaseGenerator(
                selectionRatio: paramSet.SelectionRatio,
                eliteSelectionRatio: paramSet.EliteSelectionRatio,
                maxIterations: paramSet.MaxIterations,
                mutationRate: paramSet.MutationRate,
                allowMultipleInvalidInputs: false,
                disableOnlookerSelection: true,
                disableScoutPhase: false);

            var abcTestCases = abcGenerator.RunABCAlgorithm(_parameters);
            var testCaseEvaluator = new TestCaseEvaluator();
            var abcScores = testCaseEvaluator.EvaluatePopulationToDictionary(abcTestCases, _parameters);
            double abcTotalScore = abcScores.Values.Sum();

            var topCount = (int)(_sortedPairwiseScores.Count * paramSet.SelectionRatio);
            var pairwiseTotalScore = _sortedPairwiseScores.Take(topCount).Sum(p => p.Value);
            _pairwiseScores[paramSet].Add(pairwiseTotalScore);

            return abcTotalScore;
        }

        // 🔹 Print results for each ABC parameter set
        private void PrintResultsForParameterSet(ABCParameterSet paramSet)
        {
            var avgAbcScore = _abcScores[paramSet].Average();
            var avgPairwiseScore = _pairwiseScores[paramSet].Average();
            var percentageImprovement = (avgAbcScore - avgPairwiseScore) / avgPairwiseScore * 100;

            Console.WriteLine($"\n========== Summary for Parameters: {paramSet} ==========");
            Console.WriteLine($"✅ ABC Avg Score: {avgAbcScore}");
            Console.WriteLine($"✅ Pairwise Avg Score: {avgPairwiseScore}");
            Console.WriteLine($"📈 Improvement Over Pairwise: {percentageImprovement:F2}%");
        }

        // 🔹 Print the best ABC parameters
        private void PrintBestABCParameters()
        {
            var bestABC = _abcScores.OrderByDescending(p => p.Value.Average()).First();
            Console.WriteLine("\n========== Best ABC Parameters ==========");
            Console.WriteLine($"Selection Ratio: {bestABC.Key.SelectionRatio}");
            Console.WriteLine($"Elite Selection Ratio: {bestABC.Key.EliteSelectionRatio}");
            Console.WriteLine($"Max Iterations: {bestABC.Key.MaxIterations}");
            Console.WriteLine($"Mutation Rate: {bestABC.Key.MutationRate}");
            Console.WriteLine($"Achieved Avg Score: {bestABC.Value.Average()}");
        }

        // 🔹 Print the best pairwise score
        private void PrintBestPairwisePerformance()
        {
            var bestPairwise = _pairwiseScores.OrderByDescending(p => p.Value.Average()).First();
            Console.WriteLine("\n========== Best Pairwise Performance ==========");
            Console.WriteLine($"Achieved Avg Score: {bestPairwise.Value.Average()} with ABC parameters: {bestPairwise.Key}");
        }
    }
}
