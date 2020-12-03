using Bellatrix.Web.GettingStarted.Advanced.Elements.ChildElements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.FirefoxHeadless, BrowserBehavior.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, BrowserBehavior.RestartEveryTime)]
    public class ExtendExistingElementWithChildElementsTests : WebTest
    {
        [TestMethod]
        [Ignore]
        public void PurchaseRocket()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            Select sortDropDown = App.ElementCreateService.CreateByNameEndingWith<Select>("orderby");
            Anchor protonMReadMoreButton = App.ElementCreateService.CreateByInnerTextContaining<Anchor>("Read more");
            Anchor addToCartFalcon9 = App.ElementCreateService.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
            Anchor viewCartButton = App.ElementCreateService.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();
            TextField couponCodeTextField = App.ElementCreateService.CreateById<TextField>("coupon_code");
            Button applyCouponButton = App.ElementCreateService.CreateByValueContaining<Button>("Apply coupon");
            Number quantityBox = App.ElementCreateService.CreateByClassContaining<Number>("input-text qty text");
            Div messageAlert = App.ElementCreateService.CreateByClassContaining<Div>("woocommerce-message");
            Button updateCart = App.ElementCreateService.CreateByValueContaining<Button>("Update cart").ToBeClickable();

            // 1. Instead of the regular button, we create the ExtendedButton, this way we can use its new methods.
            ExtendedButton proceedToCheckout = App.ElementCreateService.CreateByClassContaining<ExtendedButton>("checkout-button button alt wc-forward");
            Heading billingDetailsHeading = App.ElementCreateService.CreateByInnerTextContaining<Heading>("Billing details");
            Span totalSpan = App.ElementCreateService.CreateByXpath<Span>("//*[@class='order-total']//span");

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
            quantityBox.SetNumber(2);
            updateCart.Click();
            App.BrowserService.WaitForAjax();

            totalSpan.ValidateInnerTextIs("114.00€", 15000);

            // 2. Use the new custom method provided by the ExtendedButton class.
            proceedToCheckout.SubmitButtonWithEnter();
            billingDetailsHeading.ToBeVisible().WaitToBe();
        }
    }
}