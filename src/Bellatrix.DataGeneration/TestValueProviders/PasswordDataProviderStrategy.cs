namespace Bellatrix.DataGeneration.TestValueProviders;

public class PasswordDataProviderStrategy : DataProviderStrategy
{
    public PasswordDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Password";

    protected override string GenerateValue(int length)
    {
        // Ensure at least one uppercase, one lowercase, one digit, one special
        string strongBase = "Aa1@";
        if (length <= strongBase.Length)
            return strongBase.Substring(0, length);

        return strongBase + new string('x', length - strongBase.Length);
    }
}