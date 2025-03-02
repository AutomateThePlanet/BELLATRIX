using System;
using System.Collections.Generic;
using System.Linq;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Parameters;
using Bellatrix.DataGeneration.Models;

namespace Bellatrix.DataGeneration.TestCaseGenerators
{
    public class TestCaseEvaluator
    {
        private readonly bool _allowMultipleInvalidInputs;
        private readonly Dictionary<int, HashSet<string>> _seenValuesPerParameter = new();
        private readonly Dictionary<int, HashSet<string>> _globalSeenValuesPerParameter = new(); // Tracks first-time values per parameter

        public TestCaseEvaluator(bool allowMultipleInvalidInputs = false)
        {
            _allowMultipleInvalidInputs = allowMultipleInvalidInputs;
        }

        // 🔹 Evaluates a population of test cases and assigns scores
        public void EvaluatePopulation(HashSet<TestCase> population)
        {
            _seenValuesPerParameter.Clear();
            _globalSeenValuesPerParameter.Clear(); // Reset global tracking at the start

            // 🔹 Sort test cases: prioritize those introducing the most new values first
            var sortedTestCases = population
                .OrderByDescending(tc => CountNewValues(tc, population)) // Test cases with more first-time values come first
                .ToList();

            foreach (var testCase in sortedTestCases)
            {
                testCase.Score = Evaluate(testCase, population);
            }
        }

        private int CountNewValues(TestCase testCase, HashSet<TestCase> evaluatedPopulation)
        {
            Dictionary<int, HashSet<string>> alreadyCoveredValues = GetCoveredValuesPerParameter(evaluatedPopulation);
            int newValueCount = 0;

            for (int i = 0; i < testCase.Values.Count; i++)
            {
                if (!_seenValuesPerParameter.ContainsKey(i))
                {
                    _seenValuesPerParameter[i] = new HashSet<string>();
                }

                // Count as a new value only if it has not been seen in this parameter position before
                if (!_seenValuesPerParameter[i].Contains(testCase.Values[i].Value) &&
                    (!alreadyCoveredValues.ContainsKey(i) || !alreadyCoveredValues[i].Contains(testCase.Values[i].Value)))
                {
                    newValueCount++;
                }
            }

            return newValueCount;
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

                switch (value.Category)
                {
                    case TestValueCategory.BoundaryValid: score += 20; break;
                    case TestValueCategory.Valid: score += 2; break;
                    case TestValueCategory.BoundaryInvalid: score += -1; break;
                    case TestValueCategory.Invalid: score += -2; break;
                }

                if (!_seenValuesPerParameter.ContainsKey(i))
                {
                    _seenValuesPerParameter[i] = new HashSet<string>();
                }

                // 🔹 If this is the first time this value is seen in the whole evaluated set for this parameter position
                if (!_seenValuesPerParameter[i].Contains(value.Value) &&
                    (!alreadyCoveredValues.ContainsKey(i) || !alreadyCoveredValues[i].Contains(value.Value)))
                {
                    _seenValuesPerParameter[i].Add(value.Value);
                    firstTimeValueCount++; // Count how many first-time values exist
                }

                // 🔹 Global tracking per parameter ensures first-time bonus **only once per parameter**
                if (!_globalSeenValuesPerParameter.ContainsKey(i))
                {
                    _globalSeenValuesPerParameter[i] = new HashSet<string>();
                }

                if (_globalSeenValuesPerParameter[i].Add(value.Value))
                {
                    score += 25; // One-time bonus per parameter
                }
            }

            // 🔹 Apply bonus scaling: Reward test cases with multiple new values
            if (firstTimeValueCount > 0)
            {
                double multiplier = 1 + (firstTimeValueCount * 0.25); // Scale reward based on how many new values
                score += 25 * multiplier;
            }

            return score;
        }

        public Dictionary<TestCase, double> EvaluatePopulationToDictionary(HashSet<TestCase> population)
        {
            return population.ToDictionary(tc => tc, tc => Evaluate(tc, population));
        }
    }
}
