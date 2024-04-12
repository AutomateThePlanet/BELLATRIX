// <copyright file="BrowserPage.cs" company="Automate The Planet Ltd.">
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
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bellatrix.Playwright.SyncPlaywright;

/// <summary>
/// Synchronous wrapper for Playwright IPage.
/// </summary>
public class BrowserPage
{
    public Dialog Dialog { get; set; }

    internal BrowserPage(BrowserContext context, IPage page)
    {
        Context = context;
        WrappedPage = page;
    }

    internal BrowserPage(IPage page)
    {
        WrappedPage = page;
    }

    internal void ListenForDialog()
    {
        OnDialog += (sender, nativeDialog) =>
        {
            Dialog = new Dialog(nativeDialog);
        };
    }

    internal void StopListeningForDialog()
    {
        OnDialog -= (sender, nativeDialog) =>
        {
            Dialog = new Dialog(nativeDialog);
        };
    }

    public IPage WrappedPage { get; internal init; }

    public BrowserContext Context { get; internal init; }

    public IReadOnlyList<IFrame> Frames => WrappedPage.Frames;

    public bool IsClosed => WrappedPage.IsClosed;

    public Keyboard Keyboard => new Keyboard(WrappedPage.Keyboard);

    public IFrame MainFrame => WrappedPage.MainFrame;

    public Mouse Mouse => new Mouse(WrappedPage.Mouse);

    public IAPIRequestContext APIRequest => WrappedPage.APIRequest;

    public ITouchscreen Touchscreen => WrappedPage.Touchscreen;

    public string Url => WrappedPage.Url;

    public IVideo Video => WrappedPage.Video;

    public PageViewportSizeResult ViewportSize => WrappedPage.ViewportSize;

    public IReadOnlyList<IWorker> Workers => WrappedPage.Workers;

    public event EventHandler<IPage> OnClose
    {
        add => WrappedPage.Close += value;
        remove => WrappedPage.Close -= value;
    }

    public event EventHandler<IConsoleMessage> OnConsole
    {
        add => WrappedPage.Console += value;
        remove => WrappedPage.Console -= value;
    }

    public event EventHandler<IPage> OnCrash
    {
        add => WrappedPage.Crash += value;
        remove => WrappedPage.Crash -= value;
    }

    public event EventHandler<IDialog> OnDialog
    {
        add => WrappedPage.Dialog += value;
        remove => WrappedPage.Dialog -= value;
    }

    public event EventHandler<IPage> OnDOMContentLoaded
    {
        add => WrappedPage.DOMContentLoaded += value;
        remove => WrappedPage.DOMContentLoaded -= value;
    }

    public event EventHandler<IDownload> OnDownload
    {
        add => WrappedPage.Download += value;
        remove => WrappedPage.Download -= value;
    }

    public event EventHandler<IFileChooser> OnFileChooser
    {
        add => WrappedPage.FileChooser += value;
        remove => WrappedPage.FileChooser -= value;
    }

    public event EventHandler<IFrame> OnFrameAttached
    {
        add => WrappedPage.FrameAttached += value;
        remove => WrappedPage.FrameAttached -= value;
    }

    public event EventHandler<IFrame> OnFrameDetached
    {
        add => WrappedPage.FrameDetached += value;
        remove => WrappedPage.FrameDetached -= value;
    }

    public event EventHandler<IFrame> OnFrameNavigated
    {
        add => WrappedPage.FrameNavigated += value;
        remove => WrappedPage.FrameNavigated -= value;
    }

    public event EventHandler<IPage> OnLoad
    {
        add => WrappedPage.Load += value;
        remove => WrappedPage.Load -= value;
    }

    public event EventHandler<string> OnPageError
    {
        add => WrappedPage.PageError += value;
        remove => WrappedPage.PageError -= value;
    }

    public event EventHandler<IPage> OnPopup
    {
        add => WrappedPage.Popup += value;
        remove => WrappedPage.Popup -= value;
    }

    public event EventHandler<IRequest> OnRequest
    {
        add => WrappedPage.Request += value;
        remove => WrappedPage.Request -= value;
    }

    public event EventHandler<IRequest> OnRequestFailed
    {
        add => WrappedPage.RequestFailed += value;
        remove => WrappedPage.RequestFailed -= value;
    }

