// <copyright file="BrowserPageAssertions.cs" company="Automate The Planet Ltd.">
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

using System.Text.RegularExpressions;

namespace Bellatrix.Playwright.SyncPlaywright;

public class BrowserPageAssertions
{
    public BrowserPageAssertions(BrowserPage page)
    {
        Page = page;
        NativeAssertions = Expect(page.WrappedPage);
    }

    public BrowserPage Page { get; init; }
    public IPageAssertions NativeAssertions { get; init; }

    public BrowserPageAssertions Not
    {
        get
        {
            _ = NativeAssertions.Not;
            return this;
        }
    }

    public void ToHaveTitle(string title, PageAssertionsToHaveTitleOptions options = null)
    {
        NativeAssertions.ToHaveTitleAsync(title, options);
    }

    public void ToHaveTitle(Regex regExp, PageAssertionsToHaveTitleOptions options = null)
    {
        NativeAssertions.ToHaveTitleAsync(regExp, options);
    }

    public void ToHaveURL(string url, PageAssertionsToHaveURLOptions options = null)
    {
        NativeAssertions.ToHaveURLAsync(url, options);
    }

    public void ToHaveURL(Regex regExp, PageAssertionsToHaveURLOptions options = null)
    {
        NativeAssertions.ToHaveURLAsync(regExp, options);
    }
}
