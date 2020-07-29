// <copyright file="WrappedWebDriverCreateService.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Bellatrix.Trace;
using Bellatrix.Utilities;
using Bellatrix.Web.Enums;
using Bellatrix.Web.Proxy;
using Bellatrix.Web.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace Bellatrix.Web
{
    public static class WrappedWebDriverCreateService
    {
        private static readonly string _driverExecutablePath = ExecutionDirectoryResolver.GetDriverExecutablePath();

        private static ProxyService _proxyService;
        public static BrowserSettings BrowserSettings { get; set; }
        public static BrowserConfiguration BrowserConfiguration { get; set; }

        public static IWebDriver Create(BrowserConfiguration executionConfiguration)
        {
            ProcessCleanupService.KillAllDriversAndChildProcessesWindows();

            DisposeDriverService.TestRunStartTime = DateTime.Now;

            BrowserConfiguration = executionConfiguration;
            var wrappedWebDriver = default(IWebDriver);
            _proxyService = ServicesCollection.Current.Resolve<ProxyService>();
            var webDriverProxy = new OpenQA.Selenium.Proxy
            {
                HttpProxy = $"http://127.0.0.1:{_proxyService.Port}",
                SslProxy = $"http://127.0.0.1:{_proxyService.Port}",
            };

            switch (executionConfiguration.ExecutionType)
            {
                case ExecutionType.Regular:
                    wrappedWebDriver = InitializeDriverRegularMode(executionConfiguration, webDriverProxy);
                    break;
                case ExecutionType.Grid:
                    var gridUri = ConfigurationService.Instance.GetWebSettings().Remote.GridUri;
                    if (gridUri == null || !Uri.IsWellFormedUriString(gridUri.ToString(), UriKind.Absolute))
                    {
                        throw new ArgumentException("To execute your tests in WebDriver Grid mode you need to set the gridUri in the browserSettings file.");
                    }

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    wrappedWebDriver = new RemoteWebDriver(gridUri, executionConfiguration.DriverOptions);
                    var gridPageLoadTimeout = ConfigurationService.Instance.GetWebSettings().Remote.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(gridPageLoadTimeout);
                    var gridScriptTimeout = ConfigurationService.Instance.GetWebSettings().Remote.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(gridScriptTimeout);
                    BrowserSettings = ConfigurationService.Instance.GetWebSettings().Remote;
                    ChangeWindowSize(executionConfiguration.BrowserType, executionConfiguration.Size, wrappedWebDriver);
                    break;
                case ExecutionType.BrowserStack:
                    var browserStackUri = ConfigurationService.Instance.GetWebSettings().BrowserStack.GridUri;
                    BrowserSettings = ConfigurationService.Instance.GetWebSettings().BrowserStack;
                    if (browserStackUri == null || !Uri.IsWellFormedUriString(browserStackUri.ToString(), UriKind.Absolute))
                    {
                        throw new ArgumentException("To execute your tests in BrowserStack you need to set the gridUri in the browserSettings file.");
                    }

                    wrappedWebDriver = new RemoteWebDriver(browserStackUri, executionConfiguration.DriverOptions);
                    var browserStackPageLoadTimeout = ConfigurationService.Instance.GetWebSettings().BrowserStack.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(browserStackPageLoadTimeout);
                    var browserStackScriptTimeout = ConfigurationService.Instance.GetWebSettings().BrowserStack.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(browserStackScriptTimeout);
                    break;
                case ExecutionType.CrossBrowserTesting:
                    var crossBrowserTestingUri = ConfigurationService.Instance.GetWebSettings().CrossBrowserTesting.GridUri;
                    BrowserSettings = ConfigurationService.Instance.GetWebSettings().CrossBrowserTesting;
                    if (crossBrowserTestingUri == null || !Uri.IsWellFormedUriString(crossBrowserTestingUri.ToString(), UriKind.Absolute))
                    {
                        throw new ArgumentException("To execute your tests in CrossBrowserTesting you need to set the gridUri in the browserSettings file.");
                    }

                    wrappedWebDriver = new RemoteWebDriver(crossBrowserTestingUri, executionConfiguration.DriverOptions);
                    var crossBrowserTestingPageLoadTimeout = ConfigurationService.Instance.GetWebSettings().CrossBrowserTesting.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(crossBrowserTestingPageLoadTimeout);
                    var crossBrowserTestingScriptTimeout = ConfigurationService.Instance.GetWebSettings().CrossBrowserTesting.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(crossBrowserTestingScriptTimeout);
                    break;
                case ExecutionType.SauceLabs:
                    var sauceLabsSettings = ConfigurationService.Instance.GetWebSettings().SauceLabs;
                    var sauceLabsUri = sauceLabsSettings.GridUri;
                    if (sauceLabsUri == null || !Uri.IsWellFormedUriString(sauceLabsUri.ToString(), UriKind.Absolute))
                    {
                        throw new ArgumentException("To execute your tests in SauceLabs you need to set the gridUri in the browserSettings file.");
                    }

                    wrappedWebDriver = new RemoteWebDriver(sauceLabsUri, executionConfiguration.DriverOptions);
                    var sauceLabsPageLoadTimeout = sauceLabsSettings.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(sauceLabsPageLoadTimeout);
                    var sauceLabsScriptTimeout = sauceLabsSettings.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(sauceLabsScriptTimeout);
                    BrowserSettings = sauceLabsSettings;
                    break;
            }

            if (executionConfiguration.BrowserType != BrowserType.Edge)
            {
                FixDriverCommandExecutionDelay((RemoteWebDriver)wrappedWebDriver);
            }

            ChangeWindowSize(executionConfiguration.BrowserType, executionConfiguration.Size, wrappedWebDriver);

            return wrappedWebDriver;
        }

        private static void FixDriverCommandExecutionDelay(RemoteWebDriver driver)
        {
            try
            {
                PropertyInfo commandExecutorProperty = GetPropertyWithThrowOnError(typeof(RemoteWebDriver), "CommandExecutor", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty);
                ICommandExecutor commandExecutor = (ICommandExecutor)commandExecutorProperty.GetValue(driver);

                FieldInfo GetRemoteServerUriField(ICommandExecutor executor)
                {
                    return executor.GetType().GetField("remoteServerUri", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.SetField);
                }

                FieldInfo remoteServerUriField = GetRemoteServerUriField(commandExecutor);

                if (remoteServerUriField == null)
                {
                    FieldInfo internalExecutorField = commandExecutor.GetType().GetField("internalExecutor", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);
                    commandExecutor = (ICommandExecutor)internalExecutorField.GetValue(commandExecutor);
                    remoteServerUriField = GetRemoteServerUriField(commandExecutor);
                }

                if (remoteServerUriField != null)
                {
                    string remoteServerUri = remoteServerUriField.GetValue(commandExecutor).ToString();

                    string localhostUriPrefix = "http://localhost";

                    if (remoteServerUri.StartsWith(localhostUriPrefix, StringComparison.Ordinal))
                    {
                        remoteServerUri = remoteServerUri.Replace(localhostUriPrefix, "http://127.0.0.1");

                        remoteServerUriField.SetValue(commandExecutor, new Uri(remoteServerUri));
                    }
                }
            }
            catch (Exception)
            {
                // Failed to apply fix of command execution delay.
            }
        }

        internal static PropertyInfo GetPropertyWithThrowOnError(Type type, string name, BindingFlags bindingFlags = BindingFlags.Default)
        {
            PropertyInfo property = bindingFlags == BindingFlags.Default ? type.GetProperty(name) : type.GetProperty(name, bindingFlags);

            if (property == null)
            {
                throw new MissingMemberException(type.FullName, name);
            }

            return property;
        }

        private static IWebDriver InitializeDriverRegularMode(BrowserConfiguration executionConfiguration, OpenQA.Selenium.Proxy webDriverProxy)
        {
            IWebDriver wrappedWebDriver;
            switch (executionConfiguration.BrowserType)
            {
                case BrowserType.Chrome:
                    var chromeDriverService = ChromeDriverService.CreateDefaultService(_driverExecutablePath);
                    chromeDriverService.SuppressInitialDiagnosticInformation = true;
                    chromeDriverService.EnableVerboseLogging = false;
                    chromeDriverService.Port = GetFreeTcpPort();
                    var chromeOptions = GetChromeOptions(executionConfiguration.ClassFullName);
                    chromeOptions.AddArguments("--log-level=3");
                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        chromeOptions.Proxy = webDriverProxy;
                    }

                    wrappedWebDriver = new ChromeDriver(chromeDriverService, chromeOptions);
                    var chromePageLoadTimeout = ConfigurationService.Instance.GetWebSettings().Chrome.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(chromePageLoadTimeout);
                    var chromeScriptTimeout = ConfigurationService.Instance.GetWebSettings().Chrome.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(chromeScriptTimeout);
                    BrowserSettings = ConfigurationService.Instance.GetWebSettings().Chrome;
                    break;
                case BrowserType.ChromeHeadless:
                    var chromeHeadlessDriverService = ChromeDriverService.CreateDefaultService(_driverExecutablePath);
                    chromeHeadlessDriverService.SuppressInitialDiagnosticInformation = true;
                    chromeHeadlessDriverService.Port = GetFreeTcpPort();
                    var chromeHeadlessOptions = GetChromeOptions(executionConfiguration.ClassFullName);
                    chromeHeadlessOptions.AddArguments("--headless");
                    chromeHeadlessOptions.AddArguments("--log-level=3");
                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        chromeHeadlessOptions.Proxy = webDriverProxy;
                    }

                    wrappedWebDriver = new ChromeDriver(chromeHeadlessDriverService, chromeHeadlessOptions);
                    var chromeHeadlessPageLoadTimeout = ConfigurationService.Instance.GetWebSettings().ChromeHeadless.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(chromeHeadlessPageLoadTimeout);
                    var chromeHeadlessScriptTimeout = ConfigurationService.Instance.GetWebSettings().ChromeHeadless.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(chromeHeadlessScriptTimeout);
                    BrowserSettings = ConfigurationService.Instance.GetWebSettings().ChromeHeadless;
                    break;
                case BrowserType.Firefox:
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    var firefoxOptions = GetFirefoxOptions(executionConfiguration.ClassFullName);
                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        firefoxOptions.Proxy = webDriverProxy;
                    }

                    var firefoxService = FirefoxDriverService.CreateDefaultService(_driverExecutablePath);
                    firefoxService.SuppressInitialDiagnosticInformation = true;
                    firefoxService.Port = GetFreeTcpPort();
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
                        {
                            firefoxService.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                        }
                        else
                        {
                            firefoxService.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                        }

                        // TODO: Anton(15.12.2019): Add option to set the path via environment variable.
                    }

                    var firefoxProfile = ServicesCollection.Current.Resolve<FirefoxProfile>(executionConfiguration.ClassFullName);
                    if (firefoxProfile != null)
                    {
                        firefoxOptions.Profile = firefoxProfile;
                    }

                    var firefoxTimeout = TimeSpan.FromSeconds(180);
                    wrappedWebDriver = new FirefoxDriver(firefoxService, firefoxOptions, firefoxTimeout);
                    var firefoxPageLoadTimeout = ConfigurationService.Instance.GetWebSettings().Firefox.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(firefoxPageLoadTimeout);
                    var firefoxScriptTimeout = ConfigurationService.Instance.GetWebSettings().Firefox.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(firefoxScriptTimeout);
                    BrowserSettings = ConfigurationService.Instance.GetWebSettings().Firefox;
                    break;
                case BrowserType.FirefoxHeadless:
                    var firefoxHeadlessOptions = GetFirefoxOptions(executionConfiguration.ClassFullName);
                    firefoxHeadlessOptions.AddArguments("--headless");
                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        firefoxHeadlessOptions.Proxy = webDriverProxy;
                    }

                    var service = FirefoxDriverService.CreateDefaultService(_driverExecutablePath);
                    service.SuppressInitialDiagnosticInformation = true;
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
                        {
                            service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                        }
                        else
                        {
                            service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                        }

                        // TODO: Anton(15.12.2019): Add option to set the path via environment variable.
                    }

                    service.Port = GetFreeTcpPort();
                    wrappedWebDriver = new FirefoxDriver(service, firefoxHeadlessOptions);
                    var firefoxHeadlessPageLoadTimeout = ConfigurationService.Instance.GetWebSettings().FirefoxHeadless.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(firefoxHeadlessPageLoadTimeout);
                    var firefoxHeadlessScriptTimeout = ConfigurationService.Instance.GetWebSettings().FirefoxHeadless.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(firefoxHeadlessScriptTimeout);
                    BrowserSettings = ConfigurationService.Instance.GetWebSettings().FirefoxHeadless;
                    break;
                case BrowserType.Edge:
                    var edgeDriverService = Microsoft.Edge.SeleniumTools.EdgeDriverService.CreateChromiumService(_driverExecutablePath);
                    edgeDriverService.SuppressInitialDiagnosticInformation = true;
                    var edgeOptions = GetEdgeOptions(executionConfiguration.ClassFullName);
                    edgeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    edgeOptions.UseChromium = true;
                    edgeOptions.AddArguments("--log-level=3");
                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        edgeOptions.Proxy = webDriverProxy;
                    }

                    wrappedWebDriver = new Microsoft.Edge.SeleniumTools.EdgeDriver(edgeDriverService, edgeOptions);
                    var edgePageLoadTimeout = ConfigurationService.Instance.GetWebSettings().Edge.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(edgePageLoadTimeout);
                    var edgeScriptTimeout = ConfigurationService.Instance.GetWebSettings().Edge.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(edgeScriptTimeout);
                    BrowserSettings = ConfigurationService.Instance.GetWebSettings().Edge;
                    break;
                case BrowserType.EdgeHeadless:
                    var edgeHeadlessDriverService = Microsoft.Edge.SeleniumTools.EdgeDriverService.CreateChromiumService(_driverExecutablePath);
                    edgeHeadlessDriverService.SuppressInitialDiagnosticInformation = true;
                    var edgeHeadlessOptions = GetEdgeOptions(executionConfiguration.ClassFullName);
                    edgeHeadlessOptions.AddArguments("--headless");
                    edgeHeadlessOptions.AddArguments("--log-level=3");
                    edgeHeadlessOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    edgeHeadlessOptions.UseChromium = true;
                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        edgeHeadlessOptions.Proxy = webDriverProxy;
                    }

                    wrappedWebDriver = new Microsoft.Edge.SeleniumTools.EdgeDriver(edgeHeadlessDriverService, edgeHeadlessOptions);
                    var edgeHeadlessPageLoadTimeout = ConfigurationService.Instance.GetWebSettings().Edge.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(edgeHeadlessPageLoadTimeout);
                    var edgeHeadlessScriptTimeout = ConfigurationService.Instance.GetWebSettings().Edge.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(edgeHeadlessScriptTimeout);
                    BrowserSettings = ConfigurationService.Instance.GetWebSettings().Edge;
                    break;
                case BrowserType.Opera:
                    // the driver will be different for different OS.
                    // Check for different releases- https://github.com/operasoftware/operachromiumdriver/releases
                    var operaOptions = GetOperaOptions(executionConfiguration.ClassFullName);

                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        operaOptions.Proxy = webDriverProxy;
                    }

                    var operaService = OperaDriverService.CreateDefaultService(_driverExecutablePath);
                    operaService.SuppressInitialDiagnosticInformation = true;
                    operaService.Port = GetFreeTcpPort();

                    try
                    {
                        wrappedWebDriver = new OperaDriver(operaService, operaOptions);
                    }
                    catch (WebDriverException ex) when (ex.Message.Contains("DevToolsActivePort file doesn't exist"))
                    {
                        throw new Exception("This is a known issue in the latest versions of Opera driver. It is reported to the Opera team. As soon it is fixed we will update BELLATRIX.", ex);
                    }

                    var operaPageLoadTimeout = ConfigurationService.Instance.GetWebSettings().Opera.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(operaPageLoadTimeout);
                    var operaScriptTimeout = ConfigurationService.Instance.GetWebSettings().Opera.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(operaScriptTimeout);
                    BrowserSettings = ConfigurationService.Instance.GetWebSettings().Opera;
                    break;
                case BrowserType.InternetExplorer:
                    // Steps to configure IE to always allow blocked content:
                    // From Internet Explorer, select the Tools menu, then the Options...
                    // In the Internet Options dialog, select the Advanced tab...
                    // Scroll down until you see the Security options. Enable the checkbox "Allow active content to run in files on My Computer"
                    // Also, check https://github.com/SeleniumHQ/selenium/wiki/InternetExplorerDriver#required-configuration
                    // in case of OpenQA.Selenium.NoSuchWindowException: Unable to get browser --> Uncheck IE Options --> Security Tab -> Uncheck "Enable Protected Mode"
                    var ieOptions = GetInternetExplorerOptions(executionConfiguration.ClassFullName);
                    ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    ieOptions.IgnoreZoomLevel = true;
                    ieOptions.EnableNativeEvents = false;
                    ieOptions.EnsureCleanSession = true;
                    ieOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                    ieOptions.ForceShellWindowsApi = true;
                    ieOptions.AddAdditionalCapability("disable-popup-blocking", true);

                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        ieOptions.Proxy = webDriverProxy;
                    }

                    wrappedWebDriver = new InternetExplorerDriver(_driverExecutablePath, ieOptions);

                    var iePageLoadTimeout = ConfigurationService.Instance.GetWebSettings().InternetExplorer.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(iePageLoadTimeout);
                    var ieScriptTimeout = ConfigurationService.Instance.GetWebSettings().InternetExplorer.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(ieScriptTimeout);
                    BrowserSettings = ConfigurationService.Instance.GetWebSettings().InternetExplorer;
                    break;
                case BrowserType.Safari:
                    var safariOptions = GetSafariOptions(executionConfiguration.ClassFullName);

                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        safariOptions.Proxy = webDriverProxy;
                    }

                    wrappedWebDriver = new SafariDriver(safariOptions);

                    var safariPageLoadTimeout = ConfigurationService.Instance.GetWebSettings().Safari.PageLoadTimeout;
                    wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(safariPageLoadTimeout);
                    var safariScriptTimeout = ConfigurationService.Instance.GetWebSettings().Safari.ScriptTimeout;
                    wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(safariScriptTimeout);
                    BrowserSettings = ConfigurationService.Instance.GetWebSettings().Safari;
                    break;
                default:
                    throw new NotSupportedException($"Not supported browser {executionConfiguration.BrowserType}");
            }

            return wrappedWebDriver;
        }

        private static void ChangeWindowSize(BrowserType browserType, Size windowSize, IWebDriver wrappedWebDriver)
        {
            try
            {
                // HACK: (Anton: 09.12.2017) Temporary solution until they fix it in the official release.
                // Maximize in Opera throws invalid exception
                // System.InvalidOperationException: disconnected: unable to connect to renderer
                //    (Session info: chrome with embedded Chromium 62.0.3202.89)
                // (Driver info: OperaDriver = 2.32(cfa164127aab5f93e5e47d9dcf8407380eb42c50), platform = Windows NT 10.0.15063 x86_64)(102).
                if (windowSize != default)
                {
                    wrappedWebDriver.Manage().Window.Size = windowSize;
                }
                else if (browserType != BrowserType.Opera)
                {
                    wrappedWebDriver.Manage().Window.Maximize();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static int GetFreeTcpPort()
        {
            Thread.Sleep(100);
            var tcpListener = new TcpListener(IPAddress.Loopback, 0);
            tcpListener.Start();
            int port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
            tcpListener.Stop();
            return port;
        }

        private static SafariOptions GetSafariOptions(string classFullName)
        {
            var options = ServicesCollection.Current.Resolve<SafariOptions>(classFullName) ?? new SafariOptions();

            return options;
        }

        private static InternetExplorerOptions GetInternetExplorerOptions(string classFullName)
        {
            var options = ServicesCollection.Current.Resolve<InternetExplorerOptions>(classFullName) ?? new InternetExplorerOptions();

            return options;
        }

        private static OperaOptions GetOperaOptions(string classFullName)
        {
            var options = ServicesCollection.Current.Resolve<OperaOptions>(classFullName) ?? new OperaOptions();

            return options;
        }

        private static Microsoft.Edge.SeleniumTools.EdgeOptions GetEdgeOptions(string classFullName)
        {
            var options = ServicesCollection.Current.Resolve<Microsoft.Edge.SeleniumTools.EdgeOptions>(classFullName) ?? new Microsoft.Edge.SeleniumTools.EdgeOptions();

            return options;
        }

        private static ChromeOptions GetChromeOptions(string classFullName)
        {
            var options = ServicesCollection.Current.Resolve<ChromeOptions>(classFullName) ?? new ChromeOptions();

            return options;
        }

        private static FirefoxOptions GetFirefoxOptions(string classFullName)
        {
            var options = ServicesCollection.Current.Resolve<FirefoxOptions>(classFullName) ?? new FirefoxOptions();

            return options;
        }
    }
}