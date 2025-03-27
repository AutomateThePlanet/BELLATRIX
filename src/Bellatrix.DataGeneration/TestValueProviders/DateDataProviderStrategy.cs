using System.Globalization;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class DateDataProviderStrategy : BoundaryCapableDataProviderStrategy<DateTime>
{
    public DateDataProviderStrategy(DateTime? minBoundary = null, DateTime? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Date";

    protected override Type GetExpectedType() => typeof(DateTime);

    protected override TestValue CreateBoundaryTestValue(DateTime boundaryInput, TestValueCategory category)
    {
        string formatted = boundaryInput.ToString(FormatString ?? "yyyy-MM-dd", CultureInfo.InvariantCulture);
        return new TestValue(formatted, category);
    }

    protected override DateTime OffsetValue(DateTime value, BoundaryOffsetDirection direction)
    {
        bool parsed = int.TryParse(PrecisionStep, out int step);
        if (!parsed) step = 1;

        return PrecisionStepUnit switch
        {
            "Years" => direction == BoundaryOffsetDirection.Before ? value.AddYears(-step) : value.AddYears(step),
            "Months" => direction == BoundaryOffsetDirection.Before ? value.AddMonths(-step) : value.AddMonths(step),
            _ => direction == BoundaryOffsetDirection.Before ? value.AddDays(-step) : value.AddDays(step)
        };
    }
}