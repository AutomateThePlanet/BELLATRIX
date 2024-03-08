using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.Settings;

namespace Bellatrix.Playwright.Services.Browser;

public class WrappedBrowser
{
    public WrappedBrowser()
    {
    }

    public WrappedBrowser(IPlaywright playwright, IBrowser browser, IBrowserContext context, IPage page)
    {
        Playwright = playwright;
        Browser = browser;
        CurrentContext = context;
        CurrentPage = page;
    }

    public string GridSessionId { get; internal set; }
    public IPlaywright Playwright { get; internal set; }
    public IBrowser Browser { get; internal set; }
    public IBrowserContext CurrentContext { get; internal set; }
    public IPage CurrentPage { get; internal set; }

    public ConsoleMessageStorage ConsoleMessages { get; internal set; }

    public void Quit()
    {
        try
        {
            CurrentPage?.CloseAsync().GetAwaiter().GetResult();
            CurrentPage = null;

            CurrentContext?.CloseAsync().GetAwaiter().GetResult();
            CurrentContext = null;

            Browser?.CloseAsync().GetAwaiter().GetResult();
            Browser = null;

            Playwright?.Dispose();
            Playwright = null;
        }
        catch (Exception ex)
        {
            ex.PrintStackTrace();
        }
    }
}
