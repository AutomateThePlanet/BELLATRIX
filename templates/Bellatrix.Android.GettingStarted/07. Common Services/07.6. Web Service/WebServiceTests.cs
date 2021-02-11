using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    // 1. To test web apps, you can start Chrome browser using the AndroidWeb attribute.
    [TestClass]
    [AndroidWeb(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    public class WebServiceTests : MSTest.AndroidTest
    {
        // 2. BELLATRIX gives you an interface for easier work with web apps. Using it, you can access most of the features
        // of BELLATRIX web APIs.
        [TestMethod]
        [Ignore]
        public void HtmlSourceContainsShop_When_OpenWebPageWithChrome()
        {
            App.Web.NavigationService.Navigate("http://demos.bellatrix.solutions/");
            Assert.IsTrue(App.Web.BrowserService.HtmlSource.Contains("Shop"));
        }
    }
}