// <copyright file="WrappedWebDriverCreateService.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;
using System.IO;
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
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Bellatrix.Web;

public static class WrappedWebDriverCreateService
{
    private static readonly string _driverExecutablePath = ExecutionDirectoryResolver.GetDriverExecutablePath();

    private static ProxyService _proxyService;
    public static BrowserConfiguration BrowserConfiguration { get; set; }
    public static int Port { get; set; }
    public static int DebuggerPort { get; set; }

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
            case ExecutionType.SauceLabs:
                var gridUrl = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Url;
                if (gridUrl == null || !Uri.IsWellFormedUriString(gridUrl.ToString(), UriKind.Absolute))
                {
                    throw new ArgumentException("To execute your tests in WebDriver Grid mode you need to set the gridUri in the browserSettings file.");
                }

                DebuggerPort = GetFreeTcpPort();

                if (executionConfiguration.IsLighthouseEnabled && (executionConfiguration.BrowserType.Equals(BrowserType.Chrome) || executionConfiguration.BrowserType.Equals(BrowserType.ChromeHeadless)))
                {
                    executionConfiguration.DriverOptions.AddArgument("--remote-debugging-address=0.0.0.0");
                    executionConfiguration.DriverOptions.AddArgument($"--remote-debugging-port={DebuggerPort}");
                }

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var options = executionConfiguration.DriverOptions;
                if (!executionConfiguration.BrowserType.Equals(BrowserType.Safari) && !executionConfiguration.BrowserType.Equals(BrowserType.Firefox))
                {
                    options.AddLocalStatePreference("browser", new { enabled_labs_experiments = GetExperimentalOptions() });
                }
                options.SetLoggingPreference(LogType.Browser, LogLevel.All);
                options.SetLoggingPreference("performance", LogLevel.All);
                options.AddUserProfilePreference("disable-popup-blocking", "true");
                options.AddUserProfilePreference("safebrowsing.enabled", "true");
                options.AddArguments("--disable-dev-shm-usage");

                wrappedWebDriver = new RemoteWebDriver(new Uri(gridUrl), options.ToCapabilities(), TimeSpan.FromSeconds(180));

                IAllowsFileDetection allowsDetection = wrappedWebDriver as IAllowsFileDetection;
                if (allowsDetection != null)
                {
                    allowsDetection.FileDetector = new LocalFileDetector();
                }

