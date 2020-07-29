// <copyright file="PageObjectsSteps.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
// Licensed under the Royalty-free End-user License Agreement, Version 1.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://bellatrix.solutions/licensing-royalty-free/
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Web.Tests
{
    [Binding]
    public class PageObjectsSteps : WebSteps
    {
        private HomePage _homePage;
        private CartPage _cartPage;
        private CheckoutPage _checkoutPage;

        public PageObjectsSteps()
        {
        }

        [When(@"I navigate to home page")]
        public void WhenINavigateHomePage()
        {
            _homePage = App.GoTo<HomePage>();
        }

        [When(@"I filter products by popularity")]
        public void WhenIFilterProductsByPopularity()
        {
            _homePage.FilterProducts(ProductFilter.Popularity);
        }

        [When(@"I filter products by price")]
        public void WhenIFilterProductsByPrice()
        {
            _homePage.FilterProducts(ProductFilter.Price);
        }

        [When(@"I add product by ID = (.*)")]
        public void WhenIAddProductById(int productId)
        {
            _homePage.AddProductById(productId);
        }

        [When(@"I click view cart button")]
        public void WhenIClickViewCartButton()
        {
            _homePage.ViewCartButton.Click();
            _cartPage = App.Create<CartPage>();
        }

        [When(@"I apply coupon (.*)")]
        public void WhenIApplyCoupon(string code)
        {
            _cartPage.ApplyCoupon(code);
        }

        [When(@"I update product (.*) quantity to (.*)")]
        public void WhenIUpdateProductQuantity(int product, int newQuantity)
        {
            _cartPage.UpdateProductQuantity(product, newQuantity);
        }

        [Then(@"I assert total price is equal to (.*)")]
        public void WhenIAssertTotalPrice(string expectedPrice)
        {
            _cartPage.AssertTotalPrice(expectedPrice);
        }

        [When(@"I click proceed to checkout button")]
        public void WhenIClickProceedToCheckout()
        {
            _cartPage.ProceedToCheckout.Click();
            _checkoutPage = App.Create<CheckoutPage>();
        }

        [When(@"I set first name = (.*)")]
        public void WhenISetFirstName(string firstName)
        {
            _checkoutPage.BillingFirstName.SetText(firstName);
        }

        [When(@"I set last name = (.*)")]
        public void WhenISetLastName(string lastName)
        {
            _checkoutPage.BillingLastName.SetText(lastName);
        }

        [When(@"I set company = (.*)")]
        public void WhenISetCompany(string company)
        {
            _checkoutPage.BillingCompany.SetText(company);
        }

        [When(@"I set country = (.*)")]
        public void WhenISetCountry(string country)
        {
            _checkoutPage.BillingCountry.SelectByText(country);
        }

        [When(@"I set address 1 = (.*)")]
        public void WhenISetAddress1(string address1)
        {
            _checkoutPage.BillingAddress1.SetText(address1);
        }

        [When(@"I set address 2 = (.*)")]
        public void WhenISetAddress2(string address2)
        {
            _checkoutPage.BillingAddress2.SetText(address2);
        }

        [When(@"I set city = (.*)")]
        public void WhenISetCity(string city)
        {
            _checkoutPage.BillingCity.SetText(city);
        }

        [When(@"I set state = (.*)")]
        public void WhenISetState(string state)
        {
            _checkoutPage.BillingState.SelectByText(state);
        }

        [When(@"I set zip = (.*)")]
        public void WhenISetZip(string zip)
        {
            _checkoutPage.BillingZip.SetText(zip);
        }

        [When(@"I set phone = (.*)")]
        public void WhenISetPhone(string phone)
        {
            _checkoutPage.BillingPhone.SetPhone(phone);
        }

        [When(@"I set email = (.*)")]
        public void WhenISetEmail(string email)
        {
            _checkoutPage.BillingEmail.SetEmail(email);
        }

        [When(@"I create a new account")]
        public void WhenICreateNewAccount()
        {
            _checkoutPage.CreateAccountCheckBox.Check();
        }

        [When(@"I add  order comments = (.*)")]
        public void WhenIAddOrdersComments(string comments)
        {
            _checkoutPage.OrderCommentsTextArea.SetText(comments);
        }

        [When(@"I check payments button")]
        public void WhenICheckPaymentsRadioButton()
        {
            _checkoutPage.CheckPaymentsRadioButton.Click();
        }
    }
}
