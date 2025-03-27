using Bellatrix.DataGeneration.TestValueProviders;
using Bellatrix.DataGeneration.Parameters;
using Bellatrix.DataGeneration;
using NUnit.Framework;
using System.Linq;
using System;

[TestFixture]
public class BooleanDataProviderDebugInternalTests
{
    [Test]
    public void Debug_BooleanStrategy_GenerateAllTestValues()
    {
        // Arrange
        var strategy = new BooleanDataProviderStrategy();

        // Act
        var generatedTestValues = strategy.GenerateTestValues().ToList();

        // Debug-friendly output
        foreach (var testValue in generatedTestValues)
        {
            Console.WriteLine($"[{testValue.Category}] Value: {testValue.Value} | ExpectedInvalidMessage: {testValue.ExpectedInvalidMessage}");
        }

        // Optional: breakpoint here to inspect testValues
        Assert.That(generatedTestValues.Count, Is.GreaterThan(0));
    }
}
