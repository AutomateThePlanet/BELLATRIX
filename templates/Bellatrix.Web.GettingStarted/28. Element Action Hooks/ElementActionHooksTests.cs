using Bellatrix.Web.GettingStarted.Advanced._27._Element_Actions_Hooks;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class ElementActionHooksTests : NUnit.WebTest
{
    // 1. Another way to extend BELLATRIX is to use the controls hooks. This is how the BDD logging and highlighting are implemented.
    // For each method of the control, there are two hooks- one that is called before the action and one after.
    // For example, the available hooks for the button are:
    // Clicking - an event executed before button click
    // Clicked - an event executed after the button is clicked
    // Hovering - an event executed before button hover
    // Hovered - an event executed after the button is hovered
    // Focusing - an event executed before button focus
    // Focused - an event executed after the button is focused
    //
    // 2. You need to implement the event handlers for these events and subscribe them.
    // 3. BELLATRIX gives you again a shortcut- you need to create a class and inherit the {ControlName}EventHandlers
    // In the example, DebugLogger is called for each button event printing to Debug window the coordinates of the button.
    // You can call external logging provider, making screenshots before or after each action, the possibilities are limitless.
    //
    // 4. Once you have created the EventHandlers class, you need to tell BELLATRIX to use it. To do so call the App service method
    // Note: Usually, we add element event handlers in the AssemblyInitialize method which is called once for a test run.
    public override void TestsArrange()
    {
        App.AddElementEventHandler<DebugLoggingButtonEventHandlers>();

        // If you need to remove it during the run you can use the method bellow.
        App.RemoveElementEventHandler<DebugLoggingButtonEventHandlers>();

        base.TestsArrange();

        // 5. Each BELLATRIX Validate method gives you a hook too.
        // To implement them you can derive the ValidateExtensionsEventHandlers base class and override the event handler methods you need.
        // For example for the method ValidateIsChecked, ValidatedIsCheckedEvent event is called after the check is done.
    }

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