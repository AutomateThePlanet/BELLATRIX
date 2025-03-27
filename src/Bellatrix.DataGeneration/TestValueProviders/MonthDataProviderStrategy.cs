using Bellatrix.DataGeneration.Parameters;
using System.Globalization;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class MonthDataProviderStrategy : BoundaryCapableDataProviderStrategy<DateTime>
{
    public MonthDataProviderStrategy(DateTime? minBoundary = null, DateTime? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Month";

    protected override Type GetExpectedType() => typeof(string); // Output as "yyyy-MM"

    protected override TestValue CreateBoundaryTestValue(DateTime boundaryInput, TestValueCategory category)
    {
        string formatted = boundaryInput.ToString(FormatString ?? "yyyy-MM", CultureInfo.InvariantCulture);
        return new TestValue(formatted, category);
    }

    protected override DateTime OffsetValue(DateTime value, BoundaryOffsetDirection direction)
    {
        bool parsed = int.TryParse(PrecisionStep, out int months);
        int offset = parsed ? months : 1;

        return direction == BoundaryOffsetDirection.Before
            ? value.AddMonths(-offset)
            : value.AddMonths(offset);
    }
}