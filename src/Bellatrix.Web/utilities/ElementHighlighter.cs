// <copyright file="Elementhighlighter.cs" company="Automate The Planet Ltd.">
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
using System.ComponentModel;
using System.Threading;
using OpenQA.Selenium;

namespace Bellatrix.Web.Extensions.Controls;

public static class Elementhighlighter
{
    public static void Highlight(this IWebElement nativeElement, int waitBeforeUnhighlightMilliseconds = 100, string color = "yellow")
    {
        if (WrappedWebDriverCreateService.BrowserConfiguration.BrowserType == BrowserType.ChromeHeadless || WrappedWebDriverCreateService.BrowserConfiguration.BrowserType == BrowserType.FirefoxHeadless)
        {
            // No need to highlight for headless browsers.
            return;
        }

        try
        {
            var javaScriptService = ServicesCollection.Current.Resolve<JavaScriptService>();
            var originalElementBorder = javaScriptService.Execute("return arguments[0].style.background", nativeElement);
            javaScriptService.Execute($"arguments[0].style.background='{color}'; return;", nativeElement);
            if (waitBeforeUnhighlightMilliseconds >= 0)
            {
                if (waitBeforeUnhighlightMilliseconds > 1000)
                {
                    var backgroundWorker = new BackgroundWorker();
                    backgroundWorker.DoWork += (obj, e) => Unhighlight(nativeElement, originalElementBorder, waitBeforeUnhighlightMilliseconds);
                    backgroundWorker.RunWorkerAsync();
                }
                else
                {
                    Unhighlight(nativeElement, originalElementBorder, waitBeforeUnhighlightMilliseconds);
                }
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private static void Unhighlight(IWebElement nativeElement, string border, int waitBeforeUnhighlightMiliSeconds)
    {
        try
        {
            var javaScriptService = ServicesCollection.Current.Resolve<JavaScriptService>();
            Thread.Sleep(waitBeforeUnhighlightMiliSeconds);
            javaScriptService.Execute("arguments[0].style.background='" + border + "'; return;", nativeElement);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}