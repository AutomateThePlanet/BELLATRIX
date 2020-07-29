using System;
using Bellatrix.Web.Untils;
using OpenQA.Selenium;

namespace Bellatrix.Web.GettingStarted
{
    // 1. Imagine that you want to wait for an element to have a specific style. First, create a new class and inherit the BaseUntil class.
    public class UntilHasStyle : BaseUntil
    {
        private readonly string _elementStyle;

        public UntilHasStyle(string elementStyle, int? timeoutInterval = null, int? sleepInterval = null)
            : base(timeoutInterval, sleepInterval) => _elementStyle = elementStyle;

        public override void WaitUntil<TBy>(TBy by) => WaitUntil(ElementHasStyle(WrappedWebDriver, by), TimeoutInterval, SleepInterval);
        public override void WaitUntil<TBy>(TBy by, Element parent) => WaitUntil(ElementHasStyle(parent.WrappedElement, by), TimeoutInterval, SleepInterval);

        // 2. Find the element and check the current value in the style attribute.
        // The internal WaitUntil will wait until the value changes in the specified time.
        private Func<IWebDriver, bool> ElementHasStyle<TBy>(ISearchContext searchContext, TBy by)
            where TBy : By => driver =>
        {
            try
            {
                var element = FindElement(searchContext, by);
                return element != null && element.GetAttribute("style").Equals(_elementStyle);
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        };
    }
}
