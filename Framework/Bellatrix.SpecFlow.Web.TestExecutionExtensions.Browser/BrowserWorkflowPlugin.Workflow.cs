// <copyright file="BrowserWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Bellatrix.SpecFlow.TestWorkflowPlugins;
using Bellatrix.SpecFlow.Web.TestExecutionExtensions.Browser.CloudProviders;
using Bellatrix.Web;
using Bellatrix.Web.Enums;
using Bellatrix.Web.Proxy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Web.TestExecutionExtensions.Browser
{
    [Binding]
    public partial class BrowserWorkflowPlugin : TestWorkflowPlugin
    {
        private static BrowserConfiguration _currentBrowserConfiguration;
        private static SauceLabsBrowserConfiguration _sauceLabsBrowserConfiguration;
        private static GridBrowserConfiguration _gridBrowserConfiguration;
        private static CrossBrowserTestingBrowserConfiguration _crossBrowserTestingBrowserConfiguration;
        private static BrowserStackBrowserConfiguration _browserStackBrowserConfiguration;
        private static BrowserConfiguration _previousBrowserConfiguration;
        private readonly OS _currentPlatform;

        public BrowserWorkflowPlugin()
        {
            _currentBrowserConfiguration = new BrowserConfiguration();
            _sauceLabsBrowserConfiguration = new SauceLabsBrowserConfiguration();
            _gridBrowserConfiguration = new GridBrowserConfiguration();
            _crossBrowserTestingBrowserConfiguration = new CrossBrowserTestingBrowserConfiguration();
            _browserStackBrowserConfiguration = new BrowserStackBrowserConfiguration();
            _currentPlatform = DetermineOS();
        }

        [Given(@"I open browser")]
        public void GivenOpenBrowser()
        {
            _currentBrowserConfiguration.DriverOptions = InitializeDriverOptions(_currentBrowserConfiguration.ExecutionType);

            if (_currentBrowserConfiguration != null)
            {
                ResolvePreviousBrowserType();

                bool shouldRestartBrowser = ShouldRestartBrowser();
                if (shouldRestartBrowser)
                {
                    RestartBrowser(ServicesCollection.Current);
                }
            }
        }

        protected override void PostAfterScenario(object sender, TestWorkflowPluginEventArgs e)
        {
            if (_currentBrowserConfiguration != null)
            {
                if (_currentBrowserConfiguration.ShouldCaptureHttpTraffic)
                {
                    var proxyService = ServicesCollection.Current.Resolve<ProxyService>();
                    if (proxyService != null)
                    {
                        proxyService.RequestsHistory.Clear();
                        proxyService.ResponsesHistory.Clear();
                    }
                }

                if (_currentBrowserConfiguration.BrowserBehavior == BrowserBehavior.RestartEveryTime || (_currentBrowserConfiguration.BrowserBehavior == BrowserBehavior.RestartOnFail && !e.TestOutcome.Equals(TestOutcome.Passed)))
                {
                    ShutdownBrowser(e.Container);
                }
            }
        }

        private bool ShouldRestartBrowser()
        {
            bool shouldRestartBrowser = false;
            var previousTestExecutionEngine = ServicesCollection.Current.Resolve<TestExecutionEngine>();

            if (_currentBrowserConfiguration.BrowserBehavior == BrowserBehavior.RestartEveryTime ||
                _previousBrowserConfiguration.BrowserType == BrowserType.NotSet ||
                !previousTestExecutionEngine.IsBrowserStartedCorrectly)
            {
                shouldRestartBrowser = true;
            }
            else if (!_currentBrowserConfiguration.Equals(_previousBrowserConfiguration))
            {
                shouldRestartBrowser = true;
            }

            return shouldRestartBrowser;
        }

        private void ShutdownBrowser(IServicesCollection container)
        {
            // Disposing existing engine call only dispose if in parallel.
            var previousTestExecutionEngine = container.Resolve<TestExecutionEngine>();
            previousTestExecutionEngine?.Dispose(container);
            container.UnregisterSingleInstance<TestExecutionEngine>();
        }

        private void RestartBrowser(IServicesCollection container)
        {
            if (_previousBrowserConfiguration.BrowserType != BrowserType.NotSet) // NotSet = initial browser start
            {
                ShutdownBrowser(container);
            }

            // Register the ExecutionEngine that should be used for the current run. Will be used in the next test as PreviousEngineType.
            var testExecutionEngine = new TestExecutionEngine();
            container.RegisterInstance(testExecutionEngine);

            // Register the Browser type that should be used for the current run. Will be used in the next test as PreviousBrowserType.
            container.RegisterInstance(_currentBrowserConfiguration);

            // Start the current engine with current browser type.
            testExecutionEngine.StartBrowser(_currentBrowserConfiguration, container);
        }

        private void ResolvePreviousBrowserType()
        {
            var browserConfiguration = new BrowserConfiguration(BrowserType.NotSet, false, false);
            if (ServicesCollection.Current.IsRegistered<BrowserConfiguration>())
            {
                browserConfiguration = ServicesCollection.Current.Resolve<BrowserConfiguration>();
            }

            _previousBrowserConfiguration = browserConfiguration;
        }

        private TDriverOptions AddAdditionalCapabilities<TDriverOptions>(string classFullName, TDriverOptions driverOptions)
            where TDriverOptions : DriverOptions, new()
        {
            var additionalCaps = ServicesCollection.Current.Resolve<Dictionary<string, object>>($"caps-{classFullName}");
            if (additionalCaps != null)
            {
                foreach (var key in additionalCaps.Keys)
                {
                    driverOptions.AddAdditionalCapability(key, additionalCaps[key]);
                }
            }

            return driverOptions;
        }

        private dynamic GetDriverOptionsBasedOnBrowser(string classFullName, BrowserType browserType)
        {
            dynamic driverOptions;
            switch (browserType)
            {
                case BrowserType.Chrome:
                    var chromeOptions = ServicesCollection.Current.Resolve<ChromeOptions>(classFullName);
                    driverOptions = chromeOptions;
                    if (chromeOptions != null)
                    {
                        driverOptions = new ChromeOptions();
                    }

                    break;
                case BrowserType.Firefox:
                    var firefoxOptions = ServicesCollection.Current.Resolve<FirefoxOptions>(classFullName);
                    var firefoxProfile = ServicesCollection.Current.Resolve<FirefoxProfile>(classFullName);
                    driverOptions = firefoxOptions;
                    if (firefoxOptions != null)
                    {
                        driverOptions = new FirefoxOptions();
                    }

                    if (firefoxProfile != null)
                    {
                        driverOptions.Profile = firefoxProfile;
                    }

                    break;
                case BrowserType.InternetExplorer:
                    var ieOptions = ServicesCollection.Current.Resolve<InternetExplorerOptions>(classFullName);
                    driverOptions = ieOptions;
                    if (ieOptions != null)
                    {
                        driverOptions = new InternetExplorerOptions();
                    }

                    break;
                case BrowserType.Edge:
                    var edgeOptions = ServicesCollection.Current.Resolve<EdgeOptions>(classFullName);
                    driverOptions = edgeOptions;
                    if (edgeOptions != null)
                    {
                        driverOptions = new EdgeOptions();
                    }

                    break;
                case BrowserType.Opera:
                    var operaOptions = ServicesCollection.Current.Resolve<OperaOptions>(classFullName);
                    driverOptions = operaOptions;
                    if (operaOptions != null)
                    {
                        driverOptions = new OperaOptions();
                    }

                    break;
                default:
                    {
                        throw new ArgumentException("You should specify a browser.");
                    }
            }

            return driverOptions;
        }

        private dynamic CreateSauceLabsCapabilities(string classFullName)
        {
            var driverOptions = GetDriverOptionsBasedOnBrowser(classFullName, _currentBrowserConfiguration.BrowserType);
            AddAdditionalCapabilities(classFullName, driverOptions);

            string browserName = Enum.GetName(typeof(BrowserType), _currentBrowserConfiguration.BrowserType);
            driverOptions.AddAdditionalCapability("browserName", browserName);
            driverOptions.AddAdditionalCapability("platform", _sauceLabsBrowserConfiguration.Platform);
            driverOptions.AddAdditionalCapability("version", _sauceLabsBrowserConfiguration.BrowserVersion);
            driverOptions.AddAdditionalCapability("screenResolution", _sauceLabsBrowserConfiguration.ScreenResolution);
            driverOptions.AddAdditionalCapability("recordVideo", _sauceLabsBrowserConfiguration.RecordVideo);
            driverOptions.AddAdditionalCapability("recordScreenshots", _sauceLabsBrowserConfiguration.RecordScreenshots);

            var sauceLabsCredentialsResolver = new SauceLabsCredentialsResolver();
            var credentials = sauceLabsCredentialsResolver.GetCredentials();
            driverOptions.AddAdditionalCapability("username", credentials.Item1);
            driverOptions.AddAdditionalCapability("accessKey", credentials.Item2);
            driverOptions.AddAdditionalCapability("name", classFullName);

            return driverOptions;
        }

        private dynamic CreateCrossBrowserTestingCapabilities(string classFullName)
        {
            var driverOptions = GetDriverOptionsBasedOnBrowser(classFullName, _currentBrowserConfiguration.BrowserType);
            AddAdditionalCapabilities(classFullName, driverOptions);

            if (!string.IsNullOrEmpty(_crossBrowserTestingBrowserConfiguration.Build))
            {
                driverOptions.SetCapability("build", _crossBrowserTestingBrowserConfiguration.Build);
            }

            string browserName = Enum.GetName(typeof(BrowserType), _currentBrowserConfiguration.BrowserType);
            driverOptions.AddAdditionalCapability("browserName", browserName);
            driverOptions.AddAdditionalCapability("platform", _crossBrowserTestingBrowserConfiguration.Platform);
            driverOptions.AddAdditionalCapability("version", _crossBrowserTestingBrowserConfiguration.BrowserVersion);
            driverOptions.AddAdditionalCapability("screen_resolution", _crossBrowserTestingBrowserConfiguration.ScreenResolution);
            driverOptions.AddAdditionalCapability("record_video", _crossBrowserTestingBrowserConfiguration.RecordVideo);
            driverOptions.AddAdditionalCapability("record_network", _crossBrowserTestingBrowserConfiguration.RecordNetwork);

            var crossBrowserTestingCredentialsResolver = new CrossBrowserTestingCredentialsResolver();
            var credentials = crossBrowserTestingCredentialsResolver.GetCredentials();
            driverOptions.AddAdditionalCapability("username", credentials.Item1);
            driverOptions.AddAdditionalCapability("password", credentials.Item2);

            driverOptions.AddAdditionalCapability("name", classFullName);

            return driverOptions;
        }

        private dynamic CreateBrowserStackCapabilities(string classFullName)
        {
            var driverOptions = GetDriverOptionsBasedOnBrowser(classFullName, _currentBrowserConfiguration.BrowserType);
            AddAdditionalCapabilities(classFullName, driverOptions);

            driverOptions.Add("browserstack.debug", _browserStackBrowserConfiguration.Debug);

            if (!string.IsNullOrEmpty(_browserStackBrowserConfiguration.Build))
            {
                driverOptions.SetCapability("build", _browserStackBrowserConfiguration.Build);
            }

            string browserName = Enum.GetName(typeof(BrowserType), _currentBrowserConfiguration.BrowserType);
            driverOptions.AddAdditionalCapability("browser", browserName);
            driverOptions.AddAdditionalCapability("os", _browserStackBrowserConfiguration.OperatingSystem);
            driverOptions.AddAdditionalCapability("os_version", _browserStackBrowserConfiguration.OSVersion);
            driverOptions.AddAdditionalCapability("browser_version", _browserStackBrowserConfiguration.BrowserVersion);
            driverOptions.AddAdditionalCapability("resolution", _browserStackBrowserConfiguration.ScreenResolution);
            driverOptions.AddAdditionalCapability("browserstack.video", _browserStackBrowserConfiguration.CaptureVideo);
            driverOptions.AddAdditionalCapability("browserstack.networkLogs", _browserStackBrowserConfiguration.CaptureNetworkLogs);
            string consoleLogTypeText = Enum.GetName(typeof(BrowserStackConsoleLogType), _browserStackBrowserConfiguration.ConsoleLogType).ToLower();
            driverOptions.AddAdditionalCapability("browserstack.console", consoleLogTypeText);

            var browserStackCredentialsResolver = new BrowserStackCredentialsResolver();
            var credentials = browserStackCredentialsResolver.GetCredentials();
            driverOptions.AddAdditionalCapability("browserstack.user", credentials.Item1);
            driverOptions.AddAdditionalCapability("browserstack.key", credentials.Item2);

            return driverOptions;
        }

        private dynamic CreateGridCapabilities(string classFullName)
        {
            string browserName = Enum.GetName(typeof(BrowserType), _currentBrowserConfiguration.BrowserType);
            Platform platform = (Platform)Enum.Parse(typeof(Platform), _gridBrowserConfiguration.Platform);
            var driverOptions = GetDriverOptionsBasedOnBrowser(classFullName, _currentBrowserConfiguration.BrowserType);
            driverOptions.AddAdditionalCapability("browserName", browserName);
            driverOptions.AddAdditionalCapability("browserVersion", _gridBrowserConfiguration.BrowserVersion);
            driverOptions.AddAdditionalCapability("platform", platform);
            AddAdditionalCapabilities(classFullName, driverOptions);

            return driverOptions;
        }

        private DriverOptions InitializeDriverOptions(ExecutionType executionType)
        {
            var driverOptions = default(DriverOptions);
            string classFullName = DetermineTestClassFullName();
            switch (executionType)
            {
                case ExecutionType.Regular:
                    driverOptions = GetDriverOptionsBasedOnBrowser(classFullName, _currentBrowserConfiguration.BrowserType);
                    break;
                case ExecutionType.Grid:
                    driverOptions = CreateGridCapabilities(classFullName);
                    break;
                case ExecutionType.SauceLabs:
                    driverOptions = CreateSauceLabsCapabilities(classFullName);
                    break;
                case ExecutionType.BrowserStack:
                    driverOptions = CreateBrowserStackCapabilities(classFullName);
                    break;
                case ExecutionType.CrossBrowserTesting:
                    driverOptions = CreateCrossBrowserTestingCapabilities(classFullName);
                    break;
            }

            return driverOptions;
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

        private string DetermineTestClassFullName()
        {
            string classFullName = string.Empty;
            var callStackTrace = new StackTrace();
            var currentAssembly = GetType().Assembly;

            foreach (var frame in callStackTrace.GetFrames())
            {
                var frameMethodInfo = frame.GetMethod() as MethodInfo;
                if (!frameMethodInfo?.ReflectedType?.Assembly.Equals(currentAssembly) == true && frameMethodInfo.Name.Equals("ScenarioInitialize"))
                {
                    classFullName = frameMethodInfo.DeclaringType.FullName;
                    break;
                }
            }

            return classFullName;
        }
    }
}