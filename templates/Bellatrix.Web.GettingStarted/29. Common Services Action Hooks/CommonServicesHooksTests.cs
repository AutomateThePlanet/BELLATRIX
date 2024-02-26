using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class CommonServicesHooksTests : NUnit.WebTest
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
    [Test]
    [Category(Categories.CI)]
    public void PurchaseRocketWithGloballyOverridenMethods()
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
        Anchor proceedToCheckout = App.Components.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");
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

        billingDetailsHeading.ToBeVisible().WaitToBe();
    }
}