                break;
        }

        var gridPageLoadTimeout = ConfigurationService.GetSection<WebSettings>().TimeoutSettings.PageLoadTimeout;
        wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(gridPageLoadTimeout);
        var gridScriptTimeout = ConfigurationService.GetSection<WebSettings>().TimeoutSettings.ScriptTimeout;
        wrappedWebDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(gridScriptTimeout);

        if (executionConfiguration.BrowserType != BrowserType.Edge)
        {
            FixDriverCommandExecutionDelay(wrappedWebDriver);

            ////DriverCommandExecutionService commandExecutionService = new DriverCommandExecutionService((RemoteWebDriver)wrappedWebDriver);
            ////commandExecutionService.InitializeSendCommand((RemoteWebDriver)wrappedWebDriver);
        }

        ChangeWindowSize(executionConfiguration.Size, wrappedWebDriver);

        return wrappedWebDriver;
    }

    private static void FixDriverCommandExecutionDelay(IWebDriver driver)
    {
        try
        {
            PropertyInfo commandExecutorProperty = GetPropertyWithThrowOnError(typeof(WebDriver), "CommandExecutor", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty);
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

                ServicesCollection.Current.RegisterInstance(commandExecutor);

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

                ServicesCollection.Current.RegisterInstance<ChromeDriverService>(chromeDriverService);

                chromeDriverService.SuppressInitialDiagnosticInformation = true;
                chromeDriverService.EnableVerboseLogging = false;
                var chromeOptions = executionConfiguration.DriverOptions;
                chromeOptions.AddArguments("--log-level=3");
                chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                chromeOptions.AddUserProfilePreference("safebrowsing.enabled", "true");
                Port = GetFreeTcpPort();
                chromeDriverService.Port = Port;
                DebuggerPort = GetFreeTcpPort();

                if (executionConfiguration.IsLighthouseEnabled)
                {
                    chromeOptions.AddArgument("--remote-debugging-address=0.0.0.0");
                    chromeOptions.AddArgument($"--remote-debugging-port={DebuggerPort}");
                    ////ProcessProvider.StartCLIProcess($"chrome-debug --port={Port}");
                    ////chromeOptions.DebuggerAddress = $"127.0.0.1:{Port}";
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    chromeOptions.AddExtension(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath);
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath != null)
                {
                    string unpackedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load unpacked extension from path: {unpackedExtensionPath}");
                    chromeOptions.AddArguments($"load-extension={unpackedExtensionPath}");
                }

                if (executionConfiguration.ShouldDisableJavaScript)
                {
                    chromeOptions.AddArguments("--blink-settings=scriptEnabled=false");
                }

                if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled && !executionConfiguration.IsLighthouseEnabled)
                {
                    chromeOptions.Proxy = webDriverProxy;
                }

                chromeOptions.AddArgument("hide-scrollbars");
                chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
                chromeOptions.SetLoggingPreference("performance", LogLevel.All);

                wrappedWebDriver = new ChromeDriver(chromeDriverService, chromeOptions);
                //wrappedWebDriver.Manage().Window.Position = new System.Drawing.Point(2000, 1); // To 2nd monitor.
                wrappedWebDriver.Manage().Window.Maximize();
                break;
            case BrowserType.ChromeHeadless:
                new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                var chromeHeadlessDriverService = ChromeDriverService.CreateDefaultService();
                chromeHeadlessDriverService.SuppressInitialDiagnosticInformation = true;
                Port = GetFreeTcpPort();
                chromeHeadlessDriverService.Port = Port;
                var chromeHeadlessOptions = executionConfiguration.DriverOptions;
                chromeHeadlessOptions.AddArguments("--headless=new");
                chromeHeadlessOptions.AddArguments("--log-level=3");
                chromeHeadlessOptions.AddArguments("--test-type");
                chromeHeadlessOptions.AddArguments("--disable-infobars");
                chromeHeadlessOptions.AddArguments("--allow-no-sandbox-job");
                chromeHeadlessOptions.AddArguments("--ignore-certificate-errors");
                chromeHeadlessOptions.AddArguments("--disable-gpu");
                chromeHeadlessOptions.AddArguments("--no-sandbox");
                chromeHeadlessOptions.AddUserProfilePreference("credentials_enable_service", false);
                chromeHeadlessOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                chromeHeadlessOptions.AddArgument("hide-scrollbars");
                chromeHeadlessOptions.UnhandledPromptBehavior = UnhandledPromptBehavior.Dismiss;

                Port = GetFreeTcpPort();
                chromeHeadlessDriverService.Port = Port;
                DebuggerPort = GetFreeTcpPort();

                if (executionConfiguration.IsLighthouseEnabled)
                {
                    chromeHeadlessOptions.AddArgument("--remote-debugging-address=0.0.0.0");
                    chromeHeadlessOptions.AddArgument($"--remote-debugging-port={DebuggerPort}");
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    chromeHeadlessOptions.AddExtension(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath);
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath != null)
                {
                    string unpackedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load unpacked extension from path: {unpackedExtensionPath}");
                    chromeHeadlessOptions.AddArguments($"load-extension={unpackedExtensionPath}");
                }

                if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                {
                    chromeHeadlessOptions.Proxy = webDriverProxy;
                }

                if (executionConfiguration.ShouldDisableJavaScript)
                {
                    chromeHeadlessOptions.AddArguments("--blink-settings=scriptEnabled=false");
                }

                chromeHeadlessOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
                chromeHeadlessOptions.SetLoggingPreference("performance", LogLevel.All);

                wrappedWebDriver = new ChromeDriver(chromeHeadlessDriverService, chromeHeadlessOptions);
                break;
            case BrowserType.Firefox:
                new DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.Latest);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var firefoxOptions = executionConfiguration.DriverOptions;
                firefoxOptions.AddAdditionalOption("acceptInsecureCerts", true);
                if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                {
                    firefoxOptions.Proxy = webDriverProxy;
                }

                if (executionConfiguration.ShouldDisableJavaScript)
                {
                    firefoxOptions.addPreference("javascript.enabled", false);
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

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath != null)
                {
                    Logger.LogError($"Packed Extension loading not supported in Firefox!");

                    // 05-Nov-2020 navramov: Extension loading does not work
                    ////string packedExtensionPath = ConfigurationService.GetSection<WebSettings>().Firefox.PackedExtensionPath.NormalizeAppPath();
                    ////Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    ////firefoxOptions.Profile.AddExtension(ConfigurationService.GetSection<WebSettings>().Firefox.PackedExtensionPath);
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath != null)
                {
                    Logger.LogError($"Unpacked Extension loading not supported in Firefox!");
                }

                var firefoxTimeout = TimeSpan.FromSeconds(180);
                wrappedWebDriver = new FirefoxDriver(firefoxService, firefoxOptions, firefoxTimeout);
                break;
            case BrowserType.FirefoxHeadless:
                new DriverManager().SetUpDriver(new FirefoxConfig());
                var firefoxHeadlessOptions = executionConfiguration.DriverOptions;
                firefoxHeadlessOptions.AddArguments("--headless=new");
                firefoxHeadlessOptions.AddAdditionalOption("acceptInsecureCerts", true);
                if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                {
                    firefoxHeadlessOptions.Proxy = webDriverProxy;
                }

                if (executionConfiguration.ShouldDisableJavaScript)
                {
                    firefoxHeadlessOptions.addPreference("javascript.enabled", false);
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    firefoxHeadlessOptions.Profile.AddExtension(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath);
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath != null)
                {
                    Logger.LogError($"Unpacked Extension loading not supported in Firefox!");
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
                ////new DriverManager().SetUpDriver(new EdgeConfig());
                var edgeOptions = executionConfiguration.DriverOptions;
                edgeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                edgeOptions.AddArguments("--log-level=3");
                if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                {
                    edgeOptions.Proxy = webDriverProxy;
                }

                if (executionConfiguration.ShouldDisableJavaScript)
                {
                    edgeOptions.AddArguments("--blink-settings=scriptEnabled=false");
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    edgeOptions.AddExtension(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath);
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath != null)
                {
                    string unpackedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load unpacked extension from path: {unpackedExtensionPath}");
                    edgeOptions.AddArguments($"load-extension={unpackedExtensionPath}");
                }

                edgeOptions.SetLoggingPreference(LogType.Browser, LogLevel.Severe);
                edgeOptions.SetLoggingPreference("performance", LogLevel.All);


                wrappedWebDriver = new EdgeDriver(edgeOptions);
                break;
            case BrowserType.EdgeHeadless:
                ////new DriverManager().SetUpDriver(new EdgeConfig());
                var edgeHeadlessOptions = executionConfiguration.DriverOptions;
                edgeHeadlessOptions.AddArguments("--headless=new");
                edgeHeadlessOptions.AddArguments("--log-level=3");

                edgeHeadlessOptions.AddArguments("--test-type");
                edgeHeadlessOptions.AddArguments("--disable-infobars");
                edgeHeadlessOptions.AddArguments("--allow-no-sandbox-job");
                edgeHeadlessOptions.AddArguments("--ignore-certificate-errors");
                edgeHeadlessOptions.AddArguments("--disable-gpu");
                edgeHeadlessOptions.AddArguments("--no-sandbox");
                edgeHeadlessOptions.AddUserProfilePreference("credentials_enable_service", false);
                edgeHeadlessOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                edgeHeadlessOptions.AddArgument("hide-scrollbars");
                edgeHeadlessOptions.UnhandledPromptBehavior = UnhandledPromptBehavior.Dismiss;

                edgeHeadlessOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                {
                    edgeHeadlessOptions.Proxy = webDriverProxy;
                }

                if (executionConfiguration.ShouldDisableJavaScript)
                {
                    edgeHeadlessOptions.AddArguments("--blink-settings=scriptEnabled=false");
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    edgeHeadlessOptions.AddExtension(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath);
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath != null)
                {
                    string unpackedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load unpacked extension from path: {unpackedExtensionPath}");
                    edgeHeadlessOptions.AddExtensionPath(unpackedExtensionPath);
                }

                edgeHeadlessOptions.SetLoggingPreference(LogType.Browser, LogLevel.Severe);
                edgeHeadlessOptions.SetLoggingPreference("performance", LogLevel.All);

                wrappedWebDriver = new EdgeDriver(edgeHeadlessOptions);
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
                ieOptions.AddAdditionalOption("disable-popup-blocking", true);

                if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                {
                    ieOptions.Proxy = webDriverProxy;
                }

                if (executionConfiguration.ShouldDisableJavaScript)
                {
                    throw new NotSupportedException("disable javascript not suported for Safari and InternetExplorer.");
                }

                wrappedWebDriver = new InternetExplorerDriver(_driverExecutablePath, ieOptions);
                break;
            case BrowserType.Safari:
                var safariOptions = executionConfiguration.DriverOptions;

                if (executionConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
                {
                    safariOptions.Proxy = webDriverProxy;
                }

                if (executionConfiguration.ShouldDisableJavaScript)
                {
                    throw new NotSupportedException("disable javascript not suported for Safari and InternetExplorer.");
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

    private static string[] GetExperimentalOptions()
    {
        var downloadDirectory = Path.Combine("home", Environment.UserName, "Downloads");
        var values = new string[]
        {
            "profile.default_content_settings.popups@2",
            $"download.default_directory@/{downloadDirectory}",
            "download.prompt_for_download@2",
            "download.directory_upgrade@1",
            "safebrowsing.enabled@2",
            "plugins.always_open_pdf_externally@1",
            "plugins.plugins_disabled@newList<string>{'Chrome PDF Viewer'}",
        };

        return values;
    }
}