using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

// 1. To test web apps, you can start Chrome browser using the AndroidWeb attribute.
[TestFixture]
[AndroidWeb(Constants.AndroidNativeAppPath,
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class WebServiceTests : NUnit.AndroidTest
{
    // 2. BELLATRIX gives you an interface for easier work with web apps. Using it, you can access most of the features
    // of BELLATRIX web APIs.
    [Test]
    [Ignore("API example purposes only. No need to run.")]
    public void HtmlSourceContainsShop_When_OpenWebPageWithChrome()
    {
        App.Web.NavigationService.Navigate("https://demos.bellatrix.solutions/");
        Assert.That(App.Web.BrowserService.HtmlSource.Contains("Shop"));
    }
}