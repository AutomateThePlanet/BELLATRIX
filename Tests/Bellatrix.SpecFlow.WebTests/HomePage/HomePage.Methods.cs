// <copyright file="HomePage.cs" company="Automate The Planet Ltd.">
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
    public partial class HomePage : NavigatablePage
    {
        public override string Url => "http://demos.bellatrix.solutions/";

        public void FilterProducts(ProductFilter productFilter)
        {
            switch (productFilter)
            {
                case ProductFilter.Popularity:
                    SortDropDown.SelectByText("Sort by popularity");
                    break;
                case ProductFilter.Rating:
                    SortDropDown.SelectByText("Sort by average rating");
                    break;
                case ProductFilter.Date:
                    SortDropDown.SelectByText("Sort by newness");
                    break;
                case ProductFilter.Price:
                    SortDropDown.SelectByText("Sort by price: low to high");
                    break;
                case ProductFilter.PriceDesc:
                    SortDropDown.SelectByText("Sort by price: high to low");
                    break;
            }
        }

        public void AddProductById(int productId)
        {
            var product = Element.CreateByAttributesContaining<Anchor>("data-product_id", productId.ToString()).ToBeClickable();
            product.Click();
            ViewCartButton.ValidateIsVisible();
        }
    }
}