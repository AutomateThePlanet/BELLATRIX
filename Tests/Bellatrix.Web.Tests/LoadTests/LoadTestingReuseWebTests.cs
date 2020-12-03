using Bellatrix.Web.LoadTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests
{
    [TestClass]

    // To use the load testing module you need to install the Bellatrix.Web.TestExecutionExtensions.LoadTesting NuGet package.
    // Also, in the TestInitialize.cs you need to add the line- App.UseLoadTesting();
    // Lastly you need to enable HTTP traffic capturing through setting shouldCaptureHttpTraffic to true.
    [Browser(BrowserType.Chrome, BrowserBehavior.RestartEveryTime, shouldCaptureHttpTraffic: true)]
    public class LoadTestingReuseWebTests : WebTest
    {
        // A big problem of most load testing solutions is that your tests get outdated quite fast with each small update
        // of your website. The usual fix is to rewrite all existing tests. To solve this problem,
        // we integrated some of BELLATRIX most powerful features so that each time your web tests are executed,
        // they will update your load tests as well. To mark a web test to be reused for load testing you only
        // need to mark it with the LoadTest attribute and turn on the web requests recording.
        // After that you need to create a Load Test project and create your first load test based on the recordings.
        // Please read the detailed documentation- https://docs.bellatrix.solutions/web-automation/load-testing/
        [TestMethod]
        [TestCategory(Categories.CI)]
        [LoadTest]
        public void ReuseForLoadTesting()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // Home page elements
            Select sortDropDown = App.ElementCreateService.CreateByNameEndingWith<Select>("orderby");
            Anchor protonMReadMoreButton = App.ElementCreateService.CreateByInnerTextContaining<Anchor>("Read more");
            Anchor addToCartFalcon9 = App.ElementCreateService.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
            Anchor viewCartButton = App.ElementCreateService.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();

            // Home Page actions
            sortDropDown.SelectByText("Sort by price: low to high");
            protonMReadMoreButton.Hover();
            addToCartFalcon9.Focus();
            addToCartFalcon9.Click();
            viewCartButton.Click();

            // Cart page elements
            TextField couponCodeTextField = App.ElementCreateService.CreateById<TextField>("coupon_code");
            Button applyCouponButton = App.ElementCreateService.CreateByValueContaining<Button>("Apply coupon");
            Div messageAlert = App.ElementCreateService.CreateByClassContaining<Div>("woocommerce-message");
            Number quantityBox = App.ElementCreateService.CreateByClassContaining<Number>("input-text qty text");
            Button updateCart = App.ElementCreateService.CreateByValueContaining<Button>("Update cart").ToBeClickable();
            Span totalSpan = App.ElementCreateService.CreateByXpath<Span>("//*[@class='order-total']//span");
            Anchor proceedToCheckout = App.ElementCreateService.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");

            // Cart page actions
            couponCodeTextField.SetText("happybirthday");
            applyCouponButton.Click();
            messageAlert.ToHasContent().ToBeVisible().WaitToBe();
            messageAlert.ValidateInnerTextIs("Coupon code applied successfully.");

            App.BrowserService.WaitUntilReady();
            quantityBox.SetNumber(2);
            updateCart.Click();
            App.BrowserService.WaitUntilReady();
            totalSpan.ValidateInnerTextIs("114.00€", 15000);
            proceedToCheckout.Click();

            // Checkout page elements
            Heading billingDetailsHeading = App.ElementCreateService.CreateByInnerTextContaining<Heading>("Billing details");
            Anchor showLogin = App.ElementCreateService.CreateByInnerTextContaining<Anchor>("Click here to login");
            TextArea orderCommentsTextArea = App.ElementCreateService.CreateById<TextArea>("order_comments");
            TextField billingFirstName = App.ElementCreateService.CreateById<TextField>("billing_first_name");
            TextField billingLastName = App.ElementCreateService.CreateById<TextField>("billing_last_name");
            TextField billingCompany = App.ElementCreateService.CreateById<TextField>("billing_company");
            Select billingCountry = App.ElementCreateService.CreateById<Select>("billing_country");
            TextField billingAddress1 = App.ElementCreateService.CreateById<TextField>("billing_address_1");
            TextField billingAddress2 = App.ElementCreateService.CreateById<TextField>("billing_address_2");
            TextField billingCity = App.ElementCreateService.CreateById<TextField>("billing_city");
            Select billingState = App.ElementCreateService.CreateById<Select>("billing_state").ToBeVisible().ToBeClickable();
            TextField billingZip = App.ElementCreateService.CreateById<TextField>("billing_postcode");
            Phone billingPhone = App.ElementCreateService.CreateById<Phone>("billing_phone");
            Email billingEmail = App.ElementCreateService.CreateById<Email>("billing_email");
            CheckBox createAccountCheckBox = App.ElementCreateService.CreateById<CheckBox>("createaccount");
            RadioButton checkPaymentsRadioButton = App.ElementCreateService.CreateByAttributesContaining<RadioButton>("for", "payment_method_cheque");

            // Checkout page actions
            billingDetailsHeading.ToBeVisible().WaitToBe();
            showLogin.ValidateHrefIs("http://demos.bellatrix.solutions/checkout/#");
            showLogin.ValidateCssClassIs("showlogin");
            orderCommentsTextArea.ScrollToVisible();
            orderCommentsTextArea.SetText("Please send the rocket to my door step! And don't use the elevator, they don't like when it is not clean...");
            billingFirstName.SetText("In");
            billingLastName.SetText("Deepthought");
            billingCompany.SetText("Automate The Planet Ltd.");
            billingCountry.SelectByText("Bulgaria");
            billingAddress1.ValidatePlaceholderIs("House number and street name");
            billingAddress1.SetText("bul. Yerusalim 5");
            billingAddress2.SetText("bul. Yerusalim 6");
            billingCity.SetText("Sofia");
            billingState.SelectByText("Sofia-Grad");
            billingZip.SetText("1000");
            billingPhone.SetPhone("+00359894646464");
            billingEmail.SetEmail("info@bellatrix.solutions");
            createAccountCheckBox.Check();
            checkPaymentsRadioButton.Click();
        }

        [TestMethod]
        [LoadTest]
        public void PurchaseRocketWithPageObjects()
        {
            var homePage = App.GoTo<HomePage>();
            homePage.FilterProducts(ProductFilter.Popularity);
            homePage.AddProductById(28);
            homePage.ViewCartButton.Click();

            var cartPage = App.Create<CartPage>();
            cartPage.ApplyCoupon("happybirthday");
            cartPage.UpdateProductQuantity(1, 2);
            cartPage.AssertTotalPrice("114.00");
            cartPage.ProceedToCheckout.Click();

            var billingInfo = new BillingInfo
                                      {
                                          FirstName = "In",
                                          LastName = "Deepthought",
                                          Company = "Automate The Planet Ltd.",
                                          Country = "Bulgaria",
                                          Address1 = "bul. Yerusalim 5",
                                          Address2 = "bul. Yerusalim 6",
                                          City = "Sofia",
                                          State = "Sofia-Grad",
                                          Zip = "1000",
                                          Phone = "+00359894646464",
                                          Email = "info@bellatrix.solutions",
                                          ShouldCreateAccount = true,
                                          OrderCommentsTextArea = "cool product",
                                      };

            var checkoutPage = App.Create<CheckoutPage>();
            checkoutPage.FillBillingInfo(billingInfo);
            checkoutPage.CheckPaymentsRadioButton.Click();
        }
    }
}