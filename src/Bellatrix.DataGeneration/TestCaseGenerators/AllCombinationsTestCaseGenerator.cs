using System.Collections.Concurrent;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestCaseGenerators;

public static class AllCombinationsTestCaseGenerator
{
    public static HashSet<TestCase> GenerateTestCases(List<IInputParameter> parameters)
    {
        if (parameters == null || parameters.Count == 0)
        {
            throw new ArgumentException("Test case generation requires at least one parameter.");
        }

        return GenerateAllCombinations(parameters);
    }

    private static HashSet<TestCase> GenerateAllCombinations(List<IInputParameter> parameters)
    {
        // Extract possible values for each parameter
        var parameterValues = parameters.Select(p => p.TestValues).ToList();

        // Concurrent set for thread-safe operations
        var testCases = new ConcurrentBag<TestCase>();

        // Generate Cartesian Product in Parallel
        var allCombinations = parameterValues
            .AsParallel() // Enables parallel execution
            .Aggregate(
                (IEnumerable<IEnumerable<TestValue>>)new[] { Enumerable.Empty<TestValue>() },
                (prev, next) => prev.SelectMany(
                    x => next.AsParallel(), // Parallelize selection of next parameter
                    (x, y) => x.Append(y)
                )
            );

        // Convert to TestCase objects in Parallel
        Parallel.ForEach(allCombinations, combination =>
        {
            testCases.Add(new TestCase { Values = combination.ToList() });
        });

        return testCases.ToHashSet(); // Enforce uniqueness
    }
}
