using System;
using Bellatrix.Web.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    public class LocateElementsTests : MSTest.WebTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PromotionsPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 1. There are different ways to locate elements on the page. To do it you use the element create service.
            // You need to know that BELLATRIX has a built-in complex mechanism for waiting for elements, so you do not need to worry about this anymore.
            // Keep in mind that when you use the Create methods, the element is not searched on the page. All elements use lazy loading.
            // Which means that they are searched once you perform an action or assertion on them. By default on each new action, the element is searched again and be refreshed.
            var promotionsLink = App.ComponentCreateService.CreateByLinkText<Anchor>("Promotions");

            // 2. You can access the WebDriver wrapped element through WrappedElement and the current WebDriver instance through- WrappedDriver
            Console.WriteLine(promotionsLink.WrappedElement.TagName);

            // 3. Because of the proxy element mechanism (we have a separate type of element instead of single WebDriver IWebElement interface) we have several benefits.
            // Each control (element type- ComboBox, TextField and so on) contains only the actions you can do with it, and the methods are named properly.
            // In vanilla WebDriver to type the text you call SendKeys method.
            // Also, we have some additional properties in the proxy web control such as- By. Now you can get the locator with which you element was found.
            Console.WriteLine(promotionsLink.By.Value);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [Browser(BrowserType.Chrome, Lifecycle.RestartOnFail)]
        public void BlogPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 4. BELLATRIX extends the vanilla WebDriver selectors and give you additional ones.
            // Available create methods:
            //
            // CreateByIdEndingWith  --> App.ComponentCreateService.CreateByIdEndingWith<Anchor>("myIdSuffix");
            // Searches the element by ID ending with the locator.
            //
            // CreateByTag   -->  App.ComponentCreateService.CreateByTag<Anchor>("a");
            // Searches the element by its tag.
            //
            // CreateById   -->  App.ComponentCreateService.CreateById<Button>("myId");
            // Searches the element by its ID.
            //
            // CreateByIdContaining   -->  App.ComponentCreateService.CreateByIdContaining<Button>("myIdMiddle");
            // Searches the element by ID containing the specified text.
            //
            // CreateByValueEndingWith   -->  App.ComponentCreateService.CreateByIdContaining<Button>("pay");
            // Searches the element by value attribute containing the specified text.
            //
            // CreateByXpath   -->  App.ComponentCreateService.CreateByXpath<Button>("//*[@title='Add to cart']");
            // Searches the element by XPath locator.
            //
            // CreateByLinkText   -->  App.ComponentCreateService.CreateByLinkText<Anchor>("blog");
            // Searches the element by its link (href)
            //
            // CreateByLinkTextContaining   -->  App.ComponentCreateService.CreateByLinkTextContaining<Anchor>("account");
            // Searches the element by its link (href) if it contains specified value.
            //
            // CreateByClass   -->  App.ComponentCreateService.CreateByClassContaining<Anchor>("ul.products");
            // Searches the element by its CSS classes.
            //
            // CreateByClassContaining   -->  App.ComponentCreateService.CreateByClassContaining<Anchor>(".products");
            // Searches the element by its CSS classes containing the specified values.
            //
            // CreateByInnerTextContaining   -->  App.ComponentCreateService.CreateByInnerTextContaining<Div>("Showing all");
            // Searches the element by its inner text content, including all child HTML elements.
            //
            // CreateByNameEndingWith   -->  App.ComponentCreateService.CreateByNameEndingWith<Search>("a");
            // Searches the element by its name containing the specified text.
            //
            // CreateByAttributesContaining   -->  App.ComponentCreateService.CreateByAttributesContaining<Anchor>("data-product_id", "31");
            // Searches the element by some of its attribute containing the specifed value.
            var blogLink = App.ComponentCreateService.CreateByLinkText<Anchor>("Blog");

            blogLink.Click();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void CheckAllAddToCartButtons()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 5. Sometimes we need to find more than one ComponentCreateService. For example, in this test we want to locate all Add to Cart buttons.
            // To do it you can use the element create service CreateAll method.
            // Available create methods:
            //
            // CreateAllByIdEndingWith  --> App.ComponentCreateService.CreateAllByIdEndingWith<Anchor>("myIdSuffix");
            // Searches the elements by ID ending with the locator.
            //
            // CreateAllByTag   -->  App.ComponentCreateService.CreateAllByTag<Anchor>("a");
            // Searches the elements by its tag.
            //
            // CreateAllById   -->  App.ComponentCreateService.CreateAllById<Button>("myId");
            // Searches the elements by its ID.
            //
            // CreateAllByIdContaining   -->  App.ComponentCreateService.CreateAllByIdContaining<Button>("myIdMiddle");
            // Searches the elements by ID containing the specified text.
            //
            // CreateAllByValueEndingWith   -->  App.ComponentCreateService.CreateAllByValueEndingWith<Button>("pay");
            // Searches the elements by value attribute containing the specified text.
            //
            // CreateAllByXpath   -->  App.ComponentCreateService.CreateAllByXpath<Button>("//*[@title='Add to cart']");
            // Searches the elements by XPath locator.
            //
            // CreateAllByLinkText   -->  App.ComponentCreateService.CreateAllByLinkText<Anchor>("blog");
            // Searches the elements by its link (href)
            //
            // CreateAllByLinkTextContaining   -->  App.ComponentCreateService.CreateAllByLinkTextContaining<Anchor>("account");
            // Searches the elements by its link (href) if it contains specified value.
            //
            // CreateAllByClass   -->  App.ComponentCreateService.CreateAllByClass<Anchor>("ul.products");
            // Searches the elements by its CSS classes.
            //
            // CreateAllByClassContaining   -->  App.ComponentCreateService.CreateAllByClassContaining<Anchor>(".products");
            // Searches the elements by its CSS classes containing the specified values.
            //
            // CreateAllByInnerTextContaining   -->  App.ComponentCreateService.CreateAllByInnerTextContaining<Div>("Showing all");
            // Searches the elements by its inner text content, including all child HTML elements.
            //
            // CreateAllByNameEndingWith   -->  App.ComponentCreateService.CreateAllByNameEndingWith<Search>("a");
            // Searches the elements by its name containing the specified text.
            //
            // CreateAllByAttributesContaining   -->  App.ComponentCreateService.CreateAllByAttributesContaining<Anchor>("data-product_id", "31");
            // Searches the elements by some of its attribute containing the specifed value.

            ////var blogLink = App.ComponentCreateService.CreateAllByXpath<Anchor>("//*[@title='Add to cart']");
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void OpenSalesPage_When_LocatedSaleButtonInsideProductImage()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 6. Sometimes it is easier to locate one element and then find the next one that you need, inside it.
            // For example in this test we want to locate the Sale! button inside the product's description.
            // To do it you can use the element's Create methods.
            var productsColumn = App.ComponentCreateService.CreateByClassContaining<Option>("products columns-4");

            // The first products row is located. Then search inside it for the first product image, inside it search for the Sale! Span ComponentCreateService.
            // Note: it is entirely legal to create a Button instead of Span. BELLATRIX library does not care about the real type of the HTML elements.
            // The proxy types are convenience wrappers so to say. Meaning they give you a better interface of predefined properties and methods to make your tests more readable.
            var saleButton = productsColumn.CreateByClassContaining<Anchor>("woocommerce-LoopProduct-link woocommerce-loop-product__link").CreateByInnerTextContaining<Button>("Sale!");

            saleButton.Click();

            // Available create methods on element level:
            //
            // CreateByIdEndingWith  --> ComponentCreateService.CreateByIdEndingWith<Anchor>("myIdSuffix");
            // Searches the element by ID ending with the locator.
            //
            // CreateByTag   -->  ComponentCreateService.CreateByTag<Anchor>("a");
            // Searches the element by its tag.
            //
            // CreateById   -->  ComponentCreateService.CreateById<Button>("myId");
            // Searches the element by its ID.
            //
            // CreateByIdContaining   -->  ComponentCreateService.CreateByIdContaining<Button>("myIdMiddle");
            // Searches the element by ID containing the specified text.
            //
            // CreateByValueEndingWith   -->  ComponentCreateService.CreateByIdContaining<Button>("pay");
            // Searches the element by value attribute containing the specified text.
            //
            // CreateByXpath   -->  ComponentCreateService.CreateByXpath<Button>("//*[@title='Add to cart']");
            // Searches the element by XPath locator.
            //
            // CreateByLinkText   -->  ComponentCreateService.CreateByLinkText<Anchor>("blog");
            // Searches the element by its link (href)
            //
            // CreateByLinkTextContaining   -->  ComponentCreateService.CreateByLinkTextContaining<Anchor>("account");
            // Searches the element by its link (href) if it contains specified value.
            //
            // CreateByClass   -->  ComponentCreateService.CreateByClassContaining<Anchor>("ul.products");
            // Searches the element by its CSS classes.
            //
            // CreateByClassContaining   -->  ComponentCreateService.CreateByClassContaining<Anchor>(".products");
            // Searches the element by its CSS classes containing the specified values.
            //
            // CreateByInnerTextContaining   -->  ComponentCreateService.CreateByInnerTextContaining<Div>("Showing all");
            // Searches the element by its inner text content, including all child HTML elements.
            //
            // CreateByNameEndingWith   -->  ComponentCreateService.CreateByNameEndingWith<Search>("a");
            // Searches the element by its name containing the specified text.
            //
            // CreateByAttributesContaining   -->  ComponentCreateService.CreateByAttributesContaining<Anchor>("data-product_id", "31");
            // Searches the element by some of its attribute containing the specifed value.
            //
            // CreateAll Available methods:
            //
            // CreateAllByIdEndingWith  --> ComponentCreateService.CreateAllByIdEndingWith<Anchor>("myIdSuffix");
            // Searches the element by ID ending with the locator.
            //
            // CreateAllByTag   -->  ComponentCreateService.CreateAllByTag<Anchor>("a");
            // Searches the element by its tag.
            //
            // CreateAllById   -->  ComponentCreateService.CreateAllById<Button>("myId");
            // Searches the element by its ID.
            //
            // CreateAllByIdContaining   -->  ComponentCreateService.CreateAllByIdContaining<Button>("myIdMiddle");
            // Searches the element by ID containing the specified text.
            //
            // CreateAllByValueEndingWith   -->  ComponentCreateService.CreateAllByValueEndingWith<Button>("pay");
            // Searches the element by value attribute containing the specified text.
            //
            // CreateAllByXpath   -->  ComponentCreateService.CreateAllByXpath<Button>("//*[@title='Add to cart']");
            // Searches the element by XPath locator.
            //
            // CreateAllByLinkText   -->  ComponentCreateService.CreateAllByLinkText<Anchor>("blog");
            // Searches the element by its link (href)
            //
            // CreateAllByLinkTextContaining   -->  ComponentCreateService.CreateAllByLinkTextContaining<Anchor>("account");
            // Searches the element by its link (href) if it contains specified value.
            //
            // CreateAllByClass   -->  ComponentCreateService.CreateAllByClass<Anchor>("ul.products");
            // Searches the element by its CSS classes.
            //
            // CreateAllByClassContaining   -->  ComponentCreateService.CreateAllByClassContaining<Anchor>(".products");
            // Searches the element by its CSS classes containing the specified values.
            //
            // CreateAllByInnerTextContaining   -->  ComponentCreateService.CreateAllByInnerTextContaining<Div>("Showing all");
            // Searches the element by its inner text content, including all child HTML elements.
            //
            // CreateAllByNameEndingWith   -->  ComponentCreateService.CreateAllByNameEndingWith<Search>("a");
            // Searches the element by its name containing the specified text.
            //
            // CreateAllByAttributesContaining   -->  ComponentCreateService.CreateAllByAttributesContaining<Anchor>("data-product_id", "31");
            // Searches the element by some of its attribute containing the specifed value.
        }
    }
}