// <copyright file="WrappedBrowserCreateService.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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

using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.Proxy;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Settings.Extensions;
using Bellatrix.Playwright.Settings;
using Bellatrix.Settings;
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
using Newtonsoft.Json.Linq;
using System.Threading;
using Bellatrix.Playwright.SyncPlaywright;

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

        wrappedBrowser.Playwright = new BellatrixPlaywright(Microsoft.Playwright.Playwright.CreateAsync().Result);

        if (executionConfiguration.ExecutionType == ExecutionType.Regular)
        {
            InitializeWrappedBrowserRegularMode(wrappedBrowser);
        }
        else
        {
            InitializeWrappedBrowserGridMode(executionConfiguration, wrappedBrowser);
        }

        var pageLoadTimeout = WebSettings.TimeoutSettings.InMilliseconds().PageLoadTimeout;
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
            case BrowserTypes.Chromium:
                launchOptions.Headless = false;

                args.Add("--log-level=3");

                if (BrowserConfiguration.IsLighthouseEnabled)
                {
                    args.Add("--remote-debugging-address=0.0.0.0");
                    args.Add($"--remote-debugging-port={DebuggerPort}");
                }

                if (WebSettings.ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = WebSettings.ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    args.Add(WebSettings.ExecutionSettings.PackedExtensionPath);
                }

                if (WebSettings.ExecutionSettings.UnpackedExtensionPath != null)
                {
                    string unpackedExtensionPath = WebSettings.ExecutionSettings.UnpackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load unpacked extension from path: {unpackedExtensionPath}");
                    args.Add($"load-extension={unpackedExtensionPath}");
                }

                if (BrowserConfiguration.ShouldDisableJavaScript)
                {
                    args.Add("--blink-settings=scriptEnabled=false");
                }

                args.Add("--hide-scrollbars");

                launchOptions.Args = args;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Launch(launchOptions);
                break;
            case BrowserTypes.ChromiumHeadless:
                launchOptions.Headless = true;

                args.Add("--log-level=3");
                args.Add("--test-type");
                args.Add("--disable-infobars");
                args.Add("--allow-no-sandbox-job");
                args.Add("--ignore-certificate-errors");
                args.Add("--disable-gpu");

                if (BrowserConfiguration.IsLighthouseEnabled)
                {
                    args.Add("--remote-debugging-address=0.0.0.0");
                    args.Add($"--remote-debugging-port={DebuggerPort}");
                }

                if (WebSettings.ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = WebSettings.ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    args.Add(WebSettings.ExecutionSettings.PackedExtensionPath);
                }

                if (WebSettings.ExecutionSettings.UnpackedExtensionPath != null)
                {
                    string unpackedExtensionPath = WebSettings.ExecutionSettings.UnpackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load unpacked extension from path: {unpackedExtensionPath}");
                    args.Add($"load-extension={unpackedExtensionPath}");
                }

                launchOptions.Args = args;
                launchOptions.ChromiumSandbox = false;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Launch(launchOptions);

                break;
            case BrowserTypes.Chrome:
                launchOptions.Headless = false;
                launchOptions.Channel = "chrome";

                args.Add("--log-level=3");

                if (BrowserConfiguration.IsLighthouseEnabled)
                {
                    args.Add("--remote-debugging-address=0.0.0.0");
                    args.Add($"--remote-debugging-port={DebuggerPort}");
                }

                if (WebSettings.ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = WebSettings.ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    args.Add(WebSettings.ExecutionSettings.PackedExtensionPath);
                }

                if (WebSettings.ExecutionSettings.UnpackedExtensionPath != null)
                {
                    string unpackedExtensionPath = WebSettings.ExecutionSettings.UnpackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load unpacked extension from path: {unpackedExtensionPath}");
                    args.Add($"load-extension={unpackedExtensionPath}");
                }

                args.Add("--hide-scrollbars");

                launchOptions.Args = args;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Launch(launchOptions);
                break;
            case BrowserTypes.ChromeHeadless:
                launchOptions.Headless = true;
                launchOptions.Channel = "chrome";

                args.Add("--log-level=3");
                args.Add("--test-type");
                args.Add("--disable-infobars");
                args.Add("--allow-no-sandbox-job");
                args.Add("--ignore-certificate-errors");
                args.Add("--disable-gpu");

                if (BrowserConfiguration.IsLighthouseEnabled)
                {
                    args.Add("--remote-debugging-address=0.0.0.0");
                    args.Add($"--remote-debugging-port={DebuggerPort}");
                }

                if (WebSettings.ExecutionSettings.PackedExtensionPath != null)
                {
                    string packedExtensionPath = WebSettings.ExecutionSettings.PackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load packed extension from path: {packedExtensionPath}");
                    args.Add(WebSettings.ExecutionSettings.PackedExtensionPath);
                }

                if (WebSettings.ExecutionSettings.UnpackedExtensionPath != null)
                {
                    string unpackedExtensionPath = WebSettings.ExecutionSettings.UnpackedExtensionPath.NormalizeAppPath();
                    Logger.LogInformation($"Trying to load unpacked extension from path: {unpackedExtensionPath}");
                    args.Add($"load-extension={unpackedExtensionPath}");
                }

                if (BrowserConfiguration.ShouldDisableJavaScript)
                {
                    args.Add("--blink-settings=scriptEnabled=false");
                }

                launchOptions.Args = args;
                launchOptions.ChromiumSandbox = false;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Launch(launchOptions);
                break;
            case BrowserTypes.Edge:
                launchOptions.Headless = false;
                launchOptions.Channel = "msedge";

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Launch(launchOptions);
                break;
            case BrowserTypes.EdgeHeadless:
                launchOptions.Headless = true;
                launchOptions.Channel = "msedge";

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Launch(launchOptions);
                break;
            case BrowserTypes.Firefox:
                launchOptions.Headless = false;
                args.Add("--acceptInsecureCerts=true");

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Firefox.Launch(launchOptions);
                break;
            case BrowserTypes.FirefoxHeadless:
                launchOptions.Headless = true;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Firefox.Launch(launchOptions);
                break;
            case BrowserTypes.Webkit:
                launchOptions.Headless = false;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Webkit.Launch(launchOptions);
                break;
            case BrowserTypes.WebkitHeadless:
                launchOptions.Headless = true;

                wrappedBrowser.Browser = wrappedBrowser.Playwright.Webkit.Launch(launchOptions);
                break;
        }

        InitializeBrowserContextAndPage(wrappedBrowser);
    }

    internal static void InitializeBrowserContextAndPage(WrappedBrowser wrappedBrowser)
    {
        BrowserNewContextOptions options = new();

        options.JavaScriptEnabled = !BrowserConfiguration.ShouldDisableJavaScript;

        if (wrappedBrowser.CurrentContext != null) wrappedBrowser.CurrentContext.Dispose();

        if (WebSettings.PlaywrightSettings != null && WebSettings.PlaywrightSettings.ContextOptions != null)
        {
            options = WebSettings.PlaywrightSettings.ContextOptions;
        }

        wrappedBrowser.CurrentContext = wrappedBrowser.Browser.NewContext(options);
        wrappedBrowser.CurrentPage = wrappedBrowser.CurrentContext.NewPage();

        AddConsoleMessageListener(wrappedBrowser);
    }

    private static void InitializeWrappedBrowserGridMode(BrowserConfiguration executionConfiguration, WrappedBrowser wrappedBrowser)
    {
        string capabilities;

        if (executionConfiguration.GridOptions != null)
        {
            capabilities = JsonConvert.SerializeObject(executionConfiguration.GridOptions);
        }
        else
        {
            var capsObject = ParseBooleans(WebSettings.ExecutionSettings.Arguments[0]);

            capabilities = JsonConvert.SerializeObject(capsObject);
        }

        capabilities = ResolveSecrets(capabilities);

        switch (executionConfiguration.ExecutionType)
        {
            case ExecutionType.Selenoid:
            case ExecutionType.Grid:
                {
                    var browserType = executionConfiguration.BrowserType;
                    if (browserType is BrowserTypes.Firefox or BrowserTypes.FirefoxHeadless or BrowserTypes.Webkit or BrowserTypes.WebkitHeadless)
                    {
                        throw new NotSupportedException("Playwright supports running in Selenium Grid only Chromium browsers.");
                    }

                    var gridUrl = WebSettings.ExecutionSettings.GridUrl;
                    if (gridUrl is null || gridUrl == string.Empty)
                    {
                        throw new ArgumentException("No grid url found in the config!");
                    }

                    var client = new ApiClientService(gridUrl);

                    // Sending a request to the grid to get the url and to get session id from the response
                    // session id is used to close the connection after the test, if needed.
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

                    wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.ConnectOverCDP(cdpUrl.ToString(), new BrowserTypeConnectOverCDPOptions { Timeout = 10000 });

                    InitializeBrowserContextAndPage(wrappedBrowser);

                    break;
                }
            case ExecutionType.LambdaTest:
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Connect($"wss://cdp.lambdatest.com/playwright?capabilities={HttpUtility.UrlEncode(capabilities)}");

                    InitializeBrowserContextAndPage(wrappedBrowser);

                    break;
                }
            case ExecutionType.BrowserStack:
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    wrappedBrowser.Browser = wrappedBrowser.Playwright.Chromium.Connect($"wss://cdp.browserstack.com/playwright?caps={HttpUtility.UrlEncode(capabilities)}");

                    InitializeBrowserContextAndPage(wrappedBrowser);

                    break;
                }
            default: throw new NotImplementedException("The Execution Type is either not possible or not implemented.");
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

        wrappedBrowser.CurrentContext.OnConsole += (_, msg) => wrappedBrowser.ConsoleMessages.Add(msg);
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

    /// <summary>
    /// This method goes through every key value pair in a collection and if it finds "true" or "false", it replaces them with their boolean counterparts.
    /// </summary>
    /// <param name="obj">Collections of key-value pairs</param>
    private static Dictionary<string, object> ParseBooleans(object obj)
    {
        // Patch for deserialized by Microsoft.Extensions.Configuration in ConfigurationService (Bellatrix.Core.Settings) grid options.
        // From testFrameworkSettings.json, the options for cloud grid execution are deserialized as a Dictionary<string, object?>
        // Microsoft.Extensions.Configuration always tries to parse them as Dictionary<string, string>, 
        // resulting in boolean values being passed to the cloud grid as "True" or "False", which results in an error. 

        // The reason the parameter is an object, not a collection, is because we don't know in exactly what collection the Microsoft.Extensions.Configuration
        // will parse the options from the json file.

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
                newDictionary.Add(key, (ParseBooleans((Dictionary<string, object>)value)));
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

    private static WebSettings WebSettings => ConfigurationService.GetSection<WebSettings>();
}
