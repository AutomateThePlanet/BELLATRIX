// <copyright file="InputSubmitControlTestsOpera.cs" company="Automate The Planet Ltd.">
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
    [AllureSuite("Input Button Control")]
    public class InputSubmitControlTestsOpera : MSTest.WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(SettingsService.GetSection<TestPagesSettings>().ButtonLocalPage);

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void SetTextToStop_When_UseClickMethod_Opera()
        {
            var buttonElement = App.ElementCreateService.CreateById<Button>("myButton4");

            buttonElement.Click();

            Assert.AreEqual("Stop", buttonElement.Value);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnRed_When_Hover_Opera()
        {
            var buttonElement = App.ElementCreateService.CreateById<Button>("myButton5");

            buttonElement.Hover();

            Assert.AreEqual("color: red;", buttonElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnBlue_When_Focus_Opera()
        {
            var buttonElement = App.ElementCreateService.CreateById<Button>("myButton6");

            buttonElement.Focus();

            buttonElement.ValidateStyleIs("color: blue;");
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnFalse_When_DisabledAttributeNotPresent_Opera()
        {
            var buttonElement = App.ElementCreateService.CreateById<Button>("myButton4");

            bool isDisabled = buttonElement.IsDisabled;

            Assert.IsFalse(isDisabled);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnTrue_When_DisabledAttributePresent_Opera()
        {
            var buttonElement = App.ElementCreateService.CreateById<Button>("myButton7");

            bool isDisabled = buttonElement.IsDisabled;

            Assert.IsTrue(isDisabled);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnStart_When_ValueAttributePresent_Opera()
        {
            var buttonElement = App.ElementCreateService.CreateById<Button>("myButton4");

            var actualValue = buttonElement.Value;

            Assert.AreEqual("Start", actualValue);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnEmpty_When_UseInnerText_Opera()
        {
            var buttonElement = App.ElementCreateService.CreateById<Button>("myButton4");

            Assert.AreEqual(string.Empty, buttonElement.InnerText);
        }
    }
}