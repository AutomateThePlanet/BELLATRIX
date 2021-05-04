// <copyright file="UrlControlTestsOpera.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls
{
    [TestClass]
    [Browser(BrowserType.Opera, Lifecycle.ReuseIfStarted)]
    [AllureSuite("Url Control")]
    public class UrlControlTestsOpera : MSTest.WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().UrlLocalPage);

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void UrlSet_When_UseSetUrlMethod_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL");

            urlElement.SetUrl("bellatrix.solutions");

            Assert.AreEqual("bellatrix.solutions", urlElement.GetUrl());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetUrlReturnsCorrectUrl_When_DefaultUrlIsSet_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL3");

            Assert.AreEqual("http://www.example.com", urlElement.GetUrl());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL");

            Assert.AreEqual(false, urlElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL5");

            Assert.AreEqual(false, urlElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL4");

            Assert.AreEqual(true, urlElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL4");

            Assert.AreEqual(false, urlElement.IsReadonly);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL6");

            Assert.AreEqual(true, urlElement.IsReadonly);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMaxLengthReturnsNull_When_MaxLengthAttributeIsNotPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL");

            var maxLength = urlElement.MaxLength;

            Assert.IsNull(maxLength);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMinLengthReturnsNull_When_MinLengthAttributeIsNotPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL");

            Assert.IsNull(urlElement.MinLength);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetSizeReturnsDefault20_When_SizeAttributeIsNotPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL");

            // Specifies the width of an <input> element, in characters. Default value is 20
            Assert.AreEqual(20, urlElement.Size);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMaxLengthReturns80_When_MaxLengthAttributeIsPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL2");

            Assert.AreEqual(80, urlElement.MaxLength);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMinLengthReturns10_When_MinLengthAttributeIsPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL2");

            Assert.AreEqual(10, urlElement.MinLength);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetSizeReturns30_When_SizeAttributeIsNotPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL2");

            Assert.AreEqual(30, urlElement.Size);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL4");

            Assert.AreEqual(false, urlElement.IsRequired);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL7");

            Assert.AreEqual(true, urlElement.IsRequired);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetPlaceholder_When_PlaceholderAttributeIsSet_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL");

            Assert.AreEqual("http://www.example.com", urlElement.Placeholder);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetPlaceholderReturnsNull_When_PlaceholderAttributeIsNotPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL1");

            Assert.IsNull(urlElement.Placeholder);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnRed_When_Hover_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL8");

            urlElement.Hover();

            Assert.AreEqual("color: red;", urlElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnBlue_When_Focus_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL9");

            urlElement.Focus();

            Assert.AreEqual("color: blue;", urlElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnFalse_When_DisabledAttributeNotPresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL9");

            bool isDisabled = urlElement.IsDisabled;

            Assert.IsFalse(isDisabled);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnTrue_When_DisabledAttributePresent_Opera()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL10");

            bool isDisabled = urlElement.IsDisabled;

            Assert.IsTrue(isDisabled);
        }
    }
}