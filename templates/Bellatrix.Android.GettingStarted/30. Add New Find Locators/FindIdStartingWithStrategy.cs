using System.Collections.Generic;
using Bellatrix.Mobile.Locators;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android.GettingStarted;

// 1. Here is a sample implementation of the locator for finding all elements starting with ID.
// First, we need to create the find strategy.
public class FindIdStartingWithStrategy : FindStrategy<AndroidDriver, AppiumElement>
{
    private readonly string _locatorValue;

    public FindIdStartingWithStrategy(string name)
        : base(name)
    {
        _locatorValue = $"new UiSelector().resourceIdMatches(\"{Value}.*\");";
    }

    // 2. We override all available methods and use UIAutomator regular expression for finding an element with ID starting with.
    public override AppiumElement FindElement(AndroidDriver searchContext)
    {
        return searchContext.FindElement(MobileBy.AndroidUIAutomator(_locatorValue));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AndroidDriver searchContext)
    {
        return searchContext.FindElements(MobileBy.AndroidUIAutomator(_locatorValue));
    }

    public override AppiumElement FindElement(AppiumElement element)
    {
        return element.FindElement(MobileBy.AndroidUIAutomator(_locatorValue));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AppiumElement element)
    {
        return element.FindElements(MobileBy.AndroidUIAutomator(_locatorValue));
    }

    public override string ToString()
    {
        return $"ID starting with = {Value}";
    }
}
