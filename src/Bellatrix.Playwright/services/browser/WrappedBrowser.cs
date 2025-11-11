// <copyright file="WrappedBrowser.cs" company="Automate The Planet Ltd.">
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

using Bellatrix.Playwright.SyncPlaywright;

namespace Bellatrix.Playwright.Services.Browser;

public class WrappedBrowser
{
    public WrappedBrowser()
    {
    }

    public WrappedBrowser(BellatrixPlaywright playwright, BellatrixBrowser browser, BrowserContext context, BrowserPage page)
    {
        Playwright = playwright;
        Browser = browser;
        CurrentContext = context;
        CurrentPage = page;
    }

    public string GridSessionId { get; internal set; }
    public BellatrixPlaywright Playwright { get; internal set; }
    public BellatrixBrowser Browser { get; internal set; }
    public BrowserContext CurrentContext { get; internal set; }
    public BrowserPage CurrentPage { get; internal set; }

    public ConsoleMessageStorage ConsoleMessages { get; internal set; }

    public void Quit()
    {
        try
        {
            CurrentPage?.Close();
            CurrentPage = null;

            CurrentContext?.Close();
            CurrentContext = null;

            Browser?.Close();
            Browser = null;

            Playwright?.Dispose();
            Playwright = null;
        }
        catch (Exception ex)
        {
            ex.PrintStackTrace();
        }
    }
}
