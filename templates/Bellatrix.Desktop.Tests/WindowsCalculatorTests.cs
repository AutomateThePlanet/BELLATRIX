////using Microsoft.VisualStudio.TestTools.UnitTesting;
////using Bellatrix.Desktop.MSTest;
using NUnit.Framework;

namespace Bellatrix.Desktop.Tests
{
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
            App.ComponentCreateService.CreateByName<Button>("Five").Click();
            App.ComponentCreateService.CreateByName<Button>("Plus").Click();
            App.ComponentCreateService.CreateByName<Button>("Seven").Click();
            App.ComponentCreateService.CreateByName<Button>("Equals").Click();

            var calculatorResult = GetCalculatorResultText();
            Assert.AreEqual("12", calculatorResult);
        }

        ////[TestMethod]
        [Test]
        public void Division()
        {
            App.ComponentCreateService.CreateByAccessibilityId<Button>("num8Button").Click();
            App.ComponentCreateService.CreateByAccessibilityId<Button>("num8Button").Click();
            App.ComponentCreateService.CreateByAccessibilityId<Button>("divideButton").Click();
            App.ComponentCreateService.CreateByAccessibilityId<Button>("num1Button").Click();
            App.ComponentCreateService.CreateByAccessibilityId<Button>("num1Button").Click();
            App.ComponentCreateService.CreateByAccessibilityId<Button>("equalButton").Click();

            Assert.AreEqual("8", GetCalculatorResultText());
        }

        ////[TestMethod]
        [Test]
        public void Multiplication()
        {
            App.ComponentCreateService.CreateByXPath<Button>("//Button[@Name='Nine']").Click();
            App.ComponentCreateService.CreateByXPath<Button>("//Button[@Name='Multiply by']").Click();
            App.ComponentCreateService.CreateByXPath<Button>("//Button[@Name='Nine']").Click();
            App.ComponentCreateService.CreateByXPath<Button>("//Button[@Name='Equals']").Click();

            Assert.AreEqual("81", GetCalculatorResultText());
        }

        ////[TestMethod]
        [Test]
        public void Subtraction()
        {
            App.ComponentCreateService.CreateByXPath<Button>("//Button[@AutomationId='num9Button']").Click();
            App.ComponentCreateService.CreateByXPath<Button>("//Button[@AutomationId='minusButton']").Click();
            App.ComponentCreateService.CreateByXPath<Button>("//Button[@AutomationId='num1Button']").Click();
            App.ComponentCreateService.CreateByXPath<Button>("//Button[@AutomationId='equalButton']").Click();

            Assert.AreEqual("8", GetCalculatorResultText());
        }

        private string GetCalculatorResultText()
        {
            return App.ComponentCreateService.CreateByAccessibilityId<TextField>("CalculatorResults").InnerText.Replace("Display is", string.Empty).Trim();
        }
    }
}