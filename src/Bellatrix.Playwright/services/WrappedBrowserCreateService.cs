using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.Proxy;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Settings.Extensions;
using Bellatrix.Playwright.Settings;
using Bellatrix.Settings;
using Bellatrix.Utilities;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Playwright;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Web;
using Bellatrix.KeyVault;
using Microsoft.Extensions.Configuration;
using System.Collections;
using Bellatrix.Api;
using RestSharp;
using static Amazon.RegionEndpoint;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace Bellatrix.Playwright.Services;

public static class WrappedBrowserCreateService
{
    private const string USER_ENVIRONMENTAL_VARIABLE = "cloud.grid.user";

    private const string ACCESS_KEY_ENVIRONMENTAL_VARIABLE = "cloud.grid.key";

    private static ProxyService _proxyService;
    public static BrowserConfiguration BrowserConfiguration { get; set; }
    public static int Port { get; set; }
    public static int DebuggerPort { get; set; }

    internal static WrappedBrowser Create(BrowserConfiguration executionConfiguration)
    {
        ProcessCleanupService.KillAllBrowsersAdnChildProcessesWindows();

        DisposeBrowserService.TestRunStartTime = DateTime.Now;

        BrowserConfiguration = executionConfiguration;
        var wrappedBrowser = new WrappedBrowser();

        _proxyService = ServicesCollection.Current.Resolve<ProxyService>();

        wrappedBrowser.Playwright = Microsoft.Playwright.Playwright.CreateAsync().Result;

        if (executionConfiguration.ExecutionType == ExecutionType.Regular)
        {
            InitializeWrappedBrowserRegularMode(wrappedBrowser);
        }
        else
        {
            InitializeWrappedBrowserGridMode(executionConfiguration, wrappedBrowser);
        }

        var pageLoadTimeout = ConfigurationService.GetSection<WebSettings>().TimeoutSettings.InMilliseconds().PageLoadTimeout;
        wrappedBrowser.CurrentPage.SetDefaultNavigationTimeout(pageLoadTimeout);

        ChangeWindowSize(executionConfiguration.Size, wrappedBrowser);



        return wrappedBrowser;
    }


