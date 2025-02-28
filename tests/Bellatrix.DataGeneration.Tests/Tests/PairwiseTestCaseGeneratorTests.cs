using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Parameters;
using Bellatrix.DataGeneration.TestCaseGenerators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.DataGeneration.Tests.Tests
{
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
                TestValues = values.Select(v => new TestValue(v, TestValueCategory.Valid)).ToList();
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

            var testCases = ImprovedPairwiseTestCaseGenerator.GenerateTestCases(parameters);

            Assert.That(testCases, Is.Not.Null);
            Assert.That(testCases.Count, Is.GreaterThan(0), "Test cases should be generated.");

            var allPairs = new HashSet<(string, string)>();

            foreach (var testCase in testCases)
            {
                for (var i = 0; i < testCase.Length; i++)
                {
                    for (var j = i + 1; j < testCase.Length; j++)
                    {
                        allPairs.Add((testCase[i], testCase[j]));
                    }
                }
            }

            var expectedPairs = new HashSet<(string, string)>
            {
                ("A", "1"), ("A", "2"), ("A", "X"), ("A", "Y"),
                ("B", "1"), ("B", "2"), ("B", "X"), ("B", "Y"),
                ("C", "1"), ("C", "2"), ("C", "X"), ("C", "Y"),
                ("1", "X"), ("1", "Y"), ("2", "X"), ("2", "Y")
            };

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

            var testCases = ImprovedPairwiseTestCaseGenerator.GenerateTestCases(parameters);

            // Theoretical minimal number of test cases: should be lower than full cartesian product (3 * 2 * 2 = 12)
            Assert.That(testCases.Count, Is.LessThan(12), "Pairwise should generate fewer test cases than full cartesian.");
        }

        [Test]
        public void HandlesSingleParameterCase()
        {
            var parameters = new List<IInputParameter>
            {
                new MockInputParameter("Param1", "A", "B", "C")
            };

            var ex = Assert.Throws<ArgumentException>(() => ImprovedPairwiseTestCaseGenerator.GenerateTestCases(parameters));
            Assert.That(ex.Message, Is.EqualTo("Pairwise testing requires at least two parameters."));
        }

        [Test]
        public void HandlesEmptyParameterList()
        {
            var ex = Assert.Throws<ArgumentException>(() => ImprovedPairwiseTestCaseGenerator.GenerateTestCases(new List<IInputParameter>()));
            Assert.That(ex.Message, Is.EqualTo("Pairwise testing requires at least two parameters."));
        }

        [Test]
        public void HandlesVaryingParameterSizes()
        {
            var parameters = new List<IInputParameter>
            {
                new MockInputParameter("Param1", "A", "B"),
                new MockInputParameter("Param2", "1", "2", "3"),
                new MockInputParameter("Param3", "X", "Y", "Z")
            };

            var testCases = ImprovedPairwiseTestCaseGenerator.GenerateTestCases(parameters);

            // Ensure all pairs exist
            var allPairs = new HashSet<(string, string)>();

            foreach (var testCase in testCases)
            {
                for (var i = 0; i < testCase.Length; i++)
                {
                    for (var j = i + 1; j < testCase.Length; j++)
                    {
                        allPairs.Add((testCase[i], testCase[j]));
                    }
                }
            }

            var expectedPairs = new HashSet<(string, string)>();
            foreach (var param1 in new[] { "A", "B" })
            {
                foreach (var param2 in new[] { "1", "2", "3" })
                {
                    expectedPairs.Add((param1, param2));
                }
                foreach (var param3 in new[] { "X", "Y", "Z" })
                {
                    expectedPairs.Add((param1, param3));
                }
            }
            foreach (var param2 in new[] { "1", "2", "3" })
            {
                foreach (var param3 in new[] { "X", "Y", "Z" })
                {
                    expectedPairs.Add((param2, param3));
                }
            }

            foreach (var pair in expectedPairs)
            {
                Assert.That(allPairs.Contains(pair), Is.True, $"Missing pair: {pair}");
            }
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

            var testCases = ImprovedPairwiseTestCaseGenerator.GenerateTestCases(parameters);

            var uniqueTestCases = new HashSet<string>(testCases.Select(tc => string.Join(",", tc)));

            Assert.That(uniqueTestCases.Count, Is.EqualTo(testCases.Count), "No duplicate test cases should be generated.");
        }
    }
}
