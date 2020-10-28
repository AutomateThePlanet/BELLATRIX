using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, BrowserBehavior.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, BrowserBehavior.RestartEveryTime)]
    public class OverrideLocallyElementActionsTests : WebTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PurchaseRocketWithGloballyOverridenMethods()
        {
            // 1. Extendability and customization are one of the biggest advantages of Bellatrix.
            // So, each BELLATRIX web control gives you the possibility to override its behaviour locally for current test only.
            // You need to initialise the static delegates- Override{MethodName}Locally.
            // This may be useful to make a temporary fix only for certain page where the default behaviour is not working as expected.
            //
            // 2. Below we override the behaviour of the button control with an anonymous lambda function.
            // Instead of using the default webDriverElement.Click() method, we click via JavaScript code.
            Button.OverrideClickLocally = (e) =>
            {
                e.ToExists().ToBeClickable().WaitToBe();
                App.JavaScriptService.Execute("arguments[0].click();", e);
            };

            // 3. Override the element Focus method by assigning a local private function to the local delegate.
            // Note 1: Keep in mind that once the control is overridden locally, after the test's execution the default behaviour is restored.
            // Note 2: In most cases, you can call the local override in some page object, directly in the test or in the TestInit method.
            // Note 3: The local override has precedence over the global override.
            Element.OverrideFocusLocally = CustomFocus;

            // 4. Here is a list of all local override Button delegates:
            // OverrideClickLocally
            // OverrideFocusLocally
            // OverrideHoverLocally
            // OverrideInnerTextLocally
            // OverrideIsDisabledLocally
            // OverrideValueLocally
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

        private void CustomFocus(Element element)
        {
            App.JavaScriptService.Execute("window.focus();");
            App.JavaScriptService.Execute("arguments[0].focus();", element);
        }
    }
}