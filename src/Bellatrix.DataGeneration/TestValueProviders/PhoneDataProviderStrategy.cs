using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class PhoneDataProviderStrategy : DataProviderStrategy
{
    public PhoneDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Phone";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        var phone = GeneratePhoneNumber(boundaryInput);
        return new TestValue(phone, typeof(string), category);
    }

    private string GeneratePhoneNumber(int totalLength)
    {
        const string countryCode = "+359";
        int localNumberLength = Math.Max(totalLength - countryCode.Length, 1);
        string localNumber = new string('9', localNumberLength);
        return countryCode + localNumber;
    }
}