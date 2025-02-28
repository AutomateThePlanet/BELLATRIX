using Bellatrix.Web.GettingStarted;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.Core;
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
