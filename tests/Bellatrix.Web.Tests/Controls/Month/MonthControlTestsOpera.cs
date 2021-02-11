// <copyright file="MonthControlTestsOpera.cs" company="Automate The Planet Ltd.">
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
    [AllureSuite("Month Control")]
    public class MonthControlTestsOpera : MSTest.WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().MonthLocalPage);

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void MonthSet_When_UseSetMonthMethodWithMonthLessThan10_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth");

            monthElement.SetMonth(2017, 7);

            Assert.AreEqual("2017-07", monthElement.GetMonth());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void MonthSet_When_UseSetMonthMethodWithMonthBiggerThan9_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth");

            monthElement.SetMonth(2017, 11);

            Assert.AreEqual("2017-11", monthElement.GetMonth());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMonthReturnsCorrectMonth_When_DefaultMonthIsSet_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth2");

            Assert.AreEqual("2017-08", monthElement.GetMonth());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth");

            Assert.IsFalse(monthElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth4");

            Assert.IsFalse(monthElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth3");

            Assert.IsTrue(monthElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth4");

            Assert.AreEqual(false, monthElement.IsReadonly);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth5");

            Assert.AreEqual(true, monthElement.IsReadonly);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMaxReturnsEmpty_When_MaxAttributeIsNotPresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth");

            var max = monthElement.Max;

            Assert.IsNull(max);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMinReturnsEmpty_When_MinAttributeIsNotPresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth");

            Assert.IsNull(monthElement.Min);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetStepReturnsNull_When_StepAttributeIsNotPresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth");

            Assert.IsNull(monthElement.Step);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMaxReturns52Month_When_MaxAttributeIsPresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth1");

            Assert.AreEqual("2032-12", monthElement.Max);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMinReturnsFirstMonth_When_MinAttributeIsPresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth1");

            Assert.AreEqual("1900-01", monthElement.Min);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetStepReturns10_When_StepAttributeIsNotPresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth1");

            Assert.AreEqual(2, monthElement.Step);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth4");

            Assert.AreEqual(false, monthElement.IsRequired);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth6");

            Assert.IsTrue(monthElement.IsRequired);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnRed_When_Hover_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth7");

            monthElement.Hover();

            Assert.AreEqual("color: red;", monthElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnBlue_When_Focus_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth8");

            monthElement.Focus();

            Assert.AreEqual("color: blue;", monthElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnFalse_When_DisabledAttributeNotPresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth");

            bool isDisabled = monthElement.IsDisabled;

            Assert.IsFalse(isDisabled);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnTrue_When_DisabledAttributePresent_Opera()
        {
            var monthElement = App.ElementCreateService.CreateById<Month>("myMonth9");

            bool isDisabled = monthElement.IsDisabled;

            Assert.IsTrue(isDisabled);
        }
    }
}