namespace Bellatrix.Playwright.Events;

public class ElementResolvedEventArgs : EventArgs
{
    public Component Component { get; }
    public WebElement Element { get; }

    public ElementResolvedEventArgs(Component component, WebElement element)
    {
        Component = component;
        Element = element;
    }
}