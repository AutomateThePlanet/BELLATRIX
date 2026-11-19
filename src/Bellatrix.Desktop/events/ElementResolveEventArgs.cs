using System;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Desktop.Events;

public class ElementResolvedEventArgs : EventArgs
{
    public Component Component { get; }
    public AppiumElement Element { get; }

    public ElementResolvedEventArgs(Component component, AppiumElement element)
    {
        Component = component;
        Element = element;
    }
}