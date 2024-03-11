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
using Microsoft.VisualStudio.Services.WebApi;

namespace Bellatrix.Playwright;

public class NativeElementFinderService : IWebElementFinderService
{
    private readonly dynamic _searchContext;

    public NativeElementFinderService(WrappedBrowser searchContext) => _searchContext = searchContext.CurrentPage;

    public NativeElementFinderService(IPage searchContext) => _searchContext = searchContext;

    public NativeElementFinderService(ILocator searchContext) => _searchContext = searchContext;

    public ILocator Find<TBy>(TBy by)
        where TBy : FindStrategy
    {
        var element = ((ILocator)by.Convert(_searchContext)).First;

        return element;
    }

    public IEnumerable<ILocator> FindAll<TBy>(TBy by)
        where TBy : FindStrategy
    {
        var result = ((ILocator)by.Convert(_searchContext));
        try
        {
            return result.AllAsync().Result;
        } 
        catch
        {
            // patch
            ServicesCollection.Current.Resolve<WrappedBrowser>().CurrentPage.WaitForLoadStateAsync(LoadState.NetworkIdle).SyncResult();
            return result.AllAsync().Result;
        }
    }
}