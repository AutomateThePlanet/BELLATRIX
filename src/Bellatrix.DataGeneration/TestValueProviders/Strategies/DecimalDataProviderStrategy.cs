using Bellatrix.DataGeneration.TestValueProviders.Base;
using System.Globalization;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class DecimalDataProviderStrategy : BoundaryCapableDataProviderStrategy<decimal>
{
    public DecimalDataProviderStrategy(decimal? minBoundary = null, decimal? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Decimal";

    protected override Type GetExpectedType() => typeof(decimal);

    protected override TestValue CreateBoundaryTestValue(decimal boundaryInput, TestValueCategory category)
    {
        return new TestValue(boundaryInput, category);
    }

    protected override decimal OffsetValue(decimal value, BoundaryOffsetDirection direction)
    {
        var parsedSuccessfully = decimal.TryParse(PrecisionStep, NumberStyles.Float, CultureInfo.InvariantCulture, out var step);
        var offset = parsedSuccessfully ? step : 0.01m;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}