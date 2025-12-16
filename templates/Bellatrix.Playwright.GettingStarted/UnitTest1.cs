using NUnit.Framework;
using NUnitAssert = NUnit.Framework.Assert;

namespace Bellatrix.Playwright.GettingStarted;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        NUnitAssert.Pass();
    }
}