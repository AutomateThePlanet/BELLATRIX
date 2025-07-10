﻿// <copyright file="CheckBoxControlTestsWpf.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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

namespace Bellatrix.Desktop.Tests;

[TestClass]
[App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
[AllureSuite("CheckBox Control")]
[AllureTag("WPF")]
public class CheckBoxControlTestsWpf : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_CheckBoxHovered_Wpf()
    {
        var checkBox = App.Components.CreateByName<CheckBox>("BellaCheckBox");

        checkBox.Hover();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("checkBoxHovered", label.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsCheckedTrue_When_CheckBoxUncheckedAndCheckIt_Wpf()
    {
        var checkBox = App.Components.CreateByName<CheckBox>("BellaCheckBox");

        checkBox.Check();

        Assert.IsTrue(checkBox.IsChecked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void BellaCheckBoxTextReturned_When_CallInnerText_Wpf()
    {
        var checkBox = App.Components.CreateByName<CheckBox>("BellaCheckBox");

        checkBox.Check();

        Assert.IsTrue(checkBox.IsChecked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsCheckedFalse_When_CheckBoxCheckedAndUncheckIt_Wpf()
    {
        var checkBox = App.Components.CreateByName<CheckBox>("MeissaCheckBox");

        checkBox.Uncheck();

        Assert.IsFalse(checkBox.IsChecked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsCheckedReturnsTrue_When_CheckBoxChecked_Wpf()
    {
        var checkBox = App.Components.CreateByName<CheckBox>("CheckBox");

        Assert.IsTrue(checkBox.IsChecked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsFalse_When_CheckBoxIsNotDisabled_Wpf()
    {
        var checkBox = App.Components.CreateByName<CheckBox>("BellaCheckBox");

        Assert.AreEqual(false, checkBox.IsDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsTrue_When_CheckBoxIsDisabled_Wpf()
    {
        var checkBox = App.Components.CreateByName<CheckBox>("CheckBox");

        Assert.AreEqual(true, checkBox.IsDisabled);
    }
}
