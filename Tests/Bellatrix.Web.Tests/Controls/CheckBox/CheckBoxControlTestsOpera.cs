// <copyright file="CheckBoxControlTestsOpera.cs" company="Automate The Planet Ltd.">
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
    [Browser(BrowserType.Opera, BrowserBehavior.ReuseIfStarted)]
    [AllureSuite("CheckBox Control")]
    public class CheckBoxControlTestsOpera : WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().CheckBoxLocalPage);

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void Unchecked_When_UseCheckMethod_Opera()
        {
            var checkBoxElement = App.ElementCreateService.CreateById<CheckBox>("myCheckbox");

            checkBoxElement.Check(false);

            Assert.AreEqual(false, checkBoxElement.IsChecked);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void Unchecked_When_UseUncheckMethod_Opera()
        {
            var checkBoxElement = App.ElementCreateService.CreateById<CheckBox>("myCheckbox");

            checkBoxElement.Uncheck();

            Assert.AreEqual(false, checkBoxElement.IsChecked);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnRed_When_Hover_Opera()
        {
            var checkBoxElement = App.ElementCreateService.CreateById<CheckBox>("myCheckbox1");

            checkBoxElement.Hover();

            Assert.AreEqual("color: red;", checkBoxElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnBlue_When_Focus_Opera()
        {
            var checkBoxElement = App.ElementCreateService.CreateById<CheckBox>("myCheckbox2");

            checkBoxElement.Focus();

            checkBoxElement.ValidateStyleIs("color: blue;");
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnFalse_When_DisabledAttributeNotPresent_Opera()
        {
            var checkBoxElement = App.ElementCreateService.CreateById<CheckBox>("myCheckbox");

            bool isDisabled = checkBoxElement.IsDisabled;

            Assert.IsFalse(isDisabled);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnTrue_When_DisabledAttributePresent_Opera()
        {
            var checkBoxElement = App.ElementCreateService.CreateById<CheckBox>("myCheckbox3");

            bool isDisabled = checkBoxElement.IsDisabled;

            Assert.IsTrue(isDisabled);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnOn_When_ValueAttributeNotPresent_Opera()
        {
            var checkBoxElement = App.ElementCreateService.CreateById<CheckBox>("myCheckbox1");

            var actualValue = checkBoxElement.Value;

            Assert.AreEqual("on", actualValue);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnNewsletter_When_ValueAttributePresent_Opera()
        {
            var checkBoxElement = App.ElementCreateService.CreateById<CheckBox>("myCheckbox2");

            var actualValue = checkBoxElement.Value;

            Assert.AreEqual("newsletter", actualValue);
        }
    }
}