// <copyright file="TestExecutionEngine.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
