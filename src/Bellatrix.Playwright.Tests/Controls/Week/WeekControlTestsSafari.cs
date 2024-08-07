﻿// <copyright file="WeekControlTestsSafari.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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
[Browser(BrowserTypes.Webkit, Lifecycle.ReuseIfStarted)]
[AllureSuite("Week Control")]
public class WeekControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().WeekPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void WeekSet_When_UseSetWeekMethod_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        weekElement.SetWeek(2017, 7);

        Assert.AreEqual("2017-W07", weekElement.GetWeek());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetWeekReturnsCorrectWeek_When_DefaultWeekIsSet_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek2");

        Assert.AreEqual("2017-W07", weekElement.GetWeek());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        Assert.IsFalse(weekElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek4");

        Assert.IsFalse(weekElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek3");

        Assert.IsTrue(weekElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek4");

        Assert.AreEqual(false, weekElement.IsReadonly);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek5");

        Assert.AreEqual(true, weekElement.IsReadonly);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMaxReturnsEmpty_When_MaxAttributeIsNotPresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        var max = weekElement.Max;

        Assert.IsNull(max);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMinReturnsEmpty_When_MinAttributeIsNotPresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        Assert.IsNull(weekElement.Min);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetStepReturnsNull_When_StepAttributeIsNotPresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        Assert.IsNull(weekElement.Step);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMaxReturns52Week_When_MaxAttributeIsPresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek1");

        Assert.AreEqual("2017-W52", weekElement.Max);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMinReturnsFirstWeek_When_MinAttributeIsPresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek1");

        Assert.AreEqual("2017-W01", weekElement.Min);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetStepReturns10_When_StepAttributeIsNotPresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek1");

        Assert.AreEqual(10, weekElement.Step);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek4");

        Assert.AreEqual(false, weekElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek6");

        Assert.IsTrue(weekElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek7");

        weekElement.Hover();

        Assert.AreEqual("color: red;", weekElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek8");

        weekElement.Focus();

        Assert.AreEqual("color: blue;", weekElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        bool isDisabled = weekElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Safari()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek9");

        bool isDisabled = weekElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}