    public event EventHandler<IRequest> OnRequestFinished
    {
        add => WrappedPage.RequestFinished += value;
        remove => WrappedPage.RequestFinished -= value;
    }

    public event EventHandler<IResponse> OnResponse
    {
        add => WrappedPage.Response += value;
        remove => WrappedPage.Response -= value;
    }

    public event EventHandler<IWebSocket> OnWebSocket
    {
        add => WrappedPage.WebSocket += value;
        remove => WrappedPage.WebSocket -= value;
    }

    public event EventHandler<IWorker> OnWorker
    {
        add => WrappedPage.Worker += value;
        remove => WrappedPage.Worker -= value;
    }


    public void AddInitScript(string script = null, string scriptPath = null)
    {
        WrappedPage.AddInitScriptAsync(script, scriptPath);
    }

    public IElementHandle AddScriptTag(PageAddScriptTagOptions options = null)
    {
        return WrappedPage.AddScriptTagAsync(options).Result;
    }

    public IElementHandle AddStyleTag(PageAddStyleTagOptions options = null)
    {
        return WrappedPage.AddStyleTagAsync(options).Result;
    }

    public void BringToFront()
    {
        WrappedPage.BringToFrontAsync().SyncResult();
    }

    public void Check(string selector, PageCheckOptions options = null)
    {
        WrappedPage.CheckAsync(selector, options).SyncResult();
    }

    public void Click(string selector, PageClickOptions options = null)
    {
        WrappedPage.ClickAsync(selector, options).SyncResult();
    }

    public void Close(PageCloseOptions options = null)
    {
        WrappedPage.CloseAsync(options).SyncResult();
        Context.BrowserPages.Remove(this);
    }

    public string Content()
    {
        return WrappedPage.ContentAsync().Result;
    }

    public void DblClick(string selector, PageDblClickOptions options = null)
    {
        WrappedPage.DblClickAsync(selector, options).SyncResult();
    }

    public void DispatchEvent(string selector, string type, object eventInit = null, PageDispatchEventOptions options = null)
    {
        WrappedPage.DispatchEventAsync(selector, type, eventInit, options).SyncResult();
    }

    public void DragAndDrop(string source, string target, PageDragAndDropOptions options = null)
    {
        WrappedPage.DragAndDropAsync(source, target, options).SyncResult();
    }

    public void EmulateMedia(PageEmulateMediaOptions options = null)
    {
        WrappedPage.EmulateMediaAsync(options).SyncResult();
    }

    public T EvalOnSelectorAll<T>(string selector, string expression, object arg = null)
    {
        return WrappedPage.EvalOnSelectorAllAsync<T>(selector, expression, arg).SyncResult();
    }

    public JsonElement? EvalOnSelectorAll(string selector, string expression, object arg = null)
    {
        return WrappedPage.EvalOnSelectorAllAsync(selector, expression, arg).SyncResult();
    }

    public T EvalOnSelector<T>(string selector, string expression, object arg = null, PageEvalOnSelectorOptions options = null)
    {
        return WrappedPage.EvalOnSelectorAsync<T>(selector, expression, arg, options).SyncResult();
    }

    public JsonElement? EvalOnSelector(string selector, string expression, object arg = null)
    {
        return WrappedPage.EvalOnSelectorAsync(selector, expression, arg).SyncResult();
    }

    public T Evaluate<T>(string expression, object arg = null)
    {
        return WrappedPage.EvaluateAsync<T>(expression, arg).SyncResult();
    }

    public JsonElement? Evaluate(string expression, object arg = null)
    {
        return WrappedPage.EvaluateAsync(expression, arg).SyncResult();
    }

    public IJSHandle EvaluateHandle(string expression, object arg = null)
    {
        return WrappedPage.EvaluateHandleAsync(expression, arg).SyncResult();
    }

    public void ExposeBinding(string name, Action callback, PageExposeBindingOptions options = null)
    {
        WrappedPage.ExposeBindingAsync(name, callback, options).SyncResult();
    }

