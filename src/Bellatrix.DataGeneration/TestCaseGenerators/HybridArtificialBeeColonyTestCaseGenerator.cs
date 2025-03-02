using System;
using System.Collections.Generic;
using System.Linq;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.TestCaseGenerators;
using Bellatrix.DataGeneration.Parameters;
using Bellatrix.DataGeneration.OutputGenerators;
using AngleSharp.Common;
using System.Diagnostics;

namespace Bellatrix.DataGeneration;

public class HybridArtificialBeeColonyTestCaseGenerator
{
    private readonly HybridArtificialBeeColonyConfig _config;
    private readonly TestCaseEvaluator _testCaseEvaluator;
    private readonly Random _random = new Random(42);
    private int _initialPopulationSize;
    private int _elitCount;
    public HybridArtificialBeeColonyTestCaseGenerator(HybridArtificialBeeColonyConfig config)
    {
        _config = config;
        _testCaseEvaluator = new TestCaseEvaluator(config.AllowMultipleInvalidInputs);
    }

    // 🔹 Public API: Generates and outputs optimized test cases
    public void GenerateTestCases(string methodName, List<IInputParameter> parameters, TestCaseCategory testCaseCategoty = TestCaseCategory.All)
    {
        var testCases = RunABCAlgorithm(parameters);
        _config.OutputGenerator.GenerateOutput(methodName, testCases, testCaseCategoty);
    }

    // 🔹 Public API: Returns the optimized test cases
    public HashSet<TestCase> GetGeneratedTestCases(List<IInputParameter> parameters)
    {
        return RunABCAlgorithm(parameters);
    }

    public HashSet<TestCase> RunABCAlgorithm(List<IInputParameter> parameters)
    {
        HashSet<TestCase> evaluatedPopulation = GenerateInitialPopulation(parameters);
        _initialPopulationSize = evaluatedPopulation.Count;
        _elitCount = CalculateElitePopulationSize();

        for (int currentGeneration = 0; currentGeneration < _config.TotalPopulationGenerations; currentGeneration++)
        {
            _testCaseEvaluator.EvaluatePopulation(evaluatedPopulation);

            // ✅ Keep both elite & non-elite populations
            HashSet<TestCase> elitePopulation = SelectElitePopulation(evaluatedPopulation, _elitCount);
            HashSet<TestCase> nonElitePopulation = new HashSet<TestCase>(evaluatedPopulation.Except(elitePopulation));

            // ✅ Maintain diversity in non-elite population
            nonElitePopulation = MaintainDiversePopulationOnlookerSelection(nonElitePopulation, evaluatedPopulation, _elitCount);

            // ✅ Mutate only the non-elite population
            MutatePopulation(evaluatedPopulation, nonElitePopulation, parameters, currentGeneration);

            // ✅ Merge elite and non-elite populations to keep full size
            //evaluatedPopulation = new HashSet<TestCase>(elitePopulation.Concat(nonElitePopulation));

            // ✅ Perform scout phase if needed
            PerformScoutPhaseIfNeeded(parameters, evaluatedPopulation, nonElitePopulation, currentGeneration);
        }

        return LimitFinalPopulationBasedOnSelectionRatio(evaluatedPopulation);
    }


    private HashSet<TestCase> MaintainDiversePopulationOnlookerSelection(HashSet<TestCase> nonElitPopulation, HashSet<TestCase> evaluatedPopulation, int eliteCount)
    {
        if (!_config.EnableOnlookerSelection)
        {
            return nonElitPopulation;
        }

        double totalScore = Math.Max(1, evaluatedPopulation.Sum(tc => tc.Score));
        HashSet<TestCase> uniquePopulation = new HashSet<TestCase>(nonElitPopulation);

        // Select test cases with weighted probability
        var probabilitySelection = evaluatedPopulation
            .OrderByDescending(tc => tc.Score / totalScore + _random.NextDouble() * _config.OnlookerSelectionRatio) // Adds slight randomness for diversity
            .Take(Math.Max(1, (int)(_config.FinalPopulationSelectionRatio * _config.OnlookerSelectionRatio))) // Adjusted selection size
            .ToList();

        foreach (var testCase in probabilitySelection)
        {
            if (uniquePopulation.Count < _config.FinalPopulationSelectionRatio)
            {
                uniquePopulation.Add(testCase);
            }
        }

        // If population is still below expected size, add more from evaluated cases
        if (uniquePopulation.Count < _config.FinalPopulationSelectionRatio)
        {
            foreach (var testCase in evaluatedPopulation)
            {
                if (uniquePopulation.Count >= _config.FinalPopulationSelectionRatio)
                {
                    break;
                }
                uniquePopulation.Add(testCase);
            }
        }

        return uniquePopulation;
    }


    private HashSet<TestCase> GenerateInitialPopulation(List<IInputParameter> parameters)
    {
        return PairwiseTestCaseGenerator.GenerateTestCases(parameters).ToHashSet();
    }

