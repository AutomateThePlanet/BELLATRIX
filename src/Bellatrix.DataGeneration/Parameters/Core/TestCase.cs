namespace Bellatrix.DataGeneration;

public class TestCase : ICloneable
{
    public List<TestValue> Values { get; set; } = new List<TestValue>();
    public double Score { get; set; }

    public object Clone()
    {
        return new TestCase
        {
            Values = Values.Select(v => new TestValue(v.Value, v.Category, v.ExpectedInvalidMessage)).ToList(),
            Score = Score
        };
    }

    public override bool Equals(object obj)
    {
        if (obj is not TestCase other) return false;
        return Values.SequenceEqual(other.Values);
    }


    public override int GetHashCode()
    {
        var hash = 17;
        var index = 1;
        foreach (var value in Values) // Ensure order consistency
        {
            hash = hash * index++ + (value.Value?.GetHashCode() ?? 0); // Use value string hash
        }
        return hash;
    }
}
