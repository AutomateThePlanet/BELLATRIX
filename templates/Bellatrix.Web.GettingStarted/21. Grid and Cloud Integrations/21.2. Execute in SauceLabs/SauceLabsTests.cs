using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]

// 1. To execute BELLATRIX tests in SauceLabs cloud you should use the SauceLabs attribute instead of Browser.
// SauceLabs has the same parameters as Browser but adds to additional ones-
// browser version, platform type, recordVideo and recordScreenshots.
// As with the Browser attribute you can override the class behavior on Test level.
[SauceLabs(BrowserType.Chrome,
    "62",
    "Windows",
    Lifecycle.ReuseIfStarted,
    recordScreenshots: true,
    recordVideo: true)]
public class SauceLabsTests : NUnit.WebTest
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
    [SauceLabs(BrowserType.Chrome, "62", "Windows", DesktopWindowSize._1280_1024, Lifecycle.ReuseIfStarted)]
    [Ignore("no need to run")]

    // [SauceLabs(BrowserType.Chrome, "62", "Windows", 1000, 500, Lifecycle.ReuseIfStarted)]
    // [SauceLabs(BrowserType.Chrome, "62", "Windows", MobileWindowSize._320_568, Lifecycle.ReuseIfStarted)]
    // [SauceLabs(BrowserType.Chrome, "62", "Windows", TabletWindowSize._600_1024, Lifecycle.ReuseIfStarted)]
    public void BlogPageOpened_When_PromotionsButtonClicked()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        var blogLink = App.Components.CreateByLinkText<Anchor>("Blog");

        blogLink.Click();
    }
}