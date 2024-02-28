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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using Bellatrix.Assertions;
using Bellatrix.Web.Events;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Bellatrix.Web;

public class NavigationService : WebService
{
    public NavigationService(IWebDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public static event EventHandler<UrlNotNavigatedEventArgs> UrlNotNavigatedEvent;

    public static event EventHandler<UrlNavigatedEventArgs> UrlNavigatedEvent;

    public void Navigate(Uri uri)
    {
        WrappedDriver.Navigate().GoToUrl(uri);
        UrlNavigatedEvent?.Invoke(this, new UrlNavigatedEventArgs(uri.ToString()));
    }

    public void Navigate(string url)
    {
        bool tryAgain = false;
        try
        {
            WrappedDriver.Navigate().GoToUrl(url);
            UrlNavigatedEvent?.Invoke(this, new UrlNavigatedEventArgs(url));
        }
        catch (Exception)
        {
            tryAgain = true;
        }

        if (tryAgain)
        {
            try
            {
                WrappedDriver.Navigate().GoToUrl(url);
                UrlNavigatedEvent?.Invoke(this, new UrlNavigatedEventArgs(url));
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

        if (WrappedWebDriverCreateService.BrowserConfiguration.BrowserType.Equals(BrowserType.Safari) || WrappedWebDriverCreateService.BrowserConfiguration.BrowserType.Equals(BrowserType.Firefox) || WrappedWebDriverCreateService.BrowserConfiguration.BrowserType.Equals(BrowserType.FirefoxHeadless))
        {
            pageFilePath = string.Concat("file:///", pageFilePath);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            pageFilePath = pageFilePath.Replace('\\', '/').Replace("file:////", "file://////").Replace(" ", "%20");
        }

        if (!WrappedWebDriverCreateService.BrowserConfiguration.BrowserType.Equals(BrowserType.Safari))
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
            var wait = new WebDriverWait(WrappedDriver, TimeSpan.FromSeconds(ConfigurationService.GetSection<WebSettings>().TimeoutSettings.WaitForPartialUrl));
            wait.Until((d) => WrappedDriver.Url.ToLower().Contains(partialUrl.ToLower()));
        }
        catch (Exception ex)
        {
            UrlNotNavigatedEvent?.Invoke(this, new UrlNotNavigatedEventArgs(ex));
            throw;
        }
    }

    public string GetQueryParameter(string parameterName)
    {
        var currentBrowserUrl = WrappedDriver.Url;
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
}