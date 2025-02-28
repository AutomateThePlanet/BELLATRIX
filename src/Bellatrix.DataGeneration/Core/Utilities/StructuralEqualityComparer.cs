public class StructuralEqualityComparer : IEqualityComparer<string[]>
{
    public bool Equals(string[] x, string[] y)
    {
        if (x == null || y == null)
        {
            return false;
        }

        return x.SequenceEqual(y);
    }

    public int GetHashCode(string[] obj)
    {
        if (obj == null)
        {
            return 0;
        }

        unchecked
        {
            int hash = 17;
            foreach (var item in obj)
            {
                hash = hash * 31 + (item?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }
}
