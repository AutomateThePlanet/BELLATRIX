using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.OutputGenerators;
using Bellatrix.DataGeneration.TestCaseGenerators;


// 🐝 Hybrid Artificial Bee Colony (ABC) Test Case Generator
// This algorithm generates optimized test cases using an evolutionary approach
// based on the behavior of honeybee colonies.
public class HybridArtificialBeeColonyTestCaseGenerator
{
    private int _populationSize;
    private readonly int _maxPopulationGenerations;
    private readonly double _mutationRate;
    private readonly double _selectionRatio;
    private readonly double _eliteSelectionRatio;
    private readonly bool _disableOnlookerSelection;
    private readonly bool _disableScoutPhase;
    private readonly double _stagnationThresholdPercentage;
    private readonly ITestCaseOutputGenerator _outputGenerator;
    private readonly TestCaseEvaluator _testCaseEvaluator;
    private readonly Random _random = new Random(42);

    // 🔹 Constructor: Initializes ABC optimization parameters
    public HybridArtificialBeeColonyTestCaseGenerator(
        int populationSize = 10,
        int maxIterations = 20,
        double mutationRate = 0.3,
        double selectionRatio = 0.5,
        bool allowMultipleInvalidInputs = false,
        double eliteSelectionRatio = 0.5,
        bool disableOnlookerSelection = false,
        bool disableScoutPhase = false,
        double stagnationThresholdPercentage = 0.75,
        ITestCaseOutputGenerator outputGenerator = null)
    {
        _populationSize = populationSize;
        _maxPopulationGenerations = maxIterations;
        _mutationRate = mutationRate;
        _selectionRatio = selectionRatio;
        _eliteSelectionRatio = eliteSelectionRatio;
        _disableOnlookerSelection = disableOnlookerSelection;
        _disableScoutPhase = disableScoutPhase;
        _stagnationThresholdPercentage = stagnationThresholdPercentage;
        _outputGenerator = outputGenerator ?? new NUnitTestCaseOutputGenerator();
        _testCaseEvaluator = new TestCaseEvaluator(allowMultipleInvalidInputs);
    }

    // 🔹 Public API: Generates and outputs optimized test cases
    public void GenerateTestCases(string methodName, List<IInputParameter> parameters)
    {
        var testCases = RunABCAlgorithm(parameters);
        _outputGenerator.GenerateOutput(methodName, testCases);
    }

    // 🔹 Public API: Returns the optimized test cases
    public List<string[]> GetGeneratedTestCases(List<IInputParameter> parameters)
    {
        return RunABCAlgorithm(parameters);
    }

    // 🔹 Main ABC Algorithm Execution
    public List<string[]> RunABCAlgorithm(List<IInputParameter> parameters)
    {
        List<string[]> currentPopulation = GenerateInitialPopulation(parameters);
        _populationSize = currentPopulation.Count;

        for (int currentGeneration = 0; currentGeneration < _maxPopulationGenerations; currentGeneration++)
        {
            var evaluatedPopulation = _testCaseEvaluator.EvaluatePopulationToList(currentPopulation, parameters);
            int eliteCount = CalculateElitePopulationSize();

            List<string[]> newGenerationPopulation = SelectElitePopulation(evaluatedPopulation, eliteCount);
            newGenerationPopulation = MaintainDiversePopulationOnlookerSelection(newGenerationPopulation, evaluatedPopulation, eliteCount);
            
            // TODO: fix test with annealing.
            MutatePopulation(newGenerationPopulation, evaluatedPopulation, parameters, currentGeneration);
            PerformScoutPhaseIfNeeded(parameters, currentPopulation, newGenerationPopulation, currentGeneration);

            currentPopulation = newGenerationPopulation;
        }


        // TODO: do we sort by score?
        return LimitFinalPopulationBasedOnSelectionRatio(currentPopulation);
    }

    // 🔹 Step 1: Generate the Initial Test Case Population
    private List<string[]> GenerateInitialPopulation(List<IInputParameter> parameters)
    {
        return ImprovedPairwiseTestCaseGenerator.GenerateTestCases(parameters);
    }

