// <copyright file="BellatrixBrowser.cs" company="Automate The Planet Ltd.">
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

using Microsoft.VisualStudio.Services.WebApi;
using System.Diagnostics;

namespace Bellatrix.Playwright.SyncPlaywright;

/// <summary>
/// Synchronous wrapper for Playwright Browser.
/// </summary>
public class BellatrixBrowser
{
    internal BellatrixBrowser(BrowserType browserType, IBrowser browser)
    {
        BrowserType = browserType;
        WrappedBrowser = browser;
    }

    internal BellatrixBrowser(IBrowserType browserType, IBrowser browser)
    {
        BrowserType = new BrowserType(browserType);
        WrappedBrowser = browser;
    }

    internal BellatrixBrowser(IBrowser browser)
    {
        WrappedBrowser = browser;
    }

    public IBrowser WrappedBrowser { get; internal init; }

    public BrowserType BrowserType { get; internal init; }

    internal List<BrowserContext> BrowserContexts { get; private init; } = new List<BrowserContext>();

    public IReadOnlyList<BrowserContext> Contexts => BrowserContexts.AsReadOnly();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsConnected => WrappedBrowser.IsConnected;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string Version => WrappedBrowser.Version;

    public event EventHandler<IBrowser> OnDisconnected
    {
        add => WrappedBrowser.Disconnected += value;
        remove => WrappedBrowser.Disconnected -= value;
    }

    public void Close(BrowserCloseOptions options = null)
    {
        WrappedBrowser.CloseAsync(options).SyncResult();
    }

    public void Dispose()
    {
        WrappedBrowser.DisposeAsync().GetAwaiter().GetResult();
    }

    public ICDPSession NewBrowserCDPSession()
    {
        return WrappedBrowser.NewBrowserCDPSessionAsync().SyncResult();
    }

    public BrowserContext NewContext(BrowserNewContextOptions options = null)
    {
        var newContext = new BrowserContext(this, WrappedBrowser.NewContextAsync(options).Result);
        BrowserContexts.Add(newContext);

        return newContext;
    }

    public BrowserPage NewPage(BrowserNewPageOptions options = null)
    {
        var newPage = WrappedBrowser.NewPageAsync(options).Result;
        var browserContext = BrowserContexts.Find(x => x.WrappedBrowserContext.Equals(newPage.Context));

        if (browserContext == null)
        {
            browserContext = new BrowserContext(this, newPage.Context);
            BrowserContexts.Add(browserContext);
        }

        var newBrowserPage = new BrowserPage(newPage);

        browserContext.BrowserPages.Add(newBrowserPage);

        return newBrowserPage;
    }
}
