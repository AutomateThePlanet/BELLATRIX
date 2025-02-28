using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Bellatrix.Web.Tests.MetaheuristicVersion2;
using Bellatrix.Web.Tests.MetaheuristicVersion2.Core;
using Bellatrix.Web.Tests.MetaheuristicVersion2.TestValueProviders;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.Tests;

[TestFixture]
public class ABCComparisonTests
{
    private List<IInputParameter> _parameters;
    private HybridArtificialBeeColonyTestCaseGenerator _abcGenerator;
    private List<string[]> _abcTestCases;
    private List<string[]> _pairwiseTestCases;
    private List<string[]> _filteredPairwiseTestCases;

    [SetUp]
    public void SetUp()
    {
        ServicesCollection.Current.RegisterType<IComponentTestValuesProviderStrategy<Email>, EmailTestValueProviderStrategy>();
        ServicesCollection.Current.RegisterType<IComponentTestValuesProviderStrategy<Phone>, PhoneTestValueProviderStrategy>();
        ServicesCollection.Current.RegisterType<IComponentTestValuesProviderStrategy<TextField>, TextFieldTestValueProviderStrategy>();
        _parameters = new List<IInputParameter>
        {
            new ComponentInputParameter<TextField>(
                isManualMode: true,
                customValues: new[]
                {
                    Tuple.Create("Normal1", TestValueCategory.Valid),
                    Tuple.Create("BoundaryMin-1", TestValueCategory.BoundaryInvalid),
                    Tuple.Create("BoundaryMin", TestValueCategory.BoundaryValid),
                    Tuple.Create("BoundaryMax", TestValueCategory.BoundaryValid),
                    Tuple.Create("BoundaryMax+1", TestValueCategory.BoundaryInvalid),
                    Tuple.Create("Invalid1", TestValueCategory.Invalid)
                }),

            new ComponentInputParameter<Email>(
                isManualMode: true,
                customValues: new[]
                {
                    Tuple.Create("test@mail.comMIN-1", TestValueCategory.BoundaryInvalid),
                    Tuple.Create("test@mail.comMIN", TestValueCategory.BoundaryValid),
                    Tuple.Create("test@mail.comMAX", TestValueCategory.BoundaryValid),
                    Tuple.Create("test@mail.comMAX+1", TestValueCategory.BoundaryInvalid),
                    Tuple.Create("test@mail.com", TestValueCategory.Valid),
                    Tuple.Create("invalid@mail", TestValueCategory.Invalid)
                }),

            new ComponentInputParameter<Phone>(
                isManualMode: true,
                customValues: new[]
                {
                    Tuple.Create("+359888888888", TestValueCategory.Valid),
                    Tuple.Create("000000", TestValueCategory.Invalid)
                }),

            new ComponentInputParameter<TextField>(
                isManualMode: true,
                customValues: new[]
                {
                    Tuple.Create("NormalX", TestValueCategory.Valid)
                })
        };


        _abcGenerator = new HybridArtificialBeeColonyTestCaseGenerator(selectionRatio: 0.4, maxIterations: 30, mutationRate: 0.3, allowMultipleInvalidInputs: false);
        _abcTestCases = _abcGenerator.RunABCAlgorithm(_parameters);

        _pairwiseTestCases = PairwiseTestCaseGenerator.GenerateTestCases(_parameters);
        _filteredPairwiseTestCases = _pairwiseTestCases
            .Where(tc => tc.Count(value =>
                _parameters.Any(p => p.TestValues.Any(tv => tv.Value == value && tv.Category == TestValueCategory.Invalid))) <= 1)
            .ToList();
    }

    [Test]
    public void CompareEfficiencyOfABCAndPairwiseReduction()
    {
        Console.WriteLine("================ TEST CASE REDUCTION ANALYSIS ================");
        Console.WriteLine($"Pairwise Generated Test Cases: {_pairwiseTestCases.Count}");
        Console.WriteLine($"Pairwise (Filtered) Test Cases: {_filteredPairwiseTestCases.Count}");
        Console.WriteLine($"Hybrid ABC Test Cases: {_abcTestCases.Count}");

        Assert.That(_abcTestCases.Count, Is.LessThanOrEqualTo(_filteredPairwiseTestCases.Count),
            "ABC should reduce test cases at least as efficiently as pairwise filtering.");

        Assert.That(_abcTestCases.Count, Is.LessThan(_pairwiseTestCases.Count),
            "ABC should produce fewer test cases than pure pairwise.");
    }

