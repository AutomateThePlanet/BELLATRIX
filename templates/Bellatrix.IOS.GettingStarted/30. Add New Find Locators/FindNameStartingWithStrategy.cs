using System.Collections.Generic;
using Bellatrix.Mobile.Locators;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS.GettingStarted;

// 1. Here is a sample implementation of the locator for finding all elements starting with name.
// First, we need to create the find strategy.
public class FindNameStartingWithStrategy : FindStrategy<IOSDriver, AppiumElement>
{
    private readonly string _locatorValue;

    public FindNameStartingWithStrategy(string name)
        : base(name)
    {
        _locatorValue = $"//*[starts-with(@name, '{Value}')]";
    }

    // 2. We override all available methods and use XPath expression for finding an element with name starting with.
    public override AppiumElement FindElement(IOSDriver searchContext)
    {
        return searchContext.FindElement(MobileBy.XPath(_locatorValue));
    }

    public override IEnumerable<AppiumElement> FindAllElements(IOSDriver searchContext)
    {
        return searchContext.FindElements(MobileBy.XPath(_locatorValue));
    }

    public override AppiumElement FindElement(AppiumElement element)
    {
        return element.FindElement(MobileBy.XPath(_locatorValue));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AppiumElement element)
    {
        return element.FindElements(MobileBy.XPath(_locatorValue));
    }

    public override string ToString()
    {
        return $"Name starting with = {Value}";
    }
}
