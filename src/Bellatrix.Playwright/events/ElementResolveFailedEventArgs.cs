namespace Bellatrix.Playwright.Events;

public class ElementResolveFailedEventArgs : EventArgs
{
    public Component Component { get; }
    public Exception Exception { get; }
    public WebElement ResolvedElement { get; set; }

    public ElementResolveFailedEventArgs(Component component, Exception exception)
    {
        Component = component;
        Exception = exception;
    }
}