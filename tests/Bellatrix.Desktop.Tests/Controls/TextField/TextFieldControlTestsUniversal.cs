// <copyright file="TextFieldControlTestsUniversal.cs" company="Automate The Planet Ltd.">
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
[App(Constants.UniversalAppPath, Lifecycle.RestartEveryTime)]
[AllureSuite("TextField Control")]
[AllureTag("Universal")]
public class TextFieldControlTestsUniversal : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_TextFieldHovered_Universal()
    {
        var button = App.Components.CreateByAutomationId<TextField>("textBox");

        button.Hover();

        var label = App.Components.CreateByAutomationId<Label>("resultTextBlock");
        Assert.AreEqual("textFieldHovered", label.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_NewTextSet_Universal()
    {
        var textField = App.Components.CreateByAutomationId<TextField>("textBox");

        textField.SetText("Meissa Is Beautiful!");

        Assert.AreEqual("Meissa Is Beautiful!", textField.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void GetContent_When_TextFieldLocated_Universal()
    {
        var textField = App.Components.CreateByAutomationId<TextField>("disabledTextBox");

        Assert.AreEqual("Bellatrix Is Awesome!", textField.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsFalse_When_TextFieldIsNotDisabled_Universal()
    {
        var textField = App.Components.CreateByAutomationId<TextField>("textBox");

        Assert.AreEqual(false, textField.IsDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsTrue_When_TextFieldIsDisabled_Universal()
    {
        var textField = App.Components.CreateByAutomationId<TextField>("disabledTextBox");

        Assert.AreEqual(true, textField.IsDisabled);
    }
}
