using System;
using Bellatrix.Desktop.Untils;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop.GettingStarted.ExtensionMethodsWaitMethods;

// 1. Imagine that you want to wait for an element to have a specific content.
// First, create a new class and inherit the BaseUntil class.
public class WaitToHaveSpecificContentStrategy : WaitStrategy
{
    private readonly string _elementContent;

    public WaitToHaveSpecificContentStrategy(string elementContent, int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval) => _elementContent = elementContent;

    public override void WaitUntil<TBy>(TBy by)
        => WaitUntil(ElementHasSpecificContent(WrappedWebDriver, by), TimeoutInterval, SleepInterval);

    // 2. We find the element and check the current value in the Text attribute.
    // The internal WaitUntil will wait until the value changes in the specified time.
    private Func<IWebDriver, bool> ElementHasSpecificContent<TBy>(WindowsDriver<WindowsElement> searchContext, TBy by)
        where TBy : Locators.FindStrategy => driver =>
    {
        try
        {
            var element = by.FindElement(searchContext);
            return element.Text == _elementContent;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    };
}