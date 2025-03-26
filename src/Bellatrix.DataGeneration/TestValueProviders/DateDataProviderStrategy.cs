using Bellatrix.DataGeneration.Parameters;
using System.Globalization;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class DateDataProviderStrategy : DataProviderStrategy<DateTime>
{
    public DateDataProviderStrategy(DateTime? minBoundary = null, DateTime? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Date";

    protected override Type GetExpectedType() => typeof(DateTime);

    protected override TestValue CreateBoundaryTestValue(DateTime boundaryInput, TestValueCategory category)
    {
        return new TestValue(boundaryInput.Date, typeof(DateTime), category);
    }

    protected override DateTime OffsetValue(DateTime value, BoundaryOffsetDirection direction)
    {
        int daysOffset = int.TryParse(PrecisionStep, out int step) ? step : 1;
        return direction == BoundaryOffsetDirection.Before ? value.AddDays(-daysOffset) : value.AddDays(daysOffset);
    }
}