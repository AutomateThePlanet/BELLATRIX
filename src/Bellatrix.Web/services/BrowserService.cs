// <copyright file="BrowserService.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Bellatrix.Utilities;
using Bellatrix.Web.Configuration;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;

namespace Bellatrix.Web
{
    public class BrowserService : WebService
    {
        private const string NotSupportedOperationClearCacheMessage = "It is exceedingly hard to implement, especially in all major browsers. Some browsers _may_ offer that ability- Chrome, ChromeHeadless, Edge and EdgeHeadless. To clear the cache for other browsers you will need to restart them.";

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

        public void WaitUntilReady()
        {
            int maxSeconds = ConfigurationService.GetSection<TimeoutSettings>().ElementToExistTimeout * 1000;

            Bellatrix.Utilities.Wait.Until(
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
                    "Timed out waiting for complete page load",
                    retryRateDelay: 100);
        }

        public void Maximize() => WrappedDriver.Manage().Window.Maximize();

        public void ClearSessionStorage()
        {
            var browserConfig = ServicesCollection.Current.Resolve<BrowserConfiguration>();
            switch (browserConfig.BrowserType)
            {
                case BrowserType.NotSet:
                    break;
                case BrowserType.Chrome:
                case BrowserType.ChromeHeadless:
                    var chromeDriver = (ChromeDriver)WrappedDriver;
                    chromeDriver.WebStorage.SessionStorage.Clear();
                    break;
                case BrowserType.Firefox:
                case BrowserType.FirefoxHeadless:
                    var firefoxDriver = (FirefoxDriver)WrappedDriver;
                    firefoxDriver.WebStorage.SessionStorage.Clear();
                    break;
                case BrowserType.InternetExplorer:
                    var ieDriver = (InternetExplorerDriver)WrappedDriver;
                    ieDriver.WebStorage.SessionStorage.Clear();
                    break;
                case BrowserType.Edge:
                case BrowserType.EdgeHeadless:
                    var edgeDriver = (EdgeDriver)WrappedDriver;
                    edgeDriver.WebStorage.SessionStorage.Clear();
                    break;
                case BrowserType.Opera:
                    var operaDriver = (OperaDriver)WrappedDriver;
                    operaDriver.WebStorage.SessionStorage.Clear();
                    break;
                case BrowserType.Safari:
                    var safariDriver = (SafariDriver)WrappedDriver;
                    safariDriver.WebStorage.SessionStorage.Clear();
                    break;
            }
        }

        public void ClearLocalStorage()
        {
            var browserConfig = ServicesCollection.Current.Resolve<BrowserConfiguration>();
            switch (browserConfig.BrowserType)
            {
                case BrowserType.NotSet:
                    break;
                case BrowserType.Chrome:
                case BrowserType.ChromeHeadless:
                    var chromeDriver = (ChromeDriver)WrappedDriver;
                    chromeDriver.WebStorage.LocalStorage.Clear();
                    break;
                case BrowserType.Firefox:
                case BrowserType.FirefoxHeadless:
                    var firefoxDriver = (FirefoxDriver)WrappedDriver;
                    firefoxDriver.WebStorage.LocalStorage.Clear();
                    break;
                case BrowserType.InternetExplorer:
                    var ieDriver = (InternetExplorerDriver)WrappedDriver;
                    ieDriver.WebStorage.LocalStorage.Clear();
                    break;
                case BrowserType.Edge:
                case BrowserType.EdgeHeadless:
                    var edgeDriver = (EdgeDriver)WrappedDriver;
                    edgeDriver.WebStorage.LocalStorage.Clear();
                    break;
                case BrowserType.Opera:
                    var operaDriver = (OperaDriver)WrappedDriver;
                    operaDriver.WebStorage.LocalStorage.Clear();
                    break;
                case BrowserType.Safari:
                    var safariDriver = (SafariDriver)WrappedDriver;
                    safariDriver.WebStorage.LocalStorage.Clear();
                    break;
            }
        }

        ///// <summary>
        ///// It is exceedingly hard to implement, especially in all major browsers.
        ///// Some browsers _may_ offer that ability- Chrome, ChromeHeadless, Edge and EdgeHeadless.
        ///// To clear the cache for other browsers you will need to restart them.
        ///// </summary>

