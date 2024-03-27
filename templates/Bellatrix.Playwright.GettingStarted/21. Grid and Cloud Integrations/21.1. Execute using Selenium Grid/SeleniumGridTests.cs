using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.Settings;
using NUnit.Framework;


namespace Bellatrix.Playwright.GettingStarted;

[TestFixture]

// 1. To use BELLATRIX with Selenium Grid, you should use the Remote attribute instead of Browser.
// Remote has the same parameters as Browser but adds to additional ones- browser version.
// As with the Browser attribute you can override the class behavior on Test level.
//
// 2. You can find a dedicated section about execution in the testFrameworkSettings file. For Selenium Grid, the execution settings should look like this:
// "executionSettings": {
//      "executionType": "grid",
//      "defaultBrowser": "chrome",
//      "defaultLifeCycle": "restart every time",
//      "resolution": "",
//      "browserVersion": "",
//      "url": "http://127.0.0.1:4444/wd/hub",
//      "arguments": [
//        {
//           "browserName": "chrome"
//        }
//      ]
//}
// 3. Keep in mind that Playwright supports only Chrome browsers for Selenium Grid.
[Remote(BrowserChoice.Chrome, "62", Lifecycle.ReuseIfStarted)]
public class SeleniumGridTests : NUnit.WebTest
{
    [Test]
    [Ignore("no need to run")]
    public void PromotionsPageOpened_When_PromotionsButtonClicked()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        var promotionsLink = App.Components.CreateByLinkText<Anchor>("Promotions");

        promotionsLink.Click();
    }

    // 2. As mentioned if you use the Remote attribute on method level it overrides the class settings.
    // As you can see with the Remote attribute we can change the browser window size again.
    [Test]
    [Ignore("no need to run")]
    [Remote(BrowserChoice.Chrome, "62", DesktopWindowSize._1280_1024, Lifecycle.ReuseIfStarted)]

    // [Remote(BrowserChoice.Chrome, "62", 1000, 500, Lifecycle.ReuseIfStarted)]
    // [Remote(BrowserChoice.Chrome, "62", MobileWindowSize._320_568, Lifecycle.ReuseIfStarted)]
    // [Remote(BrowserChoice.Chrome, "62", TabletWindowSize._600_1024, Lifecycle.ReuseIfStarted)]
    public void BlogPageOpened_When_PromotionsButtonClicked()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        var blogLink = App.Components.CreateByLinkText<Anchor>("Blog");

        blogLink.Click();
    }
}