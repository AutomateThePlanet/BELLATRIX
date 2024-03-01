// <copyright file="ColorControlTestsFirefox.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserType.Firefox, Lifecycle.ReuseIfStarted)]
[AllureSuite("Color Control")]
public class ColorControlTestsFirefox : MSTest.WebTest
{
    public override void TestsArrange() => base.TestsArrange();
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ColorLocalPage);

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ColorSet_When_UseSetColorMethod_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor");

        colorElement.SetColor("#f00030");

        Assert.AreEqual("#f00030", colorElement.GetColor());
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetColorReturnsCorrectColor_When_DefaultColorIsSet_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor2");

        // TODO: Investigate why WebDriver returns 8 instead of 7.
        Assert.AreEqual("#f00030", colorElement.GetColor());
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor");

        Assert.IsFalse(colorElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor4");

        Assert.IsFalse(colorElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor3");

        Assert.IsTrue(colorElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor4");

        Assert.AreEqual(false, colorElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor6");

        Assert.IsTrue(colorElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor7");

        colorElement.Hover();

        colorElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor8");

        colorElement.Focus();

        colorElement.ValidateStyleIs("color: blue;");
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor");

        bool isDisabled = colorElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor9");

        bool isDisabled = colorElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetListReturnsNull_When_ListAttributeIsNotPresent_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor");

        Assert.IsNull(colorElement.List);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetListReturnsTickmarks_When_MaxAttributeIsPresent_Firefox()
    {
        var colorElement = App.Components.CreateById<Color>("myColor10");

        Assert.AreEqual("tickmarks", colorElement.List);
    }
}