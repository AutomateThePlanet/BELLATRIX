// <copyright file="BrowserService.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Bellatrix.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Safari;

namespace Bellatrix.Web;

public class BrowserService : WebService
{
    public BrowserService(IWebDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public string HtmlSource => WrappedDriver.PageSource;

    public Uri Url => new Uri(WrappedDriver.Url);

    public string Title => WrappedDriver.Title;

    public void Back() => WrappedDriver.Navigate().Back();

    public void Forward() => WrappedDriver.Navigate().Forward();

    public void Refresh() => WrappedDriver.Navigate().Refresh();

    public void RefreshHard()
    {
        var executor = ServicesCollection.Current.Resolve<DriverCommandExecutionService>();
        executor.SendCommandForHardRefresh();
    }

    public void SwitchToDefault() => WrappedDriver.SwitchTo().DefaultContent();

    public void SwitchToActive() => WrappedDriver.SwitchTo().ActiveElement();

    public void SwitchToFirstBrowserTab() => WrappedDriver.SwitchTo().Window(WrappedDriver.WindowHandles.First());

    public void SwitchToLastTab() => WrappedDriver.SwitchTo().Window(WrappedDriver.WindowHandles.Last());

    public void SwitchToTab(string tabName) => WrappedDriver.SwitchTo().Window(tabName);

    public void SwitchToFrame(Frame frame)
    {
        if (frame.WrappedElement != null)
        {
            WrappedDriver.SwitchTo().Frame(frame.WrappedElement);
        }
    }

    /// <summary>
    /// Static wait that calls Thread.Sleep().
    /// USE IN EXTREME CONDITIONS WHEN NO OTHER WAIT WORKS.
    /// </summary>
    /// <param name="timeoutMilliseconds">Timeout milliseconds. Default 1000 ms = 1 second.</param>
    public void WaitForUserInteraction(int timeoutMilliseconds = 1000)
    {
        InjectNotificationToast($"Waiting for User Timeout: {timeoutMilliseconds / 1000} s.");
        Thread.Sleep(timeoutMilliseconds);
    }

    public void WaitUntilReady()
    {
        int maxSeconds = ConfigurationService.GetSection<WebSettings>().TimeoutSettings.WaitUntilReadyTimeout;

        Bellatrix.Utilities.Wait.ForConditionUntilTimeout(
                () =>
                {
                    try
                    {
                        var isReady = InvokeScript("return document.readyState") == "complete";

                        if (isReady)
                        {
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogInformation($"Exception occured while waiting for ReadyState = complete. Message: {ex.Message}");
                    }

                    return false;
                },
                maxSeconds,
                sleepTimeMilliseconds: 100);
    }

    public void Maximize() => WrappedDriver.Manage().Window.Maximize();

    public void ClearSessionStorage()
    {
        var javaScriptService = ServicesCollection.Current.Resolve<JavaScriptService>();
        javaScriptService.Execute("sessionStorage.clear();");
    }

    public void ClearLocalStorage()
    {
        var javaScriptService = ServicesCollection.Current.Resolve<JavaScriptService>();
        javaScriptService.Execute("localStorage.clear();");
    }

    public void WaitForAngular()
    {
        int waitForAngularTimeout = ConfigurationService.GetSection<WebSettings>().TimeoutSettings.WaitForAngularTimeout;
        string isAngular5 = InvokeScript("return getAllAngularRooTComponents()[0].attributes['ng-version']");
        if (!string.IsNullOrEmpty(isAngular5))
        {
            Bellatrix.Utilities.Wait.Until(() => bool.Parse(InvokeScript("return window.getAllAngularTestabilities().findIndex(x=>!x.isStable()) === -1")), waitForAngularTimeout);
        }
        else
        {
            bool? isAngularDefined = bool.Parse(InvokeScript("return window.angular === undefined").ToString());
            if (isAngularDefined == null || !((bool)isAngularDefined))
            {
                bool isAngularInjectorUnDefined = bool.Parse(InvokeScript("return angular.element(document).injector() === undefined").ToString());
                if (!isAngularInjectorUnDefined)
                {
                    Bellatrix.Utilities.Wait.Until(() => bool.Parse(InvokeScript("return angular.element(document).injector().get('$http').pendingRequests.length === 0")), waitForAngularTimeout);
                }
            }
        }
    }

    public void WaitForJavaScriptAnimations()
    {
        WaitUntilReady();
        Bellatrix.Utilities.Wait.Until(
            () =>
            {
                if (bool.TryParse((string)InvokeScript("return jQuery && jQuery(':animated').length === 0"), out bool result))
                {
                    return result;
                }
                else
                {
                    return false;
                }
            },
            timeoutInSeconds: ConfigurationService.GetSection<WebSettings>().TimeoutSettings.WaitForJavaScriptAnimationsTimeout,
            retryRateDelay: ConfigurationService.GetSection<WebSettings>().TimeoutSettings.SleepInterval * 1000);
    }

    public void WaitForAjaxRequest(string requestPartialUrl, int additionalTimeoutInSeconds = 0)
    {
        Bellatrix.Utilities.Wait.Until(
            () =>
            {
                string result = InvokeScript($"return performance.getEntriesByType('resource').filter(item => item.initiatorType == 'xmlhttprequest' && item.name.toLowerCase().includes('{requestPartialUrl.ToLower()}'))[0] !== undefined;");
                if (result == "True")
                {
                    return true;
                }

                return false;
            },

            timeoutInSeconds: ConfigurationService.GetSection<WebSettings>().TimeoutSettings.WaitForAjaxTimeout + additionalTimeoutInSeconds,
            retryRateDelay: ConfigurationService.GetSection<WebSettings>().TimeoutSettings.SleepInterval * 1000,
            exceptionMessage: $"Ajax request with url contains '{requestPartialUrl}' was not found.");
    }

    public void WaitForAjax()
    {
        int maxSeconds = ConfigurationService.GetSection<WebSettings>().TimeoutSettings.WaitForAjaxTimeout;

        string numberOfAjaxConnections = string.Empty;

        Bellatrix.Utilities.Wait.ForConditionUntilTimeout(
            () =>
            {
                numberOfAjaxConnections = InvokeScript("return !isNaN(window.openHTTPs) ? window.openHTTPs : null");

                if (int.TryParse(numberOfAjaxConnections, out int ajaxConnections))
                {
                    if (ajaxConnections == 0)
                    {
                        return true;
                    }
                }
                else
                {
                    // If it's not a number, the page might have been freshly loaded indicating the monkey
                    // patch is replaced or we haven't yet done the patch.
                    MonkeyPatchXMLHttpRequest();
                }

                return false;
            },
            totalRunTimeoutMilliseconds: maxSeconds * 1000,
            sleepTimeMilliseconds: 300,
            onTimeout: () => { Logger.LogWarning($"Timed out waiting for open connections to be closed. Wait time: {maxSeconds} sec."); });
    }

    public void PrintConsoleOutput()
    {
        var consoleLogs = GetConsoleOutput();

        if (consoleLogs.Any())
        {
            Console.Error.WriteLine($"Browser console output: \r\n{consoleLogs.Stringify()}");
        }
    }

    public ReadOnlyCollection<LogEntry> GetConsoleOutput()
    {
        try
        {
            return WrappedDriver.Manage()?.Logs?.GetLog(LogType.Browser) ?? new ReadOnlyCollection<LogEntry>(new List<LogEntry>());

        }
        catch (Exception ex)
        {
            Logger.LogError("Browser console output cannot be extracted: " + ex.Message);
        }

        return new ReadOnlyCollection<LogEntry>(new List<LogEntry>());
    }

    public void InjectNotificationToast(string message, int timeoutMillis = 1500, ToastNotificationType type = ToastNotificationType.Information)
    {
        Console.WriteLine($"|--{type}--| {message}");

        string executionScript = @"window.$bellatrixToastContainer = !window.$bellatrixToastContainer ? Object.assign(document.createElement('div'), {id: 'bellatrixToastContainer', style: 'position: fixed; top: 0; height: 100vh; padding-bottom: 20px; display: flex; pointer-events: none; z-index: 2147483646; justify-content: flex-end; flex-direction: column; overflow: hidden;'}) : window.$bellatrixToastContainer;
                let $message = '" + message + @"';
                let $timeout = " + timeoutMillis + @";
                let $type = '" + type.ToString() + @"';
                if (!document.querySelector('#bellatrixToastContainer')) document.body.appendChild(window.$bellatrixToastContainer);
                let $bellatrixToast
                switch ($type.toLowerCase()) {
                    case 'warning':
                        $bellatrixToast = Object.assign(document.createElement('div'), {textContent: $message.trim() ? $message : 'message not set', style: 'pointer-events: none; z-index: 2147483647; color: ' + ($message.trim() ? '#2e0f00' : '#2e0f0088') + '; width: fit-content; background-color: #fdefc9; margin: 5px 10px; border-radius: 10px; padding: 15px 10px 15px 52px; background-repeat: no-repeat; background-position: 5px center; font-family: Arial, Helvetica, sans-serif; font-size: 15px; line-height: 15px; box-shadow: 0px 0.6px 0.7px rgba(0, 0, 0, 0.1), 0px 1.3px 1.7px rgba(0, 0, 0, 0.116), 0px 2.3px 3.5px rgba(0, 0, 0, 0.128), 0px 4.2px 7.3px rgba(0, 0, 0, 0.135), 0px 10px 20px rgba(0, 0, 0, 0.13); background-image: url(\'data:image/svg+xml,<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 22 22""><circle cx=""11"" cy=""-1041.36"" r=""8"" transform=""matrix(1 0 0-1 0-1030.36)"" opacity="".98"" fill=""%23ffb816""/><path d=""m-26.309 18.07c-1.18 0-2.135.968-2.135 2.129v12.82c0 1.176.948 2.129 2.135 2.129 1.183 0 2.135-.968 2.135-2.129v-12.82c0-1.176-.946-2.129-2.135-2.129zm0 21.348c-1.18 0-2.135.954-2.135 2.135 0 1.18.954 2.135 2.135 2.135 1.181 0 2.135-.954 2.135-2.135 0-1.18-.952-2.135-2.135-2.135z"" transform=""matrix(.30056 0 0 .30056 18.902 1.728)"" fill=""%23fff"" stroke=""%23fff""/></svg>\');'});
                        break;
                    case 'error':
                        $bellatrixToast = Object.assign(document.createElement('div'), {textContent: $message.trim() ? $message : 'message not set', style: 'pointer-events: none; z-index: 2147483647; color: ' + ($message.trim() ? '#2e0004' : '#2e000488') + '; width: fit-content; background-color: #fdc9d2; margin: 5px 10px; border-radius: 10px; padding: 15px 10px 15px 52px; background-repeat: no-repeat; background-position: 5px center; font-family: Arial, Helvetica, sans-serif; font-size: 15px; line-height: 15px; box-shadow: 0px 0.6px 0.7px rgba(0, 0, 0, 0.1), 0px 1.3px 1.7px rgba(0, 0, 0, 0.116), 0px 2.3px 3.5px rgba(0, 0, 0, 0.128), 0px 4.2px 7.3px rgba(0, 0, 0, 0.135), 0px 10px 20px rgba(0, 0, 0, 0.13); background-image: url(\'data:image/svg+xml,<svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" viewBox=""0 0 22 22""><defs><linearGradient gradientUnits=""userSpaceOnUse"" y2=""-2.623"" x2=""0"" y1=""986.67""><stop stop-color=""%23ffce3b""/><stop offset=""1"" stop-color=""%23ffd762""/></linearGradient><linearGradient id=""0"" gradientUnits=""userSpaceOnUse"" y1=""986.67"" x2=""0"" y2=""-2.623""><stop stop-color=""%23ffce3b""/><stop offset=""1"" stop-color=""%23fef4ab""/></linearGradient><linearGradient gradientUnits=""userSpaceOnUse"" x2=""1"" x1=""0"" xlink:href=""%230""/></defs><g transform=""matrix(2 0 0 2-11-2071.72)""><path transform=""translate(7 1037.36)"" d=""m4 0c-2.216 0-4 1.784-4 4 0 2.216 1.784 4 4 4 2.216 0 4-1.784 4-4 0-2.216-1.784-4-4-4"" fill=""%23da4453""/><path d=""m11.906 1041.46l.99-.99c.063-.062.094-.139.094-.229 0-.09-.031-.166-.094-.229l-.458-.458c-.063-.062-.139-.094-.229-.094-.09 0-.166.031-.229.094l-.99.99-.99-.99c-.063-.062-.139-.094-.229-.094-.09 0-.166.031-.229.094l-.458.458c-.063.063-.094.139-.094.229 0 .09.031.166.094.229l.99.99-.99.99c-.063.062-.094.139-.094.229 0 .09.031.166.094.229l.458.458c.063.063.139.094.229.094.09 0 .166-.031.229-.094l.99-.99.99.99c.063.063.139.094.229.094.09 0 .166-.031.229-.094l.458-.458c.063-.062.094-.139.094-.229 0-.09-.031-.166-.094-.229l-.99-.99"" fill=""%23fff""/></g></svg>\');'});
                        break;
                    case 'success':
                        $bellatrixToast = Object.assign(document.createElement('div'), {textContent: $message.trim() ? $message : 'message not set', style: 'pointer-events: none; z-index: 2147483647; color: ' + ($message.trim() ? '#002e0a' : '#002e0a88') + '; width: fit-content; background-color: #c9fdd4; margin: 5px 10px; border-radius: 10px; padding: 15px 10px 15px 52px; background-repeat: no-repeat; background-position: 5px center; font-family: Arial, Helvetica, sans-serif; font-size: 15px; line-height: 15px; box-shadow: 0px 0.6px 0.7px rgba(0, 0, 0, 0.1), 0px 1.3px 1.7px rgba(0, 0, 0, 0.116), 0px 2.3px 3.5px rgba(0, 0, 0, 0.128), 0px 4.2px 7.3px rgba(0, 0, 0, 0.135), 0px 10px 20px rgba(0, 0, 0, 0.13); background-image: url(\'data:image/svg+xml,<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 22 22""><g transform=""matrix(1.99997 0 0 1.99997-10.994-2071.68)"" fill=""%23da4453""><rect y=""1037.36"" x=""7"" height=""8"" width=""8"" fill=""%2332c671"" rx=""4""/><path d=""m123.86 12.966l-11.08-11.08c-1.52-1.521-3.368-2.281-5.54-2.281-2.173 0-4.02.76-5.541 2.281l-53.45 53.53-23.953-24.04c-1.521-1.521-3.368-2.281-5.54-2.281-2.173 0-4.02.76-5.541 2.281l-11.08 11.08c-1.521 1.521-2.281 3.368-2.281 5.541 0 2.172.76 4.02 2.281 5.54l29.493 29.493 11.08 11.08c1.52 1.521 3.367 2.281 5.54 2.281 2.172 0 4.02-.761 5.54-2.281l11.08-11.08 58.986-58.986c1.52-1.521 2.281-3.368 2.281-5.541.0001-2.172-.761-4.02-2.281-5.54"" fill=""%23fff"" transform=""matrix(.0436 0 0 .0436 8.177 1039.72)"" stroke=""none"" stroke-width=""9.512""/></g></svg>\');'});
                        break;
                    case 'information':
                        $bellatrixToast = Object.assign(document.createElement('div'), {textContent: $message.trim() ? $message : 'message not set', style: 'pointer-events: none; z-index: 2147483647; color: ' + ($message.trim() ? '#00122e' : '#00122e88') + '; width: fit-content; background-color: #c9ecfd; margin: 5px 10px; border-radius: 10px; padding: 15px 10px 15px 52px; background-repeat: no-repeat; background-position: 7.5px center; font-family: Arial, Helvetica, sans-serif; font-size: 15px; line-height: 15px; background-size: 40px; box-shadow: 0px 0.6px 0.7px rgba(0, 0, 0, 0.1), 0px 1.3px 1.7px rgba(0, 0, 0, 0.116), 0px 2.3px 3.5px rgba(0, 0, 0, 0.128), 0px 4.2px 7.3px rgba(0, 0, 0, 0.135), 0px 10px 20px rgba(0, 0, 0, 0.13); background-image: url(\'data:image/svg+xml,<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 64 64""><g transform=""matrix(.92857 0 0 .92857-691.94-149.37)""><circle cx=""779.63"" cy=""195.32"" r=""28"" fill=""%230c9fdd""/><g fill=""%23fff"" fill-rule=""evenodd"" fill-opacity="".855""><path d=""m779.62639 179.16433a3.589743 3.589743 0 0 1 3.58975 3.58975 3.589743 3.589743 0 0 1 -3.58975 3.58974 3.589743 3.589743 0 0 1 -3.58974 -3.58974 3.589743 3.589743 0 0 1 3.58974 -3.58975""/><path d=""m779.24 189.93h.764c1.278 0 2.314 1.028 2.314 2.307v16.925c0 1.278-1.035 2.307-2.314 2.307h-.764c-1.278 0-2.307-1.028-2.307-2.307v-16.925c0-1.278 1.028-2.307 2.307-2.307""/></g></g></svg>\');'});             
                }
                window.$bellatrixToastContainer.appendChild($bellatrixToast);
                setTimeout($bellatrixToast.remove.bind($bellatrixToast), $timeout);";

        InvokeScript(executionScript);
    }

    private void MonkeyPatchXMLHttpRequest()
    {
        var numberOfAjaxConnections = InvokeScript("return !isNaN(window.openHTTPs) ? window.openHTTPs : null");

        if (int.TryParse(numberOfAjaxConnections, out int ajaxConnections))
        {
            return;
        }

        var script = "  (function() {" +
                     "var oldOpen = XMLHttpRequest.prototype.open;" +
                     "window.openHTTPs = 0;" +
                     "XMLHttpRequest.prototype.open = function(method, url, async, user, pass) {" +
                     "window.openHTTPs++;" +
                     "this.addEventListener('readystatechange', function() {" +
                     "if(this.readyState == 4) {" +
                     "window.openHTTPs--;" +
                     "}" +
                     "}, false);" +
                     "oldOpen.call(this, method, url, async, user, pass);" +
                     "}" +
                     "})();";

        InvokeScript(script);
    }

    private string InvokeScript(string scriptToInvoke)
    {
        JavaScriptService javaScriptService = new JavaScriptService(WrappedDriver);
        return javaScriptService.Execute(scriptToInvoke)?.ToString();
    }
}