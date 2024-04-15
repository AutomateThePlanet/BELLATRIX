// <copyright file="BrowserContext.cs" company="Automate The Planet Ltd.">
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bellatrix.Playwright.SyncPlaywright;

/// <summary>
/// Synchronous wrapper of Playwright IBrowserContext.
/// </summary>
public class BrowserContext
{
    internal BrowserContext(BellatrixBrowser browser, IBrowserContext context)
    {
        Browser = browser;
        WrappedBrowserContext = context;
    }

    internal BrowserContext(IBrowserContext context)
    {
        WrappedBrowserContext = context;
    }

    public IBrowserContext WrappedBrowserContext { get; internal init; }

    public BellatrixBrowser Browser { get; internal init; }

    internal List<BrowserPage> BrowserPages { get; private init; } = new List<BrowserPage>();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public IReadOnlyList<BrowserPage> Pages => BrowserPages.AsReadOnly();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public IAPIRequestContext APIRequest => WrappedBrowserContext.APIRequest;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public ITracing Tracing => WrappedBrowserContext.Tracing;

    public event EventHandler<IBrowserContext> OnClose
    {
        add => WrappedBrowserContext.Close += value;
        remove => WrappedBrowserContext.Close -= value;
    }

    public event EventHandler<IConsoleMessage> OnConsole
    {
        add => WrappedBrowserContext.Console += value;
        remove => WrappedBrowserContext.Console -= value;
    }

    public event EventHandler<IDialog> OnDialog
    {
        add => WrappedBrowserContext.Dialog += value;
        remove => WrappedBrowserContext.Dialog -= value;
    }

    public event EventHandler<IPage> OnPage
    {
        add => WrappedBrowserContext.Page += value;
        remove => WrappedBrowserContext.Page -= value;
    }

    public event EventHandler<IWebError> OnWebError
    {
        add => WrappedBrowserContext.WebError += value;
        remove => WrappedBrowserContext.WebError -= value;
    }

    public event EventHandler<IRequest> OnRequest
    {
        add => WrappedBrowserContext.Request += value;
        remove => WrappedBrowserContext.Request -= value;
    }

    public event EventHandler<IRequest> OnRequestFailed
    {
        add => WrappedBrowserContext.RequestFailed += value;
        remove => WrappedBrowserContext.RequestFailed -= value;
    }

    public event EventHandler<IRequest> OnRequestFinished
    {
        add => WrappedBrowserContext.RequestFinished += value;
        remove => WrappedBrowserContext.RequestFinished -= value;
    }

    public event EventHandler<IResponse> OnResponse
    {
        add => WrappedBrowserContext.Response += value;
        remove => WrappedBrowserContext.Response -= value;
    }

    public void AddCookies(IEnumerable<Cookie> cookies)
    {
        WrappedBrowserContext.AddCookiesAsync(cookies).SyncResult();
    }

    public void AddInitScript(string script = null, string scriptPath = null)
    {
        WrappedBrowserContext.AddInitScriptAsync(script, scriptPath).SyncResult();
    }

    public void ClearCookies()
    {
        WrappedBrowserContext.ClearCookiesAsync().SyncResult();
    }

    public void ClearPermissions()
    {
        WrappedBrowserContext.ClearPermissionsAsync().SyncResult();
    }

    public void Close(BrowserContextCloseOptions options = null)
    {
        WrappedBrowserContext.CloseAsync(options).SyncResult();
    }

    public IReadOnlyList<BrowserContextCookiesResult> Cookies(IEnumerable<string> urls = null)
    {
        return WrappedBrowserContext.CookiesAsync(urls).SyncResult();
    }

    public void Dispose()
    {
        WrappedBrowserContext.DisposeAsync().GetAwaiter().GetResult();
    }

