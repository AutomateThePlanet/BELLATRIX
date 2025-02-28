using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestCaseGenerators;

public class TestCaseEvaluator
{
    private readonly bool _allowMultipleInvalidInputs;

    public TestCaseEvaluator(bool allowMultipleInvalidInputs = false)
    {
        _allowMultipleInvalidInputs = allowMultipleInvalidInputs;
    }

    public double Evaluate(string[] testCase, List<IInputParameter> parameters)
    {
        double score = 0;
        var invalidCount = testCase.Count(value =>
            parameters.Any(p => p.TestValues.Any(tv => tv.Value == value && tv.Category == TestValueCategory.Invalid)));

        if (!_allowMultipleInvalidInputs && invalidCount > 1)
        {
            return -100;
        }

        foreach (var value in testCase)
        {
            var matchedValue = parameters.SelectMany(p => p.TestValues).FirstOrDefault(tv => tv.Value == value);
            if (matchedValue != null)
            {
                switch (matchedValue.Category)
                {
                    case TestValueCategory.BoundaryValid: score += 4; break;
                    case TestValueCategory.Valid: score += 2; break;
                    case TestValueCategory.BoundaryInvalid: score += -1; break;
                    case TestValueCategory.Invalid: score += -2; break;
                }
            }
        }

        return score;
    }

    public List<Tuple<string[], double>> EvaluatePopulationToList(List<string[]> population, List<IInputParameter> parameters)
    {
        return population.Select(tc => Tuple.Create(tc, Evaluate(tc, parameters)))
                         .OrderByDescending(x => x.Item2)
                         .ToList();
    }

    public Dictionary<string[], double> EvaluatePopulationToDictionary(List<string[]> population, List<IInputParameter> parameters)
    {
        return population.ToDictionary(tc => tc, tc => Evaluate(tc, parameters)); ;
    }
}
