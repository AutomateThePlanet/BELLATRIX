// <copyright file="WrappedWebDriverCreateService.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Bellatrix.Settings;
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
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Bellatrix.Web
{
    public static class WrappedWebDriverCreateService
    {
        private static readonly string _driverExecutablePath = ExecutionDirectoryResolver.GetDriverExecutablePath();

        private static ProxyService _proxyService;
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
                    var gridUrl = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Url;
                    if (gridUrl == null || !Uri.IsWellFormedUriString(gridUrl.ToString(), UriKind.Absolute))
                    {
                        throw new ArgumentException("To execute your tests in WebDriver Grid mode you need to set the gridUri in the browserSettings file.");
                    }

                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    wrappedWebDriver = new RemoteWebDriver(new Uri(gridUrl), executionConfiguration.DriverOptions);
                    break;
            }

            var gridPageLoadTimeout = ConfigurationService.GetSection<WebSettings>().TimeoutSettings.PageLoadTimeout;
            wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(gridPageLoadTimeout);
            var gridScriptTimeout = ConfigurationService.GetSection<WebSettings>().TimeoutSettings.ScriptTimeout;
            wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(gridScriptTimeout);

            if (executionConfiguration.BrowserType != BrowserType.Edge)
            {
                FixDriverCommandExecutionDelay((RemoteWebDriver)wrappedWebDriver);
            }

            ChangeWindowSize(executionConfiguration.Size, wrappedWebDriver);

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
            catch (Exception e)
            {
                // Failed to apply fix of command execution delay.
                e.PrintStackTrace();
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
                    new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.SuppressInitialDiagnosticInformation = true;
                    chromeDriverService.EnableVerboseLogging = false;
                    chromeDriverService.Port = GetFreeTcpPort();
                    var chromeOptions = executionConfiguration.DriverOptions;
                    chromeOptions.AddArguments("--log-level=3");

                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        chromeOptions.Proxy = webDriverProxy;
                    }

                    wrappedWebDriver = new ChromeDriver(chromeDriverService, chromeOptions);
                    break;
                case BrowserType.ChromeHeadless:
                    new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    var chromeHeadlessDriverService = ChromeDriverService.CreateDefaultService();
                    chromeHeadlessDriverService.SuppressInitialDiagnosticInformation = true;
                    chromeHeadlessDriverService.Port = GetFreeTcpPort();
                    var chromeHeadlessOptions = executionConfiguration.DriverOptions;
                    chromeHeadlessOptions.AddArguments("--headless");
                    chromeHeadlessOptions.AddArguments("--log-level=3");

                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        chromeHeadlessOptions.Proxy = webDriverProxy;
                    }

                    wrappedWebDriver = new ChromeDriver(chromeHeadlessDriverService, chromeHeadlessOptions);
                    break;
                case BrowserType.Firefox:
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    var firefoxOptions = executionConfiguration.DriverOptions;
                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        firefoxOptions.Proxy = webDriverProxy;
                    }

                    var firefoxService = FirefoxDriverService.CreateDefaultService();
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
                    if (firefoxProfile == null)
                    {
                        firefoxOptions.Profile = new FirefoxProfile(_driverExecutablePath);
                        ServicesCollection.Current.RegisterInstance(firefoxOptions.Profile, executionConfiguration.ClassFullName);
                    }

                    var firefoxTimeout = TimeSpan.FromSeconds(180);
                    wrappedWebDriver = new FirefoxDriver(firefoxService, firefoxOptions, firefoxTimeout);
                    break;
                case BrowserType.FirefoxHeadless:
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    var firefoxHeadlessOptions = executionConfiguration.DriverOptions;
                    firefoxHeadlessOptions.AddArguments("--headless");
                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        firefoxHeadlessOptions.Proxy = webDriverProxy;
                    }

                    var service = FirefoxDriverService.CreateDefaultService();
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
                    break;
                case BrowserType.Edge:
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    var edgeDriverService = Microsoft.Edge.SeleniumTools.EdgeDriverService.CreateChromiumService();
                    edgeDriverService.SuppressInitialDiagnosticInformation = true;
                    var edgeOptions = executionConfiguration.DriverOptions;
                    edgeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    edgeOptions.UseChromium = true;
                    edgeOptions.AddArguments("--log-level=3");
                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        edgeOptions.Proxy = webDriverProxy;
                    }

                    wrappedWebDriver = new Microsoft.Edge.SeleniumTools.EdgeDriver(edgeDriverService, edgeOptions);
                    break;
                case BrowserType.EdgeHeadless:
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    var edgeHeadlessDriverService = Microsoft.Edge.SeleniumTools.EdgeDriverService.CreateChromiumService();
                    edgeHeadlessDriverService.SuppressInitialDiagnosticInformation = true;
                    var edgeHeadlessOptions = executionConfiguration.DriverOptions;
                    edgeHeadlessOptions.AddArguments("--headless");
                    edgeHeadlessOptions.AddArguments("--log-level=3");
                    edgeHeadlessOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    edgeHeadlessOptions.UseChromium = true;
                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        edgeHeadlessOptions.Proxy = webDriverProxy;
                    }

                    wrappedWebDriver = new Microsoft.Edge.SeleniumTools.EdgeDriver(edgeHeadlessDriverService, edgeHeadlessOptions);
                    break;
                case BrowserType.Opera:
                    new DriverManager().SetUpDriver(new OperaConfig());

                    // the driver will be different for different OS.
                    // Check for different releases- https://github.com/operasoftware/operachromiumdriver/releases
                    var operaOptions = executionConfiguration.DriverOptions;

                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        operaOptions.Proxy = webDriverProxy;
                    }

                    var operaService = OperaDriverService.CreateDefaultService();
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

                    break;
                case BrowserType.InternetExplorer:
                    new DriverManager().SetUpDriver(new InternetExplorerConfig());

                    // Steps to configure IE to always allow blocked content:
                    // From Internet Explorer, select the Tools menu, then the Options...
                    // In the Internet Options dialog, select the Advanced tab...
                    // Scroll down until you see the Security options. Enable the checkbox "Allow active content to run in files on My Computer"
                    // Also, check https://github.com/SeleniumHQ/selenium/wiki/InternetExplorerDriver#required-configuration
                    // in case of OpenQA.Selenium.NoSuchWindowException: Unable to get browser --> Uncheck IE Options --> Security Tab -> Uncheck "Enable Protected Mode"
                    var ieOptions = executionConfiguration.DriverOptions;
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
                    break;
                case BrowserType.Safari:
                    var safariOptions = executionConfiguration.DriverOptions;

                    if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                    {
                        safariOptions.Proxy = webDriverProxy;
                    }

                    wrappedWebDriver = new SafariDriver(safariOptions);
                    break;
                default:
                    throw new NotSupportedException($"Not supported browser {executionConfiguration.BrowserType}");
            }

            return wrappedWebDriver;
        }

        private static void ChangeWindowSize(Size windowSize, IWebDriver wrappedWebDriver)
        {
            if (windowSize != default)
            {
                wrappedWebDriver.Manage().Window.Size = windowSize;
            }
            else
            {
                wrappedWebDriver.Manage().Window.Maximize();
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
    }
}