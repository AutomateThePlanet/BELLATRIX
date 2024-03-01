// <copyright file="CommonControlsTests.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests;

[TestClass]
[Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
[Browser(OS.OSX, BrowserType.Safari, Lifecycle.RestartEveryTime)]
public class CommonControlsTests : MSTest.WebTest
{
    // 1. As mentioned before BELLATRIX exposes 30+ web controls. All of them implement Proxy design pattern which means that they are not located immediately when
    // they are created. Another benefit is that each of them includes only the actions that you should be able to do with the specific control and nothing more.
    // For example, you cannot type into a button. Moreover, this way all of the actions has meaningful names- Type not SendKeys as in vanilla WebDriver.
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void PurchaseRocket()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        // 2. Create methods accept a generic parameter the type of the web control. Then only the methods for this specific control are accessible.
        // Here we tell BELLATRIX to find your element by name attribute ending with 'orderby'.
        Select sortDropDown = App.Components.CreateByNameEndingWith<Select>("orderby");

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
        // <a href='https://demos.bellatrix.solutions/product/proton-m/'>Read more</a>
        Anchor protonMReadMoreButton =
            App.Components.CreateByInnerTextContaining<Anchor>("Read more");

        // 5. You can Hover and Focus on most web elements. Also, can invoke Click on anchors.
        protonMReadMoreButton.Hover();

        // 6. Locate elements by custom attribute. Also, bellow BELLATRIX waits till the anchor is clickable before doing any actions.
        // <a href="/?add-to-cart=28" data-product_id="28">Add to cart</a>
        Anchor addToCartFalcon9 = App.Components
            .CreateByAttributesContaining<Anchor>("data-product_id", "28").ToBeClickable();
        addToCartFalcon9.Focus();
        addToCartFalcon9.Click();

        // 7. Find the anchor by class 'added_to_cart wc-forward' and wait for the element again to be clickable.
        // <a href="https://demos.bellatrix.solutions/cart/" class="added_to_cart wc-forward" title="View cart">View cart</a>
        Anchor viewCartButton = App.Components
            .CreateByClassContaining<Anchor>("added_to_cart wc-forward").ToBeClickable();
        viewCartButton.Click();

        // 8. Find a regular input text element by id = 'coupon_code'.
        TextField couponCodeTextField = App.Components.CreateById<TextField>("coupon_code");

        // 9. Instead of using vanilla WebDriver SendKeys to set the text, use the SetText method.
        couponCodeTextField.SetText("happybirthday");

        // 10. Create a button control by value attribute containing the text 'Apply coupon'.
        // <input type="submit" class="button" name="apply_coupon" value="Apply coupon">
        // Button can be any of the following web elements- input button, input submit or button.
        Button applyCouponButton = App.Components.CreateByValueContaining<Button>("Apply coupon");
        applyCouponButton.Click();

        Div messageAlert = App.Components.CreateByClassContaining<Div>("woocommerce-message");

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
        Number quantityBox = App.Components.CreateByClassContaining<Number>("input-text qty text");

        // 14. For numbers elements, you can set the number and get most of the properties of these elements.
        App.Browser.WaitUntilReady();
        quantityBox.SetNumber(2);
        Button updateCart = App.Components.CreateByValueContaining<Button>("Update cart")
            .ToBeClickable();
        updateCart.Click();
        App.Browser.WaitForAjax();

        Span totalSpan = App.Components.CreateByXpath<Span>("//*[@class='order-total']//span");

        // 15. The same as the case with the DIV here we wait/assert for the total price SPAN to get updated.
        ////Assert.AreEqual("114.00€", totalSpan.InnerText);
        totalSpan.ValidateInnerTextIs("114.00€", 15000);

        Anchor proceedToCheckout =
            App.Components.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");
        proceedToCheckout.Click();

        // 16. As mentioned before, BELLATRIX has special synchronisation mechanism for locating elements, so usually, there is no need to wait for specific
        // elements to appear on the page. However, there may be some rare cases when you need to do it.
        // Bellow the statement finds the heading by its inner text containing the text 'Billing details'.
        Heading billingDetailsHeading =
            App.Components.CreateByInnerTextContaining<Heading>("Billing details");

        // Wait for the heading with the above text to be visible. This means that the correct page is loaded.
        billingDetailsHeading.ToBeVisible().WaitToBe();

        Anchor showLogin = App.Components.CreateByInnerTextContaining<Anchor>("Click here to login");

        // 17. All web controls have multiple properties for their most important attributes and Validate methods for their verification.
        ////Assert.AreEqual("https://demos.bellatrix.solutions/checkout/#", showLogin.Href);
        showLogin.ValidateHrefIs("https://demos.bellatrix.solutions/checkout/#");
        ////Assert.AreEqual("showlogin", showLogin.CssClass);
        showLogin.ValidateCssClassIs("showlogin");

        TextArea orderCommentsTextArea = App.Components.CreateById<TextArea>("order_comments");

        // 18. Here we find the order comments text area and since it is below the visible area we scroll down
        // so that it gets visible on the video recordings. Then the text is set.
        orderCommentsTextArea.ScrollToVisible();
        orderCommentsTextArea.SetText(
            "Please send the rocket to my door step! And don't use the elevator, they don't like when it is not clean...");

        TextField billingFirstName = App.Components.CreateById<TextField>("billing_first_name");
        billingFirstName.SetText("In");

        TextField billingLastName = App.Components.CreateById<TextField>("billing_last_name");
        billingLastName.SetText("Deepthought");

        TextField billingCompany = App.Components.CreateById<TextField>("billing_company");
        billingCompany.SetText("Automate The Planet Ltd.");

        Select billingCountry = App.Components.CreateById<Select>("billing_country");
        billingCountry.SelectByText("Bulgaria");

        TextField billingAddress1 = App.Components.CreateById<TextField>("billing_address_1");

        // 19. Through the Placeholder, you can get the default text of the control.
        Assert.AreEqual("House number and street name", billingAddress1.Placeholder);
        billingAddress1.SetText("bul. Yerusalim 5");

        TextField billingAddress2 = App.Components.CreateById<TextField>("billing_address_2");
        billingAddress2.SetText("bul. Yerusalim 6");

        TextField billingCity = App.Components.CreateById<TextField>("billing_city");
        billingCity.SetText("Sofia");

        Select billingState = App.Components.CreateById<Select>("billing_state").ToBeVisible()
            .ToBeClickable();
        billingState.SelectByText("Sofia-Grad");

        TextField billingZip = App.Components.CreateById<TextField>("billing_postcode");
        billingZip.SetText("1000");

        Phone billingPhone = App.Components.CreateById<Phone>("billing_phone");

        // 20. Create the special text field control Phone it contains some additional properties unique for this web element.
        billingPhone.SetPhone("+00359894646464");

        Email billingEmail = App.Components.CreateById<Email>("billing_email");

        // 21. Here we create the special text field control Email it contains some additional properties unique for this web element.
        billingEmail.SetEmail("info@bellatrix.solutions");

        CheckBox createAccountCheckBox = App.Components.CreateById<CheckBox>("createaccount");

        // 22. You can check and uncheck checkboxes.
        createAccountCheckBox.Check();

        // 23. Bellow BELLATRIX finds the first RadioButton with attribute 'for' containing the value 'payment_method_cheque'.
        RadioButton checkPaymentsRadioButton =
            App.Components.CreateByAttributesContaining<RadioButton>("for", "payment_method_cheque");

        // The radio buttons compared to checkboxes cannot be unchecked/unselected.
        checkPaymentsRadioButton.Click();
    }

    [TestMethod]
    public void Table_GetItems()
    {
        App.Navigation.NavigateToLocalPage("TestPages\\Table\\table.html");

        // 24. BELLATRIX gives you API for easing the work with HTML tables.
        // Through the SetColumn you map the headers of the table if for some reason you don't want some column, just don't add it.
        // The method returns a list of all rows' data as C# data mapped to the map you provided.
        var table = App.Components.CreateById<Table>("table1");
        table.SetColumn("Last Name");
        table.SetColumn("First Name");
        table.SetColumn("Email");
        table.SetColumn("Due");
        ////table.SetColumn("Web Site"); // this property won't be asserted if you use the AssertTable method.
        table.SetColumn("Action");

        // In order GetItems to be able to work you need to map the properties to headers through the HeaderName attribute
        // this is how we handle differences between the property name, spaces in the headers and such.
        var dataTableExampleOnes = table.GetItems<DataTableExampleOne>();

        Assert.AreEqual("Smith", dataTableExampleOnes.First().LastName);
        Assert.AreEqual("John", dataTableExampleOnes.First().FirstName);
        Assert.AreEqual("http://www.timconway.com", dataTableExampleOnes.Last().WebSite);
    }

    [TestMethod]
    public void BasicTable_Has_Header()
    {
        App.Navigation.NavigateToLocalPage("TestPages\\Table\\table.html");

        var table = App.Components.CreateById<Table>("table1");
        table.SetColumn("Last Name");
        table.SetColumn("First Name");
        table.SetColumn("Email");
        table.SetColumn("Due");
        table.SetColumn("Web Site");
        table.SetColumn("Action");

        // 25. Returns only the table header names.
        var headerNames = table.GetHeaderNames();

        // 26. You can get a particular cell as BELLATRIX element mentioning the row and column number.
        var tableCell = table.GetCell(3, 1);

        Assert.IsTrue(headerNames.Contains("Due"));
        Assert.AreEqual("$51.00", tableCell.InnerText);
    }

    [TestMethod]
    public void TableWithHeader_GetItems()
    {
        App.Navigation.NavigateToLocalPage("TestPages\\Table\\table.html");

        var table = App.Components.CreateById<Web.Table>("table1");
        table.SetColumn("Last Name");
        table.SetColumn("First Name");
        table.SetColumn("Email");
        table.SetColumn("Due");
        table.SetColumn("Web Site");
        table.SetColumn("Action");

        var dataTableExampleOnes = table.GetItems<DataTableExampleOne>();

        Assert.AreEqual("Smith", dataTableExampleOnes.First().LastName);
        Assert.AreEqual("John", dataTableExampleOnes.First().FirstName);
        Assert.AreEqual("http://www.timconway.com", dataTableExampleOnes.Last().WebSite);
    }

    [TestMethod]
    public void TableWithHeader_Returns_Value()
    {
        App.Navigation.NavigateToLocalPage("TestPages\\Table\\table.html");

        var table = App.Components.CreateById<Web.Table>("table1");
        table.SetColumn("Last Name");
        table.SetColumn("First Name");
        table.SetColumn("Email");
        table.SetColumn("Due");
        table.SetColumn("Web Site");
        table.SetColumn("Action");

        // 28. You can get a particular cell element by row number and header name.
        var tableCell = table.GetCell("Email", 1);

        Assert.AreEqual("fbach@yahoo.com", tableCell.InnerText);
    }
}