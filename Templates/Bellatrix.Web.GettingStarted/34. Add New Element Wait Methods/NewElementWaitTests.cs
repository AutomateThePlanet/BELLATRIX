// 1. You need to add a using statement to the namespace where the new wait extension methods are situated.
using Bellatrix.Web.GettingStarted.ExtensionMethodsWaits;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, BrowserBehavior.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, BrowserBehavior.RestartEveryTime)]
    public class NewElementWaitTests : WebTest
    {
        [TestMethod]
        [Ignore]
        public void PromotionsPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 2. After that, you can use the new wait method as it was originally part of Bellatrix.
            var promotionsLink = App.ElementCreateService.CreateByLinkText<Anchor>("promo").ToHasSpecificStyle("padding: 1.618em 1em");

            promotionsLink.Click();
        }
    }
}