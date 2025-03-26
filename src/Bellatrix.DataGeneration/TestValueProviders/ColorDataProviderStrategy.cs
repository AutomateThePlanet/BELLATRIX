using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class ColorDataProviderStrategy : DataProviderStrategy<int>
{
    public ColorDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Color";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        return null;
    }

    protected override void AddBoundaryValues(List<TestValue> testValues)
    {
        // No boundary logic for color values
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        // No offset logic needed
        return value;
    }
}