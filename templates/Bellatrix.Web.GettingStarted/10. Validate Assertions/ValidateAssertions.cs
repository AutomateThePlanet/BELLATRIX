using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class ValidateAssertions : NUnit.WebTest
{
    [Test]
    [Category(Categories.CI)]
    public void AssertValidateCartPageFields()
    {
        // Instead of going to the main page and clicking the Add to Cart buttons we can directly add a product to the cart following the below link.
        App.Navigation.Navigate("https://demos.bellatrix.solutions/?add-to-cart=26");

        // Instead of clicking the view cart button we can directly navigate to the cart.
        App.Navigation.Navigate("https://demos.bellatrix.solutions/cart/");

        TextField couponCodeTextField = App.Components.CreateById<TextField>("coupon_code");

        // 1. We can assert the default text in the coupon text fiend through the BELLATRIX element Placeholder property.
        // The different BELLATRIX web elements classes contain lots of these properties which are a representation of the most important HTML element attributes.
        //
        // The biggest drawback of using vanilla assertions is that the messages displayed on failure are not meaningful at all.
        // If the bellow assertion fails the following message is displayed: "Message: Assert.AreEqual failed. Expected:<Coupon code >. Actual:<Coupon code>. "
        // You can guess what happened, but you do not have information which element failed and on which page.
        //
        // If we use the Validate extension methods, BELLATRIX waits some time for the condition to pass. After this period if it is not successful, a beatified
        // meaningful exception message is displayed:
        // "The control's placeholder should be 'Coupon code ' but was 'Coupon code'. The test failed on URL: https://demos.bellatrix.solutions/cart/"
        couponCodeTextField.ValidatePlaceholderIs("Coupon code");
        ////Assert.AreEqual("Coupon code ", couponCodeTextField.Placeholder);

        Button applyCouponButton = App.Components.CreateByValueContaining<Button>("Apply coupon");

        // 2. Assert that the apply coupon button exists and is visible on the page.
        // On fail the following message is displayed: "Message: Assert.That failed."
        // Cannot learn much about what happened.
        //
        // Now if we use the ValidateIsVisible method and the check does not succeed the following error message is displayed:
        // "The control should be visible but was NOT. The test failed on URL: https://demos.bellatrix.solutions/cart/"
        // To all exception messages, the current URL is displayed, which improves the troubleshooting.

        ////Assert.That(applyCouponButton.IsPresent);
        ////Assert.That(applyCouponButton.IsVisible);
        applyCouponButton.ValidateIsVisible();

        Div messageAlert = App.Components.CreateByClassContaining<Div>("woocommerce-message");

        // 3. Since there are no validation errors, verify that the message div is not visible.
        messageAlert.ValidateIsNotVisible();
        ////Assert.IsFalse(messageAlert.IsVisible);

        Button updateCart = App.Components.CreateByValueContaining<Button>("Update cart");

        // 4. No changes are made to the added products so the update cart button should be disabled.
        updateCart.ValidateIsDisabled();
        ////Assert.That(updateCart.IsDisabled);

        Span totalSpan = App.Components.CreateByXpath<Span>("//*[@class='order-total']//span");

        // 5. Check the total price contained in the order-total span HTML ComponentCreateService.
        // By default, all Validate methods have 5 seconds timeout. However, you can specify a custom timeout and sleep interval (period for checking again)
        totalSpan.ValidateInnerTextIs("120.00€", timeout: 30, sleepInterval: 2);
        ////Assert.AreEqual("120.00€", totalSpan.InnerText);

        // 6. BELLATRIX provides you with a full BDD logging support for Validate assertions and gives you a way to hook your logic in multiple places.

        // 7. You can execute multiple Validate assertions failing only once viewing all results.
        Bellatrix.Assertions.Assert.Multiple(
            () => totalSpan.ValidateInnerTextIs("120.00€", timeout: 30, sleepInterval: 2),
            () => updateCart.ValidateIsDisabled(),
            () => messageAlert.ValidateIsNotVisible(),
            () => Assert.That("120.00€".Equals(totalSpan.InnerText)));
    }
}