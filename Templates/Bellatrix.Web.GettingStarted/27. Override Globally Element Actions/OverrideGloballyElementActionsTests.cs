using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, BrowserBehavior.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, BrowserBehavior.RestartEveryTime)]
    public class OverrideGloballyElementActionsTests : WebTest
    {
        public override void TestsArrange()
        {
            // 1. Extendability and customization are one of the biggest advantages of Bellatrix.
            // So, each BELLATRIX web control gives you the possibility to override its behaviour for the whole test run.
            // You need to initialise the static delegates- Override{MethodName}Globally.
            //
            // 2. Below we override the behaviour of the button control with an anonymous lambda function.
            // Instead of using the default webDriverElement.Click() method, we click via JavaScript code.

            ////Button.OverrideClickGlobally = (e) =>
            ////{
            ////    e.ToExists().ToBeClickable().WaitToBe();
            ////    App.JavaScriptService.Execute("arguments[0].click();", e);
            ////};

            // 3. Override the anchor Focus method by assigning a local private function to the global delegate.
            // Note 1: Keep in mind that once the control is overridden globally, all tests call your custom logic, the default behaviour is gone.
            // Note 2: Usually, we assign the control overrides in the AssemblyInitialize method which is called once for a test run.
            Anchor.OverrideFocusGlobally = CustomFocus;

            // 4. Here is a list of all global override Button delegates:
            // OverrideClickGlobally
            // OverrideFocusGlobally
            // OverrideHoverGlobally
            // OverrideInnerTextGlobally
            // OverrideIsDisabledGlobally
            // OverrideValueGlobally
        }

        private void CustomFocus(Anchor anchor)
        {
            App.JavaScriptService.Execute("window.focus();");
            App.JavaScriptService.Execute("arguments[0].focus();", anchor);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PurchaseRocketWithGloballyOverridenMethods()
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
            Anchor proceedToCheckout = App.ElementCreateService.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");
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
            messageAlert.EnsureInnerTextIs("Coupon code applied successfully.");
            App.BrowserService.WaitForAjax();
            quantityBox.SetNumber(2);
            updateCart.Click();

            // The overridden click delegate is called.
            updateCart.Click();
            App.BrowserService.WaitForAjax();

            totalSpan.EnsureInnerTextIs("114.00€", 15000);

            proceedToCheckout.Click();
            billingDetailsHeading.ToBeVisible().WaitToBe();
        }
    }
}