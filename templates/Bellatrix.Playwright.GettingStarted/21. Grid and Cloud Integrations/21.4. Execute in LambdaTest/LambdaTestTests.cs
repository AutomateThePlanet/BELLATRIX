using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.Settings;
using NUnit.Framework;

namespace Bellatrix.Playwright.GettingStarted;

[TestFixture]

// 1. To execute BELLATRIX tests in LambdaTest cloud you should use the LambdaTest attribute instead of Browser.
// LambdaTest has the same parameters as Browser but adds to additional ones-
// browser version, platform type, recordVideo and recordScreenshots.
// As with the Browser attribute you can override the class behavior on Test level.
[LambdaTest(BrowserChoice.Chrome,
    "93",
    "Windows",
    DesktopWindowSize._1280_800,
    geoLocation: "BE",
    Lifecycle.ReuseIfStarted,
    recordScreenshots: true,
    recordVideo: true)]
public class LambdaTestTests : NUnit.WebTest
{
    [Test]
    [Ignore("no need to run")]
    public void PromotionsPageOpened_When_PromotionsButtonClicked()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        var promotionsLink = App.Components.CreateByLinkText<Anchor>("Promotions");

        promotionsLink.Click();
    }

    // 2. As mentioned if you use the SauceLabs attribute on method level it overrides the class settings.
    // As you can see with the SauceLabs attribute we can change the browser window size again.
    [Test]
    [LambdaTest(BrowserChoice.Chrome, "62", "Windows", DesktopWindowSize._1280_1024, "BE", Lifecycle.ReuseIfStarted)]
    [Ignore("no need to run")]

    // [LambdaTest(BrowserChoice.Chrome, "62", "Windows", 1000, 500, "BE", Lifecycle.ReuseIfStarted)]
    // [LambdaTest(BrowserChoice.Chrome, "62", "Windows", MobileWindowSize._320_568, "BE", Lifecycle.ReuseIfStarted)]
    // [LambdaTest(BrowserChoice.Chrome, "62", "Windows", TabletWindowSize._600_1024, "BE", Lifecycle.ReuseIfStarted)]
    public void BlogPageOpened_When_PromotionsButtonClicked()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        var blogLink = App.Components.CreateByLinkText<Anchor>("Blog");

        blogLink.Click();
    }
}