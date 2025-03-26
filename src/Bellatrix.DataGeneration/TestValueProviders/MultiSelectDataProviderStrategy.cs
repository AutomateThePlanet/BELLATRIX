using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class MultiSelectDataProviderStrategy : DataProviderStrategy<int>
{
    public MultiSelectDataProviderStrategy(
             List<object> validOptions,
             List<object> invalidOptions)
             : base(
                 minBoundary: null,
                 maxBoundary: null,
                 supportsBoundaryGeneration: false,
                 customValidEquivalenceClasses: validOptions,
                 customInvalidEquivalenceClasses: invalidOptions)
    {
    }

    protected override string GetInputTypeName() => "MultiSelect";

    protected override Type GetExpectedType() => typeof(string[]);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        return null; // No-op: not applicable
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        return 0; // No-op: not applicable
    }

    protected override void AddBoundaryValues(List<TestValue> testValues)
    {
        // Do nothing — boundary values don’t apply meaningfully to MultiSelect
    }
}
