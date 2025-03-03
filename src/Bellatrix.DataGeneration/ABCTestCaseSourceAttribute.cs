using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Bellatrix.DataGeneration;
using Bellatrix.DataGeneration.Models;
using Bellatrix.DataGeneration.Parameters;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.OutputGenerators;
using NUnit.Framework.Internal.Builders;
using NUnit.Framework.Internal;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ABCTestCaseSourceAttribute : Attribute, ITestBuilder
{
    private readonly string _sourceMethodName;
    private readonly TestCaseCategory _category;
    private readonly HybridArtificialBeeColonyConfig _abcConfig;

    public ABCTestCaseSourceAttribute(
        string sourceMethodName,
        TestCaseCategory category,
        int totalPopulationGenerations = 100,
        double mutationRate = 0.5,
        double finalPopulationSelectionRatio = 0.6,
        double eliteSelectionRatio = 0.6,
        double onlookerSelectionRatio = 0.5,
        double scoutSelectionRatio = 0.5,
        bool enableOnlookerSelection = true,
        bool enableScoutPhase = true,
        bool enforceMutationUniqueness = false,
        double stagnationThresholdPercentage = 0.75,
        double coolingRate = 85,
        bool allowMultipleInvalidInputs = false)
    {
        _sourceMethodName = sourceMethodName;
        _category = category;
        _abcConfig = new HybridArtificialBeeColonyConfig
        {
            TotalPopulationGenerations = totalPopulationGenerations,
            MutationRate = mutationRate,
            FinalPopulationSelectionRatio = finalPopulationSelectionRatio,
            EliteSelectionRatio = eliteSelectionRatio,
            OnlookerSelectionRatio = onlookerSelectionRatio,
            ScoutSelectionRatio = scoutSelectionRatio,
            EnableOnlookerSelection = enableOnlookerSelection,
            EnableScoutPhase = enableScoutPhase,
            EnforceMutationUniqueness = enforceMutationUniqueness,
            StagnationThresholdPercentage = stagnationThresholdPercentage,
            CoolingRate = coolingRate,
            AllowMultipleInvalidInputs = allowMultipleInvalidInputs
        };
    }

    public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
    {
        // Retrieve test parameter method dynamically via reflection
        MethodInfo sourceMethod = method.TypeInfo.Type.GetMethod(_sourceMethodName, BindingFlags.Public | BindingFlags.Static);
        if (sourceMethod == null)
        {
            throw new InvalidOperationException($"No static method named '{_sourceMethodName}' found in test class.");
        }

        // Get test parameters from the test class
        var parameters = sourceMethod.Invoke(null, null) as List<IInputParameter>;
        if (parameters == null)
        {
            throw new InvalidOperationException("The method did not return a valid List<IInputParameter>.");
        }

        // Start measuring time
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Initialize ABC Generator with overridden config
        var abcGenerator = new HybridArtificialBeeColonyTestCaseGenerator(_abcConfig);
        var testCases = abcGenerator.RunABCAlgorithm(parameters);

        // Stop measuring time
        stopwatch.Stop();

        // Debug output for generation time
        Console.WriteLine($"Test case generation completed in {stopwatch.ElapsedMilliseconds} ms.");

        // Filter test cases based on TestCaseCategory
        IEnumerable<TestCase> filteredCases = FilterTestCasesByCategory(testCases, _category);

        // Create NUnit test cases dynamically
        foreach (var testCase in filteredCases)
        {
            var parameters1 = new TestCaseParameters(testCase.Values.Select(v => (object)v.Value).ToArray());

            yield return new NUnitTestCaseBuilder().BuildTestMethod(
                method,
                suite,
                parameters1);
        }
    }

    private IEnumerable<TestCase> FilterTestCasesByCategory(IEnumerable<TestCase> testCases, TestCaseCategory category)
    {
        return category switch
        {
            TestCaseCategory.Valid => testCases.Where(tc => tc.Values.All(v => v.Category is TestValueCategory.Valid or TestValueCategory.BoundaryValid)),
            TestCaseCategory.Validation => testCases.Where(tc => tc.Values.Any(v => v.Category is TestValueCategory.Invalid or TestValueCategory.BoundaryInvalid)),
            _ => testCases // All cases
        };
    }
}
