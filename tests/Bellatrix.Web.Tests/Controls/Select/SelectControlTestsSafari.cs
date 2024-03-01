// <copyright file="SelectControlTestsSafari.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Select Control")]
public class SelectControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().SelectLocalPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void SelectByTextToAwesome_When_UseSelectByTextMethod_Safari()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        selectComponent.SelectByText("Awesome");

        Assert.AreEqual("bella2", selectComponent.GetSelected().Value);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void SelectByIndexToAwesome_When_UseSelectByTextMethod_Safari()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        selectComponent.SelectByIndex(2);

        Assert.AreEqual("bella2", selectComponent.GetSelected().Value);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnAwesome_When_UseGetSelectedValueMethod_Safari()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect2");

        Assert.AreEqual("bella2", selectComponent.GetSelected().Value);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect1");

        selectComponent.Hover();

        Assert.AreEqual("color: red;", selectComponent.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Safari()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect2");

        selectComponent.Focus();

        Assert.AreEqual("color: blue;", selectComponent.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Safari()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        bool isDisabled = selectComponent.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Safari()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect3");

        bool isDisabled = selectComponent.IsDisabled;

        Assert.IsTrue(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_RequiredAttributePresent_Safari()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect4");

        var actualValue = selectComponent.IsRequired;

        Assert.IsTrue(actualValue);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_RequiredAttributeNotPresent_Safari()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        var actualValue = selectComponent.IsRequired;

        Assert.IsFalse(actualValue);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void Return3Options_When_GetAllOptions_Safari()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");

        var allOptions = selectComponent.GetAllOptions();

        Assert.AreEqual(3, allOptions.Count);
    }
}