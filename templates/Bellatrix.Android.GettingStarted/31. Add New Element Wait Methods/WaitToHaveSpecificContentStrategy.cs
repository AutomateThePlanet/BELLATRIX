using System;
using Bellatrix.Mobile.Configuration;
using Bellatrix.Mobile.Locators;
using Bellatrix.Mobile.Untils;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile.Android.GettingStarted;

// 1. Imagine that you want to wait for an element to have a specific content.
// First, create a new class and inherit the BaseUntil class.
public class WaitToHaveSpecificContentStrategy<TDriver, TDriverElement> : WaitStrategy<TDriver, TDriverElement>
    where TDriver : AppiumDriver
    where TDriverElement : AppiumElement
{
    private readonly string _elementContent;

    public WaitToHaveSpecificContentStrategy(string elementContent, int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
        _elementContent = elementContent;
        TimeoutInterval = timeoutInterval ?? ConfigurationService.GetSection<MobileSettings>().TimeoutSettings.ElementToHaveContentTimeout;
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntil(d => ElementHasSpecificContent(WrappedWebDriver, by), TimeoutInterval, SleepInterval);
    }

    // 2. We find the element and check the current value in the Text attribute.
    // The internal WaitUntil will wait until the value changes in the specified time.
    private bool ElementHasSpecificContent<TBy>(TDriver searchContext, TBy by)
        where TBy : FindStrategy<TDriver, TDriverElement>
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
    }
}