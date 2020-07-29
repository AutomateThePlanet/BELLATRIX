using Bellatrix.TestExecutionExtensions.Common.ExecutionTime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]

    // 1. Sometimes it is useful to use your functional tests to measure performance. Or to just make sure that your app
    // is not slow. To do that BELLATRIX libraries offer the ExecutionTimeUnder attribute. You specify a timeout and if the
    // test is executed over it the test will fail.
    //
    // 1.1. You need to add the NuGet package- Bellatrix.TestExecutionExtensions.Common
    // 1.2. After that you need to add a using statement to Bellatrix.TestExecutionExtensions.Common.ExecutionTime
    [ExecutionTimeUnder(2000, TimeUnit.Milliseconds)]
    [Browser(BrowserType.Chrome, BrowserBehavior.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, BrowserBehavior.RestartEveryTime)]
    public class MeasureTestExecutionTests : WebTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PromotionsPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            var promotionsLink = App.ElementCreateService.CreateByLinkText<Anchor>("Promotions");

            promotionsLink.Click();
        }
    }
}