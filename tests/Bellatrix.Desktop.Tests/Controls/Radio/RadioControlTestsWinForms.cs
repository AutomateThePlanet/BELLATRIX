// <copyright file="RadioControlTestsWinForms.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Desktop.Tests;

[TestClass]
[App(Constants.WinFormsAppPath, Lifecycle.RestartEveryTime)]
[AllureSuite("Radio Control")]
[AllureTag("WinForms")]
public class RadioControlTestsWinForms : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_RadioHovered_WinForms()
    {
        var button = App.Components.CreateByAutomationId<RadioButton>("enabledRadioButton");

        button.Hover();

        var label = App.Components.CreateByAutomationId<Label>("resultLabel");
        Assert.AreEqual("radioHovered", label.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void RadioChecked_When_RadioClicked_WinForms()
    {
        var button = App.Components.CreateByAutomationId<RadioButton>("enabledRadioButton");

        button.Click();

        Assert.IsTrue(button.IsChecked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsCheckedReturnsTrue_When_RadioChecked_WinForms()
    {
        var button = App.Components.CreateByAutomationId<RadioButton>("radioButton2");

        Assert.IsTrue(button.IsChecked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsFalse_When_RadioIsNotDisabled_WinForms()
    {
        var button = App.Components.CreateByAutomationId<RadioButton>("enabledRadioButton");

        Assert.AreEqual(false, button.IsDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsTrue_When_RadioIsDisabled_WinForms()
    {
        var button = App.Components.CreateByAutomationId<RadioButton>("radioButton2");

        Assert.AreEqual(true, button.IsDisabled);
    }
}
