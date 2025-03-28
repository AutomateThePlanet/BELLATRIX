using Bellatrix.DataGeneration.TestValueProviders.Base;
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
        // Generate a realistic currency value from Bogus (not required but possible)
        var raw = Faker.Finance.Amount(min: boundaryInput, max: boundaryInput);

        // Apply formatting
        var formatted = raw.ToString(FormatString ?? "F2", CultureInfo.InvariantCulture);

        return new TestValue(formatted, category);
    }

    protected override decimal OffsetValue(decimal value, BoundaryOffsetDirection direction)
    {
        var parsed = decimal.TryParse(PrecisionStep, NumberStyles.Float, CultureInfo.InvariantCulture, out var step);
        var offset = parsed ? step : 0.01m;

        return direction == BoundaryOffsetDirection.Before
            ? value - offset
            : value + offset;
    }
}