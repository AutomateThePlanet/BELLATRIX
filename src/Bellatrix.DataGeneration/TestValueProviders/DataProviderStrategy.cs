using Bellatrix.DataGeneration.Configuration;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public abstract class DataProviderStrategy<T> : IDataProviderStrategy
        where T : struct, IComparable<T>
{
    protected readonly TestValueGenerationSettings Config;
    protected readonly T? MaxBoundary;
    protected readonly T? MinBoundary;
    protected readonly string PrecisionStep;
    protected readonly string PrecisionStepUnit;
    protected readonly bool SupportsBoundaryGeneration;

    private readonly List<object> _customValidEquivalenceClasses;
    private readonly List<object> _customInvalidEquivalenceClasses;

    protected DataProviderStrategy(
        T? minBoundary = null,
        T? maxBoundary = null,
        bool supportsBoundaryGeneration = true,
        List<object> customValidEquivalenceClasses = null,
        List<object> customInvalidEquivalenceClasses = null)
    {
        MinBoundary = minBoundary;
        MaxBoundary = maxBoundary;
        SupportsBoundaryGeneration = supportsBoundaryGeneration;
        _customValidEquivalenceClasses = customValidEquivalenceClasses;
        _customInvalidEquivalenceClasses = customInvalidEquivalenceClasses;

        Config = ConfigurationService.GetSection<TestValueGenerationSettings>();
        PrecisionStep = Config.InputTypeSettings[GetInputTypeName()].PrecisionStep;
        PrecisionStepUnit = Config.InputTypeSettings[GetInputTypeName()].PrecisionStepUnit;
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

        if (SupportsBoundaryGeneration && includeBoundary)
        {
            AddBoundaryValues(testValues);
        }

        if (allowValidEquiv)
        {
            var source = _customValidEquivalenceClasses == null ?
                         Config.InputTypeSettings[GetInputTypeName()].ValidEquivalenceClasses.Select(x => (object)x) : _customValidEquivalenceClasses;

            foreach (var value in source)
            {
                testValues.Add(new TestValue(value, GetExpectedType(), TestValueCategory.Valid));
            }
        }

        if (allowInvalidEquiv)
        {
            var source = _customInvalidEquivalenceClasses == null ?
                      Config.InputTypeSettings[GetInputTypeName()].InvalidEquivalenceClasses.Select(x => (object)x) : _customInvalidEquivalenceClasses;

            foreach (var value in source)
            {
                testValues.Add(new TestValue(value, GetExpectedType(), TestValueCategory.Invalid));
            }
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
            testValues.Add(CreateBoundaryTestValue(OffsetValue(MinBoundary.Value, BoundaryOffsetDirection.Before), TestValueCategory.BoundaryInvalid));
            testValues.Add(CreateBoundaryTestValue(MinBoundary.Value, TestValueCategory.BoundaryValid));
            testValues.Add(CreateBoundaryTestValue(MaxBoundary.Value, TestValueCategory.BoundaryValid));
            testValues.Add(CreateBoundaryTestValue(OffsetValue(MaxBoundary.Value, BoundaryOffsetDirection.After), TestValueCategory.BoundaryInvalid));
        }
    }

    protected abstract T OffsetValue(T value, BoundaryOffsetDirection direction);
    protected abstract TestValue CreateBoundaryTestValue(T boundaryInput, TestValueCategory category);
    protected abstract Type GetExpectedType();
    protected abstract string GetInputTypeName();
}