using Bellatrix.DataGeneration.Parameters;
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
        string lat = boundaryInput.ToString(FormatString ?? "F6", CultureInfo.InvariantCulture);
        string lon = (boundaryInput / 2).ToString(FormatString ?? "F6", CultureInfo.InvariantCulture);

        string coord = $"{lat},{lon}";
        return new TestValue(coord, typeof(string), category);
    }

    protected override double OffsetValue(double value, BoundaryOffsetDirection direction)
    {
        bool parsed = double.TryParse(PrecisionStep, NumberStyles.Float, CultureInfo.InvariantCulture, out double step);
        double offset = parsed ? step : 0.01;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}
