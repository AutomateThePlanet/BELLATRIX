// <copyright file="AssertedNavigatablePage.cs" company="Automate The Planet Ltd.">
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
using System.Web;
using Bellatrix.Assertions;
using Bellatrix.CognitiveServices;
using Bellatrix.DynamicTestCases;
using Bellatrix.Web.Proxy;
using OpenQA.Selenium.Support.UI;

namespace Bellatrix.Web;

public abstract class WebPage
{
    public App App => ServicesCollection.Current.Resolve<App>();

    public abstract string Url { get; }

    public virtual void Open() => App.Navigation.Navigate(new Uri(Url));

    public void AssertLandedOnPage(string partialUrl, bool shouldUrlEncode = false)
    {
        if (shouldUrlEncode)
        {
            partialUrl = HttpUtility.UrlEncode(partialUrl);
        }

        App.Browser.WaitUntilReady();

        var currentBrowserUrl = App.Browser.Url.ToString();

        App.Assert.IsTrue(currentBrowserUrl.ToLower().Contains(partialUrl.ToLower()), $"The expected partialUrl: '{partialUrl}' was not found in the PageUrl: '{currentBrowserUrl}'");
    }

    public void AssertNotLandedOnPage(string partialUrl, bool shouldUrlEncode = false)
    {
        if (shouldUrlEncode)
        {
            partialUrl = HttpUtility.UrlEncode(partialUrl);
        }

        var currentBrowserUrl = App.Navigation.WrappedDriver.Url.ToString();
        App.Assert.IsFalse(currentBrowserUrl.Contains(partialUrl), $"The expected partialUrl: '{partialUrl}' was found in the PageUrl: '{currentBrowserUrl}'");
    }

    public void AssertUrl(string fullUrl)
    {
        var currentBrowserUrl = App.Browser.Url.ToString();
        Uri actualUri = new Uri(currentBrowserUrl);
        Uri expectedUri = new Uri(fullUrl);

        App.Assert.AreEqual(expectedUri.AbsoluteUri, actualUri.AbsoluteUri, $"Expected URL is different than the Actual one.");
    }

    public void AssertUrlPath(string urlPath)
    {
        var currentBrowserUrl = App.Navigation.WrappedDriver.Url.ToString();
        Uri actualUri = new Uri(currentBrowserUrl);

        App.Assert.AreEqual(urlPath, actualUri.AbsolutePath, $"Expected URL path is different than the Actual one.");
    }

    public virtual void AssertUrlPathAndQuery(string pathAndQuery)
    {
        var currentBrowserUrl = App.Navigation.WrappedDriver.Url.ToString();
        Uri actualUri = new Uri(currentBrowserUrl);

        App.Assert.AreEqual(pathAndQuery, actualUri.PathAndQuery, $"Expected URL is different than the Actual one.");
    }
}