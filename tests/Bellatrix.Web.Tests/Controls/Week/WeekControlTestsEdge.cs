// <copyright file="WeekControlTestsEdge.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Week Control")]
[AllureFeature("Edge Browser")]
public class WeekControlTestsEdge : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().WeekLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void WeekSet_When_UseSetWeekMethod_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        weekElement.SetWeek(2017, 7);

        Assert.AreEqual("2017-W07", weekElement.GetWeek());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void WeekSet_When_UseSetWeekMethodAndWeek52_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        weekElement.SetWeek(2017, 52);

        Assert.AreEqual("2017-W52", weekElement.GetWeek());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetWeekReturnsCorrectWeek_When_DefaultWeekIsSet_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek2");

        Assert.AreEqual("2017-W07", weekElement.GetWeek());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        Assert.IsFalse(weekElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek4");

        Assert.IsFalse(weekElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek3");

        Assert.IsTrue(weekElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek4");

        Assert.AreEqual(false, weekElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek5");

        Assert.AreEqual(true, weekElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetMaxReturnsEmpty_When_MaxAttributeIsNotPresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        var max = weekElement.Max;

        Assert.IsNull(max);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetMinReturnsEmpty_When_MinAttributeIsNotPresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        Assert.IsNull(weekElement.Min);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetStepReturnsNull_When_StepAttributeIsNotPresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        Assert.IsNull(weekElement.Step);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetMaxReturns52Week_When_MaxAttributeIsPresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek1");

        Assert.AreEqual("2017-W52", weekElement.Max);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetMinReturnsFirstWeek_When_MinAttributeIsPresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek1");

        Assert.AreEqual("2017-W01", weekElement.Min);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetStepReturns10_When_StepAttributeIsNotPresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek1");

        Assert.AreEqual(10, weekElement.Step);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek4");

        Assert.AreEqual(false, weekElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek6");

        Assert.IsTrue(weekElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnRed_When_Hover_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek7");

        weekElement.Hover();

        Assert.AreEqual("color: red;", weekElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnBlue_When_Focus_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek8");

        weekElement.Focus();

        Assert.AreEqual("color: blue;", weekElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek");

        bool isDisabled = weekElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnTrue_When_DisabledAttributePresent_Edge()
    {
        var weekElement = App.Components.CreateById<Week>("myWeek9");

        bool isDisabled = weekElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}