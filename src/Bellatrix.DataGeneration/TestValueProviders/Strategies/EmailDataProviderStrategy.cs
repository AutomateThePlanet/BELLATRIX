using Bellatrix.DataGeneration.TestValueProviders.Base;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class EmailDataProviderStrategy : BoundaryCapableDataProviderStrategy<int>
{
    public EmailDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Email";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        // Generate a realistic email and force it to the required length
        var baseEmail = Faker.Internet.Email(); // e.g., "john.doe@example.com"
        var adjustedEmail = baseEmail
            .EnsureMaxLength(boundaryInput)
            .EnsureMinLength(boundaryInput);

        return new TestValue(adjustedEmail, category);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        var offset = int.TryParse(PrecisionStep, out var step) ? step : 1;
        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }

    private string GenerateEmailWithLength(int totalLength)
    {
        const string domain = "@mail.com";
        var localPartLength = Math.Max(totalLength - domain.Length, 1);
        var localPart = new string('a', localPartLength);
        return localPart + domain;
    }
}