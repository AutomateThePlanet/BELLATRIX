// <copyright file="TextAreaControlTestsWinForms.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("TextArea Control")]
[AllureTag("WinForms")]
public class TextAreaControlTestsWinForms : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_TextAreaHovered_WinForms()
    {
        var textArea = App.Components.CreateByAutomationId<TextArea>("textArea");

        textArea.Hover();

        var label = App.Components.CreateByAutomationId<Label>("resultLabel");
        Assert.AreEqual("textAreaHovered", label.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_NewTextSet_WinForms()
    {
        var textArea = App.Components.CreateByAutomationId<TextArea>("textArea");

        textArea.SetText("Meissa Is Beautiful!");

        Assert.IsTrue(textArea.InnerText.Contains("Meissa Is Beautiful!"));
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void GetContent_When_TextAreaLocated_WinForms()
    {
        var textArea = App.Components.CreateByAutomationId<TextArea>("disabledTextArea");

        Assert.IsTrue(textArea.InnerText.Contains("Bellatrix Is Awesome!"));
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsFalse_When_TextAreaIsNotDisabled_WinForms()
    {
        var textArea = App.Components.CreateByAutomationId<TextArea>("textArea");

        Assert.AreEqual(false, textArea.IsDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsTrue_When_TextAreaIsDisabled_WinForms()
    {
        var textArea = App.Components.CreateByAutomationId<TextArea>("disabledTextArea");

        Assert.AreEqual(true, textArea.IsDisabled);
    }
}
