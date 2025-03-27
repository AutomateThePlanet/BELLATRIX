using System.Collections.Generic;
using Bellatrix.DataGeneration.Parameters;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.OutputGenerators;
using System.Diagnostics;

namespace Bellatrix.DataGeneration.Tests.Tests;

[TestFixture]
public class SampleTests
{
    // ✅ This method provides the test parameters.
    public static List<IInputParameter> ABCGeneratedTestParameters()
    {
        return new List<IInputParameter>
        {
            new TextDataParameter(minBoundary: 6, maxBoundary: 12),
            new EmailDataParameter(minBoundary: 5, maxBoundary: 10),
            new PhoneDataParameter(minBoundary: 6, maxBoundary: 8),
            new TextDataParameter(minBoundary: 4, maxBoundary: 10),
        };
    }

    // ✅ Test method using ABC-driven test cases
    [Test, ABCTestCaseSource(nameof(ABCGeneratedTestParameters), TestCaseCategory.Validation)]
    public void TestABCGeneration(string textValue, string email, string phone, string anotherText)
    {
        Debug.WriteLine($"Running test with: {textValue}, {email}, {phone}, {anotherText}");

        Assert.That(textValue, Is.Not.Null);
        Assert.That(email, Is.Not.Null);
        Assert.That(phone, Is.Not.Null);
        Assert.That(anotherText, Is.Not.Null);

    }
}
