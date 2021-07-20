// <copyright file="BrowserWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Bellatrix.KeyVault;
using Bellatrix.Plugins;
using Bellatrix.Web.Enums;
using Bellatrix.Web.Proxy;
using Bellatrix.Web.Services;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;

namespace Bellatrix.Web.Plugins.Browser
{
    public class BrowserLifecyclePlugin : Bellatrix.Plugins.Plugin
    {
        protected override void PreTestsArrange(object sender, Bellatrix.Plugins.PluginEventArgs e)
        {
            // Resolve required data for decision making
            var currentBrowserConfiguration = GetCurrentBrowserConfiguration(e.TestMethodMemberInfo, e.TestClassType, e.Container, e.Arguments);

            if (currentBrowserConfiguration != null)
            {
                ResolvePreviousBrowserType(e.Container);

                // Decide whether the browser needs to be restarted
                bool shouldRestartBrowser = ShouldRestartBrowser(e.Container);

                if (shouldRestartBrowser)
                {
                    RestartBrowser(e.Container);
                    e.Container.RegisterInstance(true, "_isBrowserStartedDuringPreTestsArrange");
                }
            }
            else
            {
                e.Container.RegisterInstance(false, "_isBrowserStartedDuringPreTestsArrange");
            }

            base.PreTestsArrange(sender, e);
        }

        protected override void PreTestInit(object sender, PluginEventArgs e)
        {
            bool isBrowserStartedDuringPreTestsArrange = e.Container.Resolve<bool>("_isBrowserStartedDuringPreTestsArrange");
            if (!isBrowserStartedDuringPreTestsArrange)
            {
                // Resolve required data for decision making
                var currentBrowserConfiguration = GetCurrentBrowserConfiguration(e.TestMethodMemberInfo, e.TestClassType, e.Container, e.Arguments);
                if (currentBrowserConfiguration != null)
                {
                    ResolvePreviousBrowserType(e.Container);

                    // Decide whether the browser needs to be restarted
                    bool shouldRestartBrowser = ShouldRestartBrowser(e.Container);

                    if (shouldRestartBrowser)
                    {
                        RestartBrowser(e.Container);
                    }
                }
            }

            e.Container.RegisterInstance(false, "_isBrowserStartedDuringPreTestsArrange");
            base.PreTestInit(sender, e);
        }

        protected override void PostTestCleanup(object sender, Bellatrix.Plugins.PluginEventArgs e)
        {
            var currentBrowserConfiguration = GetCurrentBrowserConfiguration(e.TestMethodMemberInfo, e.TestClassType, e.Container, e.Arguments);
            if (currentBrowserConfiguration != null)
            {
                if (currentBrowserConfiguration.ShouldCaptureHttpTraffic)
                {
                    var proxyService = e.Container.Resolve<ProxyService>();
                    if (proxyService != null)
                    {
                        proxyService.RequestsHistory.Clear();
                        proxyService.ResponsesHistory.Clear();
                    }
                }

                if (currentBrowserConfiguration.BrowserBehavior == Lifecycle.RestartEveryTime || (currentBrowserConfiguration.BrowserBehavior == Lifecycle.RestartOnFail && !e.TestOutcome.Equals(TestOutcome.Passed)))
                {
                    ShutdownBrowser(e.Container);
                    e.Container.RegisterInstance(false, "_isAppStartedDuringPreTestsArrange");
                }
            }
        }

        private bool ShouldRestartBrowser(ServicesCollection container)
        {
            bool shouldRestartBrowser = false;
            var previousTestExecutionEngine = container.Resolve<TestExecutionEngine>();
            var previousBrowserConfiguration = container.Resolve<BrowserConfiguration>("_previousBrowserConfiguration");
            var currentBrowserConfiguration = container.Resolve<BrowserConfiguration>("_currentBrowserConfiguration");
            if (previousTestExecutionEngine == null || !previousTestExecutionEngine.IsBrowserStartedCorrectly || !currentBrowserConfiguration.Equals(previousBrowserConfiguration))
            {
                shouldRestartBrowser = true;
            }

            return shouldRestartBrowser;
        }

        private void RestartBrowser(ServicesCollection container)
        {
            var currentBrowserConfiguration = container.Resolve<BrowserConfiguration>("_currentBrowserConfiguration");

            ShutdownBrowser(container);

            // Register the ExecutionEngine that should be used for the current run. Will be used in the next test as PreviousEngineType.
            var testExecutionEngine = new TestExecutionEngine();
            container.RegisterInstance(testExecutionEngine);

            // Register the Browser type that should be used for the current run. Will be used in the next test as PreviousBrowserType.
            container.RegisterInstance(currentBrowserConfiguration);

            // Start the current engine with current browser type.
            testExecutionEngine.StartBrowser(currentBrowserConfiguration, container);
        }

