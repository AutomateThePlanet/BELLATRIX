using Bellatrix.DataGeneration.Configuration;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public abstract class BoundaryCapableDataProviderStrategy<T> : EquivalenceOnlyDataProviderStrategy
        where T : struct, IComparable<T>
{
    protected readonly T? MinBoundary;
    protected readonly T? MaxBoundary;
    protected readonly string PrecisionStep;
    protected readonly string PrecisionStepUnit;
    protected readonly string FormatString;

    protected BoundaryCapableDataProviderStrategy(
        T? minBoundary = null,
        T? maxBoundary = null,
        List<object> customValidEquivalenceClasses = null,
        List<object> customInvalidEquivalenceClasses = null)
        : base(customValidEquivalenceClasses, customInvalidEquivalenceClasses)
    {
        MinBoundary = minBoundary;
        MaxBoundary = maxBoundary;

        var config = ConfigurationService.GetSection<TestValueGenerationSettings>();
        var settings = config.InputTypeSettings[GetInputTypeName()];
        PrecisionStep = settings.PrecisionStep;
        PrecisionStepUnit = settings.PrecisionStepUnit;
        FormatString = settings.FormatString;
    }

    public override List<TestValue> GenerateTestValues(
        bool? includeBoundaryValues = null,
        bool? allowValidEquivalenceClasses = null,
        bool? allowInvalidEquivalenceClasses = null,
        params TestValue[] preciseTestValues)
    {
        var testValues = base.GenerateTestValues(
            includeBoundaryValues: false, // Let us handle boundary addition here
            allowValidEquivalenceClasses,
            allowInvalidEquivalenceClasses,
            preciseTestValues);

        if ((includeBoundaryValues ?? true) && MinBoundary != null && MaxBoundary != null)
        {
            testValues.Add(CreateBoundaryTestValue(OffsetValue(MinBoundary.Value, BoundaryOffsetDirection.Before), TestValueCategory.BoundaryInvalid));
            testValues.Add(CreateBoundaryTestValue(MinBoundary.Value, TestValueCategory.BoundaryValid));
            testValues.Add(CreateBoundaryTestValue(MaxBoundary.Value, TestValueCategory.BoundaryValid));
            testValues.Add(CreateBoundaryTestValue(OffsetValue(MaxBoundary.Value, BoundaryOffsetDirection.After), TestValueCategory.BoundaryInvalid));
        }

        return testValues;
    }

    protected abstract T OffsetValue(T value, BoundaryOffsetDirection direction);
    protected abstract TestValue CreateBoundaryTestValue(T boundaryInput, TestValueCategory category);
}