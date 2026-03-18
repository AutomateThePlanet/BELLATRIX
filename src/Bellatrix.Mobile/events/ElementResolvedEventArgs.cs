using System;
using Bellatrix.Mobile.Core;

namespace Bellatrix.Mobile.Events;

public class ElementResolvedEventArgs<TDriver, TDriverElement> : EventArgs
    where TDriver : AppiumDriver
    where TDriverElement : AppiumElement
{
    public Component<TDriver, TDriverElement> Component { get; }
    public TDriverElement Element { get; }

    public ElementResolvedEventArgs(Component<TDriver, TDriverElement> component, TDriverElement element)
    {
        Component = component;
        Element = element;
    }
}