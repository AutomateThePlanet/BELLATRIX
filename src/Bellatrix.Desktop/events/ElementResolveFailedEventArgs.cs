using System;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Desktop.Events;

public class ElementResolveFailedEventArgs : EventArgs
{
    public Component Component { get; }
    public Exception Exception { get; }
    public AppiumElement ResolvedElement { get; set; }

    public ElementResolveFailedEventArgs(Component component, Exception exception)
    {
        Component = component;
        Exception = exception;
    }
}