// <copyright file="MonthControlTestsChrome.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Month Control")]
public class MonthControlTestsChrome : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().MonthLocalPage);

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void MonthSet_When_UseSetMonthMethodWithMonthLessThan10_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        monthElement.SetMonth(2017, 7);

        Assert.AreEqual("2017-07", monthElement.GetMonth());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void MonthSet_When_UseSetMonthMethodWithMonthBiggerThan9_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        monthElement.SetMonth(2017, 11);

        Assert.AreEqual("2017-11", monthElement.GetMonth());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMonthReturnsCorrectMonth_When_DefaultMonthIsSet_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth2");

        Assert.AreEqual("2017-08", monthElement.GetMonth());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        Assert.IsFalse(monthElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth4");

        Assert.IsFalse(monthElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth3");

        Assert.IsTrue(monthElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth4");

        Assert.AreEqual(false, monthElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth5");

        Assert.AreEqual(true, monthElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnsNull_When_MaxAttributeIsNotPresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        var max = monthElement.Max;

        Assert.IsNull(max);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnsNull_When_MinAttributeIsNotPresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        Assert.IsNull(monthElement.Min);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetStepReturnsNull_When_StepAttributeIsNotPresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        Assert.IsNull(monthElement.Step);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMaxReturns52Month_When_MaxAttributeIsPresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth1");

        Assert.AreEqual("2032-12", monthElement.Max);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMinReturnsFirstMonth_When_MinAttributeIsPresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth1");

        Assert.AreEqual("1900-01", monthElement.Min);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetStepReturns10_When_StepAttributeIsNotPresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth1");

        Assert.AreEqual(2, monthElement.Step);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth4");

        Assert.AreEqual(false, monthElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth6");

        Assert.IsTrue(monthElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth7");

        monthElement.Hover();

        Assert.AreEqual("color: red;", monthElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth8");

        monthElement.Focus();

        Assert.AreEqual("color: blue;", monthElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        bool isDisabled = monthElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Chrome()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth9");

        bool isDisabled = monthElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}