using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Parameters;
using Bellatrix.DataGeneration.Models;

namespace Bellatrix.DataGeneration.TestCaseGenerators;

public class TestCaseEvaluator
{
    private readonly bool _allowMultipleInvalidInputs;

    public TestCaseEvaluator(bool allowMultipleInvalidInputs = false)
    {
        _allowMultipleInvalidInputs = allowMultipleInvalidInputs;
    }

    public double Evaluate(TestCase testCase)
    {
        double score = 0;
        var invalidCount = testCase.Values.Count(value => value.Category == TestValueCategory.Invalid || value.Category == TestValueCategory.BoundaryInvalid);

        if (!_allowMultipleInvalidInputs && invalidCount > 1)
        {
            return -100;
        }

        foreach (var value in testCase.Values)
        {
            switch (value.Category)
            {
                case TestValueCategory.BoundaryValid: score += 4; break;
                case TestValueCategory.Valid: score += 2; break;
                case TestValueCategory.BoundaryInvalid: score += -1; break;
                case TestValueCategory.Invalid: score += -2; break;
            }
        }

        return score;
    }

    public List<Tuple<TestCase, double>> EvaluatePopulationToList(HashSet<TestCase> population)
    {
        return population.Select(tc => Tuple.Create(tc, Evaluate(tc)))
                         .OrderByDescending(x => x.Item2)
                         .ToList();
    }

    public Dictionary<TestCase, double> EvaluatePopulationToDictionary(HashSet<TestCase> population)
    {
        return population.ToDictionary(tc => tc, tc => Evaluate(tc)); ;
    }
}
