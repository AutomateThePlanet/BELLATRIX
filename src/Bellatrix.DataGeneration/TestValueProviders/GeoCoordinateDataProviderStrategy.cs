using Bellatrix.DataGeneration.Parameters;
using System.Globalization;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class GeoCoordinateDataProviderStrategy : DataProviderStrategy<double>
{
    public GeoCoordinateDataProviderStrategy(double? minBoundary = null, double? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "GeoCoordinate";

    protected override Type GetExpectedType() => typeof(string); // Stored as "lat,lon" string

    protected override TestValue CreateBoundaryTestValue(double boundaryInput, TestValueCategory category)
    {
        // Generate lat=boundaryInput, lon=boundaryInput/2 for symmetry
        // We'll treat it as a single string in the format "lat,lon" (e.g., "42.6975,23.3242")
        string coord = $"{boundaryInput.ToString("F6", CultureInfo.InvariantCulture)}," +
                       $"{(boundaryInput / 2).ToString("F6", CultureInfo.InvariantCulture)}";
        return new TestValue(coord, typeof(string), category);
    }

    protected override double OffsetValue(double value, BoundaryOffsetDirection direction)
    {
        bool parsed = double.TryParse(PrecisionStep, NumberStyles.Float, CultureInfo.InvariantCulture, out double step);
        double offset = parsed ? step : 0.01;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}
