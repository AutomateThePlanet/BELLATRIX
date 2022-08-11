using System;

namespace Bellatrix.Web.GettingStarted.Advanced.Elements.Extension.Methods;

public static class NavigationServiceExtensions
{
    // 1. One way to extend the BELLATRIX common services is to create an extension method for the additional action.
    // 1.1. Place it in a static class like this one.
    // 1.2. Create a static method for the action.
    // 1.3. Pass the common service as a parameter with the keyword 'this'.
    // 1.4. Access the native driver via WrappedDriver.
    //
    // Later to use the method in your tests, add a using statement containing this class's namespace.
    public static void NavigateViaJavaScript(this NavigationService navigationService, string url)
    {
        var javaScriptService = ServicesCollection.Current.Resolve<JavaScriptService>();

        if (!navigationService.IsUrlValid(url))
        {
            throw new ArgumentException($"The specified URL- {url} is not in a valid format!");
        }

        javaScriptService.Execute($"window.location.href = '{url}';");
    }

    public static bool IsUrlValid(this NavigationService navigationService, string url)
    {
        bool result = Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;

        return result;
    }
}