    private int CalculateElitePopulationSize()
    {
        return Math.Max(1, (int)(_initialPopulationSize * _config.EliteSelectionRatio));
    }

    private HashSet<TestCase> SelectElitePopulation(HashSet<TestCase> evaluatedPopulation, int eliteCount)
    {
        return evaluatedPopulation
            .OrderByDescending(tc => tc.Score)
            .Take(eliteCount)
            .ToHashSet();
    }

    // 🔹 Step 5: Apply Mutations to Non-Elite Population
    private void MutatePopulation(HashSet<TestCase> evaluatedPopulation, HashSet<TestCase> nonElitPopulation, List<IInputParameter> parameters, int iteration)
    {
        HashSet<TestCase> mutatedCases = new HashSet<TestCase>();

        for (int i = 0; i < nonElitPopulation.Count; i++)
        {
            TestCase originalTestCase = nonElitPopulation.GetItemByIndex(i);
            TestCase mutatedTestCase = ApplyMutation(originalTestCase, parameters, iteration);
            // Ensure the mutated test case is unique before adding it
            int originalCount = evaluatedPopulation.Count;

            if (_config.EnforceMutationUniqueness)
            {
                double originalScore = _testCaseEvaluator.Evaluate(originalTestCase, evaluatedPopulation);
                double mutatedScore = _testCaseEvaluator.Evaluate(mutatedTestCase, evaluatedPopulation);

                if (!mutatedTestCase.Equals(originalTestCase)
                    && !evaluatedPopulation.Contains(mutatedTestCase)
                    && mutatedScore > originalScore)
                {
                    evaluatedPopulation.RemoveWhere(tc => tc.Equals(originalTestCase));
                    evaluatedPopulation.Add(mutatedTestCase);
                }
            }
            else
            {
                if (!mutatedTestCase.Equals(originalTestCase) && !evaluatedPopulation.Contains(mutatedTestCase))
                {
                    evaluatedPopulation.RemoveWhere(tc => tc.Equals(originalTestCase));
                    evaluatedPopulation.Add(mutatedTestCase);
                }
            }

        }
    }

    private TestCase ApplyMutation(TestCase originalTestCase, List<IInputParameter> parameters, int iteration)
    {
        if (_random.NextDouble() >= _config.MutationRate)
        {
            return originalTestCase;
        }

        // Create a deep copy of the test case before mutating
        TestCase mutatedTestCase = new TestCase { Values = originalTestCase.Values.Select(v => new TestValue(v.Value, v.Category)).ToList() };

        int index = _random.Next(mutatedTestCase.Values.Count);
        var availableValues = parameters[index].TestValues.ToList();

        if (availableValues.Count > 1)
        {
            mutatedTestCase.Values[index] = availableValues[_random.Next(availableValues.Count)];
        }

        return mutatedTestCase;
    }


    private void PerformScoutPhaseIfNeeded(List<IInputParameter> parameters, HashSet<TestCase> evaluatedPopulation, HashSet<TestCase> nonElitPopulation, int iteration)
    {
        if (!_config.EnableScoutPhase || iteration <= _config.TotalPopulationGenerations * _config.StagnationThresholdPercentage)
        {
            return;
        }

        var poorPerformingTestCases = evaluatedPopulation
            .Select(tc => Tuple.Create(tc, _testCaseEvaluator.Evaluate(tc, evaluatedPopulation)))
            .OrderBy(x => x.Item2)
            .Take((int)(_config.FinalPopulationSelectionRatio * _config.ScoutSelectionRatio))
            .ToList();

        foreach (var testCase in poorPerformingTestCases)
        {
            TestCase mutatedTestCase = ApplyMutation(testCase.Item1, parameters, iteration);
            nonElitPopulation.Add(mutatedTestCase);
        }
    }

    private HashSet<TestCase> LimitFinalPopulationBasedOnSelectionRatio(HashSet<TestCase> finalPopulation)
    {
        int finalSize = Math.Max(1, (int)(finalPopulation.Count * _config.FinalPopulationSelectionRatio));

        var sortedPopulation = finalPopulation
            .OrderByDescending(tc => _testCaseEvaluator.Evaluate(tc, finalPopulation));
        var limitedFinalPopulation = sortedPopulation.Take(finalSize);

        var result = limitedFinalPopulation.ToHashSet();
        // 🔹 Debugging: Print all finally selected test cases with values and scores
        Debug.WriteLine("============= FINAL SELECTED TEST CASES =============");
        foreach (var testCase in result)
        {
            double score = _testCaseEvaluator.Evaluate(testCase, finalPopulation);
            string values = string.Join(", ", testCase.Values.Select(v => v.Value));
            Debug.WriteLine($"Test Case: [{values}] | Score: {score}");
        }
        Debug.WriteLine("=====================================================");

        return result;
    }

}
