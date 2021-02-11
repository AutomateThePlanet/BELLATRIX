// <copyright file="ColorControlTestsOpera.cs" company="Automate The Planet Ltd.">
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
    [AllureSuite("Color Control")]
    public class ColorControlTestsOpera : MSTest.WebTest
    {
        public override void TestsArrange() => base.TestsArrange();
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ColorLocalPage);

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ColorSet_When_UseSetColorMethod_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor");

            colorElement.SetColor("#f00030");

            Assert.AreEqual("#f00030", colorElement.GetColor());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetColorReturnsCorrectColor_When_DefaultColorIsSet_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor2");

            // TODO: Investigate why WebDriver returns 8 instead of 7.
            Assert.AreEqual("#f00030", colorElement.GetColor());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor");

            Assert.IsFalse(colorElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor4");

            Assert.IsFalse(colorElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor3");

            Assert.IsTrue(colorElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor4");

            Assert.AreEqual(false, colorElement.IsRequired);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor6");

            Assert.IsTrue(colorElement.IsRequired);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnRed_When_Hover_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor7");

            colorElement.Hover();

            Assert.AreEqual("color: red;", colorElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnBlue_When_Focus_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor8");

            colorElement.Focus();

            Assert.AreEqual("color: blue;", colorElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnFalse_When_DisabledAttributeNotPresent_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor");

            bool isDisabled = colorElement.IsDisabled;

            Assert.IsFalse(isDisabled);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnTrue_When_DisabledAttributePresent_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor9");

            bool isDisabled = colorElement.IsDisabled;

            Assert.IsTrue(isDisabled);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetListReturnsNull_When_ListAttributeIsNotPresent_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor");

            Assert.IsNull(colorElement.List);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetListReturnsTickmarks_When_MaxAttributeIsPresent_Opera()
        {
            var colorElement = App.ElementCreateService.CreateById<Color>("myColor10");

            Assert.AreEqual("tickmarks", colorElement.List);
        }
    }
}