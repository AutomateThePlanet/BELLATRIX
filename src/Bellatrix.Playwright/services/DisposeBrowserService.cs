// <copyright file="DisposeBrowserService.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Api;
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

                wrappedBrowser?.Quit();
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
