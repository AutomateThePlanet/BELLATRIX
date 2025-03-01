namespace Bellatrix.DataGeneration.Parameters;

// Represents a single test value with a category (Boundary, Normal, Invalid)
public class TestValue
{
    public TestValue(string value, TestValueCategory category)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
        Category = category;
    }

    public string Value { get; }
    public TestValueCategory Category { get; }

    public override bool Equals(object obj)
    {
        if (obj is not TestValue other) return false;
        return Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        int hash = Value?.GetHashCode() ?? 17;
        return hash;
    }
}
