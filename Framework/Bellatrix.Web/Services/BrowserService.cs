// <copyright file="BrowserService.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using System.Threading;
using Bellatrix.Logging;
using Bellatrix.Utilities;
using OpenQA.Selenium;

namespace Bellatrix.Web
{
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

        public void SwitchToDefault() => WrappedDriver.SwitchTo().DefaultContent();

        public void SwitchToFrame(Frame frame)
        {
            if (frame.WrappedElement != null)
            {
                WrappedDriver.SwitchTo().Frame(frame.WrappedElement);
            }
        }

        public void WaitUntilReady()
        {
            int maxSeconds = ConfigurationService.Instance.GetTimeoutSettings().ElementToExistTimeout * 1000;

            Wait.Until(
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

        public void WaitForAngular()
        {
            string isAngular5 = InvokeScript("return getAllAngularRootElements()[0].attributes['ng-version']");
            if (!string.IsNullOrEmpty(isAngular5))
            {
                Wait.Until(() => bool.Parse(InvokeScript("return window.getAllAngularTestabilities().findIndex(x=>!x.isStable()) === -1")));
            }
            else
            {
                bool? isAngularDefined = bool.Parse(InvokeScript("return window.angular === undefined").ToString());
                if (isAngularDefined == null || !((bool)isAngularDefined))
                {
                    bool isAngularInjectorUnDefined = bool.Parse(InvokeScript("return angular.element(document).injector() === undefined").ToString());
                    if (!isAngularInjectorUnDefined)
                    {
                        Wait.Until(() => bool.Parse(InvokeScript("return angular.element(document).injector().get('$http').pendingRequests.length === 0")));
                    }
                }
            }
        }

        public void WaitForJavaScriptAnimations()
        {
            WaitUntilReady();
            Wait.Until(
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
                timeoutInSeconds: ConfigurationService.Instance.GetTimeoutSettings().WaitForAjaxTimeout,
                retryRateDelay: ConfigurationService.Instance.GetTimeoutSettings().SleepInterval * 1000);
        }

        public void WaitForAjaxRequest(string requestPartialUrl, int additionalTimeoutInSeconds = 0)
        {
            Wait.Until(
                () =>
                {
                    string result = InvokeScript($"return performance.getEntriesByType('resource').filter(item => item.initiatorType == 'xmlhttprequest' && item.name.toLowerCase().includes('{requestPartialUrl.ToLower()}'))[0] !== undefined;");
                    if (result == "True")
                    {
                        return true;
                    }

                    return false;
                },

                timeoutInSeconds: ConfigurationService.Instance.GetTimeoutSettings().WaitForAjaxTimeout + additionalTimeoutInSeconds,
                retryRateDelay: ConfigurationService.Instance.GetTimeoutSettings().SleepInterval * 1000,
                exceptionMessage: $"Ajax request with url contains '{requestPartialUrl}' was not found.");
        }

        public void WaitForAjax()
        {
            int maxSeconds = ConfigurationService.Instance.GetTimeoutSettings().WaitForAjaxTimeout;

            string numberOfAjaxConnections = string.Empty;

            Wait.ForConditionUntilTimeout(
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