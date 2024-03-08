// <copyright file="ProxyService.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Bellatrix.Assertions;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Services;
using Bellatrix.Playwright.Settings;
using System.Threading;

namespace Bellatrix.Playwright.Proxy;

public class ProxyService : WebService
{
    private readonly ConcurrentDictionary<string, string> _redirectUrls;
    private readonly ConcurrentBag<string> _blockUrls;

    public ProxyService(WrappedBrowser wrappedBrowser)
        : base(wrappedBrowser)
    {
        IsEnabled = ConfigurationService.GetSection<WebSettings>().ShouldCaptureHttpTraffic;

        _redirectUrls = new ConcurrentDictionary<string, string>();
        _blockUrls = new ConcurrentBag<string>();
        RequestsHistory = new ConcurrentDictionary<int, IRequest>();
        ResponsesHistory = new ConcurrentDictionary<int, IResponse>();
    }

    //public ProxyServer ProxyServer { get; set; }

    public ConcurrentDictionary<int, IRequest> RequestsHistory { get; set; }

    public ConcurrentDictionary<int, IResponse> ResponsesHistory { get; set; }

    public bool IsEnabled { get; set; }

    public int Port { get; set; }

    public void Start()
    {
        if (IsEnabled)
        {
            Console.WriteLine($"Start proxy on port {Port}"); 
            Console.WriteLine($"PROXY STARTED");
            OnRequestCaptureTraffic();
            OnResponseCaptureTraffic();
            OnRequestBlockResource();
            OnRequestRedirectTraffic();
        }
    }

    public void SetUrlToBeRedirectedTo(string originalUrl, string redirectUrl)
    {
        ShouldExecute();
        if (string.IsNullOrEmpty(originalUrl))
        {
            throw new ArgumentException("The original URL cannot be null or empty. Please specify a valid URL.");
        }

        if (string.IsNullOrEmpty(redirectUrl))
        {
            throw new ArgumentException("The redirect URL cannot be null or empty. Please specify a valid URL.");
        }

        _redirectUrls.GetOrAdd(originalUrl, redirectUrl);
    }

    public void ClearAllRedirectUrlPairs()
    {
        ShouldExecute();
        _redirectUrls.Clear();
    }

    public void SetUrlToBeBlocked(string url)
    {
        ShouldExecute();
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentException("The URL cannot be null or empty. Please specify a valid URL.");
        }

        _blockUrls.Add(url);
    }

    public void ClearAllBlockUrls()
    {
        ShouldExecute();
        while (_blockUrls.TryTake(out _) && !_blockUrls.IsEmpty)
        {
        }
    }

    public void AssertNoErrorCodes()
    {
        ShouldExecute();
        bool areThereErrorCodes = ResponsesHistory.Values.Any(r => r.Status > 400 && r.Status < 599);
        Assert.IsFalse(areThereErrorCodes, "Error codes detected on the page.");
    }

    public void AssertRequestMade(string url)
    {
        ShouldExecute();
        bool areRequestsMade = RequestsHistory.Values.ToList().Any(r => r.Url.Contains(url));
        Assert.IsTrue(areRequestsMade, $"Request {url} was not made.");
    }

    public void AssertRequestNotMade(string url)
    {
        ShouldExecute();
        bool areRequestsMade = RequestsHistory.Values.ToList().Any(r => r.Url.Contains(url));
        Assert.IsFalse(areRequestsMade, $"Request {url} was made.");
    }

    public void AssertNoLargeImagesRequested(int contentLength = 40000)
    {
        ShouldExecute();
        bool areThereLargeImages = RequestsHistory.Values.Any(r => r.ResourceType != null && r.ResourceType.StartsWith("image", StringComparison.Ordinal) && r.SizesAsync().Result.RequestBodySize < contentLength);
        Assert.IsFalse(areThereLargeImages, $"Larger than {contentLength} images detected.");
    }

    private int GetFreeTcpPort()
    {
        Thread.Sleep(100);
        var l = new TcpListener(IPAddress.Loopback, 0);
        l.Start();
        int port = ((IPEndPoint)l.LocalEndpoint).Port;
        l.Stop();
        return port;
    }

    private void OnRequestCaptureTraffic()
    {
        Console.WriteLine($"call to OnRequestCaptureTrafficEventHandler");
        CurrentContext.Request += (_, request) => RequestsHistory.GetOrAdd(request.GetHashCode(), request);
    }

    private void OnRequestBlockResource()
    {
        if (!_blockUrls.IsEmpty)
        {
            foreach (var urlToBeBlocked in _blockUrls)
            {
                CurrentContext.RouteAsync(urlToBeBlocked, (route) =>
                {
                    route.ContinueAsync(new RouteContinueOptions
                    {
                        PostData = null
                    });
                });
            }
        }
    }

    private void OnRequestRedirectTraffic()
    {
        if (_redirectUrls.Keys.Count > 0)
        {
            foreach (var redirectUrlPair in _redirectUrls)
            {
                CurrentContext.RouteAsync(redirectUrlPair.Key, (route) =>
                {
                    route.ContinueAsync(new RouteContinueOptions
                    {
                        Url = redirectUrlPair.Value
                    });
                });
            }
        }
    }

    private void OnResponseCaptureTraffic()
    {
        CurrentContext.Response += (_, response) =>
        {
            ResponsesHistory.GetOrAdd(response.GetHashCode(), response);
        };
    }

    public void ShouldExecute()
    {
        if (!IsEnabled)
        {
            throw new ArgumentException("ProxyService is not enabled. To use open testFramework.json and set isEnabled = true of webProxySettings");
        }
    }

    //private Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
    //{
    //    if (e.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
    //    {
    //        e.IsValid = true;
    //    }

    //    return Task.FromResult(0);
    //}

    //private Task OnCertificateSelection(object sender, CertificateSelectionEventArgs e)
    //{
    //    return Task.FromResult(0);
    //}
}