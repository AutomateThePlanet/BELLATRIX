﻿// <copyright file="CheckBoxControlTestsSafari.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("CheckBox Control")]
public class CheckBoxControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().CheckBoxPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void Unchecked_When_UseCheckMethod_Safari()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox");

        checkBoxElement.Check(false);

        Assert.AreEqual(false, checkBoxElement.IsChecked);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void Unchecked_When_UseUncheckMethod_Safari()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox");

        checkBoxElement.Uncheck();

        Assert.AreEqual(false, checkBoxElement.IsChecked);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox1");

        checkBoxElement.Hover();

        Assert.AreEqual("color: red;", checkBoxElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Safari()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox2");

        checkBoxElement.Focus();

        checkBoxElement.ValidateStyleIs("color: blue;");
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Safari()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox");

        bool isDisabled = checkBoxElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    [TestCategory(Categories.KnownIssueMacOS)]
    public void ReturnTrue_When_DisabledAttributePresent_Safari()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox3");

        bool isDisabled = checkBoxElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnOn_When_ValueAttributeNotPresent_Safari()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox1");

        var actualValue = checkBoxElement.Value;

        Assert.AreEqual("on", actualValue);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnNewsletter_When_ValueAttributePresent_Safari()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox2");

        var actualValue = checkBoxElement.Value;

        Assert.AreEqual("newsletter", actualValue);
    }
}