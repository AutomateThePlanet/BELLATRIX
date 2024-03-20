// <copyright file="ElementHighlighter.cs" company="Automate The Planet Ltd.">
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

using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.Services;
using Bellatrix.Playwright.SyncPlaywright;
using System.ComponentModel;
using System.Threading;


namespace Bellatrix.Playwright.Extensions.Controls;

public static class ElementHighlighter
{
    public static void Highlight(this WebElement webElement, int waitBeforeUnhighlightMilliseconds = 100, string color = "yellow")
    {
        if (WrappedBrowserCreateService.BrowserConfiguration.BrowserType == BrowserChoice.ChromeHeadless || WrappedBrowserCreateService.BrowserConfiguration.BrowserType == BrowserChoice.FirefoxHeadless)
        {
            // No need to highlight for headless browsers.
            return;
        }

        try
        {
            var javaScriptService = ServicesCollection.Current.Resolve<JavaScriptService>();
            var originalElementBorder = javaScriptService.Execute("el => window.getComputedStyle(el).getPropertyValue('background')", webElement);
            javaScriptService.Execute($"el => el.style.background='{color}';", webElement);
            if (waitBeforeUnhighlightMilliseconds >= 0)
            {
                if (waitBeforeUnhighlightMilliseconds > 1000)
                {
                    var backgroundWorker = new BackgroundWorker();
                    backgroundWorker.DoWork += (obj, e) => Unhighlight(webElement, originalElementBorder, waitBeforeUnhighlightMilliseconds);
                    backgroundWorker.RunWorkerAsync();
                }
                else
                {
                    Unhighlight(webElement, originalElementBorder, waitBeforeUnhighlightMilliseconds);
                }
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private static void Unhighlight(WebElement nativeElement, string border, int waitBeforeUnhighlightMiliSeconds)
    {
        try
        {
            var javaScriptService = ServicesCollection.Current.Resolve<JavaScriptService>();
            Thread.Sleep(waitBeforeUnhighlightMiliSeconds);
            javaScriptService.Execute($"el => el.style.background='{border}';", nativeElement);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}