using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class EmailDataProviderStrategy : DataProviderStrategy<int>
{
    public EmailDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Email";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        var email = GenerateEmailWithLength(boundaryInput);
        return new TestValue(email, typeof(string), category);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        var offset = int.TryParse(PrecisionStep, out int step) ? step : 1;
        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }

    private string GenerateEmailWithLength(int totalLength)
    {
        const string domain = "@mail.com";
        int localPartLength = Math.Max(totalLength - domain.Length, 1);
        string localPart = new string('a', localPartLength);
        return localPart + domain;
    }
}