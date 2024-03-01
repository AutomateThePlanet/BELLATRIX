// <copyright file="ColorControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Color Control")]
[AllureFeature("ValidateExtensions")]
public class ColorControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ColorLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateColorIs_DoesNotThrowException_When_ColorIsAsSpecified()
    {
        var colorElement = App.Components.CreateById<Color>("myColor");

        colorElement.SetColor("#f00030");

        colorElement.ValidateColorIs("#f00030");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOff_DoesNotThrowException_When_AutoCompleteIsOff()
    {
        var colorElement = App.Components.CreateById<Color>("myColor");

        colorElement.ValidateAutoCompleteOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOn_DoesNotThrowException_When_AutoCompleteAttributeExistsAndIsSetToOn()
    {
        var colorElement = App.Components.CreateById<Color>("myColor3");

        colorElement.ValidateAutoCompleteOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateNotRequired_DoesNotThrowException_When_RequiredAttributeIsNotPresent()
    {
        var colorElement = App.Components.CreateById<Color>("myColor4");

        colorElement.ValidateIsNotRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateRequired_DoesNotThrowException_When_RequiredAttributeIsPresent()
    {
        var colorElement = App.Components.CreateById<Color>("myColor6");

        colorElement.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover()
    {
        var colorElement = App.Components.CreateById<Color>("myColor7");

        colorElement.Hover();

        colorElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent()
    {
        var colorElement = App.Components.CreateById<Color>("myColor");

        colorElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent()
    {
        var colorElement = App.Components.CreateById<Color>("myColor9");

        colorElement.ValidateIsDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateListIsNull_DoesNotThrowException_When_ListAttributeIsNotPresent()
    {
        var colorElement = App.Components.CreateById<Color>("myColor");

        colorElement.ValidateListIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateListIs_DoesNotThrowException_When_MaxAttributeIsPresent()
    {
        var colorElement = App.Components.CreateById<Color>("myColor10");

        Assert.AreEqual("tickmarks", colorElement.List);
        colorElement.ValidateListIs("tickmarks");
    }
}