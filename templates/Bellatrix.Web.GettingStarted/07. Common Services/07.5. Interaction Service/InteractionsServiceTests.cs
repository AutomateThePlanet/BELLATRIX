using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    public class InteractionsServiceTests : MSTest.WebTest
    {
        // 1. BELLATRIX gives you an interface for easier execution of complex UI interactions such as drag & drop, move to element, double click, etc.
        // BELLATRIX interaction APIs are simplified and made to be user-friendly as possible.
        // Their usage can eliminate lots of code duplication and boilerplate code.
        [TestMethod]
        [TestCategory(Categories.CI)]
        [Ignore("Currently not working in the latest version of WebDriver")]
        public void DragAndDrop()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            Anchor protonRocketAnchor = App.ComponentCreateService.CreateByAttributesContaining<Anchor>("href", "/proton-rocket/");
            Anchor protonMAnchor = App.ComponentCreateService.CreateByAttributesContaining<Anchor>("href", "/proton-m/");

            // 2. You can access the interaction methods through the App class.
            // 3. You can chain more than one method.
            // 4. At the end of the method chain you need to call the Perform method.
            App.InteractionsService.MoveToElement(protonRocketAnchor).DragAndDrop(protonRocketAnchor, protonMAnchor).Perform();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void KeyUp()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            Anchor protonRocketAnchor = App.ComponentCreateService.CreateByAttributesContaining<Anchor>("href", "/proton-rocket/");
            Anchor protonMAnchor = App.ComponentCreateService.CreateByAttributesContaining<Anchor>("href", "/proton-m/");

            App.InteractionsService.MoveToElement(protonRocketAnchor).KeyUp(Keys.LeftShift).ContextClick().Perform();
            App.InteractionsService.DoubleClick(protonRocketAnchor).KeyUp(Keys.LeftShift).ContextClick().Perform();
        }
    }
}