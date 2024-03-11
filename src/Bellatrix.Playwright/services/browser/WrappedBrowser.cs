// <copyright file="WrappedBrowser.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Playwright.Services.Browser;

public class WrappedBrowser
{
    public WrappedBrowser()
    {
    }

    public WrappedBrowser(IPlaywright playwright, IBrowser browser, IBrowserContext context, IPage page)
    {
        Playwright = playwright;
        Browser = browser;
        CurrentContext = context;
        CurrentPage = page;
    }

    public string GridSessionId { get; internal set; }
    public IPlaywright Playwright { get; internal set; }
    public IBrowser Browser { get; internal set; }
    public IBrowserContext CurrentContext { get; internal set; }
    public IPage CurrentPage { get; internal set; }

    public ConsoleMessageStorage ConsoleMessages { get; internal set; }

    public void Quit()
    {
        try
        {
            CurrentPage?.CloseAsync().GetAwaiter().GetResult();
            CurrentPage = null;

            CurrentContext?.CloseAsync().GetAwaiter().GetResult();
            CurrentContext = null;

            Browser?.CloseAsync().GetAwaiter().GetResult();
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
