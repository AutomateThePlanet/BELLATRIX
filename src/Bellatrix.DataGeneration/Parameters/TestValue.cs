namespace Bellatrix.DataGeneration.Parameters;

// Represents a single test value with a category (Boundary, Normal, Invalid)
public class TestValue
{
    public TestValue(object value, Type expectedType, TestValueCategory category)
    {
        Value = value;
        ExpectedType = expectedType;
        Category = category;
    }

    public object Value { get; }
    public Type ExpectedType { get; }
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
