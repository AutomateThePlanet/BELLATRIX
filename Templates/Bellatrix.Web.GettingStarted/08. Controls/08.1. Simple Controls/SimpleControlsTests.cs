using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, BrowserBehavior.RestartEveryTime)]
    [Browser(OS.OSX, BrowserType.Safari, BrowserBehavior.RestartEveryTime)]
    public class SimpleControlsTests : WebTest
    {
        // 1. As mentioned before BELLATRIX exposes 30+ web controls. All of them implement Proxy design pattern which means that they are not located immediately when
        // they are created. Another benefit is that each of them includes only the actions that you should be able to do with the specific control and nothing more.
        // For example, you cannot type into a button. Moreover, this way all of the actions has meaningful names- Type not SendKeys as in vanilla WebDriver.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PurchaseRocket()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 2. Create methods accept a generic parameter the type of the web control. Then only the methods for this specific control are accessible.
            // Here we tell BELLATRIX to find your element by name attribute ending with 'orderby'.
            Select sortDropDown = App.ElementCreateService.CreateByNameEndingWith<Select>("orderby");

            // 3. You can select from select inputs by text (SelectByText) or index (SelectByIndex)).
            // Also, you can get the selected option through GetSelected method.
            //    <select name="orderby" class="orderby">
            //       <option value="popularity" selected="selected">Sort by popularity</option>
            //       <option value="rating">Sort by average rating</option>
            //       <option value="date">Sort by newness</option>
            //       <option value="price">Sort by price: low to high</option>
            //       <option value="price-desc">Sort by price: high to low</option>
            //    </select>
            sortDropDown.SelectByText("Sort by price: low to high");

            // 4. Here BELLATRIX finds the first anchor element which has inner text containing the 'Read more' text.
            // <a href='http://demos.bellatrix.solutions/product/proton-m/'>Read more</a>
            Anchor protonMReadMoreButton = App.ElementCreateService.CreateByInnerTextContaining<Anchor>("Read more");

            // 5. You can Hover and Focus on most web elements. Also, can invoke Click on anchors.
            protonMReadMoreButton.Hover();

            // 6. Locate elements by custom attribute. Also, bellow BELLATRIX waits till the anchor is clickable before doing any actions.
            // <a href="/?add-to-cart=28" data-product_id="28">Add to cart</a>
            Anchor addToCartFalcon9 = App.ElementCreateService.CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
            addToCartFalcon9.Focus();
            addToCartFalcon9.Click();

            // 7. Find the anchor by class 'added_to_cart wc-forward' and wait for the element again to be clickable.
            // <a href="http://demos.bellatrix.solutions/cart/" class="added_to_cart wc-forward" title="View cart">View cart</a>
            Anchor viewCartButton = App.ElementCreateService.CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();
            viewCartButton.Click();

            // 8. Find a regular input text element by id = 'coupon_code'.
            TextField couponCodeTextField = App.ElementCreateService.CreateById<TextField>("coupon_code");

            // 9. Instead of using vanilla WebDriver SendKeys to set the text, use the SetText method.
            couponCodeTextField.SetText("happybirthday");

            // 10. Create a button control by value attribute containing the text 'Apply coupon'.
            // <input type="submit" class="button" name="apply_coupon" value="Apply coupon">
            // Button can be any of the following web elements- input button, input submit or button.
            Button applyCouponButton = App.ElementCreateService.CreateByValueContaining<Button>("Apply coupon");
            applyCouponButton.Click();

            Div messageAlert = App.ElementCreateService.CreateByClassContaining<Div>("woocommerce-message");

            // 11. Wait for the message DIV to show up and have some content.
            // <div class="woocommerce-message" role="alert">Coupon code applied successfully.</div>
            messageAlert.ToHasContent().ToBeVisible().WaitToBe();

            // 12. Sometimes you need to verify the content of some element. However, since the asynchronous nature of websites,
            // the text or event may not happen immediately. This makes the simple Assert methods + vanilla WebDriver useless.
            // The commented code fails 1 from 5 times.
            ////Assert.AreEqual("Coupon code applied successfully.", messageAlert.InnerText);

            // To handle these situations, BELLATRIX has hundreds of Validate methods that wait for some condition to happen before asserting.
            // Bellow the statement waits for the specific text to appear and assert it.
            // Note: There are much more details about these methods in the next chapters.
            messageAlert.ValidateInnerTextIs("Coupon code applied successfully.");

            // 13. Find the number element by class 'input-text qty text'.
            // <input type="number" id="quantity_5ad35e76b34a2" step="1" min="0" max="" value="1" size="4" pattern="[0-9]*" inputmode="numeric">
            Number quantityBox = App.ElementCreateService.CreateByClassContaining<Number>("input-text qty text");

            // 14. For numbers elements, you can set the number and get most of the properties of these elements.
            App.BrowserService.WaitForAjax();
            quantityBox.SetNumber(2);
            Button updateCart = App.ElementCreateService.CreateByValueContaining<Button>("Update cart").ToBeClickable();
            updateCart.Click();
            App.BrowserService.WaitForAjax();

            Span totalSpan = App.ElementCreateService.CreateByXpath<Span>("//*[@class='order-total']//span");

            // 15. The same as the case with the DIV here we wait/assert for the total price SPAN to get updated.
            ////Assert.AreEqual("114.00€", totalSpan.InnerText);
            totalSpan.ValidateInnerTextIs("114.00€", 15000);

            Anchor proceedToCheckout = App.ElementCreateService.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");
            proceedToCheckout.Click();

            // 16. As mentioned before, BELLATRIX has special synchronisation mechanism for locating elements, so usually, there is no need to wait for specific
            // elements to appear on the page. However, there may be some rare cases when you need to do it.
            // Bellow the statement finds the heading by its inner text containing the text 'Billing details'.
            Heading billingDetailsHeading = App.ElementCreateService.CreateByInnerTextContaining<Heading>("Billing details");

            // Wait for the heading with the above text to be visible. This means that the correct page is loaded.
            billingDetailsHeading.ToBeVisible().WaitToBe();

            Anchor showLogin = App.ElementCreateService.CreateByInnerTextContaining<Anchor>("Click here to login");

            // 17. All web controls have multiple properties for their most important attributes and Validate methods for their verification.
            ////Assert.AreEqual("http://demos.bellatrix.solutions/checkout/#", showLogin.Href);
            showLogin.ValidateHrefIs("http://demos.bellatrix.solutions/checkout/#");
            ////Assert.AreEqual("showlogin", showLogin.CssClass);
            showLogin.ValidateCssClassIs("showlogin");

            TextArea orderCommentsTextArea = App.ElementCreateService.CreateById<TextArea>("order_comments");

            // 18. Here we find the order comments text area and since it is below the visible area we scroll down
            // so that it gets visible on the video recordings. Then the text is set.
            orderCommentsTextArea.ScrollToVisible();
            orderCommentsTextArea.SetText("Please send the rocket to my door step! And don't use the elevator, they don't like when it is not clean...");

            TextField billingFirstName = App.ElementCreateService.CreateById<TextField>("billing_first_name");
            billingFirstName.SetText("In");

            TextField billingLastName = App.ElementCreateService.CreateById<TextField>("billing_last_name");
            billingLastName.SetText("Deepthought");

            TextField billingCompany = App.ElementCreateService.CreateById<TextField>("billing_company");
            billingCompany.SetText("Automate The Planet Ltd.");

            Select billingCountry = App.ElementCreateService.CreateById<Select>("billing_country");
            billingCountry.SelectByText("Bulgaria");

            TextField billingAddress1 = App.ElementCreateService.CreateById<TextField>("billing_address_1");

            // 19. Through the Placeholder, you can get the default text of the control.
            Assert.AreEqual("House number and street name", billingAddress1.Placeholder);
            billingAddress1.SetText("bul. Yerusalim 5");

            TextField billingAddress2 = App.ElementCreateService.CreateById<TextField>("billing_address_2");
            billingAddress2.SetText("bul. Yerusalim 6");

            TextField billingCity = App.ElementCreateService.CreateById<TextField>("billing_city");
            billingCity.SetText("Sofia");

            Select billingState = App.ElementCreateService.CreateById<Select>("billing_state").ToBeVisible().ToBeClickable();
            billingState.SelectByText("Sofia-Grad");

            TextField billingZip = App.ElementCreateService.CreateById<TextField>("billing_postcode");
            billingZip.SetText("1000");

            Phone billingPhone = App.ElementCreateService.CreateById<Phone>("billing_phone");

            // 20. Create the special text field control Phone it contains some additional properties unique for this web element.
            billingPhone.SetPhone("+00359894646464");

            Email billingEmail = App.ElementCreateService.CreateById<Email>("billing_email");

            // 21. Here we create the special text field control Email it contains some additional properties unique for this web element.
            billingEmail.SetEmail("info@bellatrix.solutions");

            CheckBox createAccountCheckBox = App.ElementCreateService.CreateById<CheckBox>("createaccount");

            // 22. You can check and uncheck checkboxes.
            createAccountCheckBox.Check();

            // 23. Bellow BELLATRIX finds the first RadioButton with attribute 'for' containing the value 'payment_method_cheque'.
            RadioButton checkPaymentsRadioButton = App.ElementCreateService.CreateByAttributesContaining<RadioButton>("for", "payment_method_cheque");

            // The radio buttons compared to checkboxes cannot be unchecked/unselected.
            checkPaymentsRadioButton.Click();
        }

        // 24. Full list of all supported web controls, their methods and properties:
        // Common controls:
        //
        // Element - By, GetAttribute, SetAttribute, ScrollToVisible, Create, CreateAll, WaitToBe, IsPresent, IsVisible, CssClass, ElementName, PageName
        // GetTitle, GetTabIndex, GetAccessKey, GetStyle, GetDir, GetLang
        //
        // * All other controls have access to the above methods and properties
        //
        // Anchor- Click, Hover, Focus, Href, InnetText, InnetHtml, Target, Rel
        // Button- Click, Hover, Focus, InnetText, Value, IsDisabled
        // CheckBox- Check, Uncheck, Hover, Focus, IsDisabled, Value, IsChecked
        // Div- Hover, InnerText, InnerHtml
        // Headline- Hover, InnerText
        // Image- Hover, Src, LongDesc, Alt, SrcSet, Sizes, Height, Width
        // InputFile- Upload, IsRequired, IsMultiple, Accept
        // Label- Hover, InnerText, InnerHtml, For
        // Option- Innertext, IsDisabled, Value, IsSelected
        // RadioButton- Click, Hover, Value, IsDisabled, IsChecked
        // Reset- Click, Hover, Focus, InnerText, Value, IsDisabled
        // Select- Hover, Focus, GetSelected, GetAllOptions, SelectByText, SelectByIndex, IsDisabled, IsRequired
        // Span- Hover, InnerText, InnerHtml
        // TextArea- GetText, SetText, Hover, Focus, InnerText, IsDisabled, IsAutoComplete, IsReadonly, IsRequired, Placeholder, MaxLength, MinLength, Rows, Cols, SpellCheck, Wrap
        // TextField- SetText, Hover, Focus, InnerText, InnerHtml, IsDisabled, Value, IsAutoComplete, IsReadonly, IsRequired, Placeholder, MaxLength, MinLength, Size
        //
        // Advanced Controls:
        //
        // Color- Hover, Focus, GetColor, SetColor, IsDisabled, IsAutoComplete, IsRequired, Value, List
        // Date- GetDate, SetDate, Hover, Focus, IsDisabled, IsRequired, Value, IsAutoComplete, IsReadonly, Max, Min, Step
        // DateTimeLocal- GetTime, SetTime, Hover, Focus, IsDisabled, Value, IsAutoComplete, IsReadonly, Max, Min, Step
        // Email- GetEmail, SetEmail, Hover, Focus, IsDisabled, Value, IsAutoComplete, IsReadonly, IsRequired, Placeholder, MaxLength, MinLength, Size
        // Month- GetMonth, SetMonth, Hover, Focus, IsDisabled, Value, IsAutoComplete, IsReadonly, IsRequired, Max, Min, Step
        // Number- GetNumber, SetNumber, Hover, Focus, IsDisabled, Value, IsAutoComplete, IsReadonly, IsRequired, Placeholder, Max, Min, Step
        // Output- Hover, InnerText, InnerHtml, For
        // Password- GetPassword, SetPassword, Hover, Focus, IsDisabled, Value, IsAutoComplete, IsReadonly, IsRequired, Placeholder, MaxLenght, MinLenght, Size
        // Phone- GetPhone, SetPhone, Hover, Focus, IsDisabled, Value, IsAutoComplete, IsReadonly, IsRequired, Placeholder, MaxLenght, MinLenght, Size
        // Progress- Max, Value, InnerText
        // Range- GetRange, SetRange, Hover, Focus, IsDisabled, Value, IsAutoComplete, List, IsRequired, Max, Min, Step
        // Search- GetSearch, SetSearch, Hover, Focus, IsDisabled, Value, IsAutoComplete, IsReadonly, IsRequired, Placeholder, MaxLenght, MinLenght, Size
        // Time- GetTime, SetTime, Hover, Focus, IsDisabled, Value, IsAutoComplete, IsReadonly, Max, Min, Step
        // Url- GetUrl, SetUrl, Hover, Focus, IsDisabled, Value, IsAutoComplete, IsReadonly, IsRequired, Placeholder, MaxLenght, MinLenght, Size
        // Week- GetWeek, SetWeek, Hover, Focus, IsDisabled, Value, IsAutoComplete, IsReadonly, Max, Min, Step
        // Table- GetCell, GetColumn, GetHeaderNames, MapTableToObjectList
        // Cell- Focus, Hover, Row, Column, InnerHtml, InnerText
    }
}