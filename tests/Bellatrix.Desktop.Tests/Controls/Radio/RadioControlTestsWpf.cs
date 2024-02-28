// <copyright file="RadioControlTestsWpf.cs" company="Automate The Planet Ltd.">
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
[App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
[AllureSuite("Radio Control")]
[AllureTag("WPF")]
public class RadioControlTestsWpf : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_RadioButtonHovered_Wpf()
    {
        var button = App.Components.CreateByName<RadioButton>("RadioButton");

        button.Hover();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("radioHovered", label.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_ButtonClicked_Wpf()
    {
        var radioButton = App.Components.CreateByName<RadioButton>("RadioButton");

        radioButton.Click();

        Assert.IsTrue(radioButton.IsChecked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsCheckedReturnsTrue_When_RadioChecked_Wpf()
    {
        var button = App.Components.CreateByName<RadioButton>("SelectedRadioButton");

        Assert.IsTrue(button.IsChecked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsFalse_When_RadioIsNotDisabled_Wpf()
    {
        var button = App.Components.CreateByName<RadioButton>("RadioButton");

        Assert.AreEqual(false, button.IsDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsTrue_When_RadioIsDisabled_Wpf()
    {
        var button = App.Components.CreateByName<RadioButton>("SelectedRadioButton");

        Assert.AreEqual(true, button.IsDisabled);
    }
}
