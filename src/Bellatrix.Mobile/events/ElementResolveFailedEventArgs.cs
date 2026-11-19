using System;
using Bellatrix.Mobile.Core;

namespace Bellatrix.Mobile.Events;

public class ElementResolveFailedEventArgs<TDriver, TDriverElement> : EventArgs
    where TDriver : AppiumDriver
    where TDriverElement : AppiumElement
{
    public Component<TDriver, TDriverElement> Component { get; }
    public Exception Exception { get; }
    public TDriverElement ResolvedElement { get; set; }

    public ElementResolveFailedEventArgs(Component<TDriver, TDriverElement> component, Exception exception)
    {
        Component = component;
        Exception = exception;
    }
}