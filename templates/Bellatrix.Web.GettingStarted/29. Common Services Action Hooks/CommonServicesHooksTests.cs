using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, Lifecycle.RestartEveryTime)]
    public class CommonServicesHooksTests : MSTest.WebTest
    {
        // 1. Another way to extend BELLATRIX is to use the common services hooks. This is how the failed tests analysis works.
        // Here is a list of all common services event you can subscribe to:
        //
        // NavigationService - UrlNotNavigatedEvent, called if the WaitForPartialUrl throws exception
        // ElementWaitService - OnElementNotFulfillingWaitConditionEvent, called if the Wait method throws exception (it is used in all WaitToBe classes.
        //
        // Also, the base class for all web elements- Element provides a few special events as well:
        // ScrollingToVisible - called before scrolling
        // ScrolledToVisible - called after scrolling
        // SettingAttribute - called before setting a web attribute
        // AttributeSet = called after setting a value to a web attribute
        // CreatingElement - called before creating the element
        // CreatedElement - called after the creation of the element
        // CreatingElements - called before the creation of nested element
        // CreatedElements - called after the creation of nested element
        // ReturningWrappedElement - called before searching for native WebDriver element
        //
        // To add custom logic to the element's methods you can create a class that derives from ElementEventHandlers. The override the methods you like.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PurchaseRocketWithGloballyOverridenMethods()
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
            Anchor proceedToCheckout = App.ComponentCreateService.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");
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

            proceedToCheckout.Click();
            billingDetailsHeading.ToBeVisible().WaitToBe();
        }
    }
}