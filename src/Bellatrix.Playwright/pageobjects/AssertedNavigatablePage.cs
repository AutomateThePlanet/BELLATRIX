// <copyright file="AssertedNavigatablePage.cs" company="Automate The Planet Ltd.">
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

using System.Web;
using Bellatrix.Assertions;

namespace Bellatrix.Playwright;

[Obsolete("Please refactor your pages to use the new WebPage base class which combies the old 4 base classes.")]
public abstract class AssertedNavigatablePage : NavigatablePage
{
    protected AssertedNavigatablePage() => Assert = ServicesCollection.Current.Resolve<IAssert>();

    protected IAssert Assert { get; }

    public void AssertLandedOnPage(string partialUrl, bool shouldUrlEncode = false)
    {
        if (shouldUrlEncode)
        {
            partialUrl = HttpUtility.UrlEncode(partialUrl);
        }

        Browser.WaitUntilReady();

        var currentBrowserUrl = Browser.Url.ToString().ToLower();

        Assert.IsTrue(currentBrowserUrl.Contains(partialUrl.ToLower()), $"The expected partialUrl: '{partialUrl}' was not found in the PageUrl: '{currentBrowserUrl}'");
    }

    public void AssertNotLandedOnPage(string partialUrl, bool shouldUrlEncode = false)
    {
        if (shouldUrlEncode)
        {
            partialUrl = HttpUtility.UrlEncode(partialUrl);
        }

        var currentBrowserUrl = NavigationService.WrappedBrowser.CurrentPage.Url;
        Assert.IsFalse(currentBrowserUrl.Contains(partialUrl), $"The expected partialUrl: '{partialUrl}' was found in the PageUrl: '{currentBrowserUrl}'");
    }

    public void AssertUrl(string fullUrl)
    {
        var currentBrowserUrl = Browser.Url.ToString();
        Uri actualUri = new Uri(currentBrowserUrl);
        Uri expectedUri = new Uri(fullUrl);

        Assert.AreEqual(expectedUri.AbsoluteUri, actualUri.AbsoluteUri, $"Expected URL is different than the Actual one.");
    }

    public void AssertUrlPath(string urlPath)
    {
        var currentBrowserUrl = NavigationService.WrappedBrowser.CurrentPage.Url;
        Uri actualUri = new Uri(currentBrowserUrl);

        Assert.AreEqual(urlPath, actualUri.AbsolutePath, $"Expected URL path is different than the Actual one.");
    }

    public void AssertUrlPathAndQuery(string pathAndQuery)
    {
        var currentBrowserUrl = NavigationService.WrappedBrowser.CurrentPage.Url;
        Uri actualUri = new Uri(currentBrowserUrl);

        Assert.AreEqual(pathAndQuery, actualUri.PathAndQuery, $"Expected URL is different than the Actual one.");
    }
}