        private void ShutdownBrowser(ServicesCollection container)
        {
            // Disposing existing engine call only dispose if in parallel.
            var previousTestExecutionEngine = container.Resolve<TestExecutionEngine>();
            previousTestExecutionEngine?.DisposeAll();

            bool shouldScrollToVisible = ConfigurationService.GetSection<WebSettings>().ShouldAutomaticallyScrollToVisible;
            var browserConfiguration = new BrowserConfiguration(BrowserType.NotSet, false, shouldScrollToVisible);
            container.RegisterInstance(browserConfiguration, "_previousBrowserConfiguration");
            container.UnregisterSingleInstance<TestExecutionEngine>();
        }

        private void ResolvePreviousBrowserType(ServicesCollection container)
        {
            bool shouldScrollToVisible = ConfigurationService.GetSection<WebSettings>().ShouldAutomaticallyScrollToVisible;
            var browserConfiguration = new BrowserConfiguration(BrowserType.NotSet, false, shouldScrollToVisible);
            if (container.IsRegistered<BrowserConfiguration>())
            {
                browserConfiguration = container.Resolve<BrowserConfiguration>();
            }

            container.RegisterInstance(browserConfiguration, "_previousBrowserConfiguration");
        }

        private BrowserConfiguration GetCurrentBrowserConfiguration(MemberInfo memberInfo, Type testClassType, ServicesCollection container, List<object> arguments)
        {
            var browserAttribute = GetBrowserAttribute(memberInfo, testClassType);
            string fullClassName = testClassType.FullName;

            if (browserAttribute != null)
            {
                BrowserType currentBrowserType = browserAttribute.Browser;

                Lifecycle currentLifecycle = browserAttribute.Lifecycle;
                bool shouldCaptureHttpTraffic = browserAttribute.ShouldCaptureHttpTraffic;
                bool shouldAutomaticallyScrollToVisible = browserAttribute.ShouldAutomaticallyScrollToVisible;
                Size currentBrowserSize = browserAttribute.Size;
                ExecutionType executionType = browserAttribute.ExecutionType;

                var options = (browserAttribute as IDriverOptionsAttribute)?.CreateOptions(memberInfo, testClassType) ?? GetDriverOptionsBasedOnBrowser(currentBrowserType, testClassType);
                InitializeCustomCodeOptions(options, testClassType);

                var browserConfiguration = new BrowserConfiguration(executionType, currentLifecycle, currentBrowserType, currentBrowserSize, fullClassName, shouldCaptureHttpTraffic, shouldAutomaticallyScrollToVisible, options);
                container.RegisterInstance(browserConfiguration, "_currentBrowserConfiguration");

                return browserConfiguration;
            }
            else
            {
                BrowserType currentBrowserType = Parse<BrowserType>(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.DefaultBrowser);

                if (arguments != null & arguments.Any())
                {
                    if (arguments[0] is BrowserType)
                    {
                        currentBrowserType = (BrowserType)arguments[0];
                    }
                }

                Lifecycle currentLifecycle = Parse<Lifecycle>(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.DefaultLifeCycle);

                Size currentBrowserSize = default;
                if (!string.IsNullOrEmpty(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Resolution))
                {
                    currentBrowserSize = WindowsSizeResolver.GetWindowSize(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Resolution);
                }

                ExecutionType executionType = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.ExecutionType.ToLower() == "regular" ? ExecutionType.Regular : ExecutionType.Grid;
                bool shouldCaptureHttpTraffic = ConfigurationService.GetSection<WebSettings>().ShouldCaptureHttpTraffic;
                bool shouldAutomaticallyScrollToVisible = ConfigurationService.GetSection<WebSettings>().ShouldAutomaticallyScrollToVisible;
                var options = GetDriverOptionsBasedOnBrowser(currentBrowserType, testClassType);

                if (!string.IsNullOrEmpty(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.BrowserVersion))
                {
                    options.BrowserVersion = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.BrowserVersion;
                }

                if (arguments != null & arguments.Count >= 2)
                {
                    if (arguments[0] is BrowserType && arguments[1] is int)
                    {
                        options.BrowserVersion = arguments[1].ToString();
                    }
                }

                InitializeGridOptionsFromConfiguration(options, testClassType);
                InitializeCustomCodeOptions(options, testClassType);
                var browserConfiguration = new BrowserConfiguration(executionType, currentLifecycle, currentBrowserType, currentBrowserSize, fullClassName, shouldCaptureHttpTraffic, shouldAutomaticallyScrollToVisible, options);
                container.RegisterInstance(browserConfiguration, "_currentBrowserConfiguration");

                return browserConfiguration;
            }
        }

