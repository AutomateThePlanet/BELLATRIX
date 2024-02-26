using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]

// 1. To execute BELLATRIX tests in CrossBrowserTesting cloud, you should use the CrossBrowserTesting attribute instead of Browser.
// CrossBrowserTesting has the same parameters as Browser but adds to additional ones-
// browser version, platform, recordVideo, recordNetwork and build. The last three are optional and have default values.
// As with the Browser attribute you can override the class lifecycle on Test level.
[CrossBrowserTesting(BrowserType.Chrome,
    "62",
    "Windows 10",
    Lifecycle.ReuseIfStarted,
    recordVideo: true,
    recordNetwork: true,
    build: "myUniqueBuildName")]
public class CrossBrowserTesting : NUnit.WebTest
{
    [Test]
    [Ignore("no need to run")]
    public void PromotionsPageOpened_When_PromotionsButtonClicked()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        var promotionsLink = App.Components.CreateByLinkText<Anchor>("Promotions");

        promotionsLink.Click();
    }

    // 2. As mentioned if you use the CrossBrowserTesting attribute on method level it overrides the class settings.
    // As you can see with the CrossBrowserTesting attribute we can change the browser window size again.
    [Test]
    [Ignore("no need to run")]
    [CrossBrowserTesting(BrowserType.Chrome, "62", "Windows 10", DesktopWindowSize._1280_1024, Lifecycle.ReuseIfStarted)]

    // [BrowserStack(BrowserType.Chrome, "62", "Windows 10", 1000, 500, Lifecycle.ReuseIfStarted)]
    // [BrowserStack(BrowserType.Chrome, "62", "Windows 10", MobileWindowSize._320_568, Lifecycle.ReuseIfStarted)]
    // [BrowserStack(BrowserType.Chrome, "62", "Windows 10", TabletWindowSize._600_1024, Lifecycle.ReuseIfStarted)]
    public void BlogPageOpened_When_PromotionsButtonClicked()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        var blogLink = App.Components.CreateByLinkText<Anchor>("Blog");

        blogLink.Click();
    }
}