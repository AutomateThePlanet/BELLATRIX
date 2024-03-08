using Bellatrix.Api;
using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Settings;
using RestSharp;

namespace Bellatrix.Playwright.Services;

public static class DisposeBrowserService
{
    public static DateTime? TestRunStartTime { get; set; }

    public static void Dispose()
    {
        try
        {
            var wrappedBrowser = ServicesCollection.Current.Resolve<WrappedBrowser>();

            wrappedBrowser.Quit();
            DisposeGridSession(ServicesCollection.Current);

            ServicesCollection.Current?.UnregisterSingleInstance<WrappedBrowser>();
        }
        catch (Exception ex)
        {
            DebugInformation.PrintStackTrace(ex);
        }

        ProcessCleanupService.KillPreviousBrowsersOsAgnostic(TestRunStartTime);
    }

    public static void Dispose(WrappedBrowser wrappedBrowser, ServicesCollection container)
    {
        try
        {
            wrappedBrowser.Quit();
            DisposeGridSession(container);

            container?.UnregisterSingleInstance<WrappedBrowser>();
        }
        catch (Exception ex)
        {
            DebugInformation.PrintStackTrace(ex);
        }

        ProcessCleanupService.KillPreviousBrowsersOsAgnostic(TestRunStartTime);
    }

    public static void DisposeAll()
    {
        foreach (var childContainer in ServicesCollection.Main.GetChildServicesCollections())
        {
            try
            {
                var wrappedBrowser = childContainer.Resolve<WrappedBrowser>();

                wrappedBrowser.Quit();
                DisposeGridSession(childContainer);

                childContainer?.UnregisterSingleInstance<WrappedBrowser>();
            }
            catch (Exception ex)
            {
                DebugInformation.PrintStackTrace(ex);
            }
        }
    }

    public static void DisposeGridSession(ServicesCollection container)
    {
        var sessionId = container.Resolve<string>("gridSessionId");

        if (sessionId is not null)
        {
            var client = new ApiClientService();

            client.BaseUrl = new Uri(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.GridUrl);

            var deleteRequest = new RestRequest($"/session/{sessionId}", Method.Delete);

            client.Execute(deleteRequest);
        }

        container.UnregisterSingleInstance<string>("gridSessionId");
    }
}
