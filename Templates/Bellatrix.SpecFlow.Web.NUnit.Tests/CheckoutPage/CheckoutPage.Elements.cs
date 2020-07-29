// <copyright file="CheckoutPage.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web;

namespace Bellatrix.SpecFlow.Web.Tests
{
    public partial class CheckoutPage
    {
        public Heading BillingDetailsHeading => Element.CreateByInnerTextContaining<Heading>("Billing details");
        public Anchor ShowLogin => Element.CreateByInnerTextContaining<Anchor>("Click here to login");
        public TextArea OrderCommentsTextArea => Element.CreateById<TextArea>("order_comments");
        public TextField BillingFirstName => Element.CreateById<TextField>("billing_first_name");
        public TextField BillingLastName => Element.CreateById<TextField>("billing_last_name");
        public TextField BillingCompany => Element.CreateById<TextField>("billing_company");
        public Select BillingCountry => Element.CreateById<Select>("billing_country");
        public TextField BillingAddress1 => Element.CreateById<TextField>("billing_address_1");
        public TextField BillingAddress2 => Element.CreateById<TextField>("billing_address_2");
        public TextField BillingCity => Element.CreateById<TextField>("billing_city");
        public Select BillingState => Element.CreateById<Select>("billing_state").ToBeVisible().ToBeClickable();
        public TextField BillingZip => Element.CreateById<TextField>("billing_postcode");
        public Phone BillingPhone => Element.CreateById<Phone>("billing_phone");
        public Email BillingEmail => Element.CreateById<Email>("billing_email");
        public CheckBox CreateAccountCheckBox => Element.CreateById<CheckBox>("createaccount");
        public RadioButton CheckPaymentsRadioButton => Element.CreateByAttributesContaining<RadioButton>("for", "payment_method_cheque");
    }
}