// <copyright file="ScreenshotEngine.cs" company="Automate The Planet Ltd.">
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

using Bellatrix.Playwright.Services.Browser;
using System.Reflection;

namespace Bellatrix.Playwright.plugins.screenshots;

internal static class ScreenshotEngine
{
    public static string TakeScreenshot(ServicesCollection serviceContainer, bool fullPage)
    {
        var browser = serviceContainer.Resolve<WrappedBrowser>();
        return Convert.ToBase64String(browser.CurrentPage.Screenshot(new PageScreenshotOptions { FullPage = fullPage, Type = ScreenshotType.Png }));
    }

    public static string GetEmbeddedResource(string resourceName, Assembly assembly)
    {
        resourceName = FormatResourceName(assembly, resourceName);
        using var resourceStream = assembly.GetManifestResourceStream(resourceName);
        if (resourceStream == null)
        {
            return null;
        }

        using var reader = new StreamReader(resourceStream);
        return reader.ReadToEnd();
    }

    private static string FormatResourceName(Assembly assembly, string resourceName)
    {
        return assembly.GetName().Name + "." + resourceName.Replace(" ", "_")
                                                           .Replace("\\", ".")
                                                           .Replace("/", ".");
    }
}
