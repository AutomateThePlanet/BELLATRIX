using System;
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.Core;
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
            int hash = 17;
            foreach (var val in obj)
            {
                hash = hash * 31 + (val?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }
}
