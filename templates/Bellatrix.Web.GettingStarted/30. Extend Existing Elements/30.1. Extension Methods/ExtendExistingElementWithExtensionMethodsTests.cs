// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Web.GettingStarted.Advanced.Elements.Extension.Methods;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class ExtendExistingElementWithExtensionMethodsTests : NUnit.WebTest
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
        Button proceedToCheckout = App.Components.CreateByClassContaining<Button>("checkout-button button alt wc-forward");
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

        // 2. Use the custom added submit button lifecycle through 'Enter' key.
        proceedToCheckout.SubmitButtonWithEnter();
        billingDetailsHeading.ToBeVisible().WaitToBe();
    }
}