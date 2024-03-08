using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Settings;

namespace Bellatrix.Playwright.Services;

public class TestExecutionEngine
{
    public void StartBrowser(BrowserConfiguration browserConfiguration, ServicesCollection childContainer)
    {
        try
        {
            var wrappedBrowser = WrappedBrowserCreateService.Create(browserConfiguration);

            childContainer.RegisterInstance<WrappedBrowser>(wrappedBrowser);
            childContainer.RegisterInstance<string>(wrappedBrowser.GridSessionId, "gridSessionId");
            childContainer.RegisterInstance(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.GridUrl, "GridUri");
            childContainer.RegisterInstance<IWebElementFinderService>(new NativeElementFinderService(wrappedBrowser));
            childContainer.RegisterNull<int?>();
            childContainer.RegisterNull<ILocator>();
            IsBrowserStartedCorrectly = true;
        }
        catch (Exception ex)
        {
            DebugInformation.PrintStackTrace(ex);
            IsBrowserStartedCorrectly = false;
            throw;
        }
    }

    public bool IsBrowserStartedCorrectly { get; set; }

    public void Dispose(ServicesCollection container)
    {
        var browser = container.Resolve<WrappedBrowser>();
        DisposeBrowserService.Dispose(browser, container);
    }

    public void DisposeAll()
    {
        foreach (var childContainer in ServicesCollection.Current.GetChildServicesCollections())
        {
            var browser = childContainer.Resolve<WrappedBrowser>();
            DisposeBrowserService.Dispose(browser, childContainer);
        }
    }
}
