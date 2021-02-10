////using Microsoft.VisualStudio.TestTools.UnitTesting;
////using Bellatrix.Desktop.MSTest;
using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.Tests
{
    // uncomment to use MSTest
    ////[TestClass]
    [TestFixture]
    [App("Microsoft.WindowsCalculator_8wekyb3d8bbwe!App", Lifecycle.RestartOnFail)]
    [VideoRecording(VideoRecordingMode.OnlyFail)]
    [ScreenshotOnFail(true)]
    public class WindowsCalculatorTests : DesktopTest
    {
        ////[TestMethod]
        [Test]
        public void Addition()
        {
            App.ElementCreateService.CreateByName<Button>("Five").Click();
            App.ElementCreateService.CreateByName<Button>("Plus").Click();
            App.ElementCreateService.CreateByName<Button>("Seven").Click();
            App.ElementCreateService.CreateByName<Button>("Equals").Click();

            var calculatorResult = GetCalculatorResultText();
            Assert.AreEqual("12", calculatorResult);
        }

        ////[TestMethod]
        [Test]
        public void Division()
        {
            App.ElementCreateService.CreateByAccessibilityId<Button>("num8Button").Click();
            App.ElementCreateService.CreateByAccessibilityId<Button>("num8Button").Click();
            App.ElementCreateService.CreateByAccessibilityId<Button>("divideButton").Click();
            App.ElementCreateService.CreateByAccessibilityId<Button>("num1Button").Click();
            App.ElementCreateService.CreateByAccessibilityId<Button>("num1Button").Click();
            App.ElementCreateService.CreateByAccessibilityId<Button>("equalButton").Click();

            Assert.AreEqual("8", GetCalculatorResultText());
        }

        ////[TestMethod]
        [Test]
        public void Multiplication()
        {
            App.ElementCreateService.CreateByXPath<Button>("//Button[@Name='Nine']").Click();
            App.ElementCreateService.CreateByXPath<Button>("//Button[@Name='Multiply by']").Click();
            App.ElementCreateService.CreateByXPath<Button>("//Button[@Name='Nine']").Click();
            App.ElementCreateService.CreateByXPath<Button>("//Button[@Name='Equals']").Click();

            Assert.AreEqual("81", GetCalculatorResultText());
        }

        ////[TestMethod]
        [Test]
        public void Subtraction()
        {
            App.ElementCreateService.CreateByXPath<Button>("//Button[@AutomationId='num9Button']").Click();
            App.ElementCreateService.CreateByXPath<Button>("//Button[@AutomationId='minusButton']").Click();
            App.ElementCreateService.CreateByXPath<Button>("//Button[@AutomationId='num1Button']").Click();
            App.ElementCreateService.CreateByXPath<Button>("//Button[@AutomationId='equalButton']").Click();

            Assert.AreEqual("8", GetCalculatorResultText());
        }

        private string GetCalculatorResultText()
        {
            return App.ElementCreateService.CreateByAccessibilityId<TextField>("CalculatorResults").InnerText.Replace("Display is", string.Empty).Trim();
        }
    }
}