    // 🔹 Step 2: Calculate Elite Selection Count
    private int CalculateElitePopulationSize()
    {
        return Math.Max(1, (int)(_populationSize * _eliteSelectionRatio));
    }

    // 🔹 Step 3: Select Top Elite Test Cases Based on Fitness
    private List<string[]> SelectElitePopulation(List<Tuple<string[], double>> evaluatedPopulation, int eliteCount)
    {
        return evaluatedPopulation.Take(eliteCount).Select(tc => tc.Item1).ToList();
    }

    // 🔹 Step 4: Maintain Diversity Using Onlooker Bee Phase
    private List<string[]> MaintainDiversePopulationOnlookerSelection(List<string[]> currentPopulation, List<Tuple<string[], double>> evaluatedPopulation, int eliteCount)
    {
        if (_disableOnlookerSelection)
        {
            return currentPopulation;
        }

        double totalScore = Math.Max(1, evaluatedPopulation.Sum(tc => tc.Item2));
        HashSet<string[]> uniquePopulation = new HashSet<string[]>(currentPopulation, new StructuralEqualityComparer());

        var probabilitySelection = evaluatedPopulation
            .Select(tc => new { TestCase = tc.Item1, NormalizedScore = tc.Item2 / totalScore })
            .OrderByDescending(tc => tc.NormalizedScore * _random.NextDouble())
            .Take(Math.Max(1, (int)(_populationSize * 0.05)))
            .Select(tc => tc.TestCase)
            .ToList();

        foreach (var testCase in probabilitySelection)
        {
            if (uniquePopulation.Count < _populationSize)
            {
                uniquePopulation.Add(testCase);
            }
        }

        return uniquePopulation.ToList();
    }

    // 🔹 Step 5: Apply Mutations to Non-Elite Population
    private void MutatePopulation(List<string[]> population, List<Tuple<string[], double>> evaluatedPopulation, List<IInputParameter> parameters, int iteration)
    {
        for (int i = CalculateElitePopulationSize(); i < evaluatedPopulation.Count; i++)
        {
            string[] mutatedTestCase = ApplyMutation(evaluatedPopulation[i].Item1, parameters, iteration);
            population.Add(mutatedTestCase);
        }
    }

    // 🔹 Mutation: Modify a Test Case to Introduce Variability
    private string[] ApplyMutation(string[] originalTestCase, List<IInputParameter> parameters, int iteration)
    {
        if (_random.NextDouble() >= _mutationRate)
        {
            return originalTestCase;
        }

        string[] mutatedTestCase = MutateUsingSimulatedAnnealing(originalTestCase, parameters, iteration);
        return mutatedTestCase;
    }

    // 🔹 Mutation Strategy: Simulated Annealing-Based Mutation
    private string[] MutateUsingSimulatedAnnealing(string[] testCase, List<IInputParameter> parameters, int iteration)
    {
        int index = _random.Next(testCase.Length);
        var availableValues = parameters[index].TestValues.Select(tv => tv.Value).ToList();

        if (availableValues.Count > 1)
        {
            testCase[index] = availableValues[_random.Next(availableValues.Count)];
        }

        return testCase;
    }

    // 🔹 Step 6: Introduce new test cases if the population stagnates
    private void PerformScoutPhaseIfNeeded(List<IInputParameter> parameters, List<string[]> population, List<string[]> newPopulation, int iteration)
    {
        if (_disableScoutPhase || iteration <= _maxPopulationGenerations * _stagnationThresholdPercentage)
        {
            return;
        }

        var poorPerformingTestCases = population
            .Select(tc => Tuple.Create(tc, _testCaseEvaluator.Evaluate(tc, parameters)))
            .OrderBy(x => x.Item2)
            .Take((int)(_populationSize * 0.3))
            .ToList();

        foreach (var testCase in poorPerformingTestCases)
        {
            string[] mutatedTestCase = ApplyMutation(testCase.Item1, parameters, iteration);
            newPopulation.Add(mutatedTestCase);
        }
    }

    // 🔹 Step 7: Limit final test case count
    private List<string[]> LimitFinalPopulationBasedOnSelectionRatio(List<string[]> population)
    {
        return population.Distinct().Take((int)(_populationSize * _selectionRatio)).ToList();
    }
}
