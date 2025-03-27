using Bellatrix.DataGeneration.Parameters;
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
        bool parsedSuccessfully = decimal.TryParse(PrecisionStep, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal step);
        decimal offset = parsedSuccessfully ? step : 0.01m;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}