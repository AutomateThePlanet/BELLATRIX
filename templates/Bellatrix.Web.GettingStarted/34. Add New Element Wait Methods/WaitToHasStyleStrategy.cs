using System;
using Bellatrix.Web.Untils;
using OpenQA.Selenium;

namespace Bellatrix.Web.GettingStarted;

// 1. Imagine that you want to wait for an element to have a specific style. First, create a new class and inherit the WaitStrategy class.
public class WaitToHasStyleStrategy : WaitStrategy
{
    private readonly string _elementStyle;

    public WaitToHasStyleStrategy(string elementStyle, int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
        _elementStyle = elementStyle;
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntil(d => ElementHasStyle(WrappedWebDriver, by), TimeoutInterval, SleepInterval);
    }

    public override void WaitUntil<TBy>(TBy by, Component parent)
    {
        WaitUntil(d => ElementHasStyle(parent.WrappedElement, by), TimeoutInterval, SleepInterval);
    }

    // 2. Find the element and check the current value in the style attribute.
    // The internal WaitUntil will wait until the value changes in the specified time.
    private bool ElementHasStyle<TBy>(ISearchContext searchContext, TBy by)
        where TBy : FindStrategy
    {
        try
        {
            var element = searchContext.FindElement(by.Convert());
            return element != null && element.GetAttribute("style").Equals(_elementStyle);
        }
        catch (StaleElementReferenceException)
        {
            return false;
        }
    }
}