    private static void InitializeWrappedBrowserRegularMode(WrappedBrowser wrappedBrowser)
    {
        BrowserTypeLaunchOptions launchOptions = new();
        var args = new List<string>();

        Port = GetFreeTcpPort();
        DebuggerPort = GetFreeTcpPort();

        if (BrowserConfiguration.ShouldCaptureHttpTraffic && _proxyService.IsEnabled)
        {
            launchOptions.Proxy = new Microsoft.Playwright.Proxy() { Server = $"{IPAddress.Any}:{Port}" };
        }

        switch (BrowserConfiguration.BrowserType)
        {
            case BrowserChoice.Chromium:
                launchOptions.Headless = false;

                args.Add("--log-level=3");

                if (BrowserConfiguration.IsLighthouseEnabled)
                {
                    args.Add("--remote-debugging-address=0.0.0.0");
                    args.Add($"--remote-debugging-port={DebuggerPort}");
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    args.Add(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath);
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath != null)
                {
                    string unpackedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load unpacked extension from path: {unpackedExtensionPath}");
                    args.Add($"load-extension={unpackedExtensionPath}");
                }

                if (BrowserConfiguration.ShouldDisableJavaScript)
                {
                    args.Add("--blink-settings=scriptEnabled=false");
                }

                args.Add("--hide-scrollbars");

                launchOptions.Args = args;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.LaunchAsync(launchOptions).Result;
                break;
            case BrowserChoice.ChromiumHeadless:
                launchOptions.Headless = true;

                args.Add("--log-level=3");
                args.Add("--test-type");
                args.Add("--disable-infobars");
                args.Add("--allow-no-sandbox-job");
                args.Add("--ignore-certificate-errors");
                args.Add("--disable-gpu");
                args.Add("hide-scrollbars");

                if (BrowserConfiguration.IsLighthouseEnabled)
                {
                    args.Add("--remote-debugging-address=0.0.0.0");
                    args.Add($"--remote-debugging-port={DebuggerPort}");
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    args.Add(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath);
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath != null)
                {
                    string unpackedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load unpacked extension from path: {unpackedExtensionPath}");
                    args.Add($"load-extension={unpackedExtensionPath}");
                }

                launchOptions.Args = args;
                launchOptions.ChromiumSandbox = false;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.LaunchAsync(launchOptions).Result;

                break;
            case BrowserChoice.Chrome:
                launchOptions.Headless = false;
                launchOptions.Channel = "chrome";

                args.Add("--log-level=3");

                if (BrowserConfiguration.IsLighthouseEnabled)
                {
                    args.Add("--remote-debugging-address=0.0.0.0");
                    args.Add($"--remote-debugging-port={DebuggerPort}");
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    args.Add(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath);
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath != null)
                {
                    string unpackedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load unpacked extension from path: {unpackedExtensionPath}");
                    args.Add($"load-extension={unpackedExtensionPath}");
                }

                args.Add("--hide-scrollbars");

                launchOptions.Args = args;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.LaunchAsync(launchOptions).Result;
                break;
            case BrowserChoice.ChromeHeadless:
                launchOptions.Headless = true;
                launchOptions.Channel = "chrome";

                args.Add("--log-level=3");
                args.Add("--test-type");
                args.Add("--disable-infobars");
                args.Add("--allow-no-sandbox-job");
                args.Add("--ignore-certificate-errors");
                args.Add("--disable-gpu");
                args.Add("hide-scrollbars");

                if (BrowserConfiguration.IsLighthouseEnabled)
                {
                    args.Add("--remote-debugging-address=0.0.0.0");
                    args.Add($"--remote-debugging-port={DebuggerPort}");
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    args.Add(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.PackedExtensionPath);
                }

                if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath != null)
                {
                    string unpackedExtensionPath = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.UnpackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load unpacked extension from path: {unpackedExtensionPath}");
                    args.Add($"load-extension={unpackedExtensionPath}");
                }

                if (BrowserConfiguration.ShouldDisableJavaScript)
                {
                    args.Add("--blink-settings=scriptEnabled=false");
                }

                launchOptions.Args = args;
                launchOptions.ChromiumSandbox = false;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.LaunchAsync(launchOptions).Result;
                break;
            case BrowserChoice.Edge:
                launchOptions.Headless = false;
                launchOptions.Channel = "msedge";

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.LaunchAsync(launchOptions).Result;
                break;
            case BrowserChoice.EdgeHeadless:
                launchOptions.Headless = true;
                launchOptions.Channel = "msedge";

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.LaunchAsync(launchOptions).Result;
                break;
            case BrowserChoice.Firefox:
                launchOptions.Headless = false;
                args.Add("--acceptInsecureCerts=true");

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Firefox.LaunchAsync(launchOptions).Result;
                break;
            case BrowserChoice.FirefoxHeadless:
                launchOptions.Headless = true;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Firefox.LaunchAsync(launchOptions).Result;
                break;
            case BrowserChoice.Webkit:
                launchOptions.Headless = false;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Webkit.LaunchAsync(launchOptions).Result;
                break;
            case BrowserChoice.WebkitHeadless:
                launchOptions.Headless = true;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Webkit.LaunchAsync(launchOptions).Result;
                break;
        }

