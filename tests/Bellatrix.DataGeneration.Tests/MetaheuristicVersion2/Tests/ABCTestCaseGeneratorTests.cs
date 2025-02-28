//using System;
//using System.Collections.Generic;
//using System.Linq;
//using NUnit.Framework;
//using Bellatrix.Web.Tests.MetaheuristicVersion2;
//using Bellatrix.Web.Tests.MetaheuristicVersion2.Core;

//namespace Bellatrix.Web.Tests.MetaheuristicVersion2.Tests;

//[TestFixture]
//public class ABCTestCaseGeneratorTests
//{
//    private List<IInputParameter> _parameters;
//    //private HybridArtificialBeeColonyTestCaseGenerator _generator;
//    //private List<string[]> _generatedTestCases;

//    [SetUp]
//    public void SetUp()
//    {
//        _parameters = new List<IInputParameter>
//        {
//            new ComponentInputParameter<TextField>(isManualMode: true, customValues: Tuple.Create("Normal1", TestValueCategory.Valid)),
//            new ComponentInputParameter<TextField>(isManualMode: true, customValues: Tuple.Create("NormalA", TestValueCategory.Valid)),
//            new ComponentInputParameter<Email>(isManualMode: true, customValues: Tuple.Create("test@mail.com", TestValueCategory.Valid)),
//            new ComponentInputParameter<Phone>(isManualMode: true, customValues: Tuple.Create("+359888888888", TestValueCategory.Valid)),
//            new ComponentInputParameter<TextField>(isManualMode: true, customValues: Tuple.Create("NormalX", TestValueCategory.Valid))
//        };

//        //_generator = new NWiseTestCaseGeneratorTests(populationSize: 15, maxIterations: 30, mutationRate: 0.2, allowMultipleInvalidInputs: false);
//        //_generatedTestCases = _generator.RunABCAlgorithm(_parameters);

//        //var generator1 = new NWiseTestCaseGeneratorTests(outputGenerator: new FactoryMethodTestCaseOutputGenerator());
//        //generator1.GenerateTestCases("GenerateCheckoutTestCases", _parameters);
//    }

//    [Test]
//    public void AllBoundaryValuesAreTestedAtLeastOnce()
//    {
//        var allBoundaryValues = _parameters
//            .SelectMany(p => p.TestValues.Where(tv => tv.Category == TestValueCategory.BoundaryValid  || tv.Category == TestValueCategory.BoundaryInvalid).Select(tv => tv.Value))
//            .ToHashSet();

//        var testedValues = _generatedTestCases.SelectMany(tc => tc).ToHashSet();

//        foreach (var boundaryValue in allBoundaryValues)
//        {
//            Assert.That(testedValues.Contains(boundaryValue), Is.True,
//                $"Boundary value '{boundaryValue}' was not included in any test case.");
//        }
//    }

//    [Test]
//    public void EachEquivalencePartitionIsUsedAtLeastOnce()
//    {
//        var categories = Enum.GetValues(typeof(TestValueCategory)).Cast<TestValueCategory>().ToList();

//        foreach (var category in categories)
//        {
//            var isCategoryCovered = _parameters.Any(parameter =>
//                parameter.TestValues.Any(tv => tv.Category == category &&
//                                               _generatedTestCases.Any(tc => tc.Contains(tv.Value))));

//            Assert.That(isCategoryCovered, Is.True, $"Category {category} is not covered in the generated test cases.");
//        }
//    }

//    [Test]
//    public void CompareTestCaseCountWithPairwiseTesting()
//    {
//        var pairwiseTestCaseCount = GeneratePairwiseTestCases(_parameters).Count;
//        var abcTestCaseCount = _generatedTestCases.Count;

//        Console.WriteLine($"Pairwise Test Case Count: {pairwiseTestCaseCount}");
//        Console.WriteLine($"ABC Test Case Count: {abcTestCaseCount}");

//        Assert.That(abcTestCaseCount, Is.LessThan(pairwiseTestCaseCount),
//            $"ABC test case generator should produce fewer cases than Pairwise, but generated {abcTestCaseCount} vs {pairwiseTestCaseCount}.");
//    }

//    [Test]
//    public void NoTestCaseHasMoreThanOneInvalidInput()
//    {
//        foreach (var testCase in _generatedTestCases)
//        {
//            var invalidCount = testCase.Count(value =>
//                _parameters.Any(p => p.TestValues.Any(tv => tv.Value == value && tv.Category == TestValueCategory.Invalid)));

//            Assert.That(invalidCount, Is.LessThanOrEqualTo(1),
//                $"Test case [{string.Join(", ", testCase)}] has more than one invalid input, violating the constraints.");
//        }
//    }

//    [Test]
//    public void GeneratedTestCasesAreUnique()
//    {
//        var distinctTestCases = _generatedTestCases.Distinct(new TestCaseComparer()).ToList();
//        Assert.That(distinctTestCases.Count, Is.EqualTo(_generatedTestCases.Count), "Duplicate test cases found in the generated set.");
//    }

//    [Test]
//    public void ABCGeneratorProducesFewerTestCasesThanFullPermutation()
//    {
//        var fullPermutationCount = _parameters.Select(p => p.TestValues.Count).Aggregate(1, (a, b) => a * b);
//        var abcTestCaseCount = _generatedTestCases.Count;

//        Assert.That(abcTestCaseCount, Is.LessThan(fullPermutationCount),
//            $"ABC Generator produced {abcTestCaseCount} cases, which is not lower than full permutation count ({fullPermutationCount}).");
//    }

//    public class TestCaseComparer : IEqualityComparer<string[]>
//    {
//        public bool Equals(string[] x, string[] y)
//        {
//            return x.SequenceEqual(y);
//        }

//        public int GetHashCode(string[] obj)
//        {
//            return string.Join(",", obj).GetHashCode();
//        }
//    }

//    private List<string[]> GeneratePairwiseTestCases(List<IInputParameter> parameters)
//    {
//        var pairwiseTestCases = new List<string[]>();
//        var totalCombinations = parameters.Select(p => p.TestValues.Count).Aggregate(1, (a, b) => a * b);

//        for (var i = 0; i < Math.Min(totalCombinations, 50); i++)
//        {
//            var testCase = parameters.Select(p => p.TestValues[i % p.TestValues.Count].Value).ToArray();
//            pairwiseTestCases.Add(testCase);
//        }

//        return pairwiseTestCases;
//    }
//}
