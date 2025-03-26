using Bellatrix.DataGeneration.Parameters;
using System.Globalization;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class TimeDataProviderStrategy : BoundaryCapableDataProviderStrategy<TimeSpan>
{
    public TimeDataProviderStrategy(TimeSpan? minBoundary = null, TimeSpan? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Time";

    protected override Type GetExpectedType() => typeof(TimeSpan);

    protected override TestValue CreateBoundaryTestValue(TimeSpan boundaryInput, TestValueCategory category)
    {
        string formatted = boundaryInput.ToString(FormatString ?? @"hh\:mm", CultureInfo.InvariantCulture);
        return new TestValue(formatted, typeof(string), category);
    }

    protected override TimeSpan OffsetValue(TimeSpan value, BoundaryOffsetDirection direction)
    {
        bool parsed = double.TryParse(PrecisionStep, NumberStyles.Float, CultureInfo.InvariantCulture, out double step);
        if (!parsed) step = 1;

        TimeSpan offset = PrecisionStepUnit switch
        {
            "Seconds" => TimeSpan.FromSeconds(step),
            "Minutes" => TimeSpan.FromMinutes(step),
            "Hours" => TimeSpan.FromHours(step),
            _ => TimeSpan.FromMinutes(step) // default fallback
        };

        return direction == BoundaryOffsetDirection.Before
            ? value.Subtract(offset)
            : value.Add(offset);
    }
}