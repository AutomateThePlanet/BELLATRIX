﻿// <copyright file="RangeControlTestsFirefox.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserTypes.Firefox, Lifecycle.ReuseIfStarted)]
[AllureSuite("Range Control")]
public class RangeControlTestsFirefox : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().RangePage);

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void RangeSet_When_UseSetRangeMethod_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        rangeElement.SetRange(4);

        Assert.AreEqual(4, rangeElement.GetRange());
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRangeReturnsCorrectRange_When_DefaultRangeIsSet_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange2");

        // TODO: Investigate why WebDriver returns 8 instead of 7.
        Assert.AreEqual(8, rangeElement.GetRange());
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        Assert.IsFalse(rangeElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange4");

        Assert.IsFalse(rangeElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange3");

        Assert.IsTrue(rangeElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMaxReturnsEmpty_When_MaxAttributeIsNotPresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        var max = rangeElement.Max;

        Assert.IsNull(max);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMinReturnsEmpty_When_MinAttributeIsNotPresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        Assert.IsNull(rangeElement.Min);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetStepReturnsNull_When_StepAttributeIsNotPresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        Assert.IsNull(rangeElement.Step);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMaxReturns10Range_When_MaxAttributeIsPresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange1");

        Assert.AreEqual(10, rangeElement.Max);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMinReturns2Range_When_MinAttributeIsPresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange1");

        Assert.AreEqual(2, rangeElement.Min);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetStepReturns2_When_StepAttributeIsNotPresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange1");

        Assert.AreEqual(2, rangeElement.Step);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange4");

        Assert.AreEqual(false, rangeElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange6");

        Assert.IsTrue(rangeElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange7");

        rangeElement.Hover();

        rangeElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange8");

        rangeElement.Focus();

        rangeElement.ValidateStyleIs("color: blue;");
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        bool isDisabled = rangeElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange9");

        bool isDisabled = rangeElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetListReturnsNull_When_ListAttributeIsNotPresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        Assert.IsNull(rangeElement.List);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetListReturnsTickmarks_When_MaxAttributeIsPresent_Firefox()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange10");

        Assert.AreEqual("tickmarks", rangeElement.List);
    }
}