// <copyright file="TimeControlTestsEdge.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Time Control")]
[AllureFeature("Edge Browser")]
public class TimeControlTestsEdge : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().TimeLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void DecimalTimeSet_When_UseSetTimeMethod_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        timeElement.SetTime(11, 11);

        Assert.AreEqual("11:11:00", timeElement.GetTime());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void IntegerTimeSet_When_UseSetTimeMethod_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        timeElement.SetTime(12, 12);

        Assert.AreEqual("12:12:00", timeElement.GetTime());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetTimeReturnsCorrectTime_When_DefaultTimeIsSet_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime2");

        Assert.AreEqual("12:11", timeElement.GetTime());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        Assert.IsFalse(timeElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime4");

        Assert.IsFalse(timeElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime3");

        Assert.IsTrue(timeElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime4");

        Assert.AreEqual(false, timeElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime5");

        Assert.AreEqual(true, timeElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetMaxReturnsEmpty_When_MaxAttributeIsNotPresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        var max = timeElement.Max;

        Assert.IsNull(max);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetMinReturnsEmpty_When_MinAttributeIsNotPresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        Assert.IsNull(timeElement.Min);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetStepReturnsNull_When_StepAttributeIsNotPresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        Assert.IsNull(timeElement.Step);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetMaxReturns80_When_MaxAttributeIsPresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime1");

        Assert.AreEqual("11:11", timeElement.Max);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetMinReturns10_When_MinAttributeIsPresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime1");

        Assert.AreEqual("00:01", timeElement.Min);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetStepReturns10_When_StepAttributeIsNotPresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime1");

        Assert.AreEqual(10, timeElement.Step);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime4");

        Assert.AreEqual(false, timeElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime6");

        Assert.IsTrue(timeElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnRed_When_Hover_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime7");

        timeElement.Hover();

        Assert.AreEqual("color: red;", timeElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnBlue_When_Focus_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime8");

        timeElement.Focus();

        Assert.AreEqual("color: blue;", timeElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        bool isDisabled = timeElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnTrue_When_DisabledAttributePresent_Edge()
    {
        var timeElement = App.Components.CreateById<Time>("myTime9");

        bool isDisabled = timeElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}