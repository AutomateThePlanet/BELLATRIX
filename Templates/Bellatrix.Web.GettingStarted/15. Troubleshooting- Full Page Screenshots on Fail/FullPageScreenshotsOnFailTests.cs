using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]

    // 1. This is the attribute for automatic generation of full-page screenshots by Bellatrix.
    // The engine checks after each test, its result, if failed, makes the screenshots. We have a unique engine for the screenshots.
    // We do not use vanilla WebDriver. If you use the WebDriver method, it makes a screenshot only of the visible part of the page.
    // If you have to do it manually precisely, you need thousands of lines of code.
    //
    // If you place attribute over the class all tests inherit the behaviour.
    // It is possible to put it over each test and this way you override the class behaviour only for this particular test.
    [ScreenshotOnFail(true)]
    [Browser(BrowserType.Chrome, BrowserBehavior.ReuseIfStarted)]
    [Browser(OS.OSX, BrowserType.Chrome, BrowserBehavior.ReuseIfStarted)]
    public class FullPageScreenshotsOnFailTests : WebTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PromotionsPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");
            var promotionsLink = App.ElementCreateService.CreateByLinkText<Anchor>("Promotions");
            promotionsLink.Click();
        }

        // 2. As mentioned above we can override the screenshot behaviour for a particular test.
        // The global behaviour for all tests in the class is to make screenshots on fail.
        // Only for this particular test, we tell BELLATRIX not to make screenshots.
        [TestMethod]
        [ScreenshotOnFail(false)]
        [TestCategory(Categories.CI)]
        public void BlogPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            var blogLink = App.ElementCreateService.CreateByLinkText<Anchor>("Blog");

            blogLink.Click();
        }

        // 3. If you open the testFrameworkSettings file, you find the screenshotsSettings section that controls this behaviour.
        // "screenshotsSettings": {
        //     "isEnabled": "true",
        //     "filePath": "ApplicationData\\Troubleshooting\\Screenshots"
        // }
        //
        // You can turn off the making of screenshots for all tests and specify where the screenshots to be saved.
        // In the extensibility chapters read more about how you can create different screenshots engine or change the saving strategy.
    }
}