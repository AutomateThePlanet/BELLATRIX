using Bellatrix.DataGeneration.TestValueProviders.Base;
using System.Globalization;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class WeekDataProviderStrategy : BoundaryCapableDataProviderStrategy<DateTime>
{
    public WeekDataProviderStrategy(DateTime? minBoundary = null, DateTime? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Week";

    protected override Type GetExpectedType() => typeof(string); // Output as "yyyy-'W'ww"

    protected override TestValue CreateBoundaryTestValue(DateTime boundaryInput, TestValueCategory category)
    {
        var formatted = FormatString ?? "yyyy-'W'ww";
        var weekFormatted = boundaryInput.ToString(formatted, CultureInfo.InvariantCulture);
        return new TestValue(weekFormatted, category);
    }

    protected override DateTime OffsetValue(DateTime value, BoundaryOffsetDirection direction)
    {
        var parsed = int.TryParse(PrecisionStep, out var weeks);
        var offset = parsed ? weeks : 1;

        return direction == BoundaryOffsetDirection.Before
            ? value.AddDays(-7 * offset)
            : value.AddDays(7 * offset);
    }
}