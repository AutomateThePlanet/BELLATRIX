using Bellatrix.Web.GettingStarted._12._Page_Objects;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class PageObjectsTests : NUnit.WebTest
{
    // 1. As you most probably noticed this is like the 4th time we use almost the same elements and logic inside our tests.
    // Similar test writing approach leads to unreadable and hard to maintain tests.
    // Because of that people use the so-called Page Object design pattern to reuse their elements and pages' logic.
    // BELLATRIX comes with powerful built-in page objects which are much more readable and maintainable than regular vanilla WebDriver ones.

    // 2. On most pages, you need to define elements. Placing them in a single place makes the changing of the locators easy.
    // It is a matter of choice whether to have action methods or not. If you use the same combination of same actions against a group of elements then
    // it may be a good idea to wrap them in a page object action method. In our example, we can wrap the filling the billing info such a method.
    //
    // 3. In the assertions file, we may place some predefined Validate methods. For example, if you always check the same email or title of a page,
    // there is no need to hardcode the string in each test. Later if the title is changed, you can do it in a single place.
    // The same is true about most of the things you can assert in your tests.
    //
    // This is the same test that doesn't use page objects.
    [Test]
    [Category(Categories.CI)]
    public void PurchaseRocketWithoutPageObjects()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");
        // Home page elements
        Select sortDropDown = App.Components.CreateByNameEndingWith<Select>("orderby");
        Anchor protonMReadMoreButton = App.Components.CreateByInnerTextContaining<Anchor>("Read more");
        Anchor addToCartFalcon9 = App.Components.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
        Anchor viewCartButton = App.Components.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();

        // Home Page actions
        sortDropDown.SelectByText("Sort by price: low to high");
        protonMReadMoreButton.Hover();
        addToCartFalcon9.Focus();
        addToCartFalcon9.Click();
        viewCartButton.Click();

        // Cart page elements
        TextField couponCodeTextField = App.Components.CreateById<TextField>("coupon_code");
        Button applyCouponButton = App.Components.CreateByValueContaining<Button>("Apply coupon");
        Div messageAlert = App.Components.CreateByClassContaining<Div>("woocommerce-message");
        Number quantityBox = App.Components.CreateByClassContaining<Number>("input-text qty text");
        Button updateCart = App.Components.CreateByValueContaining<Button>("Update cart").ToBeClickable();
        Span totalSpan = App.Components.CreateByXpath<Span>("//*[@class='order-total']//span");
        Anchor proceedToCheckout = App.Components.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");

        // Cart page actions
        couponCodeTextField.SetText("happybirthday");
        applyCouponButton.Click();
        messageAlert.ToHasContent().ToBeVisible().WaitToBe();
        messageAlert.ValidateInnerTextIs("Coupon code applied successfully.");
        App.Browser.WaitForAjax();
        totalSpan.ValidateInnerTextIs("54.00€");
        proceedToCheckout.Click();

        // Checkout page elements
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

        // Checkout page actions
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
    }

    [Test]
    public void PurchaseRocketWithPageObjects()
    {
        // 6. You can use the App GoTo method to navigate to the page and gets an instance of it.
        var homePage = App.GoTo<HomePage>();

        // 7. After you have the instance, you can directly start using the action methods of the page.
        // As you can see the test became much shorter and more readable.
        // The additional code pays off in future when changes are made to the page, or you need to reuse some of the methods.
        homePage.FilterProducts(ProductFilter.Popularity);
        homePage.AddProductById(28);
        homePage.ViewCartButton.Click();

        // 8. Navigate to the shopping cart page by clicking the view cart button, so we do not have to call the GoTo method.
        // But we still need an instance. We can get only an instance of the page through the App Create method.
        var cartPage = App.Create<CartPage>();

        // 9. Removing all elements and some implementation details from the test made it much more clear and readable.
        // This is one of the strategies to follow for long-term successful automated testing.
        cartPage.ApplyCoupon("happybirthday");
        cartPage.UpdateProductQuantity(1, 2);
        cartPage.AssertTotalPrice("114.00");
        cartPage.ProceedToCheckout.Click();

        // You can move the creation of the data objects in a separate factory method or class.
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