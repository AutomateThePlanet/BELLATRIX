using Bellatrix.DataGeneration.TestValueProviders.Base;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class UsernameDataProviderStrategy : BoundaryCapableDataProviderStrategy<int>
{
    public UsernameDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Username";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        var rawUsername = Faker.Internet.UserName(); // e.g., "anton.angelov42"
        var finalUsername = rawUsername
            .EnsureMaxLength(boundaryInput)
            .EnsureMinLength(boundaryInput, paddingChar: 'x');

        return new TestValue(finalUsername, category);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        var parsed = int.TryParse(PrecisionStep, out var step);
        var offset = parsed ? step : 1;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}