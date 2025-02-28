using Bellatrix.Web.Tests.MetaheuristicVersion2.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.Tests
{
    [TestFixture]
    public class NWiseTestCaseGeneratorTests
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
        public void GeneratesValidNWiseCombinations()
        {
            int n = 3; // N-wise level (e.g., triples)
            var parameters = new List<IInputParameter>
            {
                new MockInputParameter("Param1", "A", "B", "C"),
                new MockInputParameter("Param2", "1", "2"),
                new MockInputParameter("Param3", "X", "Y"),
                new MockInputParameter("Param4", "M", "N")
            };

            var testCases = NWiseTestCaseGenerator.GenerateTestCases(parameters, n);

            Assert.That(testCases, Is.Not.Null);
            Assert.That(testCases.Count, Is.GreaterThan(0), "Test cases should be generated.");

            var allNWiseCombinations = new HashSet<(string, string, string)>();

            foreach (var testCase in testCases)
            {
                for (int i = 0; i < testCase.Length; i++)
                {
                    for (int j = i + 1; j < testCase.Length; j++)
                    {
                        for (int k = j + 1; k < testCase.Length; k++)
                        {
                            allNWiseCombinations.Add((testCase[i], testCase[j], testCase[k]));
                        }
                    }
                }
            }

            var expectedCombinations = new HashSet<(string, string, string)>();
            foreach (var param1 in new[] { "A", "B", "C" })
            {
                foreach (var param2 in new[] { "1", "2" })
                {
                    foreach (var param3 in new[] { "X", "Y" })
                    {
                        expectedCombinations.Add((param1, param2, param3));
                    }
                }
            }

            foreach (var combination in expectedCombinations)
            {
                Assert.That(allNWiseCombinations.Contains(combination), Is.True, $"Missing combination: {combination}");
            }
        }

        [Test]
        public void GeneratesMinimalNumberOfTestCases()
        {
            int n = 3;
            var parameters = new List<IInputParameter>
            {
                new MockInputParameter("Param1", "A", "B", "C"),
                new MockInputParameter("Param2", "1", "2"),
                new MockInputParameter("Param3", "X", "Y"),
                new MockInputParameter("Param4", "M", "N")
            };

            var testCases = NWiseTestCaseGenerator.GenerateTestCases(parameters, n);

            // Full Cartesian product would be 3 * 2 * 2 * 2 = 24, so we expect fewer than this.
            Assert.That(testCases.Count, Is.LessThan(24), "N-wise should generate fewer test cases than full cartesian.");
        }

        [Test]
        public void HandlesSingleParameterCase()
        {
            int n = 2;
            var parameters = new List<IInputParameter>
            {
                new MockInputParameter("Param1", "A", "B", "C")
            };

            var ex = Assert.Throws<ArgumentException>(() => NWiseTestCaseGenerator.GenerateTestCases(parameters, n));
            Assert.That(ex.Message, Is.EqualTo($"N-wise testing requires at least {n} parameters."));
        }

        [Test]
        public void HandlesEmptyParameterList()
        {
            int n = 2;
            var ex = Assert.Throws<ArgumentException>(() => NWiseTestCaseGenerator.GenerateTestCases(new List<IInputParameter>(), n));
            Assert.That(ex.Message, Is.EqualTo($"N-wise testing requires at least {n} parameters."));
        }

        [Test]
        public void HandlesVaryingParameterSizes()
        {
            int n = 3;
            var parameters = new List<IInputParameter>
            {
                new MockInputParameter("Param1", "A", "B"),
                new MockInputParameter("Param2", "1", "2", "3"),
                new MockInputParameter("Param3", "X", "Y", "Z"),
                new MockInputParameter("Param4", "M", "N")
            };

            var testCases = NWiseTestCaseGenerator.GenerateTestCases(parameters, n);

            var allNWiseCombinations = new HashSet<(string, string, string)>();

            foreach (var testCase in testCases)
            {
                for (int i = 0; i < testCase.Length; i++)
                {
                    for (int j = i + 1; j < testCase.Length; j++)
                    {
                        for (int k = j + 1; k < testCase.Length; k++)
                        {
                            allNWiseCombinations.Add((testCase[i], testCase[j], testCase[k]));
                        }
                    }
                }
            }

            var expectedCombinations = new HashSet<(string, string, string)>();
            foreach (var param1 in new[] { "A", "B" })
            {
                foreach (var param2 in new[] { "1", "2", "3" })
                {
                    foreach (var param3 in new[] { "X", "Y", "Z" })
                    {
                        expectedCombinations.Add((param1, param2, param3));
                    }
                }
            }

            foreach (var combination in expectedCombinations)
            {
                Assert.That(allNWiseCombinations.Contains(combination), Is.True, $"Missing combination: {combination}");
            }
        }
    }
}
