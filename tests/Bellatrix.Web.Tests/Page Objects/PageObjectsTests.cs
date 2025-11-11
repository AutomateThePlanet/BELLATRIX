using Bellatrix.DynamicTestCases.AzureDevOps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests;

[TestClass]
[Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
[AzureDevOpsDynamicTestCaseAttribute(AreaPath = "AutomateThePlanet", IterationPath = "AutomateThePlanet", RequirementId = "482")]
////[DynamicTestCase(SuiteId = "8260474")]
public class PageObjectsTests : MSTest.WebTest
{
    [TestMethod]
    ////[DynamicTestCase(
    ////    TestCaseId = "4d001440-bf6c-4a8b-b3e6-796cbad361e1",
    ////    Description = "Create a purchase of a rocket through the online rocket shop https://demos.bellatrix.solutions/")]
    [AzureDevOpsDynamicTestCaseAttribute]
    public void PurchaseRocketWithoutPageObjects20()
    {
        App.TestCases.AddPrecondition($"Navigate to https://demos.bellatrix.solutions/");
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        Select sortDropDown = App.Components.CreateByNameEndingWith<Select>("orderby");
        Anchor protonMReadMoreButton = App.Components.CreateByInnerTextContaining<Anchor>("Read more");
        Anchor addToCartFalcon9 = App.Components.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
        Anchor viewCartButton = App.Components.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();

        sortDropDown.SelectByText("Sort by price: low to high");
        protonMReadMoreButton.Hover();
        addToCartFalcon9.Focus();
        addToCartFalcon9.Click();
        viewCartButton.Click();

        TextField couponCodeTextField = App.Components.CreateById<TextField>("coupon_code");
        Button applyCouponButton = App.Components.CreateByValueContaining<Button>("Apply coupon");
        Div messageAlert = App.Components.CreateByClassContaining<Div>("woocommerce-message");
        Anchor proceedToCheckout = App.Components.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");

        couponCodeTextField.SetText("happybirthday");
        applyCouponButton.Click();
        messageAlert.ToHasContent().ToBeVisible().WaitToBe();
        messageAlert.ValidateInnerTextIs("Coupon code applied successfully.");

        App.Browser.WaitForAjax();
        proceedToCheckout.Click();

        Heading billingDetailsHeading = App.Components.CreateByInnerTextContaining<Heading>("Billing details");
        Anchor showLogin = App.Components.CreateByInnerTextContaining<Anchor>("Click here to login");
        TextArea orderCommentsTextArea = App.Components.CreateById<TextArea>("order_comments");
        TextField billingFirstName = App.Components.CreateById<TextField>("billing_first_name");
        TextField billingLastName = App.Components.CreateById<TextField>("billing_last_name");
        TextField billingCompany = App.Components.CreateById<TextField>("billing_company");
        Select billingCountry = App.Components.CreateById<Select>("billing_country");
        TextField billingAddress1 = App.Components.CreateById<TextField>("billing_address_1");
        TextField billingAddress2 = App.Components.CreateById<TextField>("billing_address_2");
        TextField billingCity = App.Components.CreateById<TextField>("billing_city");
        Select billingState = App.Components.CreateById<Select>("billing_state").ToBeVisible().ToBeClickable();
        TextField billingZip = App.Components.CreateById<TextField>("billing_postcode");
        Phone billingPhone = App.Components.CreateById<Phone>("billing_phone");
        Email billingEmail = App.Components.CreateById<Email>("billing_email");
        CheckBox createAccountCheckBox = App.Components.CreateById<CheckBox>("createaccount");
        RadioButton checkPaymentsRadioButton = App.Components.CreateByAttributesContaining<RadioButton>("for", "payment_method_cheque");

        billingDetailsHeading.ToBeVisible().WaitToBe();
        showLogin.ValidateHrefIs("https://demos.bellatrix.solutions/checkout/#");
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

        ////Assert.Fail("Ops");
    }

    [TestMethod]
    ////[DynamicTestCase(TestCaseId = "1999033957")]
    [AzureDevOpsDynamicTestCaseAttribute(TestCaseId = "487")]
    public void PurchaseRocketWithPageObjects()
    {
        var homePage = App.GoTo<HomePage>();
        homePage.FilterProducts(ProductFilter.Popularity);
        homePage.AddProductById(28);
        homePage.ViewCartButton.Click();

        var cartPage = App.Create<CartPage>();
        cartPage.ApplyCoupon("happybirthday");
        ////cartPage.UpdateProductQuantity(1, 2);
        ////cartPage.AssertTotalPrice("114.00");
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