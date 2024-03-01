// <copyright file="WeekControlTestsChrome.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
[AllureSuite("Week Control")]
public class WeekControlTestsChrome : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().WeekLocalPage);

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void WeekSet_When_UseSetWeekMethod_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        weekElement.SetWeek(2017, 7);

        Assert.AreEqual("2017-W07", weekElement.GetWeek());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetWeekReturnsCorrectWeek_When_DefaultWeekIsSet_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek2");

        Assert.AreEqual("2017-W07", weekElement.GetWeek());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        Assert.IsFalse(weekElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek4");

        Assert.IsFalse(weekElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek3");

        Assert.IsTrue(weekElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek4");

        Assert.AreEqual(false, weekElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek5");

        Assert.AreEqual(true, weekElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnsNull_When_MaxAttributeIsNotPresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        var max = weekElement.Max;

        Assert.IsNull(max);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnsNull_When_MinAttributeIsNotPresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        Assert.IsNull(weekElement.Min);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetStepReturnsNull_When_StepAttributeIsNotPresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        Assert.IsNull(weekElement.Step);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMaxReturns52Week_When_MaxAttributeIsPresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek1");

        Assert.AreEqual("2017-W52", weekElement.Max);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMinReturnsFirstWeek_When_MinAttributeIsPresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek1");

        Assert.AreEqual("2017-W01", weekElement.Min);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetStepReturns10_When_StepAttributeIsNotPresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek1");

        Assert.AreEqual(10, weekElement.Step);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek4");

        Assert.AreEqual(false, weekElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek6");

        Assert.IsTrue(weekElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek7");

        weekElement.Hover();

        Assert.AreEqual("color: red;", weekElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek8");

        weekElement.Focus();

        Assert.AreEqual("color: blue;", weekElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        bool isDisabled = weekElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Chrome()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek9");

        bool isDisabled = weekElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}