using Bellatrix.DataGeneration.TestValueProviders.Base;
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
        var formatted = boundaryInput.ToString(FormatString ?? @"hh\:mm", CultureInfo.InvariantCulture);
        return new TestValue(formatted, category);
    }

    protected override TimeSpan OffsetValue(TimeSpan value, BoundaryOffsetDirection direction)
    {
        var parsed = double.TryParse(PrecisionStep, NumberStyles.Float, CultureInfo.InvariantCulture, out var step);
        if (!parsed) step = 1;

        var offset = PrecisionStepUnit switch
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