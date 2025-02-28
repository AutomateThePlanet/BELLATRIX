using Bellatrix.Web.Tests.MetaheuristicVersion2.Core;
using System;
using System.Collections.Generic;
using System.Linq;

public static class NWiseTestCaseGenerator
{
    public static List<string[]> GenerateTestCases(List<IInputParameter> parameters, int n)
    {
        if (parameters == null || parameters.Count < n)
            throw new ArgumentException($"N-wise testing requires at least {n} parameters.");

        // Extract parameter values
        var parameterValues = parameters
            .Select(p => p.TestValues.Select(tv => tv.Value).ToList())
            .ToList();

        // Generate n-wise combinations
        return GenerateNWiseCombinations(parameterValues, n);
    }

    private static List<string[]> GenerateNWiseCombinations(List<List<string>> parameterValues, int n)
    {
        var testCases = new List<string[]>();
        var uncoveredCombinations = new HashSet<List<(int, string)>>();

        // Initialize uncovered combinations for each set of n parameters
        var parameterIndices = Enumerable.Range(0, parameterValues.Count).ToList();
        var nWiseParameterSets = GetCombinations(parameterIndices, n);

        foreach (var paramSet in nWiseParameterSets)
        {
            var combinations = GetCartesianProduct(paramSet.Select(index => parameterValues[index]).ToList());
            foreach (var combination in combinations)
            {
                var indexedCombination = paramSet.Zip(combination, (index, value) => (index, value)).ToList();
                uncoveredCombinations.Add(indexedCombination);
            }
        }

        // Generate test cases until all n-wise combinations are covered
        while (uncoveredCombinations.Count > 0)
        {
            var newTestCase = new string[parameterValues.Count];
            var coveredCombinations = new List<List<(int, string)>>();

            // Randomly assign values to each parameter
            for (int i = 0; i < parameterValues.Count; i++)
            {
                newTestCase[i] = parameterValues[i][new Random().Next(parameterValues[i].Count)];
            }

            // Check which n-wise combinations are covered by this test case
            foreach (var paramSet in nWiseParameterSets)
            {
                var combination = paramSet.Select(index => (index, newTestCase[index])).ToList();
                if (uncoveredCombinations.Contains(combination))
                {
                    coveredCombinations.Add(combination);
                }
            }

            // If any new combinations are covered, add the test case and update uncovered combinations
            if (coveredCombinations.Count > 0)
            {
                testCases.Add(newTestCase);
                foreach (var combination in coveredCombinations)
                {
                    uncoveredCombinations.Remove(combination);
                }
            }
        }

        return testCases;
    }

    private static List<List<T>> GetCombinations<T>(List<T> list, int length)
    {
        if (length == 1)
            return list.Select(t => new List<T> { t }).ToList();

        return GetCombinations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                        (t1, t2) => t1.Concat(new List<T> { t2 }).ToList())
            .ToList();
    }

    private static List<List<T>> GetCartesianProduct<T>(List<List<T>> lists)
    {
        IEnumerable<IEnumerable<T>> tempProduct = new[] { Enumerable.Empty<T>() };
        foreach (var list in lists)
        {
            tempProduct = tempProduct.SelectMany(seq => list, (seq, item) => seq.Concat(new[] { item }));
        }
        return tempProduct.Select(seq => seq.ToList()).ToList();
    }
}

