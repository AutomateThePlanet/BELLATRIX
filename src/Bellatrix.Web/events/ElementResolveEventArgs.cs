using System;
using OpenQA.Selenium;

namespace Bellatrix.Web.Events;

public class ElementResolvedEventArgs : EventArgs
{
    public Component Component { get; }
    public IWebElement Element { get; }

    public ElementResolvedEventArgs(Component component, IWebElement element)
    {
        Component = component;
        Element = element;
    }
}