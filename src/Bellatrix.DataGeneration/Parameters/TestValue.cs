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
        return Value == other.Value && Category == other.Category;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + Value.GetHashCode();
            hash = hash * 31 + Category.GetHashCode();
            return hash;
        }
    }
}
