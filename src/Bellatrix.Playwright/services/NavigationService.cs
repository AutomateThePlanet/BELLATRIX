// <copyright file="NavigationService.cs" company="Automate The Planet Ltd.">
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

using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.Events;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Services;
using Bellatrix.Playwright.Settings;
using Bellatrix.Playwright.Settings.Extensions;


namespace Bellatrix.Playwright;

public class NavigationService : WebService
{
    public NavigationService(WrappedBrowser wrappedBrowser)
        : base(wrappedBrowser)
    {
    }

    public static event EventHandler<UrlNotNavigatedEventArgs> UrlNotNavigatedEvent;

    public static event EventHandler<UrlNavigatedEventArgs> UrlNavigatedEvent;

    public void Navigate(Uri uri)
    {
        NavigateInternal(uri.ToString());
    }

    public void Navigate(string url)
    {
        try
        {
           NavigateInternal(url);
        }
        catch (Exception)
        {
            try
            {
                NavigateInternal(url);
            }
            catch (Exception ex)
            {
                UrlNotNavigatedEvent?.Invoke(this, new UrlNotNavigatedEventArgs(ex));
                throw new Exception($"Navigation to page {url} has failed after two attempts. Error was: {ex.Message}");
            }
        }
    }

    public void NavigateToLocalPage(string filePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
        string path = Path.GetDirectoryName(assembly.Location);

        string pageFilePath = Path.Combine(path ?? throw new InvalidOperationException(), filePath);

        if (WrappedBrowserCreateService.BrowserConfiguration.BrowserType.Equals(BrowserChoice.Webkit) || WrappedBrowserCreateService.BrowserConfiguration.BrowserType.Equals(BrowserChoice.Firefox) || WrappedBrowserCreateService.BrowserConfiguration.BrowserType.Equals(BrowserChoice.FirefoxHeadless))
        {
            pageFilePath = string.Concat("file:///", pageFilePath);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            pageFilePath = pageFilePath.Replace('\\', '/').Replace("file:////", "file://////").Replace(" ", "%20");
        }

        if (!WrappedBrowserCreateService.BrowserConfiguration.BrowserType.Equals(BrowserChoice.Webkit))
        {
            Navigate(new Uri(pageFilePath, uriKind: UriKind.Absolute));
        }
        else
        {
            Navigate(pageFilePath);
        }
    }

    public void WaitForPartialUrl(string partialUrl)
    {
        try
        {
            Expect(CurrentPage.WrappedPage).ToHaveURLAsync(new Regex(@$".*{partialUrl}.*"), new() { Timeout = ConfigurationService.GetSection<WebSettings>().TimeoutSettings.InMilliseconds().WaitForPartialUrl });
        }
        catch (Exception ex)
        {
            UrlNotNavigatedEvent?.Invoke(this, new UrlNotNavigatedEventArgs(ex));
            throw;
        }
    }

    public string GetQueryParameter(string parameterName)
    {
        var currentBrowserUrl = CurrentPage.Url;
        Uri uri = new Uri(currentBrowserUrl);

        return HttpUtility.ParseQueryString(uri.Query).Get(parameterName);
    }

    public string SetQueryParameter(string url, string parameterName, string parameterValue)
    {
        Uri uri = new Uri(url);
        NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);
        query.Add(new NameValueCollection() { { parameterName, parameterValue } });
        return uri.GetLeftPart(UriPartial.Path) + "?" + query.ToString();
    }

    private void NavigateInternal(string url)
    {
        _ = CurrentPage.GoTo(url);
        UrlNavigatedEvent?.Invoke(this, new UrlNavigatedEventArgs(url));
    }
}