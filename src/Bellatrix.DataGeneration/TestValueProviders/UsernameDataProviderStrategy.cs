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
        // Simple alphanumeric username: "user123"
        if (boundaryInput < 4)
        {
            return new TestValue("usr", category);
        }

        string baseName = "user_";
        string suffix = new string('a', boundaryInput - baseName.Length);
        return new TestValue(baseName + suffix, category);
    }

    protected override int OffsetValue(int value, BoundaryOffsetDirection direction)
    {
        bool parsed = int.TryParse(PrecisionStep, out int step);
        int offset = parsed ? step : 1;

        return direction == BoundaryOffsetDirection.Before ? value - offset : value + offset;
    }
}