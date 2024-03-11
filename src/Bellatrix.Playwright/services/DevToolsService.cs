// <copyright file="DevToolsService.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Assertions;
using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Services;
using Microsoft.VisualStudio.Services.Common;
using System.Text.RegularExpressions;

namespace Bellatrix.Playwright;

public class DevToolsService : WebService
{
    public DevToolsService(WrappedBrowser wrappedBrowser)
        : base(wrappedBrowser)
    {
        var browserType = WrappedBrowserCreateService.BrowserConfiguration.BrowserType;
        if (browserType == BrowserChoice.Chromium || browserType == BrowserChoice.ChromiumHeadless || browserType == BrowserChoice.Chrome || browserType == BrowserChoice.ChromeHeadless || browserType == BrowserChoice.Edge || browserType == BrowserChoice.EdgeHeadless)
        {
            DevToolsSession = wrappedBrowser.CurrentContext.NewCDPSessionAsync(wrappedBrowser.CurrentPage).Result;
        }
        //DevToolsSessionDomains = DevToolsSession.GetVersionSpecificDomains<DevToolsSessionDomains>();
        
    }

    //public DevToolsSessionDomains DevToolsSessionDomains { get; set; }
    public ICDPSession DevToolsSession { get; set; }
    public List<IRequest> RequestsHistory { get; set; }
    public List<IResponse> ResponsesHistory { get; set; }

    public void StartNetworkTrafficMonitoring()
    {
        RequestsHistory = new();
        ResponsesHistory = new();

        CurrentContext.Request += (_, request) =>
        {
            RequestsHistory.Add(request);
        };

        CurrentContext.Response += (_, response) =>
        {
            ResponsesHistory.Add(response);
        };
    }

    public void ClearNetworkTrafficHistory()
    {
        RequestsHistory.Clear();
        ResponsesHistory.Clear();
    }

    public List<string> GetSpecificRequestUrls(string requestName)
    {
        return RequestsHistory.ToList()
            .FindAll(r => r.Url.ToString().Contains(requestName))
            .Select(fr => fr.Url).ToList();
    }

    public void OverrideScreenResolution(long screenHeight, long screenWidth, bool mobile, double deviceScaleFactor)
    {
        //var settings = new SetDeviceMetricsOverrideCommandSettings();
        //settings.ScreenHeight = screenHeight;
        //settings.ScreenWidth = screenWidth;
        //settings.Mobile = mobile;
        //settings.DeviceScaleFactor = deviceScaleFactor;

        //DevToolsSession.SendCommand(settings);
    }

    public long GetResponseContentLengthByPartialUrl(string partialUrl)
    {
        var contentLength = ResponsesHistory.ToList().Find(r => r.Url.Contains(partialUrl)).Headers["content-length"].ToString();

        return contentLength.ToLong();
    }

    public string GetResponseContentTypeByPartialUrl(string partialUrl)
    {
        var contentType = ResponsesHistory.ToList().Find(r => r.Url.Contains(partialUrl)).Headers["content-type"].ToString();

        return contentType;
    }

    public void AssertResponse404ErrorCodeRecievedByPartialUrl(string partialUrl)
    {
        var responseStatusCode = ResponsesHistory.ToList().Find(r => r.Url.Contains(partialUrl)).Status;

        Assert.AreEqual(responseStatusCode, 404, "404 Error code not detected on the page.");
    }

    public void AssertNoErrorCodes()
    {
        bool hasErrorCode = ResponsesHistory.Any(r => r.Status > 400 && r.Status < 599);

        Assert.IsFalse(hasErrorCode, "Error codes detected on the page.");
    }

    public void AssertRequestMade(string url)
    {
        bool isRequestMade = ResponsesHistory.Any(r => r.Url.Contains(url));

        Assert.IsTrue(isRequestMade, $"Request {url} was not made.");
    }

    public void AssertRequestNotMade(string url)
    {
        bool areRequestsMade = ResponsesHistory.Any(r => r.Url.Contains(url));

        Assert.IsFalse(areRequestsMade, $"Request {url} was made.");
    }

    public int CountRequestsMadeByFileFormat(string fileFormat)
    {
        var responsesList = ResponsesHistory.ToList().FindAll(r => r.Url.EndsWith(fileFormat)).ToList();

        var numberOfResponses = responsesList.Count;

        return numberOfResponses;
    }

    public void DisableCache()
    {
        //DevToolsSessionDomains.Network.Disable();
    }

