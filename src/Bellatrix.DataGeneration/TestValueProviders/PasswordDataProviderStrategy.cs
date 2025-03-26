using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.TestValueProviders;

public class PasswordDataProviderStrategy : DataProviderStrategy
{
    public PasswordDataProviderStrategy(int? minBoundary = null, int? maxBoundary = null)
        : base(minBoundary, maxBoundary)
    {
    }

    protected override string GetInputTypeName() => "Password";

    protected override Type GetExpectedType() => typeof(string);

    protected override TestValue CreateBoundaryTestValue(int boundaryInput, TestValueCategory category)
    {
        var password = GenerateStrongPassword(boundaryInput);
        return new TestValue(password, typeof(string), category);
    }

    private string GenerateStrongPassword(int length)
    {
        // Ensure at least one of each: upper, lower, digit, special
        const string basePart = "Aa1@";
        if (length <= basePart.Length)
        {
            return basePart.Substring(0, Math.Max(1, length));
        }

        return basePart + new string('x', length - basePart.Length);
    }
}