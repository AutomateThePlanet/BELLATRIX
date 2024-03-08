// <copyright file="JavaScriptErrorsPlugin.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using System.Collections.ObjectModel;
using Bellatrix.Assertions;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Settings;
using Bellatrix.Plugins;

namespace Bellatrix.Playwright.Plugins.Browser;

public class JavaScriptErrorsPlugin : Plugin
{
    protected override void PreTestCleanup(object sender, PluginEventArgs e)
    {
        WrappedBrowser browser = e.Container.Resolve<WrappedBrowser>();
        bool shouldCheckForJsErrors = ConfigurationService.GetSection<WebSettings>().ShouldCheckForJavaScriptErrors;
        if (browser == null || !shouldCheckForJsErrors)
        {
            return;
        }

        var errorStrings = new List<string>
         {
             "SyntaxError",
             "EvalError",
             "ReferenceError",
             "RangeError",
             "TypeError",
             "URIError",
         };

        var jsErrors = browser.ConsoleMessages.Where(x => errorStrings.Any(e => !string.IsNullOrEmpty(x.Text) && x.Text.Contains(e)));

        if (jsErrors != null && jsErrors.Any())
        {
            Logger.LogWarning($"JavaScript error(s): {Environment.NewLine} {jsErrors.Aggregate(string.Empty, (s, entry) => s + entry.Text)}{Environment.NewLine}");
        }
    }
}