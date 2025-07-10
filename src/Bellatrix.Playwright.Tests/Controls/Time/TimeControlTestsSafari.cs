﻿// <copyright file="TimeControlTestsSafari.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserTypes.Webkit, Lifecycle.ReuseIfStarted)]
[AllureSuite("Time Control")]
public class TimeControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().TimePage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void DecimalTimeSet_When_UseSetTimeMethod_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        timeElement.SetTime(11, 11);

        Assert.AreEqual("11:11:00", timeElement.GetTime());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void IntegerTimeSet_When_UseSetTimeMethod_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        timeElement.SetTime(12, 12);

        Assert.AreEqual("12:12:00", timeElement.GetTime());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetTimeReturnsCorrectTime_When_DefaultTimeIsSet_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime2");

        Assert.AreEqual("12:11", timeElement.GetTime());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        Assert.IsFalse(timeElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime4");

        Assert.IsFalse(timeElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime3");

        Assert.IsTrue(timeElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime4");

        Assert.AreEqual(false, timeElement.IsReadonly);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime5");

        Assert.AreEqual(true, timeElement.IsReadonly);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMaxReturnsNull_When_MaxAttributeIsNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        var max = timeElement.Max;

        Assert.IsNull(max);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMinReturnsEmpty_When_MinAttributeIsNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        Assert.IsNull(timeElement.Min);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetStepReturnsNull_When_StepAttributeIsNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        Assert.IsNull(timeElement.Step);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMaxReturns80_When_MaxAttributeIsPresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime1");

        Assert.AreEqual("11:11", timeElement.Max);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMinReturns10_When_MinAttributeIsPresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime1");

        Assert.AreEqual("00:01", timeElement.Min);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetStepReturns10_When_StepAttributeIsNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime1");

        Assert.AreEqual(10, timeElement.Step);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime4");

        Assert.AreEqual(false, timeElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime6");

        Assert.IsTrue(timeElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime7");

        timeElement.Hover();

        Assert.AreEqual("color: red;", timeElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime8");

        timeElement.Focus();

        Assert.AreEqual("color: blue;", timeElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        bool isDisabled = timeElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Safari()
    {
        var timeElement = App.Components.CreateById<Time>("myTime9");

        bool isDisabled = timeElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}