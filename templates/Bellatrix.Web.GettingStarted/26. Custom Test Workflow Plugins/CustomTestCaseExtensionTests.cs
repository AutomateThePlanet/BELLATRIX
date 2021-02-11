using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, Lifecycle.RestartEveryTime)]
    public class CustomTestCaseExtensionTests : MSTest.WebTest
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

        [TestMethod]
        [TestCategory(Categories.CI)]
        [ManualTestCase(1532)]
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
            Button proceedToCheckout = App.ElementCreateService.CreateByClassContaining<Button>("checkout-button button alt wc-forward");
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
            totalSpan.ValidateInnerTextIs("54.00€");
            proceedToCheckout.Click();

            proceedToCheckout.Click();
            billingDetailsHeading.ToBeVisible().WaitToBe();
        }
    }
}