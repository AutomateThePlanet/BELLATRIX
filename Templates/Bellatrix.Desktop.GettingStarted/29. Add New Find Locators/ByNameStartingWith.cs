using System.Collections.Generic;
using Bellatrix.Desktop.Locators;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop.GettingStarted
{
    // 1. Here is a sample implementation of the locator for finding all elements starting with Name.
    // First, we need to create the By locator.
    public class ByNameStartingWith : By
    {
        private const string XpathStartingWithExpression = "//*[starts-with(@Name, '{0}')]";

        public ByNameStartingWith(string value)
            : base(value)
        {
        }

        // 2. We override all available methods and use XPath expression for finding an element with Name starting with.
        public override WindowsElement FindElement(WindowsDriver<WindowsElement> searchContext)
            => searchContext.FindElementByXPath(string.Format(XpathStartingWithExpression, Value));

        public override IEnumerable<WindowsElement> FindAllElements(WindowsDriver<WindowsElement> searchContext)
            => searchContext.FindElementsByXPath(string.Format(XpathStartingWithExpression, Value));

        public override AppiumWebElement FindElement(WindowsElement element)
            => element.FindElementByXPath(string.Format(XpathStartingWithExpression, Value));

        public override IEnumerable<AppiumWebElement> FindAllElements(WindowsElement element)
            => element.FindElementsByXPath(string.Format(XpathStartingWithExpression, Value));

        public override string ToString() => $"By Name starting with = {Value}";
    }
}