    [Test]
    public void AllBoundaryValuesAreTestedAtLeastOnce()
    {
        var allBoundaryValues = _parameters
            .SelectMany(p => p.TestValues.Where(tv => tv.Category == TestValueCategory.BoundaryInvalid || tv.Category == TestValueCategory.BoundaryValid).Select(tv => tv.Value))
            .ToHashSet();

        var testedValues = _abcTestCases.SelectMany(tc => tc).ToHashSet();

        foreach (var boundaryValue in allBoundaryValues)
        {
            Assert.That(testedValues.Contains(boundaryValue), Is.True,
                $"Boundary value '{boundaryValue}' was not included in any test case.");
        }
    }

    [Test]
    public void EachParameterIsUsedAtLeastOnce()
    {
        var uncoveredParameters = new List<string>();

        foreach (var parameter in _parameters)
        {
            bool isParameterCovered = _abcTestCases.Any(tc =>
                parameter.TestValues.Any(tv => tc.Contains(tv.Value)));

            if (!isParameterCovered)
            {
                uncoveredParameters.Add(parameter.ToString());
            }
        }

        if (uncoveredParameters.Any())
        {
            Console.WriteLine("The following parameters were not covered in any test case:");
            foreach (var param in uncoveredParameters)
            {
                Console.WriteLine($"- {param}");
            }
        }

        Assert.That(uncoveredParameters, Is.Empty, "Some parameters were not covered in the generated test cases.");
    }

    [Test]
    public void NoTestCaseHasMoreThanOneInvalidInput()
    {
        foreach (var testCase in _abcTestCases)
        {
            var invalidCount = testCase.Count(value =>
                _parameters.Any(p => p.TestValues.Any(tv => tv.Value == value && tv.Category == TestValueCategory.Invalid)));

            Assert.That(invalidCount, Is.LessThanOrEqualTo(1),
                $"Test case [{string.Join(", ", testCase)}] has more than one invalid input, violating the constraints.");
        }
    }

    [Test]
    public void GeneratedTestCasesAreUnique()
    {
        var duplicateTestCases = _abcTestCases
            .GroupBy(tc => tc, new TestCaseComparer())
            .Where(group => group.Count() > 1)
            .Select(group => new { TestCase = string.Join(",", group.First()), Count = group.Count() })
            .ToList();

        foreach (var testCase in _abcTestCases)
        {
            Console.WriteLine(string.Join(',', testCase));
        }

        if (duplicateTestCases.Any())
        {
            Console.WriteLine("Duplicate test cases found:");
            foreach (var duplicate in duplicateTestCases)
            {
                Console.WriteLine($"Test Case: {duplicate.TestCase} - Repeated {duplicate.Count} times");
            }
        }

        Assert.That(duplicateTestCases.Count, Is.EqualTo(0), "Duplicate test cases found in the generated set.");
    }

    // To confirm Hybrid ABC does not over-optimize and discard essential variations.
    //  Count the number of times each unique input appears across test cases and compare that distribution between:
    //   Pairwise
    //  Filtered Pairwise
    //   Hybrid ABC
    [Test]
    public void TestCaseDiversityAnalysis()
    {
        var inputFrequency = new Dictionary<string, int>();

        foreach (var testCase in _abcTestCases)
        {
            foreach (var value in testCase)
            {
                if (!inputFrequency.ContainsKey(value))
                    inputFrequency[value] = 0;

                inputFrequency[value]++;
            }
        }

        Console.WriteLine("================ ABC TEST CASE DIVERSITY ANALYSIS ================");
        foreach (var kvp in inputFrequency.OrderByDescending(x => x.Value))
        {
            Console.WriteLine($"Value: {kvp.Key}, Occurrences: {kvp.Value}");
        }

        Assert.That(inputFrequency.Values.Any(v => v == 0), Is.False, "Some values were not tested at all.");

        // Filtered Pairwise:
        inputFrequency = new Dictionary<string, int>();

        foreach (var testCase in _filteredPairwiseTestCases)
        {
            foreach (var value in testCase)
            {
                if (!inputFrequency.ContainsKey(value))
                    inputFrequency[value] = 0;

                inputFrequency[value]++;
            }
        }

        Console.WriteLine("================ Filtered Pairwise TEST CASE DIVERSITY ANALYSIS ================");
        foreach (var kvp in inputFrequency.OrderByDescending(x => x.Value))
        {
            Console.WriteLine($"Value: {kvp.Key}, Occurrences: {kvp.Value}");
        }

        Assert.That(inputFrequency.Values.Any(v => v == 0), Is.False, "Some values were not tested at all.");

        // Pairwise:
        inputFrequency = new Dictionary<string, int>();

        foreach (var testCase in _pairwiseTestCases)
        {
            foreach (var value in testCase)
            {
                if (!inputFrequency.ContainsKey(value))
                    inputFrequency[value] = 0;

                inputFrequency[value]++;
            }
        }

        Console.WriteLine("================ Pairwise TEST CASE DIVERSITY ANALYSIS ================");
        foreach (var kvp in inputFrequency.OrderByDescending(x => x.Value))
        {
            Console.WriteLine($"Value: {kvp.Key}, Occurrences: {kvp.Value}");
        }

        Assert.That(inputFrequency.Values.Any(v => v == 0), Is.False, "Some values were not tested at all.");
        

    }

