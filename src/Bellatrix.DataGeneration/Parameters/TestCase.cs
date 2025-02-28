using Bellatrix.DataGeneration.Parameters;
namespace Bellatrix.DataGeneration.Models;
public class TestCase
{
    public List<TestValue> Values { get; set; } = new List<TestValue>();

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            foreach (var value in Values.OrderBy(v => v.GetHashCode())) // Ensure order consistency
            {
                hash = hash * 31 + value.GetHashCode();
            }
            return hash;
        }
    }
}
