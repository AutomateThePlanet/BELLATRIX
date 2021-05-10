using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, Lifecycle.RestartEveryTime)]
    public class CookiesServiceTests : MSTest.WebTest
    {
        // 1. BELLATRIX gives you an interface for easier work with cookies using the CookiesService.
        // You need to make sure that you have navigated to the desired web page.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void GetAllCookies()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/welcome/");

            App.CookiesService.AddCookie("woocommerce_items_in_cart1", "3");
            App.CookiesService.AddCookie("woocommerce_items_in_cart2", "3");
            App.CookiesService.AddCookie("woocommerce_items_in_cart3", "3");

            // 2. Get all cookies.
            var cookies = App.CookiesService.GetAllCookies();

            Assert.IsTrue(cookies.Count > 0);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void GetSpecificCookie()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/welcome/");

            App.CookiesService.AddCookie("woocommerce_items_in_cart", "3");

            // 3. Get a specific cookie by name.
            var itemsInCartCookie = App.CookiesService.GetCookie("woocommerce_items_in_cart");

            Assert.AreEqual("3", itemsInCartCookie);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void DeleteAllCookies()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/welcome/");

            var protonRocketAddToCartBtn = App.ElementCreateService.CreateAllByInnerTextContaining<Anchor>("Add to cart").First();
            protonRocketAddToCartBtn.Click();

            // 4. Delete all cookies.
            App.CookiesService.DeleteAllCookies();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void DeleteSpecificCookie()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/welcome/");

            var protonRocketAddToCartBtn = App.ElementCreateService.CreateAllByInnerTextContaining<Anchor>("Add to cart").First();
            protonRocketAddToCartBtn.Click();

            // 5. Delete a specific cookie by name.
            App.CookiesService.DeleteCookie("woocommerce_items_in_cart");
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void AddNewCookie()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/welcome/");

            // 6. Add a new cookie.
            App.CookiesService.AddCookie("woocommerce_items_in_cart", "3");
        }
    }
}