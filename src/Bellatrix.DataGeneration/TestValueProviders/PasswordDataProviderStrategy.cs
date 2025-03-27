namespace Bellatrix.DataGeneration.TestValueProviders;

public class PasswordDataProviderStrategy : BoundaryCapableDataProviderStrategy<int>
{
    public PasswordDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Password";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        string password = GenerateStrongPassword(boundaryInput);
        return new TestValue(password, category);
    }

    private string GenerateStrongPassword(int length)
    {
        const string basePart = "Aa1@";
        if (length <= basePart.Length)
        {
            return basePart.Substring(0, Math.Max(1, length));
        }

        return basePart + new string('x', length - basePart.Length);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        bool parsedSuccessfully = int.TryParse(PrecisionStep, out int step);
        int offset = parsedSuccessfully ? step : 1;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}