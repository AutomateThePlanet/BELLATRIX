using Bellatrix.DataGeneration.Parameters;
using Bellatrix.DataGeneration.Models;

namespace Bellatrix.DataGeneration.TestCaseGenerators
{
    public class TestCaseEvaluator
    {
        private readonly bool _allowMultipleInvalidInputs;
        private readonly Dictionary<int, HashSet<string>> _globalSeenValuesPerParameter = new(); // Tracks first-time values per parameter

        public TestCaseEvaluator(bool allowMultipleInvalidInputs = false)
        {
            _allowMultipleInvalidInputs = allowMultipleInvalidInputs;
        }

        // 🔹 Evaluates a population of test cases and assigns scores
        public void EvaluatePopulation(HashSet<TestCase> population)
        {
            _globalSeenValuesPerParameter.Clear(); // Reset global tracking at the start
            foreach (var testCase in population)
            {
                testCase.Score = Evaluate(testCase, population);
            }
        }

        public double Evaluate(TestCase testCase, HashSet<TestCase> evaluatedTestCases)
        {
            double score = 0;
            int firstTimeValueCount = 0; // Track new values in this test case
            Dictionary<int, HashSet<string>> alreadyCoveredValues = GetCoveredValuesPerParameter(evaluatedTestCases);

            var invalidCount = testCase.Values.Count(value =>
                value.Category == TestValueCategory.Invalid ||
                value.Category == TestValueCategory.BoundaryInvalid);

            if (!_allowMultipleInvalidInputs && invalidCount > 1)
            {
                return -50; // Penalty for multiple invalid inputs
            }

            for (int i = 0; i < testCase.Values.Count; i++)
            {
                var value = testCase.Values[i];

                // 🔹 Assign base score based on category
                switch (value.Category)
                {
                    case TestValueCategory.BoundaryValid: score += 20; break;
                    case TestValueCategory.Valid: score += 2; break;
                    case TestValueCategory.BoundaryInvalid: score += -1; break;
                    case TestValueCategory.Invalid: score += -2; break;
                }

                // 🔹 Ensure global tracking per parameter is initialized
                if (!_globalSeenValuesPerParameter.ContainsKey(i))
                {
                    _globalSeenValuesPerParameter[i] = new HashSet<string>();
                }

                // 🔹 If this value has never been seen globally, apply a one-time bonus
                if (_globalSeenValuesPerParameter[i].Add(value.Value))
                {
                    score += 25;
                }

                // 🔹 If this is the first time this value is seen in the whole evaluated set for this parameter position
                if (!alreadyCoveredValues.ContainsKey(i) || !alreadyCoveredValues[i].Contains(value.Value))
                {
                    alreadyCoveredValues[i].Add(value.Value);
                    firstTimeValueCount++;
                }
            }

            // 🔹 Apply bonus scaling if multiple first-time values exist
            if (firstTimeValueCount > 0)
            {
                double multiplier = 1 + (firstTimeValueCount * 0.25);
                score += 25 * multiplier;
            }

            return score;
        }

        private Dictionary<int, HashSet<string>> GetCoveredValuesPerParameter(HashSet<TestCase> evaluatedPopulation)
        {
            var coveredValues = new Dictionary<int, HashSet<string>>();

            foreach (var testCase in evaluatedPopulation)
            {
                for (int i = 0; i < testCase.Values.Count; i++)
                {
                    if (!coveredValues.ContainsKey(i))
                    {
                        coveredValues[i] = new HashSet<string>();
                    }
                    coveredValues[i].Add(testCase.Values[i].Value);
                }
            }

            return coveredValues;
        }

        public Dictionary<TestCase, double> EvaluatePopulationToDictionary(HashSet<TestCase> population)
        {
            return population.ToDictionary(tc => tc, tc => Evaluate(tc, population));
        }
    }
}