        InitializeBrowserContextAndPage(wrappedBrowser);
    }

    internal static void InitializeBrowserContextAndPage(WrappedBrowser wrappedBrowser)
    {
        BrowserNewContextOptions options = new();

        options.JavaScriptEnabled = !BrowserConfiguration.ShouldDisableJavaScript;

        if (wrappedBrowser.CurrentContext != null) wrappedBrowser.CurrentContext.DisposeAsync().AsTask().SyncResult();

        wrappedBrowser.CurrentContext = wrappedBrowser.Browser.NewContextAsync().Result;
        wrappedBrowser.CurrentPage = wrappedBrowser.CurrentContext.NewPageAsync().Result;

        AddConsoleMessageListener(wrappedBrowser);
    }

    private static void InitializeWrappedBrowserGridMode(BrowserConfiguration executionConfiguration, WrappedBrowser wrappedBrowser)
    {
        var capsObject = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Arguments[0].ParseBooleans();

        var capabilities = JsonConvert.SerializeObject(capsObject);

        capabilities = ResolveSecrets(capabilities);

        switch (executionConfiguration.ExecutionType)
        {
            case ExecutionType.Selenoid:
            case ExecutionType.Grid:
                {
                    var browserType = executionConfiguration.BrowserType;
                    if (browserType is BrowserChoice.Firefox or BrowserChoice.FirefoxHeadless or BrowserChoice.Webkit or BrowserChoice.WebkitHeadless)
                    {
                        throw new NotSupportedException("Playwright supports running in Selenium Grid only Chromium browsers.");
                    }

                    var gridUrl = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.GridUrl;
                    if (gridUrl is null || gridUrl == string.Empty)
                    {
                        throw new ArgumentException("No grid url found in the config!");
                    }

                    var client = new ApiClientService(gridUrl);

                    var request = new RestRequest("/session", Method.Post);
                    var body = new Dictionary<string, object>()
                        {
                            { "capabilities", new Dictionary<string, object>()
                                {
                                    { "alwaysMatch", JsonConvert.DeserializeObject(capabilities) }
                                }
                            },
                        };
                    request.AddJsonBody(body);

                    var response = client.Execute(request);

                    var json = JObject.Parse(response.Content);
                    wrappedBrowser.GridSessionId = (string)json.SelectToken("$.value.sessionId");

                    var cdpUrl = new UriBuilder((string)json.SelectToken("$.value.capabilities['se:cdp']")) { Host = client.BaseUrl.Host }.Uri;

                    wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.ConnectOverCDPAsync(cdpUrl.ToString(), new BrowserTypeConnectOverCDPOptions { Timeout = 10000 }).Result;

                    InitializeBrowserContextAndPage(wrappedBrowser);

                    break;
                }
            case ExecutionType.LambdaTest:
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.ConnectAsync($"wss://cdp.lambdatest.com/playwright?capabilities={HttpUtility.UrlEncode(capabilities)}").Result;

                    InitializeBrowserContextAndPage(wrappedBrowser);

                    break;
                }
            case ExecutionType.BrowserStack:
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.ConnectAsync($"wss://cdp.browserstack.com/playwright?caps={HttpUtility.UrlEncode(capabilities)}").Result;

                    InitializeBrowserContextAndPage(wrappedBrowser);

                    break;
                }
            default:
                throw new NotImplementedException("The Execution Type is either not possible or not implemented.");
        }
    }

    private static void ChangeWindowSize(Size windowSize, WrappedBrowser wrappedBrowser)
    {
        if (windowSize != default)
        {
            wrappedBrowser.CurrentPage.ViewportSize.Width = windowSize.Width;
            wrappedBrowser.CurrentPage.ViewportSize.Height = windowSize.Height;
        }
        // There is no maximize option in playwright
        //else
        //{
        //    wrappedWebDriver.Manage().Window.Maximize();
        //}
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

    private static void AddConsoleMessageListener(WrappedBrowser wrappedBrowser)
    {
        wrappedBrowser.ConsoleMessages = new ConsoleMessageStorage(wrappedBrowser.CurrentContext);

        wrappedBrowser.CurrentContext.Console += (_, msg) => wrappedBrowser.ConsoleMessages.Add(msg);
    }

    private static string ResolveSecrets(string capabilities)
    {
        if (capabilities.Contains(USER_ENVIRONMENTAL_VARIABLE))
        {
            capabilities = capabilities.Replace(USER_ENVIRONMENTAL_VARIABLE, SecretsResolver.GetSecret(USER_ENVIRONMENTAL_VARIABLE));
        }

        if (capabilities.Contains(ACCESS_KEY_ENVIRONMENTAL_VARIABLE))
        {
            capabilities = capabilities.Replace(ACCESS_KEY_ENVIRONMENTAL_VARIABLE, SecretsResolver.GetSecret(ACCESS_KEY_ENVIRONMENTAL_VARIABLE));
        }

        return capabilities;
    }

    private static Dictionary<string, object> ParseBooleans(this object obj)
    {
        Dictionary<string, object> dictionary;

        if (obj is IDictionary)
        {
            dictionary = (Dictionary<string, object>)obj;
        }
        else
        {
            dictionary = ConvertToDictionary(obj);
        }

        var newDictionary = new Dictionary<string, object>();

        foreach (var (key, value) in dictionary)
        {
            if (value is null)
            {
                newDictionary.Add(key, null);
                continue;
            }

            if (value.GetType().Equals(typeof(string)))
            {
                if (value.ToString().ToLower().Equals("true") || value.ToString().ToLower().Equals("false"))
                {
                    newDictionary.Add(key, bool.Parse(value.ToString().ToLower()));
                }

                else
                {
                    newDictionary.Add(key, value);
                }
            }

            else
            {
                newDictionary.Add(key, ((Dictionary<string, object>)value).ParseBooleans());
            }
        }

        return newDictionary;
    }

    private static Dictionary<string, object> ConvertToDictionary(object obj)
    {
        var dictionary = new Dictionary<string, object>();

        if (obj is not null)
        {
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyName = char.ToLower(property.Name[0]) + (property.Name.Length > 1 ? property.Name.Substring(1) : string.Empty);

                var value = property.GetValue(obj);

                if (value is null) continue;

                dictionary[property.GetCustomAttribute<ConfigurationKeyNameAttribute>()?.Name ?? propertyName] = value;
            }
        }

        return dictionary;
    }
}