    public void ExposeBinding(string name, Action<BindingSource> callback)
    {
        WrappedPage.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T>(string name, Action<BindingSource, T> callback)
    {
        WrappedPage.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<TResult>(string name, Func<BindingSource, TResult> callback)
    {
        WrappedPage.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<TResult>(string name, Func<BindingSource, IJSHandle, TResult> callback)
    {
        WrappedPage.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T, TResult>(string name, Func<BindingSource, T, TResult> callback)
    {
        WrappedPage.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T1, T2, TResult>(string name, Func<BindingSource, T1, T2, TResult> callback)
    {
        WrappedPage.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T1, T2, T3, TResult>(string name, Func<BindingSource, T1, T2, T3, TResult> callback)
    {
        WrappedPage.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeBinding<T1, T2, T3, T4, TResult>(string name, Func<BindingSource, T1, T2, T3, T4, TResult> callback)
    {
        WrappedPage.ExposeBindingAsync(name, callback).SyncResult();
    }

    public void ExposeFunction(string name, Action callback)
    {
        WrappedPage.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T>(string name, Action<T> callback)
    {
        WrappedPage.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<TResult>(string name, Func<TResult> callback)
    {
        WrappedPage.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T, TResult>(string name, Func<T, TResult> callback)
    {
        WrappedPage.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T1, T2, TResult>(string name, Func<T1, T2, TResult> callback)
    {
        WrappedPage.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T1, T2, T3, TResult>(string name, Func<T1, T2, T3, TResult> callback)
    {
        WrappedPage.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void ExposeFunction<T1, T2, T3, T4, TResult>(string name, Func<T1, T2, T3, T4, TResult> callback)
    {
        WrappedPage.ExposeFunctionAsync(name, callback).SyncResult();
    }

    public void Fill(string selector, string value, PageFillOptions options = null)
    {
        WrappedPage.FillAsync(selector, value, options).SyncResult();
    }

    public void Focus(string selector, PageFocusOptions options = null)
    {
        WrappedPage.FocusAsync(selector, options).SyncResult();
    }

    public IFrame Frame(string name)
    {
        return WrappedPage.Frame(name);
    }

    public IFrame FrameByUrl(string url)
    {
        return WrappedPage.FrameByUrl(url);
    }

    public IFrame FrameByUrl(Regex url)
    {
        return WrappedPage.FrameByUrl(url);
    }

    public IFrame FrameByUrl(Func<string, bool> url)
    {
        return WrappedPage.FrameByUrl(url);
    }

    public WebElement Locate(string selector)
    {
        return new WebElement(this, WrappedPage.Locator(selector));
    }

    public string GetAttribute(string selector, string name, PageGetAttributeOptions options = null)
    {
        return WrappedPage.GetAttributeAsync(selector, name, options).SyncResult();
    }

    public WebElement GetByAltText(string text, GetByAltTextOptions options = null)
    {
        return new WebElement(this, WrappedPage.GetByAltText(text, options.ConvertTo<PageGetByAltTextOptions>()));
    }

    public WebElement GetByAltText(Regex text, GetByAltTextOptions options = null)
    {
        return new WebElement(this, WrappedPage.GetByAltText(text, options.ConvertTo<PageGetByAltTextOptions>()));
    }

    public WebElement GetByLabel(string text, GetByLabelOptions options = null)
    {
        return new WebElement(this, WrappedPage.GetByLabel(text, options.ConvertTo<PageGetByLabelOptions>()));
    }

    public WebElement GetByLabel(Regex text, GetByLabelOptions options = null)
    {
        return new WebElement(this, WrappedPage.GetByLabel(text, options.ConvertTo<PageGetByLabelOptions>()));
    }

    public WebElement GetByPlaceholder(string text, GetByPlaceholderOptions options = null)
    {
        return new WebElement(this, WrappedPage.GetByPlaceholder(text, options.ConvertTo<PageGetByPlaceholderOptions>()));
    }

    public WebElement GetByPlaceholder(Regex text, GetByPlaceholderOptions options = null)
    {
        return new WebElement(this, WrappedPage.GetByPlaceholder(text, options.ConvertTo<PageGetByPlaceholderOptions>()));
    }

    public WebElement GetByRole(AriaRole role, GetByRoleOptions options = null)
    {
        return new WebElement(this, WrappedPage.GetByRole(role, options.ConvertTo<PageGetByRoleOptions>()));
    }

    public WebElement GetByTestId(string testId)
    {
        return new WebElement(this, WrappedPage.GetByTestId(testId));
    }

    public WebElement GetByTestId(Regex testId)
    {
        return new WebElement(this, WrappedPage.GetByTestId(testId));
    }

    public WebElement GetByText(string text, GetByTextOptions options = null)
    {
        return new WebElement(this, WrappedPage.GetByText(text, options.ConvertTo<PageGetByTextOptions>()));
    }

    public WebElement GetByText(Regex text, GetByTextOptions options = null)
    {
        return new WebElement(this, WrappedPage.GetByText(text, options.ConvertTo<PageGetByTextOptions>()));
    }

    public WebElement GetByTitle(string text, GetByTitleOptions options = null)
    {
        return new WebElement(this, WrappedPage.GetByTitle(text, options.ConvertTo<PageGetByTitleOptions>()));
    }

    public WebElement GetByTitle(Regex text, GetByTitleOptions options = null)
    {
        return new WebElement(this, WrappedPage.GetByTitle(text, options.ConvertTo<PageGetByTitleOptions>()));
    }

    public IResponse GoBack(PageGoBackOptions options = null)
    {
        return WrappedPage.GoBackAsync(options).SyncResult();
    }

    public IResponse GoForward(PageGoForwardOptions options = null)
    {
        return WrappedPage.GoForwardAsync(options).SyncResult();
    }

    public IResponse GoTo(string url, PageGotoOptions options = null)
    {
        return WrappedPage.GotoAsync(url, options).SyncResult();
    }

    public void Hover(string selector, PageHoverOptions options = null)
    {
        WrappedPage.HoverAsync(selector, options).SyncResult();
    }

    public string InnerHTML(string selector, PageInnerHTMLOptions options = null)
    {
        return WrappedPage.InnerHTMLAsync(selector, options).SyncResult();
    }

    public string InnerText(string selector, PageInnerTextOptions options = null)
    {
        return WrappedPage.InnerTextAsync(selector, options).SyncResult();
    }

    public string InputValue(string selector, PageInputValueOptions options = null)
    {
        return WrappedPage.InputValueAsync(selector, options).SyncResult();
    }

    public bool IsChecked(string selector, PageIsCheckedOptions options = null)
    {
        return WrappedPage.IsCheckedAsync(selector, options).SyncResult();
    }

    public bool IsDisabled(string selector, PageIsDisabledOptions options = null)
    {
        return WrappedPage.IsDisabledAsync(selector, options).SyncResult();
    }

    public bool IsEditable(string selector, PageIsEditableOptions options = null)
    {
        return WrappedPage.IsEditableAsync(selector, options).SyncResult();
    }

    public bool IsEnabled(string selector, PageIsEnabledOptions options = null)
    {
        return WrappedPage.IsEnabledAsync(selector, options).SyncResult();
    }

    public bool IsHidden(string selector, PageIsHiddenOptions options = null)
    {
        return WrappedPage.IsHiddenAsync(selector, options).SyncResult();
    }

    public bool IsVisible(string selector, PageIsVisibleOptions options = null)
    {
        return WrappedPage.IsVisibleAsync(selector, options).SyncResult();
    }

    public BrowserPage Opener()
    {
        return new BrowserPage(WrappedPage.OpenerAsync().SyncResult());
    }

    public void Pause()
    {
        WrappedPage.PauseAsync().SyncResult();
    }

    public byte[] Pdf(PagePdfOptions options = null)
    {
        return WrappedPage.PdfAsync(options).SyncResult();
    }

    public void Press(string selector, string key, PagePressOptions options = null)
    {
        WrappedPage.PressAsync(selector, key, options).SyncResult();
    }

    public IReadOnlyList<IElementHandle> QuerySelectorAll(string selector)
    {
        return WrappedPage.QuerySelectorAllAsync(selector).SyncResult();
    }

    public IElementHandle QuerySelector(string selector, PageQuerySelectorOptions options = null)
    {
        return WrappedPage.QuerySelectorAsync(selector, options).SyncResult();
    }

    public IResponse Reload(PageReloadOptions options = null)
    {
        return WrappedPage.ReloadAsync(options).SyncResult();
    }

    public void Route(string url, Action<IRoute> handler, PageRouteOptions options = null)
    {
        WrappedPage.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(Regex url, Action<IRoute> handler, PageRouteOptions options = null)
    {
        WrappedPage.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(Func<string, bool> url, Action<IRoute> handler, PageRouteOptions options = null)
    {
        WrappedPage.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(string url, Func<IRoute, Task> handler, PageRouteOptions options = null)
    {
        WrappedPage.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(Regex url, Func<IRoute, Task> handler, PageRouteOptions options = null)
    {
        WrappedPage.RouteAsync(url, handler, options).SyncResult();
    }

    public void Route(Func<string, bool> url, Func<IRoute, Task> handler, PageRouteOptions options = null)
    {
        WrappedPage.RouteAsync(url, handler, options).SyncResult();
    }

    public void RouteFromHAR(string har, PageRouteFromHAROptions options = null)
    {
        WrappedPage.RouteFromHARAsync(har, options).SyncResult();
    }

    public IConsoleMessage RunAndWaitForConsoleMessage(Func<Task> action, PageRunAndWaitForConsoleMessageOptions options = null)
    {
        return WrappedPage.RunAndWaitForConsoleMessageAsync(action, options).SyncResult();
    }

    public IDownload RunAndWaitForDownload(Func<Task> action, PageRunAndWaitForDownloadOptions options = null)
    {
        return WrappedPage.RunAndWaitForDownloadAsync(action, options).SyncResult();
    }

    public IFileChooser RunAndWaitForFileChooser(Func<Task> action, PageRunAndWaitForFileChooserOptions options = null)
    {
        return WrappedPage.RunAndWaitForFileChooserAsync(action, options).SyncResult();
    }

    [Obsolete]
    public IResponse RunAndWaitForNavigation(Func<Task> action, PageRunAndWaitForNavigationOptions options = null)
    {
        return WrappedPage.RunAndWaitForNavigationAsync(action, options).SyncResult();
    }

    public IPage RunAndWaitForPopup(Func<Task> action, PageRunAndWaitForPopupOptions options = null)
    {
        return WrappedPage.RunAndWaitForPopupAsync(action, options).SyncResult();
    }

    public IRequest RunAndWaitForRequest(Func<Task> action, string urlOrPredicate, PageRunAndWaitForRequestOptions options = null)
    {
        return WrappedPage.RunAndWaitForRequestAsync(action, urlOrPredicate, options).SyncResult();
    }

    public IRequest RunAndWaitForRequest(Func<Task> action, Regex urlOrPredicate, PageRunAndWaitForRequestOptions options = null)
    {
        return WrappedPage.RunAndWaitForRequestAsync(action, urlOrPredicate, options).SyncResult();
    }

    public IRequest RunAndWaitForRequest(Func<Task> action, Func<IRequest, bool> urlOrPredicate, PageRunAndWaitForRequestOptions options = null)
    {
        return WrappedPage.RunAndWaitForRequestAsync(action, urlOrPredicate, options).SyncResult();
    }

    public IRequest RunAndWaitForRequestFinished(Func<Task> action, PageRunAndWaitForRequestFinishedOptions options = null)
    {
        return WrappedPage.RunAndWaitForRequestFinishedAsync(action, options).SyncResult();
    }

    public IResponse RunAndWaitForResponse(Func<Task> action, string urlOrPredicate, PageRunAndWaitForResponseOptions options = null)
    {
        return WrappedPage.RunAndWaitForResponseAsync(action, urlOrPredicate, options).SyncResult();
    }

    public IResponse RunAndWaitForResponse(Func<Task> action, Regex urlOrPredicate, PageRunAndWaitForResponseOptions options = null)
    {
        return WrappedPage.RunAndWaitForResponseAsync(action, urlOrPredicate, options).SyncResult();
    }

    public IResponse RunAndWaitForResponse(Func<Task> action, Func<IResponse, bool> urlOrPredicate, PageRunAndWaitForResponseOptions options = null)
    {
        return WrappedPage.RunAndWaitForResponseAsync(action, urlOrPredicate, options).SyncResult();
    }

    public IWebSocket RunAndWaitForWebSocket(Func<Task> action, PageRunAndWaitForWebSocketOptions options = null)
    {
        return WrappedPage.RunAndWaitForWebSocketAsync(action, options).SyncResult();
    }

    public IWorker RunAndWaitForWorker(Func<Task> action, PageRunAndWaitForWorkerOptions options = null)
    {
        return WrappedPage.RunAndWaitForWorkerAsync(action, options).SyncResult();
    }

    public byte[] Screenshot(PageScreenshotOptions options = null)
    {
        return WrappedPage.ScreenshotAsync(options).SyncResult();
    }

    public IReadOnlyList<string> SelectOption(string selector, string values, PageSelectOptionOptions options = null)
    {
        return WrappedPage.SelectOptionAsync(selector, values, options).SyncResult();
    }

    public IReadOnlyList<string> SelectOption(string selector, IElementHandle values, PageSelectOptionOptions options = null)
    {
        return WrappedPage.SelectOptionAsync(selector, values, options).SyncResult();
    }

    public IReadOnlyList<string> SelectOption(string selector, IEnumerable<string> values, PageSelectOptionOptions options = null)
    {
        return WrappedPage.SelectOptionAsync(selector, values, options).SyncResult();
    }

    public IReadOnlyList<string> SelectOption(string selector, SelectOptionValue values, PageSelectOptionOptions options = null)
    {
        return WrappedPage.SelectOptionAsync(selector, values, options).SyncResult();
    }

    public IReadOnlyList<string> SelectOption(string selector, IEnumerable<IElementHandle> values, PageSelectOptionOptions options = null)
    {
        return WrappedPage.SelectOptionAsync(selector, values, options).SyncResult();
    }

    public IReadOnlyList<string> SelectOption(string selector, IEnumerable<SelectOptionValue> values, PageSelectOptionOptions options = null)
    {
        return WrappedPage.SelectOptionAsync(selector, values, options).SyncResult();
    }

    public void SetChecked(string selector, bool checkedState, PageSetCheckedOptions options = null)
    {
        WrappedPage.SetCheckedAsync(selector, checkedState, options).SyncResult();
    }

    public void SetContent(string html, PageSetContentOptions options = null)
    {
        WrappedPage.SetContentAsync(html, options).SyncResult();
    }

    public void SetDefaultNavigationTimeout(float timeout)
    {
        WrappedPage.SetDefaultNavigationTimeout(timeout);
    }

    public void SetDefaultTimeout(float timeout)
    {
        WrappedPage.SetDefaultTimeout(timeout);
    }

    public void SetExtraHTTPHeaders(IEnumerable<KeyValuePair<string, string>> headers)
    {
        WrappedPage.SetExtraHTTPHeadersAsync(headers).SyncResult();
    }

    public void SetInputFiles(string selector, string files, PageSetInputFilesOptions options = null)
    {
        WrappedPage.SetInputFilesAsync(selector, files, options).SyncResult();
    }

    public void SetInputFiles(string selector, IEnumerable<string> files, PageSetInputFilesOptions options = null)
    {
        WrappedPage.SetInputFilesAsync(selector, files, options).SyncResult();
    }

    public void SetInputFiles(string selector, FilePayload files, PageSetInputFilesOptions options = null)
    {
        WrappedPage.SetInputFilesAsync(selector, files, options).SyncResult();
    }

    public void SetInputFiles(string selector, IEnumerable<FilePayload> files, PageSetInputFilesOptions options = null)
    {
        WrappedPage.SetInputFilesAsync(selector, files, options).SyncResult();
    }

    public void SetViewportSize(int width, int height)
    {
        WrappedPage.SetViewportSizeAsync(width, height).SyncResult();
    }

    public void Tap(string selector, PageTapOptions options = null)
    {
        WrappedPage.TapAsync(selector, options).SyncResult();
    }

    public string TextContent(string selector, PageTextContentOptions options = null)
    {
        return WrappedPage.TextContentAsync(selector, options).SyncResult();
    }

    public string Title()
    {
        return WrappedPage.TitleAsync().SyncResult();
    }

    [Obsolete]
    public void Type(string selector, string text, PageTypeOptions options = null)
    {
        WrappedPage.TypeAsync(selector, text, options).SyncResult();
    }

    public void Uncheck(string selector, PageUncheckOptions options = null)
    {
        WrappedPage.UncheckAsync(selector, options).SyncResult();
    }

    public void Unroute(string url, Action<IRoute> handler = null)
    {
        WrappedPage.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(Regex url, Action<IRoute> handler = null)
    {
        WrappedPage.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(Func<string, bool> url, Action<IRoute> handler = null)
    {
        WrappedPage.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(string url, Func<IRoute, Task> handler)
    {
        WrappedPage.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(Regex url, Func<IRoute, Task> handler)
    {
        WrappedPage.UnrouteAsync(url, handler).SyncResult();
    }

    public void Unroute(Func<string, bool> url, Func<IRoute, Task> handler)
    {
        WrappedPage.UnrouteAsync(url, handler).SyncResult();
    }

    public IConsoleMessage WaitForConsoleMessage(PageWaitForConsoleMessageOptions options = null)
    {
        return WrappedPage.WaitForConsoleMessageAsync(options).SyncResult();
    }

    public IDownload WaitForDownload(PageWaitForDownloadOptions options = null)
    {
        return WrappedPage.WaitForDownloadAsync(options).SyncResult();
    }

    public IFileChooser WaitForFileChooser(PageWaitForFileChooserOptions options = null)
    {
        return WrappedPage.WaitForFileChooserAsync(options).SyncResult();
    }

    public IJSHandle WaitForFunction(string expression, object arg = null, PageWaitForFunctionOptions options = null)
    {
        return WrappedPage.WaitForFunctionAsync(expression, arg, options).SyncResult();
    }

    public void WaitForLoadState(LoadState? state = null, PageWaitForLoadStateOptions options = null)
    {
        WrappedPage.WaitForLoadStateAsync(state, options).SyncResult();
    }

    [Obsolete]
    public IResponse WaitForNavigation(PageWaitForNavigationOptions options = null)
    {
        return WrappedPage.WaitForNavigationAsync(options).SyncResult();
    }

    public IPage WaitForPopup(PageWaitForPopupOptions options = null)
    {
        return WrappedPage.WaitForPopupAsync(options).SyncResult();
    }

    public IRequest WaitForRequest(string urlOrPredicate, PageWaitForRequestOptions options = null)
    {
        return WrappedPage.WaitForRequestAsync(urlOrPredicate, options).SyncResult();
    }

    public IRequest WaitForRequest(Regex urlOrPredicate, PageWaitForRequestOptions options = null)
    {
        return WrappedPage.WaitForRequestAsync(urlOrPredicate, options).SyncResult();
    }

    public IRequest WaitForRequest(Func<IRequest, bool> urlOrPredicate, PageWaitForRequestOptions options = null)
    {
        return WrappedPage.WaitForRequestAsync(urlOrPredicate, options).SyncResult();
    }

    public IRequest WaitForRequestFinished(PageWaitForRequestFinishedOptions options = null)
    {
        return WrappedPage.WaitForRequestFinishedAsync(options).SyncResult();
    }

    public IResponse WaitForResponse(string urlOrPredicate, PageWaitForResponseOptions options = null)
    {
        return WrappedPage.WaitForResponseAsync(urlOrPredicate, options).SyncResult();
    }

    public IResponse WaitForResponse(Regex urlOrPredicate, PageWaitForResponseOptions options = null)
    {
        return WrappedPage.WaitForResponseAsync(urlOrPredicate, options).SyncResult();
    }

    public IResponse WaitForResponse(Func<IResponse, bool> urlOrPredicate, PageWaitForResponseOptions options = null)
    {
        return WrappedPage.WaitForResponseAsync(urlOrPredicate, options).SyncResult();
    }

    public IElementHandle WaitForSelector(string selector, PageWaitForSelectorOptions options = null)
    {
        return WrappedPage.WaitForSelectorAsync(selector, options).SyncResult();
    }

    public void WaitForTimeout(float timeout)
    {
        WrappedPage.WaitForTimeoutAsync(timeout).SyncResult();
    }

    public void WaitForURL(string url, PageWaitForURLOptions options = null)
    {
        WrappedPage.WaitForURLAsync(url, options).SyncResult();
    }

    public void WaitForURL(Regex url, PageWaitForURLOptions options = null)
    {
        WrappedPage.WaitForURLAsync(url, options).SyncResult();
    }

    public void WaitForURL(Func<string, bool> url, PageWaitForURLOptions options = null)
    {
        WrappedPage.WaitForURLAsync(url, options).SyncResult();
    }

    public IWebSocket WaitForWebSocket(PageWaitForWebSocketOptions options = null)
    {
        return WrappedPage.WaitForWebSocketAsync(options).SyncResult();
    }

    public IWorker WaitForWorker(PageWaitForWorkerOptions options = null)
    {
        return WrappedPage.WaitForWorkerAsync(options).SyncResult();
    }
}
