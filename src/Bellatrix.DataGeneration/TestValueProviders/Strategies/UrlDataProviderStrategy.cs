using Bellatrix.DataGeneration.TestValueProviders.Base;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class UrlDataProviderStrategy : BoundaryCapableDataProviderStrategy<int>
{
    public UrlDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "URL";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        var baseUrl = Faker.Internet.Url(); // e.g., "https://example.com"
        var finalUrl = baseUrl
            .EnsureMaxLength(boundaryInput)
            .EnsureMinLength(boundaryInput, paddingChar: 'x'); // add trailing chars to hit boundary

        return new TestValue(finalUrl, category);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        var parsed = int.TryParse(PrecisionStep, out var step);
        var offset = parsed ? step : 1;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}