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

    public virtual List<TestValue> GenerateTestValues(
        bool? includeBoundaryValues = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] customValues)
    {
        var testValues = new List<TestValue>();
        var includeBoundary = includeBoundaryValues ?? Config.IncludeBoundaryValues;
        var allowValidEquiv = allowValidEquivalenceClasses ?? Config.AllowValidEquivalenceClasses;
        var allowInvalidEquiv = allowInvalidEquivalenceClasses ?? Config.AllowInvalidEquivalenceClasses;

        if (includeBoundary) AddBoundaryValues(testValues);

        if (allowValidEquiv)
        {
            foreach (var value in Config.InputTypeSettings[GetInputTypeName()].ValidEquivalenceClasses)
                testValues.Add(new TestValue(value, GetExpectedType(), TestValueCategory.Valid));
        }

        if (allowInvalidEquiv)
        {
            foreach (var value in Config.InputTypeSettings[GetInputTypeName()].InvalidEquivalenceClasses)
                testValues.Add(new TestValue(value, GetExpectedType(), TestValueCategory.Invalid));
        }

        foreach (var customValue in customValues)
        {
            testValues.Add(new TestValue(customValue.Value, customValue.ExpectedType, customValue.Category));
        }

        return testValues;
    }

    protected virtual void AddBoundaryValues(List<TestValue> testValues)
    {
        if (MinBoundary != null && MaxBoundary != null)
        {
            testValues.Add(CreateBoundaryTestValue(MinBoundary.Value - 1, TestValueCategory.BoundaryInvalid));
            testValues.Add(CreateBoundaryTestValue(MinBoundary.Value, TestValueCategory.BoundaryValid));
            testValues.Add(CreateBoundaryTestValue(MaxBoundary.Value, TestValueCategory.BoundaryValid));
            testValues.Add(CreateBoundaryTestValue(MaxBoundary.Value + 1, TestValueCategory.BoundaryInvalid));
        }
    }

    protected abstract TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category);
    protected abstract Type GetExpectedType();
    protected abstract string GetInputTypeName();
}

