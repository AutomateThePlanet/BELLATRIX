using Bellatrix.Playwright.Services.Browser;

namespace Bellatrix.Playwright.Services;
public abstract class WebService
{
    internal WebService(WrappedBrowser wrappedBrowser)
    {
        WrappedBrowser = wrappedBrowser;
    }

    public WrappedBrowser WrappedBrowser { get; set; }

    //public IPlaywright Playwright => WrappedBrowser.Playwright;
    public IBrowser Browser => WrappedBrowser.Browser;
    public IBrowserContext CurrentContext => WrappedBrowser.CurrentContext;
    public IPage CurrentPage => WrappedBrowser.CurrentPage;
}
