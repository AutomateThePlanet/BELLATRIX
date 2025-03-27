using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class PhoneDataProviderStrategy : BoundaryCapableDataProviderStrategy<int>
{
    public PhoneDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Phone";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        string phone = GeneratePhoneNumber(boundaryInput);
        return new TestValue(phone, category);
    }

    private string GeneratePhoneNumber(int totalLength)
    {
        const string countryCode = "+359";
        int localNumberLength = Math.Max(totalLength - countryCode.Length, 1);
        string localNumber = new string('9', localNumberLength);
        return countryCode + localNumber;
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        bool parsedSuccessfully = int.TryParse(PrecisionStep, out int step);
        int offset = parsedSuccessfully ? step : 1;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}