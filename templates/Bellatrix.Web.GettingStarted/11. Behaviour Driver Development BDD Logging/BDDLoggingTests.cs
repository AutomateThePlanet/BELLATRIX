using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, Lifecycle.RestartEveryTime)]
    public class BDDLoggingTests : MSTest.WebTest
    {
        // 1. There cases when you need to show your colleagues or managers what tests do you have.
        // Sometimes you may have manual test cases, but their maintenance and up-to-date state are questionable.
        // Also, many times you need additional work to associate the tests with the test cases.
        // Some frameworks give you a way to write human readable tests through the Gherkin language.
        // The main idea is non-technical people to write these tests. However, we believe this approach is doomed.
        // Or it is doable only for simple tests.
        // This is why in BELLATRIX we built a feature that generates the test cases after the tests execution.
        // After each action or assertion, a new entry is logged.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PurchaseRocketWithLogs()
        {
            // 2. In the testFrameworkSettings.json file find a section called logging, responsible for controlling the BDD logs generation.
            //  "loggingSettings": {
            //      "isEnabled": "true",
            //      "isConsoleLoggingEnabled": "true",
            //      "isDebugLoggingEnabled": "true",
            //      "isEventLoggingEnabled": "false",
            //      "isFileLoggingEnabled": "true",
            //      "outputTemplate":  "{Message:lj}{NewLine}",
            //      "addUrlToBddLogging": "false"
            //  }
            //
            // You can disable the logs entirely. There are different places where the logs are populated.
            // By default, you can see the logs in the output window of each test.
            // Also, a file called logs.txt is generated in the folder with the DLLs of your tests.
            // If you execute your tests in CI with some CLI test runner the logs are printed there as well.
            // outputTemplate - controls how the message is formatted. You can add additional info such as timestamp and much more.
            // for more info visit- https://github.com/serilog/serilog/wiki/Formatting-Output
            // If addUrlToBddLogging is true, after each action the current page's URL will be added.
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 3. As mentioned before BELLATRIX searches for elements not immediately but after you perform an action or assert.
            // This is why we can place all elements and later perform actions on them. It is possible at the moment of declaring them,
            // not to be yet present on the page.
            // Home page elements
            Select sortDropDown = App.ComponentCreateService.CreateByNameEndingWith<Select>("orderby");
            Anchor protonMReadMoreButton = App.ComponentCreateService.CreateByInnerTextContaining<Anchor>("Read more");
            Anchor addToCartFalcon9 = App.ComponentCreateService.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
            Anchor viewCartButton = App.ComponentCreateService.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();

            // Home Page actions
            sortDropDown.SelectByText("Sort by price: low to high");
            protonMReadMoreButton.Hover();
            addToCartFalcon9.Focus();
            addToCartFalcon9.Click();
            viewCartButton.Click();

            // Cart page elements
            TextField couponCodeTextField = App.ComponentCreateService.CreateById<TextField>("coupon_code");
            Button applyCouponButton = App.ComponentCreateService.CreateByValueContaining<Button>("Apply coupon");
            Div messageAlert = App.ComponentCreateService.CreateByClassContaining<Div>("woocommerce-message");
            Number quantityBox = App.ComponentCreateService.CreateByClassContaining<Number>("input-text qty text");
            Button updateCart = App.ComponentCreateService.CreateByValueContaining<Button>("Update cart").ToBeClickable();
            Span totalSpan = App.ComponentCreateService.CreateByXpath<Span>("//*[@class='order-total']//span");
            Anchor proceedToCheckout = App.ComponentCreateService.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");

            // Cart page actions
            couponCodeTextField.SetText("happybirthday");
            applyCouponButton.Click();
            messageAlert.ToHasContent().ToBeVisible().WaitToBe();
            messageAlert.ValidateInnerTextIs("Coupon code applied successfully.");
            App.BrowserService.WaitForAjax();
            totalSpan.ValidateInnerTextIs("54.00€");
            proceedToCheckout.Click();

            // Checkout page elements
            Heading billingDetailsHeading = App.ComponentCreateService.CreateByInnerTextContaining<Heading>("Billing details");
            Anchor showLogin = App.ComponentCreateService.CreateByInnerTextContaining<Anchor>("Click here to login");
            TextArea orderCommentsTextArea = App.ComponentCreateService.CreateById<TextArea>("order_comments");
            TextField billingFirstName = App.ComponentCreateService.CreateById<TextField>("billing_first_name");
            TextField billingLastName = App.ComponentCreateService.CreateById<TextField>("billing_last_name");
            TextField billingCompany = App.ComponentCreateService.CreateById<TextField>("billing_company");
            Select billingCountry = App.ComponentCreateService.CreateById<Select>("billing_country");
            TextField billingAddress1 = App.ComponentCreateService.CreateById<TextField>("billing_address_1");
            TextField billingAddress2 = App.ComponentCreateService.CreateById<TextField>("billing_address_2");
            TextField billingCity = App.ComponentCreateService.CreateById<TextField>("billing_city");
            Select billingState = App.ComponentCreateService.CreateById<Select>("billing_state").ToBeVisible().ToBeClickable();
            TextField billingZip = App.ComponentCreateService.CreateById<TextField>("billing_postcode");
            Phone billingPhone = App.ComponentCreateService.CreateById<Phone>("billing_phone");
            Email billingEmail = App.ComponentCreateService.CreateById<Email>("billing_email");
            CheckBox createAccountCheckBox = App.ComponentCreateService.CreateById<CheckBox>("createaccount");
            RadioButton checkPaymentsRadioButton = App.ComponentCreateService.CreateByAttributesContaining<RadioButton>("for", "payment_method_cheque");

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

            // 4. After the test is executed the following log is created:
            //
            //  #### Start Chrome on PORT = 34079
            //  Start Test
            //  Class = BDDLoggingTests Name = PurchaseRocketWithLogs
            //  Select 'Sort by price: low to high' from control (Name ending with orderby)
            //  Hover control (InnerText containing Read more)
            //  Focus control (data-product_id = 28)
            //  Click control (data-product_id = 28)
            //  Click control (Class = added_to_cart wc-forward)
            //  Type 'happybirthday' into control (ID = coupon_code)
            //  Click control (Value containing Apply coupon)
            //  Validate control (Class = woocommerce-message) inner text is 'Coupon code applied successfully.'
            //  Set '0' into control (Class = input-text qty text)
            //  Set '2' into control (Class = input-text qty text)
            //  Click control (Value containing Update cart)
            //  Validate control (XPath = //*[@class='order-total']//span) inner text is '95.00€'
            //  Click control (Class = checkout-button button alt wc-forward)
            //  Validate control (InnerText containing Click here to login) href is 'http://demos.bellatrix.solutions/checkout/#'
            //  Validate control (InnerText containing Click here to login) CSS class is 'showlogin'
            //  Scroll to visible control (ID = order_comments)
            //  Type 'Please send the rocket to my door step! And don't use the elevator, they don't like when it is not clean...' into control (ID = order_comments)
            //  Type 'In' into control (ID = billing_first_name)
            //  Type 'Deepthought' into control (ID = billing_last_name)
            //  Type 'Automate The Planet Ltd.' into control (ID = billing_company)
            //  Select 'Bulgaria' from control (ID = billing_country)
            //  Validate control (ID = billing_address_1) placeholder is 'House number and street name'
            //  Type 'bul. Yerusalim 5' into control (ID = billing_address_1)
            //  Type 'bul. Yerusalim 6' into control (ID = billing_address_2)
            //  Type 'Sofia' into control (ID = billing_city)
            //  Select 'Sofia-Grad' from control (ID = billing_state)
            //  Type '1000' into control (ID = billing_postcode)
            //  Type '+00359894646464' into control (ID = billing_phone)
            //  Type 'info@bellatrix.solutions' into control (ID = billing_email)
            //  Check control (ID = createaccount)
            //  Click control (for = payment_method_cheque)

            // 5. You can notice that since we use Validate assertions not the regular one they also present in the log:
            //  Validate control (XPath = //*[@class='order-total']//span) inner text is '95.00€'

            // 6. There are two specifics about the generation of the logs. If page objects are used, which are discussed in next chapters.
            // Two things change.
            // 1. Instead of locators, the exact names of the element properties are printed.
            // 2. Instead of page URL, the name of the page is displayed.
        }
    }
}