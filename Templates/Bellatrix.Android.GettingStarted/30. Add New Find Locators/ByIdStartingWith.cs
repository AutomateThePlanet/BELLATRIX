using System.Collections.Generic;
using Bellatrix.Mobile.Locators;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    // 1. Here is a sample implementation of the locator for finding all elements starting with ID.
    // First, we need to create the By locator.
    public class ByIdStartingWith : By<AndroidDriver<AndroidElement>, AndroidElement>
    {
        private readonly string _locatorValue;

        public ByIdStartingWith(string name)
            : base(name) => _locatorValue = $"new UiSelector().resourceIdMatches(\"{Value}.*\");";

        // 2. We override all available methods and use UIAutomator regular expression for finding an element with ID starting with.
        public override AndroidElement FindElement(AndroidDriver<AndroidElement> searchContext) => searchContext.FindElementByAndroidUIAutomator(_locatorValue);

        public override IEnumerable<AndroidElement> FindAllElements(AndroidDriver<AndroidElement> searchContext) => searchContext.FindElementsByAndroidUIAutomator(_locatorValue);

        public override AppiumWebElement FindElement(AndroidElement element) => element.FindElementByAndroidUIAutomator(_locatorValue);

        public override IEnumerable<AppiumWebElement> FindAllElements(AndroidElement element) => element.FindElementsByAndroidUIAutomator(_locatorValue);

        public override string ToString() => $"ID starting with = {Value}";
    }
}
