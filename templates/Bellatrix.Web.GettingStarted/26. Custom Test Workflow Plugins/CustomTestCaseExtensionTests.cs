using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
[Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
[Browser(OS.OSX, BrowserType.Safari, Lifecycle.RestartEveryTime)]
public class CustomTestCaseExtensionTests : NUnit.WebTest
{
    // 1. Once we created the test workflow plugin, we need to add it to the existing test workflow.
    // It is done using the App service's method AddPlugin.
    // It doesn't need to be added multiple times as will happen here with the TestInit method.
    // Usually this is done in the TestsInitialize file in the AssemblyInitialize method.
    //
    //  public static void AssemblyInitialize(TestContext testContext)
    //  {
    //      App.AddPlugin<AssociatedTestCaseExtension>();
    //  }
    public override void TestInit()
    {
        // App.AddPlugin<AssociatedTestCaseExtension>();
    }

    [Test]
    [Category(Categories.CI)]
    [ManualTestCase(1532)]
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

        billingDetailsHeading.ToBeVisible().WaitToBe();
    }
}