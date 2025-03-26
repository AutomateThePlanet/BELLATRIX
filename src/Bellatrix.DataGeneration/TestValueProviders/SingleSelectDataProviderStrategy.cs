using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class SingleSelectDataProviderStrategy : DataProviderStrategy<int>
{
    public SingleSelectDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "SingleSelect";

    protected override Type GetExpectedType() => typeof(string); // Even if the internal enum is int, frontends typically use strings.

    protected override void AddBoundaryValues(List<TestValue> testValues)
    {
        // Do nothing: Select/Enum has no meaningful boundary values
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        return value; // No-op: not applicable
    }

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        return null; // No-op: not applicable
    }
}
