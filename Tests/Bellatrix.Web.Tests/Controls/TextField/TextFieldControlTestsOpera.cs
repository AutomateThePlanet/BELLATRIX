// <copyright file="TextFieldControlTestsOpera.cs" company="Automate The Planet Ltd.">
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
    [AllureSuite("TextField Control")]
    public class TextFieldControlTestsOpera : MSTest.WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(SettingsService.GetSection<TestPagesSettings>().TextFieldLocalPage);

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void TextSet_When_UseSetTextMethod_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText");

            textFieldElement.SetText("aangelov@bellatrix.solutions");

            Assert.AreEqual("aangelov@bellatrix.solutions", textFieldElement.Value);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText");

            Assert.AreEqual(false, textFieldElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText5");

            Assert.AreEqual(false, textFieldElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText4");

            Assert.AreEqual(true, textFieldElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText4");

            Assert.AreEqual(false, textFieldElement.IsReadonly);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText6");

            Assert.AreEqual(true, textFieldElement.IsReadonly);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMaxLengthReturnsNull_When_MaxLengthAttributeIsNotPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText");

            var maxLength = textFieldElement.MaxLength;

            Assert.IsNull(maxLength);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMinLengthReturnsNull_When_MinLengthAttributeIsNotPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText");

            Assert.IsNull(textFieldElement.MinLength);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetSizeReturnsDefault20_When_SizeAttributeIsNotPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText");

            // Specifies the width of an <input> element, in characters. Default value is 20
            Assert.AreEqual(20, textFieldElement.Size);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMaxLengthReturns80_When_MaxLengthAttributeIsPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText2");

            Assert.AreEqual(80, textFieldElement.MaxLength);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMinLengthReturns10_When_MinLengthAttributeIsPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText2");

            Assert.AreEqual(10, textFieldElement.MinLength);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetSizeReturns30_When_SizeAttributeIsNotPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText2");

            Assert.AreEqual(30, textFieldElement.Size);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText4");

            Assert.AreEqual(false, textFieldElement.IsRequired);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText7");

            Assert.AreEqual(true, textFieldElement.IsRequired);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetPlaceholder_When_PlaceholderAttributeIsSet_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText");

            Assert.AreEqual("your Text term goes here", textFieldElement.Placeholder);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetPlaceholderReturnsNull_When_PlaceholderAttributeIsNotPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText1");

            Assert.IsNull(textFieldElement.Placeholder);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnRed_When_Hover_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText8");

            textFieldElement.Hover();

            Assert.AreEqual("color: red;", textFieldElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnBlue_When_Focus_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText9");

            textFieldElement.Focus();

            Assert.AreEqual("color: blue;", textFieldElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnFalse_When_DisabledAttributeNotPresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText9");

            bool isDisabled = textFieldElement.IsDisabled;

            Assert.IsFalse(isDisabled);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnTrue_When_DisabledAttributePresent_Opera()
        {
            var textFieldElement = App.ElementCreateService.CreateById<TextField>("myText10");

            bool isDisabled = textFieldElement.IsDisabled;

            Assert.IsTrue(isDisabled);
        }
    }
}