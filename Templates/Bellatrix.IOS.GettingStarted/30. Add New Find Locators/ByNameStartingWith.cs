using System.Collections.Generic;
using Bellatrix.Mobile.Locators;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    // 1. Here is a sample implementation of the locator for finding all elements starting with name.
    // First, we need to create the By locator.
    public class ByNameStartingWith : By<IOSDriver<IOSElement>, IOSElement>
    {
        private readonly string _locatorValue;

        public ByNameStartingWith(string name)
            : base(name) => _locatorValue = $"//*[starts-with(@name, '{Value}')]";

        // 2. We override all available methods and use XPath expression for finding an element with name starting with.
        public override IOSElement FindElement(IOSDriver<IOSElement> searchContext) => searchContext.FindElementByXPath(_locatorValue);

        public override IEnumerable<IOSElement> FindAllElements(IOSDriver<IOSElement> searchContext) => searchContext.FindElementsByXPath(_locatorValue);

        public override AppiumWebElement FindElement(IOSElement element) => element.FindElementByXPath(_locatorValue);

        public override IEnumerable<AppiumWebElement> FindAllElements(IOSElement element) => element.FindElementsByXPath(_locatorValue);

        public override string ToString() => $"Name starting with = {Value}";
    }
}