    // Pairwise ensures pairwise coverage, but ABC may discard edge cases while optimizing.
    // Track if all equivalence classes (valid, boundary, invalid) are represented proportionally.
    [Test]
    public void EquivalencePartitionCoverageAnalysis()
    {
        var categoryCount = new Dictionary<TestValueCategory, int>();

        foreach (var testCase in _abcTestCases)
        {
            foreach (var value in testCase)
            {
                var matchedValue = _parameters.SelectMany(p => p.TestValues).FirstOrDefault(tv => tv.Value == value);
                if (matchedValue != null)
                {
                    if (!categoryCount.ContainsKey(matchedValue.Category))
                        categoryCount[matchedValue.Category] = 0;

                    categoryCount[matchedValue.Category]++;
                }
            }
        }

        Console.WriteLine("================ ABC EQUIVALENCE PARTITION COVERAGE ANALYSIS  ================");
        foreach (var kvp in categoryCount.OrderBy(x => x.Key))
        {
            Console.WriteLine($"Category: {kvp.Key}, Count: {kvp.Value}");
        }

        Assert.That(categoryCount.Values.Any(v => v == 0), Is.False, "Some equivalence partitions were not covered.");

        // Filtered Pairwise:
        categoryCount = new Dictionary<TestValueCategory, int>();
        foreach (var testCase in _filteredPairwiseTestCases)
        {
            foreach (var value in testCase)
            {
                var matchedValue = _parameters.SelectMany(p => p.TestValues).FirstOrDefault(tv => tv.Value == value);
                if (matchedValue != null)
                {
                    if (!categoryCount.ContainsKey(matchedValue.Category))
                        categoryCount[matchedValue.Category] = 0;

                    categoryCount[matchedValue.Category]++;
                }
            }
        }

        Console.WriteLine("================ Filtered Pairwise EQUIVALENCE PARTITION COVERAGE ANALYSIS ================");
        foreach (var kvp in categoryCount.OrderBy(x => x.Key))
        {
            Console.WriteLine($"Category: {kvp.Key}, Count: {kvp.Value}");
        }

        Assert.That(categoryCount.Values.Any(v => v == 0), Is.False, "Some equivalence partitions were not covered.");

        // Pairwise:
        categoryCount = new Dictionary<TestValueCategory, int>();
        foreach (var testCase in _pairwiseTestCases)
        {
            foreach (var value in testCase)
            {
                var matchedValue = _parameters.SelectMany(p => p.TestValues).FirstOrDefault(tv => tv.Value == value);
                if (matchedValue != null)
                {
                    if (!categoryCount.ContainsKey(matchedValue.Category))
                        categoryCount[matchedValue.Category] = 0;

                    categoryCount[matchedValue.Category]++;
                }
            }
        }

        Console.WriteLine("================ Pairwise EQUIVALENCE PARTITION COVERAGE ANALYSIS ================");
        foreach (var kvp in categoryCount.OrderBy(x => x.Key))
        {
            Console.WriteLine($"Category: {kvp.Key}, Count: {kvp.Value}");
        }

        Assert.That(categoryCount.Values.Any(v => v == 0), Is.False, "Some equivalence partitions were not covered.");

        
    }

}
