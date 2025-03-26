using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class IntegerDataProviderStrategy : DataProviderStrategy
{
    public IntegerDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Integer";

    protected override Type GetExpectedType() => typeof(int);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        // Return the boundary value as an integer
        return new TestValue(boundaryInput, typeof(int), category);
    }
}