    //public async Task<DocumentSnapshot[]> CaptureSnapshot()
    //{
    //    var result = await DevToolsSessionDomains.DOMSnapshot.CaptureSnapshot(new CaptureSnapshotCommandSettings()
    //    {
    //        ComputedStyles = new string[] { "background-color", "color", "font-weight", "font-family", "display" },
    //        IncludeBlendedBackgroundColors = true,
    //        IncludePaintOrder = true,
    //        IncludeDOMRects = true,
    //        IncludeTextColorOpacities = true,
    //    });
    //    return result.Documents;
    //}

    public void AddExtraHttpHeader(string header, string value)
    {
        var headers = new KeyValuePair<string, string>[1];
        headers.Add(header, value);
        CurrentContext.SetExtraHTTPHeadersAsync(headers).GetAwaiter().GetResult();
    }

    //public void OverrideUserAgent(string userAgent)
    //{
    //    var settings = new SetUserAgentOverrideCommandSettings();
    //    settings.UserAgent = userAgent;

    //    DevToolsSession.SendCommand(settings);
    //}

    public void OverrideGeolocationSettings(double latitude, double longitude, int accuracy)
    {
        CurrentContext.SetGeolocationAsync(new() { Latitude = (float)latitude, Longitude = (float)longitude, Accuracy = accuracy }).GetAwaiter().GetResult();
    }

    public void OverrideDeviceMetrics(long width, long height, bool mobile, double deviceScaleFactor)
    {
        //var settings = new SetDeviceMetricsOverrideCommandSettings();
        //settings.Width = width;
        //settings.Height = height;
        //settings.Mobile = mobile;
        //settings.DeviceScaleFactor = deviceScaleFactor;

        //DevToolsSession.SendCommand(settings);
    }

    public void IgnoreCertificateError()
    {
        //var settings = new SetIgnoreCertificateErrorsCommandSettings();
        //settings.Ignore = true;

        //DevToolsSession.SendCommand(settings);
    }

    public void BlockUrls(string pattern)
    {
       CurrentContext.RouteAsync(new Regex(pattern), route => route.AbortAsync()).GetAwaiter().GetResult();
    }

    public void EmulateNetworkConditionOffline()
    {
        CurrentContext.SetOfflineAsync(true).GetAwaiter().GetResult();
    }

    //public async Task EmulateNetworkConditions(ConnectionType connectionType, int downloadThroughput, double latency, int uploadThroughput)
    //{
    //    await DevToolsSessionDomains.Network.Enable(new EnableCommandSettings()
    //    {
    //        MaxTotalBufferSize = 100000000
    //    });

    //    await DevToolsSessionDomains.Network.EmulateNetworkConditions(new EmulateNetworkConditionsCommandSettings()
    //    {
    //        ConnectionType = connectionType,
    //        DownloadThroughput = downloadThroughput,
    //        Latency = latency,
    //        UploadThroughput = uploadThroughput,
    //    });
    //}

    //public async Task ListenConsoleLogs(EventHandler<MessageAddedEventArgs> messageAddedHandler)
    //{
    //    DevToolsSessionDomains.Console.MessageAdded += messageAddedHandler;
    //    await DevToolsSessionDomains.Console.Enable();
    //}

    //public async Task ListenJavaScriptConsoleLogs(EventHandler<JavaScriptConsoleApiCalledEventArgs> javaScriptConsoleApiCalled)
    //{
    //    IJavaScriptEngine monitor = new JavaScriptEngine(WrappedDriver);
    //    monitor.JavaScriptConsoleApiCalled += javaScriptConsoleApiCalled;
    //    await monitor.StartEventMonitoring();
    //}

    //public async Task ListenJavaScriptExceptionsThrown(EventHandler<JavaScriptExceptionThrownEventArgs> javaScriptExceptionThrown)
    //{
    //    IJavaScriptEngine monitor = new JavaScriptEngine(WrappedDriver);
    //    monitor.JavaScriptExceptionThrown += javaScriptExceptionThrown;
    //    await monitor.StartEventMonitoring();
    //}

    //public async Task AddInitializationScript(string name, string script)
    //{
    //    IJavaScriptEngine monitor = new JavaScriptEngine(WrappedDriver);
    //    await monitor.AddInitializationScript(name, script);
    //}

    //public async Task TurnOnPerformanceMetrics()
    //{
    //    var enableCommand = new EnableCommandSettings();
    //    await DevToolsSession.SendCommand(enableCommand);           
    //}

    //public async Task<Metric[]> GetPerformanceMetrics()
    //{
    //    var metricsResponse = await DevToolsSession.SendCommand<GetMetricsCommandSettings, GetMetricsCommandResponse>(new GetMetricsCommandSettings());
    //    return metricsResponse.Metrics;
    //}
}
