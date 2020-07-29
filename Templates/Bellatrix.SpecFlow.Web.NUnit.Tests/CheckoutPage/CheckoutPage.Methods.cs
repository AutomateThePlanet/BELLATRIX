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
    public partial class CheckoutPage : NavigatablePage
    {
        public override string Url => "http://demos.bellatrix.solutions/checkout/";

        public void FillBillingInfo(BillingInfo billingInfo)
        {
            OrderCommentsTextArea.SetText(billingInfo.OrderCommentsTextArea);
            BillingFirstName.SetText(billingInfo.FirstName);
            BillingLastName.SetText(billingInfo.LastName);
            BillingCompany.SetText(billingInfo.Company);
            BillingCountry.SelectByText(billingInfo.Country);
            BillingAddress1.SetText(billingInfo.Address1);
            BillingAddress2.SetText(billingInfo.Address2);
            BillingCity.SetText(billingInfo.City);
            BillingState.SelectByText(billingInfo.State);
            BillingZip.SetText(billingInfo.Zip);
            BillingPhone.SetPhone(billingInfo.Phone);
            BillingEmail.SetEmail(billingInfo.Email);
            if (billingInfo.ShouldCreateAccount)
            {
                CreateAccountCheckBox.Check();
            }
        }
    }
}