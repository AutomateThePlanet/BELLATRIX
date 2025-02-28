using System;
using System.Collections.Generic;
using System.Linq;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.TestCaseGenerators;
using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration;

public class HybridArtificialBeeColonyTestCaseGenerator
{
    private readonly HybridArtificialBeeColonyConfig _config;
    private readonly TestCaseEvaluator _testCaseEvaluator;
    private readonly Random _random = new Random(42);

    public HybridArtificialBeeColonyTestCaseGenerator(HybridArtificialBeeColonyConfig config)
    {
        _config = config;
        _testCaseEvaluator = new TestCaseEvaluator(config.AllowMultipleInvalidInputs);
    }

    public HashSet<TestCase> RunABCAlgorithm(List<IInputParameter> parameters)
    {
        HashSet<TestCase> currentPopulation = GenerateInitialPopulation(parameters);
        int populationSize = currentPopulation.Count;

        for (int currentGeneration = 0; currentGeneration < _config.TotalPopulationGenerations; currentGeneration++)
        {
            var evaluatedPopulation = _testCaseEvaluator.EvaluatePopulationToList(currentPopulation);
            int eliteCount = CalculateElitePopulationSize(populationSize);

            HashSet<TestCase> newGenerationPopulation = SelectElitePopulation(evaluatedPopulation, eliteCount);
            newGenerationPopulation = MaintainDiversePopulationOnlookerSelection(newGenerationPopulation, evaluatedPopulation, eliteCount);

            MutatePopulation(newGenerationPopulation, evaluatedPopulation, parameters, currentGeneration);
            PerformScoutPhaseIfNeeded(parameters, currentPopulation, newGenerationPopulation, currentGeneration);

            currentPopulation = newGenerationPopulation;
        }

        return LimitFinalPopulationBasedOnSelectionRatio(currentPopulation);
    }

    private HashSet<TestCase> MaintainDiversePopulationOnlookerSelection(HashSet<TestCase> currentPopulation, List<Tuple<TestCase, double>> evaluatedPopulation, int eliteCount)
    {
        if (_config.DisableOnlookerSelection)
        {
            return currentPopulation;
        }

        double totalScore = Math.Max(1, evaluatedPopulation.Sum(tc => tc.Item2));
        HashSet<TestCase> uniquePopulation = new HashSet<TestCase>(currentPopulation);

        var probabilitySelection = evaluatedPopulation
            .Select(tc => new { TestCase = tc.Item1, NormalizedScore = tc.Item2 / totalScore })
            .OrderByDescending(tc => tc.NormalizedScore * _random.NextDouble())
            .Take(Math.Max(1, (int)(_config.TotalPopulationGenerations * 0.05)))
            .Select(tc => tc.TestCase)
            .ToList();

        foreach (var testCase in probabilitySelection)
        {
            if (uniquePopulation.Count < _config.PopulationSize)
            {
                uniquePopulation.Add(testCase);
            }
        }

        return uniquePopulation;
    }

    private HashSet<TestCase> GenerateInitialPopulation(List<IInputParameter> parameters)
    {
        return ImprovedPairwiseTestCaseGenerator.GenerateTestCases(parameters).ToHashSet();
    }

    private int CalculateElitePopulationSize(int populationSize)
    {
        return Math.Max(1, (int)(populationSize * _config.EliteSelectionRatio));
    }

    private HashSet<TestCase> SelectElitePopulation(List<Tuple<TestCase, double>> evaluatedPopulation, int eliteCount)
    {
        return evaluatedPopulation
            .OrderByDescending(tc => tc.Item2)
            .Take(eliteCount)
            .Select(tc => tc.Item1)
            .ToHashSet();
    }

    // 🔹 Step 5: Apply Mutations to Non-Elite Population
    private void MutatePopulation(HashSet<TestCase> population, List<Tuple<TestCase, double>> evaluatedPopulation, List<IInputParameter> parameters, int iteration)
    {
        for (int i = CalculateElitePopulationSize(population.Count); i < evaluatedPopulation.Count; i++)
        {
            TestCase mutatedTestCase = ApplyMutation(evaluatedPopulation[i].Item1, parameters, iteration);
            population.Add(mutatedTestCase);
        }
    }

    private TestCase ApplyMutation(TestCase originalTestCase, List<IInputParameter> parameters, int iteration)
    {
        if (_random.NextDouble() >= _config.MutationRate)
        {
            return originalTestCase;
        }

        TestCase mutatedTestCase = MutateUsingSimulatedAnnealing(originalTestCase, parameters, iteration);
        return mutatedTestCase;
    }

    // 🔹 Mutation Strategy: Simulated Annealing-Based Mutation
    private TestCase MutateUsingSimulatedAnnealing(TestCase testCase, List<IInputParameter> parameters, int iteration)
    {
        int index = _random.Next(testCase.Values.Count);
        var availableValues = parameters[index].TestValues.ToList();

        if (availableValues.Count > 1)
        {
            testCase.Values[index] = availableValues[_random.Next(availableValues.Count)];
        }

        return testCase;
    }

    private void PerformScoutPhaseIfNeeded(List<IInputParameter> parameters, HashSet<TestCase> population, HashSet<TestCase> newPopulation, int iteration)
    {
        if (_config.DisableScoutPhase || iteration <= _config.TotalPopulationGenerations * _config.StagnationThresholdPercentage)
        {
            return;
        }

        var poorPerformingTestCases = population
            .Select(tc => Tuple.Create(tc, _testCaseEvaluator.Evaluate(tc)))
            .OrderBy(x => x.Item2)
            .Take((int)(_config.PopulationSize * 0.3))
            .ToList();

        foreach (var testCase in poorPerformingTestCases)
        {
            TestCase mutatedTestCase = ApplyMutation(testCase.Item1, parameters, iteration);
            newPopulation.Add(mutatedTestCase);
        }
    }

    private HashSet<TestCase> LimitFinalPopulationBasedOnSelectionRatio(HashSet<TestCase> population)
    {
        int finalSize = Math.Max(1, (int)(population.Count * _config.FinalPopulationSelectionRatio));

        return population
            .OrderByDescending(tc => _testCaseEvaluator.Evaluate(tc)) // Ensure best test cases are kept
            .Take(finalSize)
            .ToHashSet();
    }

}
