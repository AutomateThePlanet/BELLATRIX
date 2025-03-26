using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class AddressDataProviderStrategy : DataProviderStrategy<int>
{
    public AddressDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Address";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        var baseAddress = "123 Main St, Springfield, ZZ 12345";
        if (boundaryInput <= baseAddress.Length)
        {
            return new TestValue(baseAddress.Substring(0, boundaryInput), typeof(string), category);
        }

        string extended = baseAddress + " " + new string('A', boundaryInput - baseAddress.Length);
        return new TestValue(extended, typeof(string), category);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        bool parsed = int.TryParse(PrecisionStep, out int step);
        int offset = parsed ? step : 1;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}