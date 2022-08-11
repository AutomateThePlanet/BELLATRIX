////using Microsoft.VisualStudio.TestTools.UnitTesting;
////using Bellatrix.Desktop.MSTest;
using NUnit.Framework;

namespace Bellatrix.Desktop.Tests;

// uncomment to use MSTest
////[TestClass]
[TestFixture]
[App("Microsoft.WindowsCalculator_8wekyb3d8bbwe!App", Lifecycle.RestartOnFail)]
public class WindowsCalculatorTests : NUnit.DesktopTest
{
    ////[TestMethod]
    [Test]
    public void Addition()
    {
        App.Components.CreateByName<Button>("Five").Click();
        App.Components.CreateByName<Button>("Plus").Click();
        App.Components.CreateByName<Button>("Seven").Click();
        App.Components.CreateByName<Button>("Equals").Click();

        var calculatorResult = GetCalculatorResultText();
        Assert.AreEqual("12", calculatorResult);
    }

    ////[TestMethod]
    [Test]
    public void Division()
    {
        App.Components.CreateByAccessibilityId<Button>("num8Button").Click();
        App.Components.CreateByAccessibilityId<Button>("num8Button").Click();
        App.Components.CreateByAccessibilityId<Button>("divideButton").Click();
        App.Components.CreateByAccessibilityId<Button>("num1Button").Click();
        App.Components.CreateByAccessibilityId<Button>("num1Button").Click();
        App.Components.CreateByAccessibilityId<Button>("equalButton").Click();

        Assert.AreEqual("8", GetCalculatorResultText());
    }

    ////[TestMethod]
    [Test]
    public void Multiplication()
    {
        App.Components.CreateByXPath<Button>("//Button[@Name='Nine']").Click();
        App.Components.CreateByXPath<Button>("//Button[@Name='Multiply by']").Click();
        App.Components.CreateByXPath<Button>("//Button[@Name='Nine']").Click();
        App.Components.CreateByXPath<Button>("//Button[@Name='Equals']").Click();

        Assert.AreEqual("81", GetCalculatorResultText());
    }

    ////[TestMethod]
    [Test]
    public void Subtraction()
    {
        App.Components.CreateByXPath<Button>("//Button[@AutomationId='num9Button']").Click();
        App.Components.CreateByXPath<Button>("//Button[@AutomationId='minusButton']").Click();
        App.Components.CreateByXPath<Button>("//Button[@AutomationId='num1Button']").Click();
        App.Components.CreateByXPath<Button>("//Button[@AutomationId='equalButton']").Click();

        Assert.AreEqual("8", GetCalculatorResultText());
    }

    private string GetCalculatorResultText()
    {
        return App.Components.CreateByAccessibilityId<TextField>("CalculatorResults").InnerText.Replace("Display is", string.Empty).Trim();
    }
}