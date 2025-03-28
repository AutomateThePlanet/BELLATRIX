using Bellatrix.DataGeneration.TestValueProviders.Base;

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
        // Start with a realistic phone number
        var rawPhone = Faker.Phone.PhoneNumber(); // e.g., "+1 (555) 123-4567"
        var finalPhone = rawPhone
            .EnsureMaxLength(boundaryInput)
            .EnsureMinLength(boundaryInput, paddingChar: '9'); // padding with digits to keep numeric feel

        return new TestValue(finalPhone, category);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        var parsedSuccessfully = int.TryParse(PrecisionStep, out var step);
        var offset = parsedSuccessfully ? step : 1;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}