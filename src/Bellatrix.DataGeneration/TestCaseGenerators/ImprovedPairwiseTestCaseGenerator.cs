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
        private static List<TestCase> cachedTestCases = new List<TestCase>();

        public static List<TestCase> GenerateTestCases(List<IInputParameter> parameters)
        {
            if (cachedTestCases.Any())
            {
                return cachedTestCases;
            }

            if (parameters == null || parameters.Count < 2)
            {
                throw new ArgumentException("Pairwise testing requires at least two parameters.");
            }

            List<List<TestValue>> parameterValues = parameters
                .Select(p => p.TestValues.ToList())
                .ToList();

            return GeneratePairwiseCombinations(parameterValues);
        }

        private static List<TestCase> GeneratePairwiseCombinations(List<List<TestValue>> parameterValues)
        {
            int numParameters = parameterValues.Count;
            List<TestCase> testCases = new List<TestCase>();

            // Track uncovered pairs
            Dictionary<(int, int), HashSet<(TestValue, TestValue)>> uncoveredPairs = new();

            // Initialize uncovered pairs for every pair of parameters
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

            // Generate test cases deterministically
            while (uncoveredPairs.Values.Any(set => set.Count > 0))
            {
                TestCase newTestCase = new TestCase();
                HashSet<(int, int, TestValue, TestValue)> coveredPairs = new();

                // Greedily select values ensuring new pairs are covered
                for (int i = 0; i < numParameters; i++)
                {
                    if (i == 0)
                    {
                        newTestCase.Values.Add(parameterValues[i][testCases.Count % parameterValues[i].Count]);
                    }
                    else
                    {
                        // Select value that maximizes new pair coverage
                        newTestCase.Values.Add(SelectBestValue(i, parameterValues, uncoveredPairs, newTestCase));
                    }
                }

                testCases.Add(newTestCase);

                // Remove covered pairs from uncoveredPairs
                for (int i = 0; i < numParameters; i++)
                {
                    for (int j = i + 1; j < numParameters; j++)
                    {
                        if (uncoveredPairs[(i, j)].Contains((newTestCase.Values[i], newTestCase.Values[j])))
                        {
                            uncoveredPairs[(i, j)].Remove((newTestCase.Values[i], newTestCase.Values[j]));
                        }
                    }
                }
            }

            cachedTestCases = testCases;
            return testCases;
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
