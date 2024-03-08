namespace Bellatrix.Playwright.Services.Browser;

public class ConsoleMessageStorage : List<IConsoleMessage>
{
    public IBrowserContext BrowserContext { get; set; }

    public ConsoleMessageStorage(IBrowserContext browserContext)
        : base()
    {
        BrowserContext = browserContext;
    }
}
