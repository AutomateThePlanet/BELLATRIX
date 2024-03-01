using System.Linq;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class CookiesServiceTests : NUnit.WebTest
{
    // 1. BELLATRIX gives you an interface for easier work with cookies using the CookiesService.
    // You need to make sure that you have navigated to the desired web page.
    [Test]
    [Category(Categories.CI)]
    public void GetAllCookies()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/welcome/");

        App.Cookies.AddCookie("woocommerce_items_in_cart1", "3");
        App.Cookies.AddCookie("woocommerce_items_in_cart2", "3");
        App.Cookies.AddCookie("woocommerce_items_in_cart3", "3");

        // 2. Get all cookies.
        var cookies = App.Cookies.GetAllCookies();

        Assert.That(cookies.Count > 0);
    }

    [Test]
    [Category(Categories.CI)]
    public void GetSpecificCookie()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/welcome/");

        App.Cookies.AddCookie("woocommerce_items_in_cart", "3");

        // 3. Get a specific cookie by name.
        var itemsInCartCookie = App.Cookies.GetCookie("woocommerce_items_in_cart");

        Assert.That("3".Equals(itemsInCartCookie));
    }

    [Test]
    [Category(Categories.CI)]
    public void DeleteAllCookies()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/welcome/");

        var protonRocketAddToCartBtn = App.Components.CreateAllByInnerTextContaining<Anchor>("Add to cart").First();
        protonRocketAddToCartBtn.Click();

        // 4. Delete all cookies.
        App.Cookies.DeleteAllCookies();
    }

    [Test]
    [Category(Categories.CI)]
    public void DeleteSpecificCookie()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/welcome/");

        var protonRocketAddToCartBtn = App.Components.CreateAllByInnerTextContaining<Anchor>("Add to cart").First();
        protonRocketAddToCartBtn.Click();

        // 5. Delete a specific cookie by name.
        App.Cookies.DeleteCookie("woocommerce_items_in_cart");
    }

    [Test]
    [Category(Categories.CI)]
    public void AddNewCookie()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/welcome/");

        // 6. Add a new cookie.
        App.Cookies.AddCookie("woocommerce_items_in_cart", "3");
    }
}