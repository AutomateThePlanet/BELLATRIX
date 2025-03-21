using Bellatrix.DataGeneration.Configuration;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public abstract class DataProviderStrategy : IDataProviderStrategy
{
    protected readonly TestValueGenerationSettings Config;
    protected readonly int? MaxBoundary;
    protected readonly int? MinBoundary;

    protected DataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
    {
        MinBoundary = minBoundary;
        MaxBoundary = maxBoundary;
        Config = ConfigurationService.GetSection<TestValueGenerationSettings>();
    }

    public virtual List<TestValue> GenerateTestValues(bool? includeBoundaryValues = null, bool? allowValidEquivalenceClasses = null, bool? allowInvalidEquivalenceClasses = null, params TestValue[] customValues)
    {
        var testValues = new List<TestValue>();

        // Use the provided parameters or fallback to the configuration settings
        var includeBoundary = includeBoundaryValues ?? Config.IncludeBoundaryValues;
        var allowValidEquiv = allowValidEquivalenceClasses ?? Config.AllowValidEquivalenceClasses;
        var allowInvalidEquiv = allowInvalidEquivalenceClasses ?? Config.AllowInvalidEquivalenceClasses;

        // **Boundary Values Handling**
        if (includeBoundary)
        {
            AddBoundaryValues(testValues);
        }

        // **Valid Equivalence Classes**
        if (allowValidEquiv)
        {
            foreach (var value in Config.InputTypeSettings[GetInputTypeName()].ValidEquivalenceClasses)
            {
                testValues.Add(new TestValue(value, TestValueCategory.Valid));
            }
        }

        // **Invalid Equivalence Classes**
        if (allowInvalidEquiv)
        {
            foreach (var value in Config.InputTypeSettings[GetInputTypeName()].InvalidEquivalenceClasses)
            {
                testValues.Add(new TestValue(value, TestValueCategory.Invalid));
            }
        }

        // **Custom Values**
        foreach (var customValue in customValues)
        {
            testValues.Add(new TestValue(customValue.Value, customValue.Category));
        }

        return testValues;
    }

    protected virtual void AddBoundaryValues(List<TestValue> testValues)
    {
        if (MinBoundary != null && MaxBoundary != null)
        {
            testValues.Add(new TestValue(GenerateValue((int)(MinBoundary - 1)), TestValueCategory.BoundaryInvalid));
            testValues.Add(new TestValue(GenerateValue((int)MinBoundary), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GenerateValue((int)MaxBoundary), TestValueCategory.BoundaryValid));
            testValues.Add(new TestValue(GenerateValue((int)MaxBoundary + 1), TestValueCategory.BoundaryInvalid));
        }
    }

    // Template Method Design Pattern
    protected abstract string GenerateValue(int length);

    protected abstract string GetInputTypeName();
}
