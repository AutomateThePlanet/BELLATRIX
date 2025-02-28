namespace Bellatrix.DataGeneration.Core.Parameters;
// Represents a single test value with a category (Boundary, Normal, Invalid)
public class TestValue
{
    public string Value { get; }
    public TestValueCategory Category { get; }

    public TestValue(string value, TestValueCategory category)
    {
        Value = value;
        Category = category;
    }
}
