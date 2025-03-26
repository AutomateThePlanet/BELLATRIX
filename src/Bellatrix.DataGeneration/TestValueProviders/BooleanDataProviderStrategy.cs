using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class BooleanDataProviderStrategy : DataProviderStrategy
{
    public BooleanDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Boolean";

    protected override string GenerateValue(int length)
    {
        // Alternate based on length to generate a realistic boolean sample
        return length % 2 == 0 ? "true" : "false";
    }
}