        //// TODO: Ventsi 13.10 This implementation work, but it depends on the Selemium 4.0, which is still not available in plex nuget store.
        ////public void ClearCache()
        ////{
        ////    var browserConfig = ServicesCollection.Current.Resolve<BrowserConfiguration>();
        ////    switch (browserConfig.BrowserType)
        ////    {
        ////        case BrowserType.NotSet:
        ////            break;
        ////        case BrowserType.Chrome:
        ////        case BrowserType.ChromeHeadless:
        ////            var chromeDriver = (ChromeDriver)WrappedDriver;
        ////            var session = chromeDriver.CreateDevToolsSession();
        ////            session.Network.ClearBrowserCache();
        ////            break;
        ////        case BrowserType.Edge:
        ////        case BrowserType.EdgeHeadless:
        ////            var edgeDriver = (EdgeDriver)WrappedDriver;
        ////            var edgeSession = edgeDriver.CreateDevToolsSession();
        ////            edgeSession.Network.ClearBrowserCache();
        ////            break;
        ////        case BrowserType.Firefox:
        ////        case BrowserType.FirefoxHeadless:
        ////        case BrowserType.Opera:
        ////        case BrowserType.Safari:
        ////        case BrowserType.InternetExplorer:
        ////            throw new NotSupportedException(NotSupportedOperationClearCacheMessage);
        ////    }
        ////}

        public void WaitForAngular()
        {
            string isAngular5 = InvokeScript("return getAllAngularRootElements()[0].attributes['ng-version']");
            if (!string.IsNullOrEmpty(isAngular5))
            {
                Bellatrix.Utilities.Wait.Until(() => bool.Parse(InvokeScript("return window.getAllAngularTestabilities().findIndex(x=>!x.isStable()) === -1")));
            }
            else
            {
                bool? isAngularDefined = bool.Parse(InvokeScript("return window.angular === undefined").ToString());
                if (isAngularDefined == null || !((bool)isAngularDefined))
                {
                    bool isAngularInjectorUnDefined = bool.Parse(InvokeScript("return angular.element(document).injector() === undefined").ToString());
                    if (!isAngularInjectorUnDefined)
                    {
                        Bellatrix.Utilities.Wait.Until(() => bool.Parse(InvokeScript("return angular.element(document).injector().get('$http').pendingRequests.length === 0")));
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
                timeoutInSeconds: ConfigurationService.GetSection<TimeoutSettings>().WaitForAjaxTimeout,
                retryRateDelay: ConfigurationService.GetSection<TimeoutSettings>().SleepInterval * 1000);
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

                timeoutInSeconds: ConfigurationService.GetSection<TimeoutSettings>().WaitForAjaxTimeout + additionalTimeoutInSeconds,
                retryRateDelay: ConfigurationService.GetSection<TimeoutSettings>().SleepInterval * 1000,
                exceptionMessage: $"Ajax request with url contains '{requestPartialUrl}' was not found.");
        }

        public void InjectNotificationToast(string message, int timeoutMillis = 1500)
        {
            string executionScript = @"	var elemDiv = document.createElement('div');
            var dynamicId = Date.now;
            elemDiv.id = dynamicId;
            elemDiv.innerHTML = """ + message + @""";
            elemDiv.style.cssText = 'position:fixed;z-index:9999;bottom: 0px;color: #00529B;background-color: #BDE5F8;border: 1px solid;margin: 10px 0px;padding: 15px 10px 15px 50px;background-repeat: no-repeat;background-position: 10px center;font-family: Arial, Helvetica, sans-serif;font-size: 13px;background-image: url(""https://i.imgur.com/Z8q7ww7.png"");';
            document.body.appendChild(elemDiv);
            setTimeout(() => { document.getElementById(dynamicId).remove(); }, " + timeoutMillis.ToString() + ")";

            InvokeScript(executionScript);
        }

        public void WaitForAjax()
        {
            int maxSeconds = ConfigurationService.GetSection<TimeoutSettings>().WaitForAjaxTimeout;

            string numberOfAjaxConnections = string.Empty;

            Bellatrix.Utilities.Wait.ForConditionUntilTimeout(
                () =>
                {
                    numberOfAjaxConnections = InvokeScript("return window.openHTTPs ? window.openHTTPs.length : null");

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

        private void MonkeyPatchXMLHttpRequest()
        {
            var numberOfAjaxConnections = InvokeScript("return window.openHTTPs ? window.openHTTPs.length : null");

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
}