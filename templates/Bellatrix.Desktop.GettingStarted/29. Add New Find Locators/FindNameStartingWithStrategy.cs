using System.Collections.Generic;
using Bellatrix.Desktop.Locators;
using OpenQA.Selenium;
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
    public override AppiumElement FindElement(WindowsDriver searchContext)
    {
        return searchContext.FindElement(By.XPath(string.Format(XpathStartingWithExpression, Value)));
    }

    public override IEnumerable<AppiumElement> FindAllElements(WindowsDriver searchContext)
    {
        return searchContext.FindElements(By.XPath(string.Format(XpathStartingWithExpression, Value)));
    }

    public override AppiumElement FindElement(AppiumElement element)
    {
        return element.FindElement(By.XPath(string.Format(XpathStartingWithExpression, Value)));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AppiumElement element)
    {
        return element.FindElements(By.XPath(string.Format(XpathStartingWithExpression, Value)));
    }

    public override string ToString()
    {
        return $"By Name starting with = {Value}";
    }
}
