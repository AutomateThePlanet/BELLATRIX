// 1. You need to add a using statement to the namespace where the new wait extension methods are situated.
using Bellatrix.Web.GettingStarted.ExtensionMethodsWaits;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, Lifecycle.RestartEveryTime)]
    public class NewElementWaitTests : MSTest.WebTest
    {
        [TestMethod]
        [Ignore]
        public void PromotionsPageOpened_When_PromotionsButtonClicked()
        {
            App.Navigation.Navigate("http://demos.bellatrix.solutions/");

            // 2. After that, you can use the new wait method as it was originally part of Bellatrix.
            var promotionsLink = App.Components.CreateByLinkText<Anchor>("promo").ToHasSpecificStyle("padding: 1.618em 1em");

            promotionsLink.Click();
        }
    }
}