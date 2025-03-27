namespace Bellatrix.DataGeneration.Parameters;

// Represents a single test value with a category (Boundary, Normal, Invalid)
public class TestValue
{
    public TestValue(object value, TestValueCategory category)
    {
        Value = value;
        Category = category;
    }

    public TestValue(object value, TestValueCategory category, string expectedInvalidMessage)
        : this(value, category)
    {
        ExpectedInvalidMessage = expectedInvalidMessage;
    }

    public object Value { get; }
    public string ExpectedInvalidMessage { get; }
    public TestValueCategory Category { get; }

    public override bool Equals(object obj)
    {
        if (obj is not TestValue other)
        {
            return false;
        }

        if (Value is null || other.Value is null)
        {
            return false;
        }

        return Value.Equals(other.Value);
    }

    public override int GetHashCode()
    {
        int hash = Value?.GetHashCode() ?? 17;
        return hash;
    }
}
