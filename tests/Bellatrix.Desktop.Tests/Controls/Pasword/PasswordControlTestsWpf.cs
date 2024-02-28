// <copyright file="PasswordControlTestsWpf.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Password Control")]
[AllureTag("WPF")]
public class PasswordControlTestsWpf : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_PasswordHovered_Wpf()
    {
        var password = App.Components.CreateByAutomationId<Password>("passwordBox");

        password.Hover();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("passwordBoxHovered", label.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_NewTextSet_Wpf()
    {
        var textField = App.Components.CreateByAutomationId<Password>("passwordBox");

        textField.SetPassword("topsecret");

        Assert.AreEqual("●●●●●●●●●", textField.GetPassword());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsFalse_When_PasswordIsNotDisabled_Wpf()
    {
        var textField = App.Components.CreateByAutomationId<Password>("passwordBox");

        Assert.AreEqual(false, textField.IsDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsTrue_When_PasswordIsDisabled_Wpf()
    {
        var textField = App.Components.CreateByAutomationId<Password>("disabledPasswordBox");

        Assert.AreEqual(true, textField.IsDisabled);
    }
}
