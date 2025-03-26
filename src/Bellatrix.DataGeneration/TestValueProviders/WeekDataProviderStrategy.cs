using Bellatrix.DataGeneration.Parameters;
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
        string formatted = FormatString ?? "yyyy-'W'ww";
        string weekFormatted = boundaryInput.ToString(formatted, CultureInfo.InvariantCulture);
        return new TestValue(weekFormatted, typeof(string), category);
    }

    protected override DateTime OffsetValue(DateTime value, BoundaryOffsetDirection direction)
    {
        bool parsed = int.TryParse(PrecisionStep, out int weeks);
        int offset = parsed ? weeks : 1;

        return direction == BoundaryOffsetDirection.Before
            ? value.AddDays(-7 * offset)
            : value.AddDays(7 * offset);
    }
}