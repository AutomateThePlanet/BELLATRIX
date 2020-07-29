using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, BrowserBehavior.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, BrowserBehavior.RestartEveryTime)]
    public class LocateAndWaitElementsTests : WebTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void BlogPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 1. Sometimes you need to perform an action against an element only when a specific condition is true.
            // As mentioned in previous part of the guide, BELLATRIX by default always waits for elements to exist.
            // However, sometimes this may not be enough. For example, you may want to click on a button once it is clickable.
            // It may be disabled at the beginning of the tests because some validation is not met. Your test fulfil the initial condition and if you use
            // vanilla WebDriver the test most probably fails because WebDriver clicks too fast before your button is enabled by your code.
            // So we created additional syntax sugar methods to help you deal with this.
            // You can use element "ToBe" methods after the Create and CreateAll methods.
            // As you can see in the example below you can chain multiple of this methods.
            //
            // Note: Since Bellatrix, elements creation logic is lazy loading as mentioned before,
            // BELLATRIX waits for the conditions to be True on the first action you perform with the element.
            //
            // Note 2: Keep in mind that with this syntax these conditions are checked everytime you perform an action with the element.
            // Which can lead tо small execution delays.
            //
            // The default timeouts that BELLATRIX use are placed inside the testFrameworkSettings file, mentioned in '01. Folder and File Structure' chapter.
            // Inside it, is the timeoutSettings section. All values are in seconds.
            // "timeoutSettings": {
            //    "waitForAjaxTimeout": "30",
            //    "sleepInterval": "1",
            //    "elementToBeVisibleTimeout": "30",
            //    "elementToExistTimeout": "30",
            //    "elementToNotExistTimeout": "30",
            //    "elementToBeClickableTimeout": "30",
            //    "elementNotToBeVisibleTimeout": "30",
            //    "elementToHaveContentTimeout": "15"
            // },
            var blogLink = App.ElementCreateService.CreateByLinkText<Anchor>("Blog").ToBeClickable().ToBeVisible();

            blogLink.Click();

            // 2. You can always override the timeout settings for each method.
            // The first value is the timeout in seconds and the second one controls how often the engine checks the condition.
            var promotionsLink = App.ElementCreateService.CreateByLinkText<Anchor>("Promotions").ToHasContent(40, 1);

            promotionsLink.Click();

            // 3. All available ToBe methods:
            // ToExists  --> App.ElementCreateService.CreateByLinkText<Anchor>("Blog").ToExists();
            // Waits for the element to exist on the page. BELLATRIX always does it by default.
            // But if use another ToBe methods you need to add it again since you have to override the default behaviour.
            //
            // ToNotExists  --> App.ElementCreateService.CreateByLinkText<Anchor>("Blog").ToNotExists();
            // Waits for the element to disappear. Usually, we use in assertion methods.
            //
            // ToBeVisible  --> App.ElementCreateService.CreateByLinkText<Anchor>("Blog").ToBeVisible();
            // Waits for the element to be visible.
            //
            // ToNotBeVisible  --> App.ElementCreateService.CreateByLinkText<Anchor>("Blog").ToNotBeVisible();
            // Waits for the element to be invisible.
            //
            // ToBeClickable  --> App.ElementCreateService.CreateByLinkText<Anchor>("Blog").ToBeClickable();
            // Waits for the element to be clickable (may be disabled at first).
            //
            // ToHasContent  --> App.ElementCreateService.CreateByLinkText<Anchor>("Blog").ToHasContent();
            // Waits for the element to has some content in it. For example, some validation DIV or label.
            //
            // ToHasStyle  --> App.ElementCreateService.CreateByLinkText<Anchor>("Blog").ToHasStyle("disabled");
            // Waits for the element to have some content in it. For example, some validation DIV or label.
            //
            // ToBeDisabled  --> App.ElementCreateService.CreateByLinkText<Anchor>("Blog").ToBeDisabled();
            // Waits for the element to be disabled.
        }
    }
}