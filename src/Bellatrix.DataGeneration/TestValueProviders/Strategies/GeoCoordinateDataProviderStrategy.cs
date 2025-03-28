using Bellatrix.DataGeneration.TestValueProviders.Base;
using System.Globalization;

namespace Bellatrix.DataGeneration.TestValueProviders;
public class GeoCoordinateDataProviderStrategy : BoundaryCapableDataProviderStrategy<double>
{
    public GeoCoordinateDataProviderStrategy(double? minBoundary = null, double? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "GeoCoordinate";

    protected override Type GetExpectedType() => typeof(string); // Stored as "lat,lon" string

    protected override TestValue CreateBoundaryTestValue(double boundaryInput, TestValueCategory category)
    {
        var lat = boundaryInput.ToString(FormatString ?? "F6", CultureInfo.InvariantCulture);
        var lon = (boundaryInput / 2).ToString(FormatString ?? "F6", CultureInfo.InvariantCulture);

        var coord = $"{lat},{lon}";
        return new TestValue(coord, category);
    }

    protected override double OffsetValue(double value, BoundaryOffsetDirection direction)
    {
        var parsed = double.TryParse(PrecisionStep, NumberStyles.Float, CultureInfo.InvariantCulture, out var step);
        var offset = parsed ? step : 0.01;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}
