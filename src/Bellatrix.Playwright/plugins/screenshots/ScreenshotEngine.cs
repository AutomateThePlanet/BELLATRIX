using Bellatrix.Playwright.Services.Browser;
using System.Reflection;

namespace Bellatrix.Playwright.plugins.screenshots;

internal static class ScreenshotEngine
{
    public static string TakeScreenshot(ServicesCollection serviceContainer, bool fullPage)
    {
        var browser = serviceContainer.Resolve<WrappedBrowser>();
        return Convert.ToBase64String(browser.CurrentPage.ScreenshotAsync(new PageScreenshotOptions { FullPage = fullPage, Type = ScreenshotType.Png }).Result);
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
