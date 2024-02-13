// <copyright file="MonthControlTestsSafari.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Safari, Lifecycle.ReuseIfStarted)]
[AllureSuite("Month Control")]
public class MonthControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().MonthLocalPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void MonthSet_When_UseSetMonthMethodWithMonthLessThan10_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        monthElement.SetMonth(2017, 7);

        Assert.AreEqual("2017-07", monthElement.GetMonth());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void MonthSet_When_UseSetMonthMethodWithMonthBiggerThan9_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        monthElement.SetMonth(2017, 11);

        Assert.AreEqual("2017-11", monthElement.GetMonth());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMonthReturnsCorrectMonth_When_DefaultMonthIsSet_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth2");

        Assert.AreEqual("2017-08", monthElement.GetMonth());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        Assert.IsFalse(monthElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth4");

        Assert.IsFalse(monthElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth3");

        Assert.IsTrue(monthElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth4");

        Assert.AreEqual(false, monthElement.IsReadonly);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth5");

        Assert.AreEqual(true, monthElement.IsReadonly);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMaxReturnsEmpty_When_MaxAttributeIsNotPresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        var max = monthElement.Max;

        Assert.IsNull(max);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMinReturnsEmpty_When_MinAttributeIsNotPresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        Assert.IsNull(monthElement.Min);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetStepReturnsNull_When_StepAttributeIsNotPresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        Assert.IsNull(monthElement.Step);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMaxReturns52Month_When_MaxAttributeIsPresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth1");

        Assert.AreEqual("2032-12", monthElement.Max);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMinReturnsFirstMonth_When_MinAttributeIsPresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth1");

        Assert.AreEqual("1900-01", monthElement.Min);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetStepReturns10_When_StepAttributeIsNotPresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth1");

        Assert.AreEqual(2, monthElement.Step);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth4");

        Assert.AreEqual(false, monthElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth6");

        Assert.IsTrue(monthElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth7");

        monthElement.Hover();

        Assert.AreEqual("color: red;", monthElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth8");

        monthElement.Focus();

        Assert.AreEqual("color: blue;", monthElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        bool isDisabled = monthElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Safari()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth9");

        bool isDisabled = monthElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}