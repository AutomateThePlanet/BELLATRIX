using Bellatrix.DataGeneration;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.TestCaseGenerators;
using Bellatrix.DataGeneration.Models;
using System.Linq;
using Bellatrix.DataGeneration.Parameters;

// 🐝 Hybrid Artificial Bee Colony (ABC) Test Case Generator
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

    public List<TestCase> RunABCAlgorithm(List<IInputParameter> parameters)
    {
        List<TestCase> currentPopulation = GenerateInitialPopulation(parameters);
        int populationSize = currentPopulation.Count;

        for (int currentGeneration = 0; currentGeneration < _config.TotalPopulationGenerations; currentGeneration++)
        {
            var evaluatedPopulation = _testCaseEvaluator.EvaluatePopulationToList(currentPopulation);
            int eliteCount = CalculateElitePopulationSize(populationSize);

            List<TestCase> newGenerationPopulation = SelectElitePopulation(evaluatedPopulation, eliteCount);
            MutatePopulation(newGenerationPopulation, evaluatedPopulation, parameters, currentGeneration);

            currentPopulation = newGenerationPopulation;
        }

        return currentPopulation;
    }

    private List<TestCase> GenerateInitialPopulation(List<IInputParameter> parameters)
    {
        return ImprovedPairwiseTestCaseGenerator.GenerateTestCases(parameters);
    }

    private int CalculateElitePopulationSize(int populationSize)
    {
        return Math.Max(1, (int)(populationSize * _config.EliteSelectionRatio));
    }

    private List<TestCase> SelectElitePopulation(List<Tuple<TestCase, double>> evaluatedPopulation, int eliteCount)
    {
        return evaluatedPopulation.Take(eliteCount).Select(tc => tc.Item1).ToList();
    }

    private void MutatePopulation(List<TestCase> population, List<Tuple<TestCase, double>> evaluatedPopulation, List<IInputParameter> parameters, int iteration)
    {
        for (int i = CalculateElitePopulationSize(population.Count); i < evaluatedPopulation.Count; i++)
        {
            TestCase mutatedTestCase = ApplyMutation(evaluatedPopulation[i].Item1, parameters, iteration);
            population.Add(mutatedTestCase);
        }
    }

    private TestCase ApplyMutation(TestCase originalTestCase, List<IInputParameter> parameters, int iteration)
    {
        if (_random.NextDouble() >= _config.MutationRate) return originalTestCase;
        return MutateUsingSimulatedAnnealing(originalTestCase, parameters, iteration);
    }

    private TestCase MutateUsingSimulatedAnnealing(TestCase testCase, List<IInputParameter> parameters, int iteration)
    {
        TestCase mutatedTestCase = new TestCase { Values = testCase.Values.ToList() };
        int index = _random.Next(mutatedTestCase.Values.Count);
        var availableValues = parameters[index].TestValues.ToList();

        if (availableValues.Count > 1)
        {
            double temperature = _config.InitialTemperature * Math.Exp(-_config.CoolingRate * iteration);
            TestValue newValue = availableValues[_random.Next(availableValues.Count)];

            double currentScore = _testCaseEvaluator.Evaluate(testCase);
            mutatedTestCase.Values[index] = newValue;
            double newScore = _testCaseEvaluator.Evaluate(mutatedTestCase);

            if (newScore > currentScore || _random.NextDouble() < Math.Exp((newScore - currentScore) / temperature))
            {
                return mutatedTestCase;
            }
        }
        return testCase;
    }
}
