// <copyright file="DateTimeLocalControlTestsOpera.cs" company="Automate The Planet Ltd.">
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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls
{
    [TestClass]
    [Browser(BrowserType.Opera, Lifecycle.ReuseIfStarted)]
    [AllureSuite("DateTimeLocal Control")]
    public class DateDateTimeLocalLocalControlTestsOpera : MSTest.WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().DateTimeLocalLocalPage);

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void DateTimeLocalSet_When_UseSetDateTimeLocalMethod_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime");

            timeElement.SetTime(new DateTime(1989, 10, 28, 23, 23, 0));

            Assert.AreEqual("1989-10-28T23:23", timeElement.GetTime());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetTimeReturnsCorrectDateTimeLocal_When_DefaultDateTimeLocalIsSet_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime2");

            Assert.AreEqual("2017-06-01T08:30", timeElement.GetTime());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime");

            Assert.IsFalse(timeElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime4");

            Assert.IsFalse(timeElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime3");

            Assert.IsTrue(timeElement.IsAutoComplete);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime4");

            Assert.AreEqual(false, timeElement.IsReadonly);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime5");

            Assert.AreEqual(true, timeElement.IsReadonly);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMaxReturnsEmpty_When_MaxAttributeIsNotPresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime");

            var max = timeElement.Max;

            Assert.IsNull(max);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMinReturnsEmpty_When_MinAttributeIsNotPresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime");

            Assert.IsNull(timeElement.Min);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetStepReturnsNull_When_StepAttributeIsNotPresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime");

            Assert.IsNull(timeElement.Step);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMaxReturns80_When_MaxAttributeIsPresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime1");

            Assert.AreEqual("2017-06-30T16:30", timeElement.Max);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetMinReturns10_When_MinAttributeIsPresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime1");

            Assert.AreEqual("2017-06-01T08:30", timeElement.Min);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetStepReturns10_When_StepAttributeIsNotPresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime1");

            Assert.AreEqual(10, timeElement.Step);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime4");

            Assert.AreEqual(false, timeElement.IsRequired);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime6");

            Assert.IsTrue(timeElement.IsRequired);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnRed_When_Hover_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime7");

            timeElement.Hover();

            Assert.AreEqual("color: red;", timeElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnBlue_When_Focus_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime8");

            timeElement.Focus();

            Assert.AreEqual("color: blue;", timeElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnFalse_When_DisabledAttributeNotPresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime");

            bool isDisabled = timeElement.IsDisabled;

            Assert.IsFalse(isDisabled);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnTrue_When_DisabledAttributePresent_Opera()
        {
            var timeElement = App.ComponentCreateService.CreateById<DateTimeLocal>("myTime9");

            bool isDisabled = timeElement.IsDisabled;

            Assert.IsTrue(isDisabled);
        }
    }
}