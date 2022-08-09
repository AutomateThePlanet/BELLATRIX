using System.Collections.Generic;
using Bellatrix.Desktop.Locators;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop.GettingStarted;

// 1. Here is a sample implementation of the locator for finding all elements starting with Name.
// First, we need to create the By locator.
public class FindNameStartingWithStrategy : FindStrategy
{
    private const string XpathStartingWithExpression = "//*[starts-with(@Name, '{0}')]";

    public FindNameStartingWithStrategy(string value)
        : base(value)
    {
    }

    // 2. We override all available methods and use XPath expression for finding an element with Name starting with.
    public override WindowsElement FindElement(WindowsDriver<WindowsElement> searchContext)
    {
        return searchContext.FindElementByXPath(string.Format(XpathStartingWithExpression, Value));
    }

    public override IEnumerable<WindowsElement> FindAllElements(WindowsDriver<WindowsElement> searchContext)
    {
        return searchContext.FindElementsByXPath(string.Format(XpathStartingWithExpression, Value));
    }

    public override AppiumWebElement FindElement(WindowsElement element)
    {
        return element.FindElementByXPath(string.Format(XpathStartingWithExpression, Value));
    }

    public override IEnumerable<AppiumWebElement> FindAllElements(WindowsElement element)
    {
        return element.FindElementsByXPath(string.Format(XpathStartingWithExpression, Value));
    }

    public override string ToString()
    {
        return $"By Name starting with = {Value}";
    }
}
