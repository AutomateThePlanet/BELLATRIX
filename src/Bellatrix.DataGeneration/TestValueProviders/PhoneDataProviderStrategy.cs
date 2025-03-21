using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class PhoneDataProviderStrategy : DataProviderStrategy
{
    public PhoneDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null) 
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Phone";

    protected override string GenerateValue(int length)
    {
        return "+359" + new string('9', length - 4);
    }
}