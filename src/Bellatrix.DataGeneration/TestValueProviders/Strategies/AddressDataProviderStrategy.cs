using Bellatrix.DataGeneration.TestValueProviders.Base;
using Bellatrix.DataGeneration.Utilities;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class AddressDataProviderStrategy : BoundaryCapableDataProviderStrategy<int>
{
    public AddressDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Address";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        string generated = Faker.Address.FullAddress()
            .EnsureMaxLength(boundaryInput)
            .EnsureMinLength(boundaryInput);

        return new TestValue(generated, category);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        bool parsed = int.TryParse(PrecisionStep, out int step);
        int offset = parsed ? step : 1;
        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}