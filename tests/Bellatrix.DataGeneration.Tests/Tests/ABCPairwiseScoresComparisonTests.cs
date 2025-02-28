using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Diagnostics;
using Bellatrix.DataGeneration.Parameters;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.TestCaseGenerators;
using Bellatrix.DataGeneration.Models;

namespace Bellatrix.DataGeneration.Tests.Tests
{
    [TestFixture]
    public class ABCOptimizationBenchmarkTests
    {
        private const int Iterations = 10;
        private List<IInputParameter> _parameters;
        private List<HybridArtificialBeeColonyConfig> _parameterSets;
        private Dictionary<HybridArtificialBeeColonyConfig, List<double>> _abcScores = new();
        private Dictionary<HybridArtificialBeeColonyConfig, List<double>> _pairwiseScores = new();
        private List<Tuple<TestCase, double>> _sortedPairwiseScores = new();

        [SetUp]
        public void SetUp()
        {
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

        // 🔹 Initialize input parameters for testing different fields
        private void InitializeParameters()
        {
            _parameters = new List<IInputParameter>
            {
                new TextDataParameter(isManualMode: true, customValues: new[]
                {
                    new TestValue("Normal1", TestValueCategory.Valid),
                    new TestValue("BoundaryMin-1", TestValueCategory.BoundaryInvalid),
                    new TestValue("BoundaryMin", TestValueCategory.BoundaryValid),
                    new TestValue("BoundaryMax", TestValueCategory.BoundaryValid),
                    new TestValue("BoundaryMax+1", TestValueCategory.BoundaryInvalid),
                    new TestValue("Invalid1", TestValueCategory.Invalid)
                }),
                new EmailDataParameter(isManualMode: true, customValues: new[]
                {
                    new TestValue("test@mail.comMIN-1", TestValueCategory.BoundaryInvalid),
                    new TestValue("test@mail.comMIN", TestValueCategory.BoundaryValid),
                    new TestValue("test@mail.comMAX", TestValueCategory.BoundaryValid),
                    new TestValue("test@mail.comMAX+1", TestValueCategory.BoundaryInvalid),
                    new TestValue("test@mail.com", TestValueCategory.Valid),
                    new TestValue("invalid@mail", TestValueCategory.Invalid)
                }),
                new PhoneDataParameter(isManualMode: true, customValues: new[]
                {
                    new TestValue("+359888888888", TestValueCategory.Valid),
                    new TestValue("000000", TestValueCategory.Invalid)
                }),
                new TextDataParameter(isManualMode: true, customValues: new[]
                {
                    new TestValue("NormalX", TestValueCategory.Valid)
                }),
            };
        }

        // 🔹 Define different ABC parameter sets for benchmarking
        public void InitializeParameterSets()
        {
            _parameterSets = new List<HybridArtificialBeeColonyConfig>
            {
                // 🔹 Best general configuration: Balanced selection & mutation
                new HybridArtificialBeeColonyConfig
                {
                    FinalPopulationSelectionRatio = 0.6,
                    EliteSelectionRatio = 0.5,
                    TotalPopulationGenerations = 100,
                    MutationRate = 0.2,
                    AllowMultipleInvalidInputs = false,
                    DisableOnlookerSelection = true,
                    DisableScoutPhase = false
                },

                // 🔹 Stronger selection & refinement: Ideal when test cases must be stable
                new HybridArtificialBeeColonyConfig
                {
                    FinalPopulationSelectionRatio = 0.5,
                    EliteSelectionRatio = 0.4,
                    TotalPopulationGenerations = 100,
                    MutationRate = 0.3,
                    AllowMultipleInvalidInputs = false,
                    DisableOnlookerSelection = true,
                    DisableScoutPhase = true
                },

                // 🔹 Higher mutation rate: Ensures wider test coverage
                new HybridArtificialBeeColonyConfig
                {
                    FinalPopulationSelectionRatio = 0.6,
                    EliteSelectionRatio = 0.6,
                    TotalPopulationGenerations = 100,
                    MutationRate = 0.3,
                    AllowMultipleInvalidInputs = false,
                    DisableOnlookerSelection = true,
                    DisableScoutPhase = true
                },

                //// 🔹 Balanced exploitation & diversity: Great for complex test scenarios
                new HybridArtificialBeeColonyConfig
                {
                    FinalPopulationSelectionRatio = 0.5,
                    EliteSelectionRatio = 0.6,
                    TotalPopulationGenerations = 100,
                    MutationRate = 0.4,
                    AllowMultipleInvalidInputs = false,
                    DisableOnlookerSelection = true,
                    DisableScoutPhase = true
                },

                // 🔹 More diverse test cases: Prevents overfitting to high-scoring cases
                new HybridArtificialBeeColonyConfig
                {
                    FinalPopulationSelectionRatio = 0.4,
                    EliteSelectionRatio = 0.6,
                    TotalPopulationGenerations = 100,
                    MutationRate = 0.4,
                    AllowMultipleInvalidInputs = false,
                    DisableOnlookerSelection = true,
                    DisableScoutPhase = true
                },

                // 🔹 Balanced mutation & selection: Useful when both exploration and exploitation are needed
                new HybridArtificialBeeColonyConfig
                {
                    FinalPopulationSelectionRatio = 0.5,
                    EliteSelectionRatio = 0.5,
                    TotalPopulationGenerations = 100,
                    MutationRate = 0.4,
                    AllowMultipleInvalidInputs = false,
                    DisableOnlookerSelection = true,
                    DisableScoutPhase = true
                },

                // 🔹 Maximum exploration: Ensures high diversity, best for finding edge cases
                new HybridArtificialBeeColonyConfig
                {
                    FinalPopulationSelectionRatio = 0.4,
                    EliteSelectionRatio = 0.5,
                    TotalPopulationGenerations = 100,
                    MutationRate = 0.4,
                    AllowMultipleInvalidInputs = false,
                    DisableOnlookerSelection = true,
                    DisableScoutPhase = true
                }
            };
        }

        // 🔹 Precompute pairwise scores for baseline comparison
        private void PrecomputePairwiseScores()
        {
            var pairwiseTestCases = PairwiseTestCaseGenerator.GenerateTestCases(_parameters);
            var testCaseEvaluator = new TestCaseEvaluator();
            _sortedPairwiseScores = testCaseEvaluator.EvaluatePopulationToList(pairwiseTestCases.ToHashSet());
        }

        // 🔹 Run benchmarking for a given ABC parameter set
        private void RunBenchmarkForParameterSet(HybridArtificialBeeColonyConfig paramSet)
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
        private double RunSingleIteration(HybridArtificialBeeColonyConfig config)
        {
            var abcGenerator = new HybridArtificialBeeColonyTestCaseGenerator(config);

            var abcTestCases = abcGenerator.RunABCAlgorithm(_parameters);
            var testCaseEvaluator = new TestCaseEvaluator();
            var abcScores = testCaseEvaluator.EvaluatePopulationToDictionary(abcTestCases);
            double abcTotalScore = abcScores.Values.Sum();

            var topCount = (int)(_sortedPairwiseScores.Count * config.FinalPopulationSelectionRatio);
            var pairwiseTotalScore = _sortedPairwiseScores.Take(topCount).Sum(p => p.Item2);
            _pairwiseScores[config].Add(pairwiseTotalScore);

            return abcTotalScore;
        }

        // 🔹 Print results for each ABC parameter set
        private void PrintResultsForParameterSet(HybridArtificialBeeColonyConfig paramSet)
        {
            var avgAbcScore = _abcScores[paramSet].Average();
            var avgPairwiseScore = _pairwiseScores[paramSet].Average();
            var percentageImprovement = (avgAbcScore - avgPairwiseScore) / avgPairwiseScore * 100;

            Console.WriteLine($"\n========== Summary for Parameters: {paramSet} ==========");
            Console.WriteLine($"✅ ABC Avg Score: {avgAbcScore}");
            Console.WriteLine($"✅ Pairwise Avg Score: {avgPairwiseScore}");
            Console.WriteLine($"📈 Improvement Over Pairwise: {percentageImprovement:F2}%");

            Debug.WriteLine($"\n========== Summary for Parameters: {paramSet} ==========");
            Debug.WriteLine($"✅ ABC Avg Score: {avgAbcScore}");
            Debug.WriteLine($"✅ Pairwise Avg Score: {avgPairwiseScore}");
            Debug.WriteLine($"📈 Improvement Over Pairwise: {percentageImprovement:F2}%");
        }

        // 🔹 Print the best ABC parameters
        private void PrintBestABCParameters()
        {
            var bestABC = _abcScores.OrderByDescending(p => p.Value.Average()).First();
            Console.WriteLine("\n========== Best ABC Parameters ==========");
            Console.WriteLine($"Final Population Ratio: {bestABC.Key.FinalPopulationSelectionRatio}");
            Console.WriteLine($"Elite Selection Ratio: {bestABC.Key.EliteSelectionRatio}");
            Console.WriteLine($"Total Generations: {bestABC.Key.TotalPopulationGenerations}");
            Console.WriteLine($"Mutation Rate: {bestABC.Key.MutationRate}");
            Console.WriteLine($"Achieved Avg Score: {bestABC.Value.Average()}");

            Debug.WriteLine("\n========== Best ABC Parameters ==========");
            Debug.WriteLine($"Final Population Ratio: {bestABC.Key.FinalPopulationSelectionRatio}");
            Debug.WriteLine($"Elite Selection Ratio: {bestABC.Key.EliteSelectionRatio}");
            Debug.WriteLine($"Total Generations: {bestABC.Key.TotalPopulationGenerations}");
            Debug.WriteLine($"Mutation Rate: {bestABC.Key.MutationRate}");
            Debug.WriteLine($"Achieved Avg Score: {bestABC.Value.Average()}");
        }

        // 🔹 Print the best pairwise score
        private void PrintBestPairwisePerformance()
        {
            var bestPairwise = _pairwiseScores.OrderByDescending(p => p.Value.Average()).First();
            Console.WriteLine("\n========== Best Pairwise Performance ==========");
            Console.WriteLine($"Achieved Avg Score: {bestPairwise.Value.Average()} with ABC parameters: {bestPairwise.Key}");

            Debug.WriteLine("\n========== Best Pairwise Performance ==========");
            Debug.WriteLine($"Achieved Avg Score: {bestPairwise.Value.Average()} with ABC parameters: {bestPairwise.Key}");
        }
    }
}
