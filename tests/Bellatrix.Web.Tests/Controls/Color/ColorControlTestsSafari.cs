// <copyright file="ColorControlTestsSafari.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Color Control")]
public class ColorControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ColorLocalPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ColorSet_When_UseSetColorMethod_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor");

        colorElement.SetColor("#f00030");

        Assert.AreEqual("#f00030", colorElement.GetColor());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetColorReturnsCorrectColor_When_DefaultColorIsSet_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor2");

        // TODO: Investigate why WebDriver returns 8 instead of 7.
        Assert.AreEqual("#f00030", colorElement.GetColor());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor");

        Assert.IsFalse(colorElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor4");

        Assert.IsFalse(colorElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor3");

        Assert.IsTrue(colorElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor4");

        Assert.AreEqual(false, colorElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor6");

        Assert.IsTrue(colorElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor7");

        colorElement.Hover();

        Assert.AreEqual("color: red;", colorElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor8");

        colorElement.Focus();

        Assert.AreEqual("color: blue;", colorElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor");

        bool isDisabled = colorElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor9");

        bool isDisabled = colorElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetListReturnsNull_When_ListAttributeIsNotPresent_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor");

        Assert.IsNull(colorElement.List);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetListReturnsTickmarks_When_MaxAttributeIsPresent_Safari()
    {
        var colorElement = App.Components.CreateById<Color>("myColor10");

        Assert.AreEqual("tickmarks", colorElement.List);
    }
}