// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Web.GettingStarted.Advanced.Elements.Extension.Methods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, Lifecycle.RestartEveryTime)]
    public class ExtendExistingElementWithExtensionMethodsTests : MSTest.WebTest
    {
        [TestMethod]
        [Ignore]
        public void PurchaseRocket()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            Select sortDropDown = App.ComponentCreateService.CreateByNameEndingWith<Select>("orderby");
            Anchor protonMReadMoreButton = App.ComponentCreateService.CreateByInnerTextContaining<Anchor>("Read more");
            Anchor addToCartFalcon9 = App.ComponentCreateService.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
            Anchor viewCartButton = App.ComponentCreateService.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();
            TextField couponCodeTextField = App.ComponentCreateService.CreateById<TextField>("coupon_code");
            Button applyCouponButton = App.ComponentCreateService.CreateByValueContaining<Button>("Apply coupon");
            Number quantityBox = App.ComponentCreateService.CreateByClassContaining<Number>("input-text qty text");
            Div messageAlert = App.ComponentCreateService.CreateByClassContaining<Div>("woocommerce-message");
            Button updateCart = App.ComponentCreateService.CreateByValueContaining<Button>("Update cart").ToBeClickable();
            Button proceedToCheckout = App.ComponentCreateService.CreateByClassContaining<Button>("checkout-button button alt wc-forward");
            Heading billingDetailsHeading = App.ComponentCreateService.CreateByInnerTextContaining<Heading>("Billing details");
            Span totalSpan = App.ComponentCreateService.CreateByXpath<Span>("//*[@class='order-total']//span");

            sortDropDown.SelectByText("Sort by price: low to high");
            protonMReadMoreButton.Hover();
            addToCartFalcon9.Focus();
            addToCartFalcon9.Click();
            viewCartButton.Click();
            couponCodeTextField.SetText("happybirthday");
            applyCouponButton.Click();
            messageAlert.ToHasContent().ToBeVisible().WaitToBe();
            messageAlert.ValidateInnerTextIs("Coupon code applied successfully.");
            App.BrowserService.WaitForAjax();
            totalSpan.ValidateInnerTextIs("54.00€");
            proceedToCheckout.Click();

            // 2. Use the custom added submit button lifecycle through 'Enter' key.
            proceedToCheckout.SubmitButtonWithEnter();
            billingDetailsHeading.ToBeVisible().WaitToBe();
        }
    }
}