using System;
using System.Collections.Generic;
using System.Linq;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestCaseGenerators
{
    public static class ImprovedPairwiseTestCaseGenerator
    {
        private static HashSet<TestCase> cachedTestCases = new HashSet<TestCase>();

        public static List<TestCase> GenerateTestCases(List<IInputParameter> parameters)
        {
            if (cachedTestCases.Any())
            {
                return cachedTestCases.ToList();
            }

            if (parameters == null || parameters.Count < 2)
            {
                throw new ArgumentException("Pairwise testing requires at least two parameters.");
            }

            List<List<TestValue>> parameterValues = parameters
                .Select(p => p.TestValues.ToList())
                .ToList();

            var testCases = GeneratePairwiseCombinations(parameterValues);

            cachedTestCases = new HashSet<TestCase>(testCases); // Enforce uniqueness
            return cachedTestCases.ToList();
        }

        private static List<TestCase> GeneratePairwiseCombinations(List<List<TestValue>> parameterValues)
        {
            int numParameters = parameterValues.Count;
            HashSet<TestCase> uniqueTestCases = new HashSet<TestCase>();

            Dictionary<(int, int), HashSet<(TestValue, TestValue)>> uncoveredPairs = new();

            for (int i = 0; i < numParameters; i++)
            {
                for (int j = i + 1; j < numParameters; j++)
                {
                    uncoveredPairs[(i, j)] = new HashSet<(TestValue, TestValue)>();
                    foreach (var val1 in parameterValues[i])
                    {
                        foreach (var val2 in parameterValues[j])
                        {
                            uncoveredPairs[(i, j)].Add((val1, val2));
                        }
                    }
                }
            }

            while (uncoveredPairs.Values.Any(set => set.Count > 0))
            {
                TestCase newTestCase = new TestCase();
                for (int i = 0; i < numParameters; i++)
                {
                    if (i == 0)
                    {
                        newTestCase.Values.Add(parameterValues[i][uniqueTestCases.Count % parameterValues[i].Count]);
                    }
                    else
                    {
                        newTestCase.Values.Add(SelectBestValue(i, parameterValues, uncoveredPairs, newTestCase));
                    }
                }

                uniqueTestCases.Add(newTestCase);

                for (int i = 0; i < numParameters; i++)
                {
                    for (int j = i + 1; j < numParameters; j++)
                    {
                        uncoveredPairs[(i, j)].Remove((newTestCase.Values[i], newTestCase.Values[j]));
                    }
                }
            }

            return uniqueTestCases.ToList();
        }

        private static TestValue SelectBestValue(int paramIndex, List<List<TestValue>> parameterValues,
            Dictionary<(int, int), HashSet<(TestValue, TestValue)>> uncoveredPairs, TestCase currentTestCase)
        {
            var candidates = parameterValues[paramIndex];
            TestValue bestValue = candidates[0];
            int maxPairsCovered = -1;

            foreach (var candidate in candidates)
            {
                int pairsCovered = 0;
                for (int j = 0; j < paramIndex; j++)
                {
                    if (uncoveredPairs.ContainsKey((j, paramIndex)) &&
                        uncoveredPairs[(j, paramIndex)].Contains((currentTestCase.Values[j], candidate)))
                    {
                        pairsCovered++;
                    }
                }

                if (pairsCovered > maxPairsCovered)
                {
                    maxPairsCovered = pairsCovered;
                    bestValue = candidate;
                }
            }

            return bestValue;
        }
    }
}