    public void ExposeBinding(string name, Action callback, BrowserContextExposeBindingOptions options = null)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback, options).SyncResult();
    }

    public void ExposeBinding(string name, Action<BindingSource> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T>(string name, Action<BindingSource, T> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<TResult>(string name, Func<BindingSource, TResult> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<TResult>(string name, Func<BindingSource, IJSHandle, TResult> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T, TResult>(string name, Func<BindingSource, T, TResult> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T1, T2, TResult>(string name, Func<BindingSource, T1, T2, TResult> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T1, T2, T3, TResult>(string name, Func<BindingSource, T1, T2, T3, TResult> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T1, T2, T3, T4, TResult>(string name, Func<BindingSource, T1, T2, T3, T4, TResult> callback)
    {
        WrappedBrowserContext.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeFunction(string name, Action callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T>(string name, Action<T> callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<TResult>(string name, Func<TResult> callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T, TResult>(string name, Func<T, TResult> callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T1, T2, TResult>(string name, Func<T1, T2, TResult> callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T1, T2, T3, TResult>(string name, Func<T1, T2, T3, TResult> callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T1, T2, T3, T4, TResult>(string name, Func<T1, T2, T3, T4, TResult> callback)
    {
        WrappedBrowserContext.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void GrantPermissions(IEnumerable<string> permissions, BrowserContextGrantPermissionsOptions options = null)
    {
        WrappedBrowserContext.GrantPermissionsAsync(permissions, options).SyncResult();
    }

    public ICDPSession NewCDPSession(BrowserPage page)
    {
        return WrappedBrowserContext.NewCDPSessionAsync(page.WrappedPage).SyncResult();
    }

    public ICDPSession NewCDPSession(IFrame page)
    {
        return WrappedBrowserContext.NewCDPSessionAsync(page).SyncResult();
    }

    public BrowserPage NewPage()
    {
        var newPage = new BrowserPage(this, WrappedBrowserContext.NewPageAsync().SyncResult());
        BrowserPages.Add(newPage);

        return newPage;
    }

    public void Route(string url, Action<IRoute> handler, BrowserContextRouteOptions options = null)
    {
        WrappedBrowserContext.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(Regex url, Action<IRoute> handler, BrowserContextRouteOptions options = null)
    {
        WrappedBrowserContext.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(Func<string, bool> url, Action<IRoute> handler, BrowserContextRouteOptions options = null)
    {
        WrappedBrowserContext.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(string url, Func<IRoute, Task> handler, BrowserContextRouteOptions options = null)
    {
        WrappedBrowserContext.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(Regex url, Func<IRoute, Task> handler, BrowserContextRouteOptions options = null)
    {
        WrappedBrowserContext.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(Func<string, bool> url, Func<IRoute, Task> handler, BrowserContextRouteOptions options = null)
    {
        WrappedBrowserContext.RouteAsync(url, handler, options).SyncResult();
    }

    public void RouteFromHAR(string har, BrowserContextRouteFromHAROptions options = null)
    {
        WrappedBrowserContext.RouteFromHARAsync(har, options).SyncResult();
    }

    public IConsoleMessage RunAndWaitForConsoleMessage(Func<Task> action, BrowserContextRunAndWaitForConsoleMessageOptions options = null)
    {
        return WrappedBrowserContext.RunAndWaitForConsoleMessageAsync(action, options).SyncResult();
    }

    public BrowserPage RunAndWaitForPage(Func<Task> action, BrowserContextRunAndWaitForPageOptions options = null)
    {
        var page = new BrowserPage(this, WrappedBrowserContext.RunAndWaitForPageAsync(action, options).SyncResult());
        BrowserPages.Add(page);

        return page;
    }

    public void SetDefaultNavigationTimeout(float timeout)
    {
        WrappedBrowserContext.SetDefaultNavigationTimeout(timeout);
    }

    public void SetDefaultTimeout(float timeout)
    {
        WrappedBrowserContext.SetDefaultTimeout(timeout);
    }

    public void SetExtraHTTPHeaders(IEnumerable<KeyValuePair<string, string>> headers)
    {
        WrappedBrowserContext.SetExtraHTTPHeadersAsync(headers).SyncResult();
    }

    public void SetGeolocation(Geolocation geolocation)
    {
        WrappedBrowserContext.SetGeolocationAsync(geolocation).SyncResult();
    }

    public void SetOffline(bool offline)
    {
        WrappedBrowserContext.SetOfflineAsync(offline).SyncResult();
    }

    public string StorageState(BrowserContextStorageStateOptions options = null)
    {
        return WrappedBrowserContext.StorageStateAsync(options).SyncResult();
    }

    public void Unroute(string url, Action<IRoute> handler = null)
    {
        WrappedBrowserContext.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(Regex url, Action<IRoute> handler = null)
    {
        WrappedBrowserContext.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(Func<string, bool> url, Action<IRoute> handler = null)
    {
        WrappedBrowserContext.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(string url, Func<IRoute, Task> handler)
    {
        WrappedBrowserContext.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(Regex url, Func<IRoute, Task> handler)
    {
        WrappedBrowserContext.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(Func<string, bool> url, Func<IRoute, Task> handler)
    {
        WrappedBrowserContext.UnrouteAsync(url, handler).SyncResult();
    }

    public IConsoleMessage WaitForConsoleMessage(BrowserContextWaitForConsoleMessageOptions options = null)
    {
        return WrappedBrowserContext.WaitForConsoleMessageAsync(options).SyncResult();
    }

    public BrowserPage WaitForPage(BrowserContextWaitForPageOptions options = null)
    {
        var page = new BrowserPage(this, WrappedBrowserContext.WaitForPageAsync(options).SyncResult());
        BrowserPages.Add(page);

        return page;
    }
}
