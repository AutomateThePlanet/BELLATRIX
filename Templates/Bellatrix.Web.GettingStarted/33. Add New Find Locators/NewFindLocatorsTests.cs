// 1. You need to add a using statement to the namespace where the extension methods for new locator are situated.
using Bellatrix.Web.GettingStarted.ExtensionMethodsLocators;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, BrowserBehavior.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, BrowserBehavior.RestartEveryTime)]
    public class NewFindLocatorsTests : WebTest
    {
        [TestMethod]
        [Ignore]
        public void PromotionsPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 2. After that, you can use the new locator as it was originally part of Bellatrix.
            var promotionsLink = App.ElementCreateService.CreateByIdStartingWith<Anchor>("promo");

            promotionsLink.Click();
        }
    }
}