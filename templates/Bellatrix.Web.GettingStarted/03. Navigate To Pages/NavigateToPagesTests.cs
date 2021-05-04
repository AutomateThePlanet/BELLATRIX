using Bellatrix.Web.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    public class NavigateToPagesTests : MSTest.WebTest
    {
        // Depending on the types of tests you want to write there are a couple of ways to navigate to specific pages.
        // In later chapters, there are more details about the different test workflow hooks. Find here two of them.
        //
        // 1. If you reuse your browser and want to navigate once to a specific page. You can use the TestsAct method.
        // It executes once for all tests in the class.
        public override void TestsAct() => App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

        // 2. If you need each test to navigate each time to the same page, you can use the TestInit method.
        public override void TestInit() => App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PromotionsPageOpened_When_PromotionsButtonClicked()
        {
            // 3. You can always navigate in each separate tests, but if all of them go to the same page, you can use the above techniques for code reuse.
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // Use the element creation service to create an instance of the anchor. There are much more details about this process in the next sections.
            var promotionsLink = App.ComponentCreateService.CreateByLinkText<Anchor>("Promotions");

            promotionsLink.Click();

            // 4. Sometimes, some AJAX async calls are not caught natively by WebDriver. So you can use the BELLATRIX browser service's method.
            // WaitUntilReady which waits for these calls automatically to finish.
            // Keep in mind that usually this is not necessary since BELLATRIX has a complex built-in mechanism for handling element waits.
            App.BrowserService.WaitUntilReady();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void BlogPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            var blogLink = App.ComponentCreateService.CreateByLinkText<Anchor>("Blog");

            blogLink.Click();

            // 5. Sometimes before proceeding with searching and making actions on the next page, we need to wait for something.
            // It is useful in some cases to wait for a partial URL instead hard-coding the whole URL since it can change depending on the environment.
            // Keep in mind that usually this is not necessary since BELLATRIX has a complex built-in mechanism for handling element waits.
            App.NavigationService.WaitForPartialUrl("/blog/");
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void TestFileOpened_When_NavigateToLocalPage()
        {
            // 6. Sometimes you may need to navigate to a local HTML file. We make it easier for you since it is complicated depending on the different browsers.
            // Make sure to copy the file to the folder with your tests files. To do it, include it in the project and mark it as Copy Always.
            App.NavigationService.NavigateToLocalPage("testPage.html");
        }
    }
}