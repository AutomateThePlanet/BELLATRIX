namespace Bellatrix.DataGeneration.Core.Utilities;
public class TestCaseComparer : IEqualityComparer<string[]>
{
    public bool Equals(string[] x, string[] y)
    {
        return x.SequenceEqual(y);
    }

    public int GetHashCode(string[] obj)
    {
        unchecked
        {
            var hash = 17;
            foreach (var val in obj)
            {
                hash = hash * 31 + (val?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }
}
