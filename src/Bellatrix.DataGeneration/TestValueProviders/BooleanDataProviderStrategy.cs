using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class BooleanDataProviderStrategy : DataProviderStrategy<int>
{
    public BooleanDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Boolean";

    protected override Type GetExpectedType() => typeof(bool);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        bool simulated = boundaryInput % 2 == 0;
        return new TestValue(simulated, typeof(bool), category);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        return direction == BoundaryOffsetDirection.Before ? value - 1 : value + 1;
    }
}