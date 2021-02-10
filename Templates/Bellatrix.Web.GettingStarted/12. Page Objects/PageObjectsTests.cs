using Bellatrix.Web.GettingStarted._12._Page_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, Lifecycle.RestartEveryTime)]
    public class PageObjectsTests : MSTest.WebTest
    {
          // 1. As you most probably noticed this is like the 4th time we use almost the same elements and logic inside our tests.
        // Similar test writing approach leads to unreadable and hard to maintain tests.
        // Because of that people use the so-called Page Object design pattern to reuse their elements and pages' logic.
        // BELLATRIX comes with powerful built-in page objects which are much more readable and maintainable than regular vanilla WebDriver ones.

        // 2. To create a new page object, you have a couple of options. You can create it manually. However, why wasting time?
        // BELLATRIX comes with ready-to-go page object templates. How to create a new page object?
        // 2.1. Create a new folder for your page and name it properly.
        // 2.2. Open the context menu and click 'New Item...'
        // 2.3. Choose one of the 3 web page objects templates
        // - Bellatrix-AssertedNavigatableWebPage - contains 3 files- one for actions, one for element declarations and one for assertions (all of them make one-page object)
        // - Bellatrix-NavigatableWebPage- one for actions and one for elements (all of them make a one-page object)
        // - Bellatrix-WebPage- one for actions and one for elements (all of them make a one-page object), don't have methods for navigation
        //
        // 3. On most pages, you need to define elements. Placing them in a single place makes the changing of the locators easy.
        // It is a matter of choice whether to have action methods or not. If you use the same combination of same actions against a group of elements then
        // it may be a good idea to wrap them in a page object action method. In our example, we can wrap the filling the billing info such a method.
        //
        // 4. In the assertions file, we may place some predefined Validate methods. For example, if you always check the same email or title of a page,
        // there is no need to hardcode the string in each test. Later if the title is changed, you can do it in a single place.
        // The same is true about most of the things you can assert in your tests.
        //
        // 5. There are navigatable, and non-navigatable page objects since some pages are only part of a workflow, and you access them not via URL but
        // after clicking some link or button. The same is valid if you work with single page applications.
        //
        // This is the same test that doesn't use page objects.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PurchaseRocketWithoutPageObjects()
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
            App.BrowserService.WaitForAjax();
            messageAlert.ToHasContent().ToBeVisible().WaitToBe();
            messageAlert.ValidateInnerTextIs("Coupon code applied successfully.");

            quantityBox.SetNumber(2);
            updateCart.Click();
            App.BrowserService.WaitForAjax();
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
}