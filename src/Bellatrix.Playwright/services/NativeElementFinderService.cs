// <copyright file="NativeElementFinderService.cs" company="Automate The Planet Ltd.">
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

using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.SyncPlaywright;

namespace Bellatrix.Playwright;

public class NativeElementFinderService : IWebElementFinderService
{
    private readonly dynamic _searchContext;

    public NativeElementFinderService(WrappedBrowser searchContext) => _searchContext = searchContext.CurrentPage;

    public NativeElementFinderService(BrowserPage searchContext) => _searchContext = searchContext;

    public NativeElementFinderService(WebElement searchContext) => _searchContext = searchContext;

    public WebElement Find<TBy>(TBy by, int index = 0)
        where TBy : FindStrategy
    {
        dynamic element = by.Resolve(_searchContext).Nth(index);

        return element;
    }

    public IEnumerable<WebElement> FindAll<TBy>(TBy by)
        where TBy : FindStrategy
    {
        var result = (WebElement)by.Resolve(_searchContext);

        return result.All();
    }
}