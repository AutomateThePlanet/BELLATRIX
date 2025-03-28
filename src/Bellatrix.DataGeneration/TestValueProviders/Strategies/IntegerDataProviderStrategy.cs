using Bellatrix.DataGeneration.TestValueProviders.Base;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class IntegerDataProviderStrategy : BoundaryCapableDataProviderStrategy<int>
{
    public IntegerDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Integer";

    protected override Type GetExpectedType() => typeof(int);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        return new TestValue(boundaryInput, category);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        var parsedSuccessfully = int.TryParse(PrecisionStep, out var step);
        var offset = parsedSuccessfully ? step : 1;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}