using Bellatrix.Web.Tests.MetaheuristicVersion2.Core;
using System;
using System.Collections.Generic;
using System.Linq;

public static class PairwiseTestCaseGenerator
{
    public static List<string[]> GenerateTestCases(List<IInputParameter> parameters)
    {
        if (parameters == null || parameters.Count < 2)
        {
            throw new ArgumentException("Pairwise testing requires at least two parameters.");
        }

        List<List<string>> parameterValues = parameters
            .Select(p => p.TestValues.Select(tv => tv.Value).ToList())
            .ToList();

        return GenerateTruePairwiseCombinations(parameterValues);
    }

    private static List<string[]> GenerateTruePairwiseCombinations(List<List<string>> parameterValues)
    {
        List<string[]> testCases = new List<string[]>();
        var pairs = new Dictionary<(int, int), HashSet<(string, string)>>();

        for (int i = 0; i < parameterValues.Count; i++)
        {
            for (int j = i + 1; j < parameterValues.Count; j++)
            {
                pairs[(i, j)] = new HashSet<(string, string)>();
            }
        }

        // Generate a seed test case with initial values
        string[] firstTestCase = parameterValues.Select(vals => vals[0]).ToArray();
        testCases.Add(firstTestCase);

        // Track uncovered pairs
        var uncoveredPairs = new HashSet<(int, int, string, string)>(
            pairs.SelectMany(p => parameterValues[p.Key.Item1]
                .SelectMany(v1 => parameterValues[p.Key.Item2]
                    .Select(v2 => (p.Key.Item1, p.Key.Item2, v1, v2))
                )
            )
        );

        while (uncoveredPairs.Count > 0)
        {
            string[] newTestCase = new string[parameterValues.Count];
            var coveredPairs = new HashSet<(int, int, string, string)>();

            for (int i = 0; i < parameterValues.Count; i++)
            {
                newTestCase[i] = parameterValues[i][_random.Next(parameterValues[i].Count)];
            }

            for (int i = 0; i < parameterValues.Count; i++)
            {
                for (int j = i + 1; j < parameterValues.Count; j++)
                {
                    var pair = (i, j, newTestCase[i], newTestCase[j]);
                    if (uncoveredPairs.Contains(pair))
                    {
                        coveredPairs.Add(pair);
                    }
                }
            }

            if (coveredPairs.Count > 0)
            {
                testCases.Add(newTestCase);
                foreach (var pair in coveredPairs)
                {
                    uncoveredPairs.Remove(pair);
                }
            }
        }

        return testCases;
    }

    private static readonly Random _random = new Random();
}