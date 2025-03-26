using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class BooleanDataProviderStrategy : DataProviderStrategy
{
    public BooleanDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Boolean";

    protected override Type GetExpectedType() => typeof(bool);

    // Since boolean doesn't have numeric boundaries, simulate toggle logic
    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        // Toggle: Even boundaryInput → true, Odd → false
        bool simulated = boundaryInput % 2 == 0;
        return new TestValue(simulated, typeof(bool), category);
    }
}