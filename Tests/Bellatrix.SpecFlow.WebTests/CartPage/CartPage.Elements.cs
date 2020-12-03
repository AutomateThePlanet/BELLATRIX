// <copyright file="CartPage.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using Bellatrix.Web;

namespace Bellatrix.SpecFlow.Web.Tests
{
    public partial class CartPage
    {
        public TextField CouponCode => Element.CreateById<TextField>("coupon_code");
        public Button ApplyCouponButton => Element.CreateByValueContaining<Button>("Apply coupon");
        public Div MessageAlert => Element.CreateByClassContaining<Div>("woocommerce-message");
        public ElementsList<Number> QuantityBoxes => Element.CreateAllByClassContaining<Number>("input-text qty text");
        public Button UpdateCart => Element.CreateByValueContaining<Button>("Update cart").ToBeClickable();
        public Span TotalSpan => Element.CreateByXpath<Span>("//*[@class='order-total']//span");
        public Anchor ProceedToCheckout => Element.CreateByClassContaining<Anchor>("checkout-button button alt wc-forward");
    }
}