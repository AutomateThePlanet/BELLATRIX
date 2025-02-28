using Bellatrix.DataGeneration.Contracts;

namespace Bellatrix.DataGeneration.TestCaseGenerators
{
    public static class ImprovedPairwiseTestCaseGenerator
    {
        private static List<string[]> cachedTestCases = new List<string[]>();
        public static List<string[]> GenerateTestCases(List<IInputParameter> parameters)
        {
            if (cachedTestCases.Any())
            {
                return cachedTestCases;
            }

            if (parameters == null || parameters.Count < 2)
            {
                throw new ArgumentException("Pairwise testing requires at least two parameters.");
            }

            var parameterValues = parameters
                .Select(p => p.TestValues.Select(tv => tv.Value).ToList())
                .ToList();

            return GeneratePairwiseCombinations(parameterValues);
        }

        private static List<string[]> GeneratePairwiseCombinations(List<List<string>> parameterValues)
        {
            var numParameters = parameterValues.Count;
            var testCases = new List<string[]>();

            // Track uncovered pairs
            Dictionary<(int, int), HashSet<(string, string)>> uncoveredPairs = new();

            // Initialize uncovered pairs for every pair of parameters
            for (var i = 0; i < numParameters; i++)
            {
                for (var j = i + 1; j < numParameters; j++)
                {
                    uncoveredPairs[(i, j)] = new HashSet<(string, string)>();
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
                var newTestCase = new string[numParameters];
                HashSet<(int, int, string, string)> coveredPairs = new();

                // Greedily select values ensuring new pairs are covered
                for (var i = 0; i < numParameters; i++)
                {
                    if (i == 0)
                    {
                        newTestCase[i] = parameterValues[i][testCases.Count % parameterValues[i].Count];
                    }
                    else
                    {
                        // Select value that maximizes new pair coverage
                        newTestCase[i] = SelectBestValue(i, parameterValues, uncoveredPairs, newTestCase);
                    }
                }

                testCases.Add(newTestCase);

                // Remove covered pairs from uncoveredPairs
                for (var i = 0; i < numParameters; i++)
                {
                    for (var j = i + 1; j < numParameters; j++)
                    {
                        if (uncoveredPairs[(i, j)].Contains((newTestCase[i], newTestCase[j])))
                        {
                            uncoveredPairs[(i, j)].Remove((newTestCase[i], newTestCase[j]));
                        }
                    }
                }
            }

            cachedTestCases = testCases;

            return testCases;
        }

        private static string SelectBestValue(int paramIndex, List<List<string>> parameterValues,
            Dictionary<(int, int), HashSet<(string, string)>> uncoveredPairs, string[] currentTestCase)
        {
            var candidates = parameterValues[paramIndex];
            var bestValue = candidates[0];
            var maxPairsCovered = -1;

            foreach (var candidate in candidates)
            {
                var pairsCovered = 0;

                for (var j = 0; j < paramIndex; j++)
                {
                    if (uncoveredPairs.ContainsKey((j, paramIndex)) &&
                        uncoveredPairs[(j, paramIndex)].Contains((currentTestCase[j], candidate)))
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
