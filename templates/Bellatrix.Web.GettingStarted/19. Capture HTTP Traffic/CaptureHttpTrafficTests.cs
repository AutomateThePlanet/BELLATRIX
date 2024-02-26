using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

// 1. Capture HTTP traffic is one of the most requested features for WebDriver.
// However by design WebDriver does not include such feature. Happily, for you, we added it to Bellatrix.
// To turn it on you need open the testFrameworkSettings.json file and set the shouldCaptureHttpTraffic to true.
// "webSettings": {
//    "isParallelExecutionEnabled": "false",
//    "artificialDelayBeforeAction": "0",
//    "automaticallyScrollToVisible": "false",
//    "waitUntilReadyOnElementFound": "false",
//    "waitForAngular": "false",
//    "shouldHighlightElements": "true",
//    "shouldCaptureHttpTraffic": "false",
//
// By default, the proxy is not used in your tests even if it is enabled.
// You need to set the shouldCaptureHttpTraffic to true in the Browser attribute.
// After that, each request and response made by the browser is captured, and you have
// the option to modify it or make assertions against it.
[TestFixture]
[Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime, shouldCaptureHttpTraffic: true)]
[Browser(OS.OSX, BrowserType.Chrome, Lifecycle.RestartEveryTime, shouldCaptureHttpTraffic: false)]
public class CaptureHttpTrafficTests : NUnit.WebTest
{
    [Test]
    [Category(Categories.CI)]
    [Category(Categories.Windows)]
    public void CaptureTrafficTests()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        Select sortDropDown = App.Components.CreateByNameEndingWith<Select>("orderby");
        Anchor protonMReadMoreButton = App.Components.CreateByInnerTextContaining<Anchor>("Read more");
        Anchor addToCartFalcon9 = App.Components.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
        Anchor viewCartButton = App.Components.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();

        sortDropDown.SelectByText("Sort by price: low to high");
        protonMReadMoreButton.Hover();
        addToCartFalcon9.Focus();
        addToCartFalcon9.Click();
        viewCartButton.Click();

        // 2. You can access the proxy through the BELLATRIX App service.
        // The proxy service includes several useful assert methods.
        // The first one asserts that no error codes are present in the requests.
        // This way we can catch problems with not loaded images or CSS files.
        App.Proxy.AssertNoErrorCodes();

        // Make sure that our images size is optimised.
        App.Proxy.AssertNoLargeImagesRequested();

        // Check if some specific request is made.
        App.Proxy.AssertRequestMade("https://demos.bellatrix.solutions/wp-content/uploads/2018/04/cropped-bellatrix-logo.png");
    }

    [Test]
    [Category(Categories.CI)]
    [Category(Categories.Windows)]
    public void RedirectRequestsTest()
    {
        // 3. You can set various URLs to be redirected. This is useful if you do not have
        // access to production code and want to use a mock service instead.
        App.Proxy.SetUrlToBeRedirectedTo("http://demos.bellatrix.sokilmlutions/favicon.ico", "https://www.automatetheplanet.com/wp-content/uploads/2016/12/logo.svg");

        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        Select sortDropDown = App.Components.CreateByNameEndingWith<Select>("orderby");
        Anchor protonMReadMoreButton = App.Components.CreateByInnerTextContaining<Anchor>("Read more");
        Anchor addToCartFalcon9 = App.Components.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
        Anchor viewCartButton = App.Components.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();

        sortDropDown.SelectByText("Sort by price: low to high");
        protonMReadMoreButton.Hover();
        addToCartFalcon9.Focus();
        addToCartFalcon9.Click();
        viewCartButton.Click();
    }

    [Test]
    [Category(Categories.CI)]
    [Category(Categories.Windows)]
    public void BlockRequestsTest()
    {
        // 4. To make web pages load faster, you can block some not required services- for example
        // analytics scripts, you do not need them in test environments.
        App.Proxy.SetUrlToBeBlocked("https://demos.bellatrix.solutions/favicon.ico");

        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        Select sortDropDown = App.Components.CreateByNameEndingWith<Select>("orderby");
        Anchor protonMReadMoreButton = App.Components.CreateByInnerTextContaining<Anchor>("Read more");
        Anchor addToCartFalcon9 = App.Components.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
        Anchor viewCartButton = App.Components.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();

        sortDropDown.SelectByText("Sort by price: low to high");
        protonMReadMoreButton.Hover();
        addToCartFalcon9.Focus();
        addToCartFalcon9.Click();
        viewCartButton.Click();

        // Check that no request is made to specific URL.
        App.Proxy.AssertRequestNotMade("https://demos.bellatrix.solutions/welcome");
    }
}