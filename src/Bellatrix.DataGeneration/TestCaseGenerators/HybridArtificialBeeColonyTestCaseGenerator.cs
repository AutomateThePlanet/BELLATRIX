using Bellatrix.DataGeneration;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.TestCaseGenerators;


// 🐝 Hybrid Artificial Bee Colony (ABC) Test Case Generator
// This algorithm generates optimized test cases using an evolutionary approach
// based on the behavior of honeybee colonies.
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

    public void GenerateTestCases(string methodName, List<IInputParameter> parameters)
    {
        var testCases = RunABCAlgorithm(parameters);
        _config.OutputGenerator.GenerateOutput(methodName, testCases);
    }

    public List<string[]> GetGeneratedTestCases(List<IInputParameter> parameters)
    {
        return RunABCAlgorithm(parameters);
    }

    public List<string[]> RunABCAlgorithm(List<IInputParameter> parameters)
    {
        List<string[]> currentPopulation = GenerateInitialPopulation(parameters);
        int populationSize = currentPopulation.Count;

        for (int currentGeneration = 0; currentGeneration < _config.TotalPopulationGenerations; currentGeneration++)
        {
            var evaluatedPopulation = _testCaseEvaluator.EvaluatePopulationToList(currentPopulation, parameters);
            int eliteCount = CalculateElitePopulationSize(populationSize);

            List<string[]> newGenerationPopulation = SelectElitePopulation(evaluatedPopulation, eliteCount);
            newGenerationPopulation = MaintainDiversePopulationOnlookerSelection(newGenerationPopulation, evaluatedPopulation, eliteCount);

            MutatePopulation(newGenerationPopulation, evaluatedPopulation, parameters, currentGeneration);
            PerformScoutPhaseIfNeeded(parameters, currentPopulation, newGenerationPopulation, currentGeneration);

            currentPopulation = newGenerationPopulation;
        }

        return LimitFinalPopulationBasedOnSelectionRatio(currentPopulation, populationSize);
    }

    private List<string[]> GenerateInitialPopulation(List<IInputParameter> parameters)
    {
        return ImprovedPairwiseTestCaseGenerator.GenerateTestCases(parameters);
    }

    private int CalculateElitePopulationSize(int populationSize)
    {
        return Math.Max(1, (int)(populationSize * _config.EliteSelectionRatio));
    }

    private List<string[]> SelectElitePopulation(List<Tuple<string[], double>> evaluatedPopulation, int eliteCount)
    {
        return evaluatedPopulation.Take(eliteCount).Select(tc => tc.Item1).ToList();
    }

    private List<string[]> MaintainDiversePopulationOnlookerSelection(List<string[]> currentPopulation, List<Tuple<string[], double>> evaluatedPopulation, int eliteCount)
    {
        if (_config.DisableOnlookerSelection) return currentPopulation;

        double totalScore = Math.Max(1, evaluatedPopulation.Sum(tc => tc.Item2));
        HashSet<string[]> uniquePopulation = new HashSet<string[]>(currentPopulation, new StructuralEqualityComparer());

        var probabilitySelection = evaluatedPopulation
            .Select(tc => new { TestCase = tc.Item1, NormalizedScore = tc.Item2 / totalScore })
            .OrderByDescending(tc => tc.NormalizedScore * _random.NextDouble())
            .Take(Math.Max(1, (int)(currentPopulation.Count * _config.OnlookerSelectionRatio)))
            .Select(tc => tc.TestCase)
            .ToList();

        foreach (var testCase in probabilitySelection)
        {
            if (uniquePopulation.Count < currentPopulation.Count)
            {
                uniquePopulation.Add(testCase);
            }
        }

        return uniquePopulation.ToList();
    }

    private void MutatePopulation(List<string[]> population, List<Tuple<string[], double>> evaluatedPopulation, List<IInputParameter> parameters, int iteration)
    {
        for (int i = CalculateElitePopulationSize(population.Count); i < evaluatedPopulation.Count; i++)
        {
            string[] mutatedTestCase = ApplyMutation(evaluatedPopulation[i].Item1, parameters, iteration);
            population.Add(mutatedTestCase);
        }
    }

    private string[] ApplyMutation(string[] originalTestCase, List<IInputParameter> parameters, int iteration)
    {
        if (_random.NextDouble() >= _config.MutationRate) return originalTestCase;
        return MutateUsingSimulatedAnnealing(originalTestCase, parameters, iteration);
    }

    private string[] MutateUsingSimulatedAnnealing(string[] testCase, List<IInputParameter> parameters, int iteration)
    {
        string[] mutatedTestCase = (string[])testCase.Clone();
        int index = _random.Next(mutatedTestCase.Length);
        var availableValues = parameters[index].TestValues.Select(tv => tv.Value).ToList();

        if (availableValues.Count > 1)
        {
            double temperature = _config.InitialTemperature * Math.Exp(-_config.CoolingRate * iteration);
            string newValue = availableValues[_random.Next(availableValues.Count)];

            double currentScore = _testCaseEvaluator.Evaluate(testCase, parameters);
            mutatedTestCase[index] = newValue;
            double newScore = _testCaseEvaluator.Evaluate(mutatedTestCase, parameters);

            if (newScore > currentScore || _random.NextDouble() < Math.Exp((newScore - currentScore) / temperature))
            {
                return mutatedTestCase;
            }
        }
        return testCase;
    }

    private void PerformScoutPhaseIfNeeded(List<IInputParameter> parameters, List<string[]> population, List<string[]> newPopulation, int iteration)
    {
        if (_config.DisableScoutPhase || iteration <= _config.TotalPopulationGenerations * _config.StagnationThresholdPercentage) return;

        var poorPerformingTestCases = population
            .Select(tc => Tuple.Create(tc, _testCaseEvaluator.Evaluate(tc, parameters)))
            .OrderBy(x => x.Item2)
            .Take((int)(population.Count * _config.ScoutSelectionRatio))
            .ToList();

        foreach (var testCase in poorPerformingTestCases)
        {
            newPopulation.Add(ApplyMutation(testCase.Item1, parameters, iteration));
        }
    }

    private List<string[]> LimitFinalPopulationBasedOnSelectionRatio(List<string[]> population, int populationSize)
    {
        return population.Distinct().Take((int)(populationSize * _config.FinalPopulationSelectionRatio)).ToList();
    }
}
