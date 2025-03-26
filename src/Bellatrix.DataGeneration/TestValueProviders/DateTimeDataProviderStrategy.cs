using Bellatrix.DataGeneration.Parameters;
using System.Globalization;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class DateTimeDataProviderStrategy : BoundaryCapableDataProviderStrategy<DateTime>
{
    public DateTimeDataProviderStrategy(DateTime? minBoundary = null, DateTime? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "DateTime";

    protected override Type GetExpectedType() => typeof(DateTime);

    protected override TestValue CreateBoundaryTestValue(DateTime boundaryInput, TestValueCategory category)
    {
        string formatted = boundaryInput.ToString(FormatString ?? "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
        return new TestValue(formatted, typeof(string), category);
    }

    protected override DateTime OffsetValue(DateTime value, BoundaryOffsetDirection direction)
    {
        bool parsed = double.TryParse(PrecisionStep, NumberStyles.Float, CultureInfo.InvariantCulture, out double step);
        if (!parsed) step = 1.0;

        TimeSpan offset = PrecisionStepUnit switch
        {
            "Seconds" => TimeSpan.FromSeconds(step),
            "Minutes" => TimeSpan.FromMinutes(step),
            "Hours" => TimeSpan.FromHours(step),
            "Days" => TimeSpan.FromDays(step),
            "Milliseconds" => TimeSpan.FromMilliseconds(step),
            _ => TimeSpan.FromMinutes(step)
        };

        return direction == BoundaryOffsetDirection.Before
            ? value.Subtract(offset)
            : value.Add(offset);
    }
}