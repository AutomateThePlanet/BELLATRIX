using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.TestCaseGenerators;
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

            // ✅ Perform scout phase if needed
            PerformScoutPhaseIfNeeded(parameters, evaluatedPopulation, nonElitePopulation, currentGeneration);
        }

        return LimitFinalPopulationBasedOnSelectionRatio(evaluatedPopulation);
    }

    private HashSet<TestCase> MaintainDiversePopulationOnlookerSelection(
        HashSet<TestCase> nonElitePopulation,
        HashSet<TestCase> evaluatedPopulation,
        int eliteCount)
    {
        // If onlooker selection is disabled, return the non-elite population as is
        if (!_config.EnableOnlookerSelection)
        {
            return nonElitePopulation;
        }

        // Ensure the total score is at least 1 to avoid division errors
        double totalScore = Math.Max(1, evaluatedPopulation.Sum(tc => tc.Score));

        // Initialize the new population with non-elite test cases
        HashSet<TestCase> selectedPopulation = new HashSet<TestCase>(nonElitePopulation);

        // 🔹 Step 1: Select test cases probabilistically, favoring higher scores
        var probabilisticSelection = evaluatedPopulation
            .OrderByDescending(tc =>
                (tc.Score / totalScore) + (_random.NextDouble() * _config.OnlookerSelectionRatio)) // Adds randomness for diversity
            .Take(Math.Max(1, (int)(_config.FinalPopulationSelectionRatio * _config.OnlookerSelectionRatio))) // Ensures at least one selection
            .ToList();

        // 🔹 Step 2: Add selected test cases to the new population, maintaining diversity
        foreach (var testCase in probabilisticSelection)
        {
            if (selectedPopulation.Count < _config.FinalPopulationSelectionRatio)
            {
                selectedPopulation.Add(testCase);
            }
        }

        // 🔹 Step 3: If the population is still too small, add more test cases from the evaluated set
        foreach (var testCase in evaluatedPopulation)
        {
            if (selectedPopulation.Count >= _config.FinalPopulationSelectionRatio)
            {
                break;
            }
            selectedPopulation.Add(testCase);
        }

        return selectedPopulation;
    }

    private HashSet<TestCase> GenerateInitialPopulation(List<IInputParameter> parameters)
    {
        return PairwiseTestCaseGenerator.GenerateTestCases(parameters);
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
    private void MutatePopulation(HashSet<TestCase> evaluatedPopulation, HashSet<TestCase> nonElitePopulation, List<IInputParameter> parameters, int iteration)
    {
        HashSet<TestCase> mutatedCases = new HashSet<TestCase>();

        double temperature = Math.Max(0.1, Math.Pow(_config.CoolingRate, iteration));

        foreach (var originalTestCase in nonElitePopulation)
        {
            TestCase mutatedTestCase = ApplyMutation(originalTestCase, parameters, iteration);

            if (!mutatedTestCase.Equals(originalTestCase) && !evaluatedPopulation.Contains(mutatedTestCase))
            {
                double originalScore = _testCaseEvaluator.Evaluate(originalTestCase, evaluatedPopulation);
                double mutatedScore = _testCaseEvaluator.Evaluate(mutatedTestCase, evaluatedPopulation);

                bool acceptMutation = false;

                if (_config.EnforceMutationUniqueness)
                {
                    // 🔹 Enforce strict uniqueness: Only accept if the mutation is strictly better
                    acceptMutation = mutatedScore > originalScore;
                }
                else
                {
                    // 🔹 Simulated Annealing: Accept better OR sometimes worse mutations
                    double acceptanceProbability = Math.Exp((mutatedScore - originalScore) / temperature);
                    acceptMutation = mutatedScore > originalScore || _random.NextDouble() < acceptanceProbability;
                }


                if (acceptMutation)
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
        TestCase mutatedTestCase = new TestCase { Values = originalTestCase.Values.Select(v => new TestValue(v.Value, v.Category, v.ExpectedInvalidMessage)).ToList() };

        // Select a mutation index and change the value
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
            .OrderBy(x => x.Score)
            .Take((int)(_config.FinalPopulationSelectionRatio * _config.ScoutSelectionRatio))
            .ToList();

        foreach (var testCase in poorPerformingTestCases)
        {
            TestCase mutatedTestCase = ApplyMutation(testCase, parameters, iteration);
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
