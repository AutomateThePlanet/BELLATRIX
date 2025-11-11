using Bellatrix.Web.GettingStarted.Advanced.Elements.ChildElements;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class ExtendExistingElementWithChildElementsTests : NUnit.WebTest
{
    [Test]
    [Ignore("no need to run")]
    public void PurchaseRocket()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        Select sortDropDown = App.Components.CreateByNameEndingWith<Select>("orderby");
        Anchor protonMReadMoreButton = App.Components.CreateByInnerTextContaining<Anchor>("Read more");
        Anchor addToCartFalcon9 = App.Components.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
        Anchor viewCartButton = App.Components.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();
        TextField couponCodeTextField = App.Components.CreateById<TextField>("coupon_code");
        Button applyCouponButton = App.Components.CreateByValueContaining<Button>("Apply coupon");
        Number quantityBox = App.Components.CreateByClassContaining<Number>("input-text qty text");
        Div messageAlert = App.Components.CreateByClassContaining<Div>("woocommerce-message");
        Button updateCart = App.Components.CreateByValueContaining<Button>("Update cart").ToBeClickable();

        // 1. Instead of the regular button, we create the ExtendedButton, this way we can use its new methods.
        ExtendedButton proceedToCheckout = App.Components.CreateByClassContaining<ExtendedButton>("checkout-button button alt wc-forward");
        Heading billingDetailsHeading = App.Components.CreateByInnerTextContaining<Heading>("Billing details");
        Span totalSpan = App.Components.CreateByXpath<Span>("//*[@class='order-total']//span");

        sortDropDown.SelectByText("Sort by price: low to high");
        protonMReadMoreButton.Hover();
        addToCartFalcon9.Focus();
        addToCartFalcon9.Click();
        viewCartButton.Click();
        couponCodeTextField.SetText("happybirthday");
        applyCouponButton.Click();
        messageAlert.ToHasContent().ToBeVisible().WaitToBe();
        messageAlert.ValidateInnerTextIs("Coupon code applied successfully.");
        App.Browser.WaitForAjax();
        totalSpan.ValidateInnerTextIs("54.00€");
        proceedToCheckout.Click();

        // 2. Use the new custom method provided by the ExtendedButton class.
        proceedToCheckout.SubmitButtonWithEnter();
        billingDetailsHeading.ToBeVisible().WaitToBe();
    }
}