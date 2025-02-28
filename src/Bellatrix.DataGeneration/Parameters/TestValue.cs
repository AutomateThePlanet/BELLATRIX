namespace Bellatrix.DataGeneration.Parameters;
// Represents a single test value with a category (Boundary, Normal, Invalid)
public class TestValue
{
    public TestValue(string value, TestValueCategory category)
    {
        Value = value;
        Category = category;
    }

    public string Value { get; }
    public TestValueCategory Category { get; }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + (Value?.GetHashCode() ?? 0);
            hash = hash * 31 + Category.GetHashCode();
            return hash;
        }
    }
}
