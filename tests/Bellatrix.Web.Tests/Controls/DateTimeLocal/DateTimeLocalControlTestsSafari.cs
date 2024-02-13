// <copyright file="DateTimeLocalControlTestsSafari.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Safari, Lifecycle.ReuseIfStarted)]
[AllureSuite("DateTimeLocal Control")]
public class DateTimeLocalControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().DateTimeLocalLocalPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void DateTimeLocalSet_When_UseSetDateTimeLocalMethod_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        timeElement.SetTime(new DateTime(1989, 10, 28, 23, 23, 0));

        Assert.AreEqual("1989-10-28T23:23", timeElement.GetTime());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetTimeReturnsCorrectDateTimeLocal_When_DefaultDateTimeLocalIsSet_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime2");

        Assert.AreEqual("2017-06-01T08:30", timeElement.GetTime());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        Assert.IsFalse(timeElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime4");

        Assert.IsFalse(timeElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime3");

        Assert.IsTrue(timeElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime4");

        Assert.AreEqual(false, timeElement.IsReadonly);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime5");

        Assert.AreEqual(true, timeElement.IsReadonly);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMaxReturnsEmpty_When_MaxAttributeIsNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        var max = timeElement.Max;

        Assert.IsNull(max);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMinReturnsEmpty_When_MinAttributeIsNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        Assert.IsNull(timeElement.Min);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetStepReturnsNull_When_StepAttributeIsNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        Assert.IsNull(timeElement.Step);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMaxReturns80_When_MaxAttributeIsPresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime1");

        Assert.AreEqual("2017-06-30T16:30", timeElement.Max);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMinReturns10_When_MinAttributeIsPresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime1");

        Assert.AreEqual("2017-06-01T08:30", timeElement.Min);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetStepReturns10_When_StepAttributeIsNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime1");

        Assert.AreEqual(10, timeElement.Step);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime4");

        Assert.AreEqual(false, timeElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime6");

        Assert.IsTrue(timeElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime7");

        timeElement.Hover();

        Assert.AreEqual("color: red;", timeElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime8");

        timeElement.Focus();

        Assert.AreEqual("color: blue;", timeElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        bool isDisabled = timeElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Safari()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime9");

        bool isDisabled = timeElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}