        private TEnum Parse<TEnum>(string value)
         where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value.Replace(" ", string.Empty), true);
        }

        private void InitializeCustomCodeOptions(dynamic options, Type testClassType)
        {
            var customCodeOptions = ServicesCollection.Current.Resolve<Dictionary<string, string>>($"caps-{testClassType.FullName}");
            if (customCodeOptions != null && customCodeOptions.Count > 0)
            {
                foreach (var item in customCodeOptions)
                {
                    if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value))
                    {
                        options.AddAdditionalCapability(item.Key, FormatGridOptions(item.Value, testClassType), true);
                    }
                }
            }
        }

        private void InitializeGridOptionsFromConfiguration(dynamic options, Type testClassType)
        {
            if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Arguments == null)
            {
                return;
            }

            if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Arguments[0].Count > 0)
            {
                foreach (var item in ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Arguments[0])
                {
                    if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value))
                    {
                        options.AddAdditionalCapability(item.Key, FormatGridOptions(item.Value, testClassType), true);
                    }
                }
            }
        }

        private dynamic FormatGridOptions(string option, Type testClassType)
        {
            if (bool.TryParse(option, out bool result))
            {
                return result;
            }
            else if (int.TryParse(option, out int resultNumber))
            {
                return resultNumber;
            }
            else if (double.TryParse(option, out double resultRealNumber))
            {
                return resultRealNumber;
            }
            else if (option.StartsWith("env_") || option.StartsWith("vault_"))
            {
                return SecretsResolver.GetSecret(() => option);
            }
            else
            {
                var runName = testClassType.Assembly.GetName().Name;
                var timestamp = $"{DateTime.Now:yyyyMMdd.HHmm}";
                return option.Replace("{runName}", timestamp).Replace("{runName}", runName);
            }
        }

        private dynamic GetDriverOptionsBasedOnBrowser(BrowserType browserType, Type type)
        {
            dynamic driverOptions;
            switch (browserType)
            {
                case BrowserType.Chrome:
                case BrowserType.ChromeHeadless:
                    driverOptions = ServicesCollection.Current.Resolve<ChromeOptions>(type.FullName) ?? new ChromeOptions();
                    break;
                case BrowserType.Firefox:
                case BrowserType.FirefoxHeadless:
                    driverOptions = ServicesCollection.Current.Resolve<FirefoxOptions>(type.FullName) ?? new FirefoxOptions();
                    var firefoxProfile = ServicesCollection.Current.Resolve<FirefoxProfile>(type.FullName);

                    if (firefoxProfile != null)
                    {
                        driverOptions.Profile = firefoxProfile;
                    }

                    break;
                case BrowserType.InternetExplorer:
                    driverOptions = ServicesCollection.Current.Resolve<InternetExplorerOptions>(type.FullName) ?? new InternetExplorerOptions();
                    break;
                case BrowserType.Edge:
                    driverOptions = ServicesCollection.Current.Resolve<EdgeOptions>(type.FullName) ?? new EdgeOptions();
                    break;
                case BrowserType.Opera:
                    driverOptions = ServicesCollection.Current.Resolve<OperaOptions>(type.FullName) ?? new OperaOptions();
                    break;
                default:
                    {
                        throw new ArgumentException("You should specify a browser.");
                    }
            }

            return driverOptions;
        }

        private BrowserAttribute GetBrowserAttribute(MemberInfo memberInfo, Type testClassType)
        {
            var currentPlatform = DetermineOS();

            var methodBrowserAttribute = memberInfo?.GetCustomAttributes<BrowserAttribute>(true).FirstOrDefault(x => x.OS.Equals(currentPlatform));
            var classBrowserAttribute = testClassType.GetCustomAttributes<BrowserAttribute>(true).FirstOrDefault(x => x.OS.Equals(currentPlatform));

            int? appMethodsAttributesCount = memberInfo?.GetCustomAttributes<BrowserAttribute>(true).Count();
            int appClassAttributesCount = testClassType.GetCustomAttributes<BrowserAttribute>(true).Count();

            if (methodBrowserAttribute != null && appMethodsAttributesCount == 1)
            {
                methodBrowserAttribute.OS = currentPlatform;
            }

            if (appClassAttributesCount == 1 && classBrowserAttribute != null)
            {
                classBrowserAttribute.OS = currentPlatform;
            }

            if (methodBrowserAttribute != null)
            {
                return methodBrowserAttribute;
            }
            else
            {
                return classBrowserAttribute;
            }
        }

        private OS DetermineOS()
        {
            var result = OS.Windows;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                result = OS.OSX;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                result = OS.Linux;
            }

            return result;
        }
    }
}