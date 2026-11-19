using System;
using OpenQA.Selenium;

namespace Bellatrix.Web.Events;

public class ElementResolveFailedEventArgs : EventArgs
{
    public Component Component { get; }
    public Exception Exception { get; }
    public IWebElement ResolvedElement { get; set; }

    public ElementResolveFailedEventArgs(Component component, Exception exception)
    {
        Component = component;
        Exception = exception;
    }
}