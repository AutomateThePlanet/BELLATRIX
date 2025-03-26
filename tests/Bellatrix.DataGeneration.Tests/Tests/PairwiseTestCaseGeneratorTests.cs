using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Parameters;
using Bellatrix.DataGeneration.TestCaseGenerators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.DataGeneration.Tests.Tests;

[TestFixture]
public class PairwiseTestCaseGeneratorTests
{
    private class MockInputParameter : IInputParameter
    {
        public string Name { get; }
        public List<TestValue> TestValues { get; }

        public MockInputParameter(string name, params string[] values)
        {
            Name = name;
            TestValues = values.Select(v => new TestValue(v, typeof(string), TestValueCategory.Valid)).ToList();
        }
    }

    [Test]
    public void GeneratesValidPairwiseCombinations()
    {
        var parameters = new List<IInputParameter>
        {
            new MockInputParameter("Param1", "A", "B", "C"),
            new MockInputParameter("Param2", "1", "2"),
            new MockInputParameter("Param3", "X", "Y")
        };

        var testCases = PairwiseTestCaseGenerator.GenerateTestCases(parameters);

        Assert.That(testCases, Is.Not.Null);
        Assert.That(testCases.Count, Is.GreaterThan(0), "Test cases should be generated.");

        var allPairs = new HashSet<(object, object)>();

        foreach (var testCase in testCases)
        {
            var values = testCase.Values.Select(v => v.Value).ToList();
            for (var i = 0; i < values.Count; i++)
            {
                for (var j = i + 1; j < values.Count; j++)
                {
                    allPairs.Add((values[i], values[j]));
                }
            }
        }

        var expectedPairs = new HashSet<(object, object)>();
        for (int i = 0; i < parameters.Count; i++)
        {
            for (int j = i + 1; j < parameters.Count; j++)
            {
                foreach (var value1 in parameters[i].TestValues)
                {
                    foreach (var value2 in parameters[j].TestValues)
                    {
                        expectedPairs.Add((value1.Value, value2.Value));
                    }
                }
            }
        }

        foreach (var pair in expectedPairs)
        {
            Assert.That(allPairs.Contains(pair), Is.True, $"Missing pair: {pair}");
        }
    }

    [Test]
    public void GeneratesMinimalNumberOfTestCases()
    {
        var parameters = new List<IInputParameter>
        {
            new MockInputParameter("Param1", "A", "B", "C"),
            new MockInputParameter("Param2", "1", "2"),
            new MockInputParameter("Param3", "X", "Y")
        };

        var testCases = PairwiseTestCaseGenerator.GenerateTestCases(parameters);
        Assert.That(testCases.Count, Is.LessThan(12), "Pairwise should generate fewer test cases than full cartesian.");
    }

    [Test]
    public void HandlesSingleParameterCase()
    {
        var parameters = new List<IInputParameter>
        {
            new MockInputParameter("Param1", "A", "B", "C")
        };

        var ex = Assert.Throws<ArgumentException>(() => PairwiseTestCaseGenerator.GenerateTestCases(parameters));
        Assert.That(ex.Message, Is.EqualTo("Pairwise testing requires at least two parameters."));
    }

    [Test]
    public void HandlesEmptyParameterList()
    {
        var ex = Assert.Throws<ArgumentException>(() => PairwiseTestCaseGenerator.GenerateTestCases(new List<IInputParameter>()));
        Assert.That(ex.Message, Is.EqualTo("Pairwise testing requires at least two parameters."));
    }

    [Test]
    public void ValidatesNoDuplicateTestCases()
    {
        var parameters = new List<IInputParameter>
        {
            new MockInputParameter("Param1", "A", "B", "C"),
            new MockInputParameter("Param2", "1", "2"),
            new MockInputParameter("Param3", "X", "Y")
        };

        var testCases = PairwiseTestCaseGenerator.GenerateTestCases(parameters);
        var uniqueTestCases = new HashSet<string>(testCases.Select(tc => string.Join(",", tc.Values.Select(v => v.Value))));

        Assert.That(uniqueTestCases.Count, Is.EqualTo(testCases.Count), "No duplicate test cases should be generated.");
    }
}