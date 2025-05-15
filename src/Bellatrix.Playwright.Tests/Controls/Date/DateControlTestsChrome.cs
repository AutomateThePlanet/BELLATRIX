﻿// <copyright file="DateControlTestsChrome.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Playwright.Tests.Controls;

[TestClass]
[Browser(BrowserTypes.Chrome, Lifecycle.ReuseIfStarted)]
[AllureSuite("Date Control")]
public class DateControlTestsChrome : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().DatePage);

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void DateSet_When_UseSetDateMethodWithDateLessThan10_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        dateElement.SetDate(2017, 7, 6);

        Assert.AreEqual("2017-07-06", dateElement.GetDate());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void DateSet_When_UseSetDateMethodWithMonthBiggerThan9_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        dateElement.SetDate(2017, 11, 09);

        Assert.AreEqual("2017-11-09", dateElement.GetDate());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void DateSet_When_UseSetDateMethodWithDayBiggerThan9_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        dateElement.SetDate(2017, 11, 15);

        Assert.AreEqual("2017-11-15", dateElement.GetDate());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void DateSet_When_UseSetDateMethodWithDay31_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        dateElement.SetDate(2017, 11, 30);

        Assert.AreEqual("2017-11-30", dateElement.GetDate());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetDateReturnsCorrectDate_When_DefaultDateIsSet_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate2");

        Assert.AreEqual("2017-08-07", dateElement.GetDate());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        Assert.IsFalse(dateElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate4");

        Assert.IsFalse(dateElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate3");

        Assert.IsTrue(dateElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate4");

        Assert.AreEqual(false, dateElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate5");

        Assert.AreEqual(true, dateElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnsNull_When_MaxAttributeIsNotPresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        var max = dateElement.Max;

        Assert.IsNull(max);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnsNull_When_MinAttributeIsNotPresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        Assert.IsNull(dateElement.Min);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetStepReturnsNull_When_StepAttributeIsNotPresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        Assert.IsNull(dateElement.Step);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMaxReturns52Date_When_MaxAttributeIsPresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate1");

        Assert.AreEqual("2032-12-01", dateElement.Max);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMinReturnsFirstDate_When_MinAttributeIsPresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate1");

        Assert.AreEqual("1900-01-01", dateElement.Min);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetStepReturns10_When_StepAttributeIsNotPresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate1");

        Assert.AreEqual(2, dateElement.Step);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate4");

        Assert.AreEqual(false, dateElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate6");

        Assert.IsTrue(dateElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate7");

        dateElement.Hover();

        Assert.AreEqual("color: red;", dateElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate8");

        dateElement.Focus();

        Assert.AreEqual("color: blue;", dateElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        bool isDisabled = dateElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Chrome()
    {
        var dateElement = App.Components.CreateById<Date>("myDate9");

        bool isDisabled = dateElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}