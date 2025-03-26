using Bellatrix.DataGeneration.Parameters;
using System.Globalization;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class CurrencyDataProviderStrategy : BoundaryCapableDataProviderStrategy<decimal>
{
    public CurrencyDataProviderStrategy(decimal? minBoundary = null, decimal? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Currency";

    protected override Type GetExpectedType() => typeof(decimal);

    protected override TestValue CreateBoundaryTestValue(decimal boundaryInput, TestValueCategory category)
    {
        // Format can be adjusted if needed to return as "$123.45" or "123.45 EUR"
        string formatted = boundaryInput.ToString(FormatString ?? "F2", CultureInfo.InvariantCulture);
        return new TestValue(formatted, typeof(decimal), category);
    }

    protected override decimal OffsetValue(decimal value, BoundaryOffsetDirection direction)
    {
        bool parsed = decimal.TryParse(PrecisionStep, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal step);
        decimal offset = parsed ? step : 0.01m;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}