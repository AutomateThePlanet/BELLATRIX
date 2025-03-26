using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class UrlDataProviderStrategy : DataProviderStrategy<int>
{
    public UrlDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "URL";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        // Generate "https://www.a...a.com" of appropriate length
        const string prefix = "https://www.";
        const string suffix = ".com";
        int coreLength = Math.Max(1, boundaryInput - prefix.Length - suffix.Length);

        string domain = new string('a', coreLength);
        string url = prefix + domain + suffix;

        return new TestValue(url, typeof(string), category);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        bool parsed = int.TryParse(PrecisionStep, out int step);
        int offset = parsed ? step : 1;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}