using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, BrowserBehavior.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, BrowserBehavior.RestartEveryTime)]
    public class NormalAssertions : WebTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void AssertCartPageFields()
        {
            // Instead of going to the main page and clicking the Add to Cart buttons we can directly add a product to the cart following the below link.
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/?add-to-cart=26");

            // Instead of clicking the view cart button we can directly navigate to the cart.
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/cart/");

            TextField couponCodeTextField = App.ElementCreateService.CreateById<TextField>("coupon_code");

            // 1. We can assert the default text in the coupon text fiend through the BELLATRIX element Placeholder property.
            // The different BELLATRIX web elements classes contain lots of these properties which are a representation of the most important HTML element attributes.
            //
            // the biggest drawback of using vanilla assertions is that the messages displayed on failure are not meaningful at all.
            // This is so because most unit testing frameworks are created for much simpler and shorter unit tests. In next chapter, there is information how BELLATRIX solves
            // the problems with the introduction of Validate methods.
            // If the bellow assertion fails the following message is displayed: "Message: Assert.AreEqual failed. Expected:<Coupon code >. Actual:<Coupon code>. "
            // You can guess what happened, but you do not have information which element failed and on which page.
            Assert.AreEqual("Coupon code", couponCodeTextField.Placeholder);

            Button applyCouponButton = App.ElementCreateService.CreateByValueContaining<Button>("Apply coupon");

            // 2. Here we assert that the apply coupon button exists and is visible on the page.
            // On fail the following message is displayed: "Message: Assert.IsTrue failed."
            // Cannot learn much about what happened.
            Assert.IsTrue(applyCouponButton.IsPresent);
            Assert.IsTrue(applyCouponButton.IsVisible);

            Div messageAlert = App.ElementCreateService.CreateByClassContaining<Div>("woocommerce-message");

            // 3. Since there are no validation errors, verify that the message div is not visible.
            Assert.IsFalse(messageAlert.IsVisible);

            Button updateCart = App.ElementCreateService.CreateByValueContaining<Button>("Update cart");

            // 4. We have not made any changes to the added products so the update cart button should be disabled.
            Assert.IsTrue(updateCart.IsDisabled);

            Span totalSpan = App.ElementCreateService.CreateByXpath<Span>("//*[@class='order-total']//span");

            // 5. We check the total price contained in the order-total span HTML element.
            Assert.AreEqual("120.00€", totalSpan.InnerText);

            // 6. One more thing you need to keep in mind is that normal assertion methods do not include BDD logging and any available hooks.
            // BELLATRIX provides you with a full BDD logging support for Validate assertions and gives you a way to hook your logic in multiple places.

            // 7. You can execute multiple assertions failing only once viewing all results.
            Bellatrix.Assertions.Assert.Multiple(
                () => Assert.AreEqual("120.00€", totalSpan.InnerText),
                () => Assert.IsTrue(updateCart.IsDisabled));
         }
    }
}