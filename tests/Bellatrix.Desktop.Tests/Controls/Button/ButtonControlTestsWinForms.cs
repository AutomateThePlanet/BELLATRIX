// <copyright file="ButtonControlTestsWinForms.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Button Control")]
[AllureTag("WinForms")]
public class ButtonControlTestsWinForms : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_ButtonHovered_WinForms()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        button.Hover();

        var label = App.Components.CreateByAutomationId<Label>("resultLabel");
        Assert.IsTrue(label.IsVisible);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_ButtonClicked_WinForms()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        button.Click();

        var label = App.Components.CreateByAutomationId<Label>("resultLabel");
        Assert.IsTrue(label.IsPresent);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void EButtonContent_When_ButtonLocated_WinForms()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        Assert.AreEqual("E Button", button.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsFalse_When_ButtonIsNotDisabled_WinForms()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        Assert.AreEqual(false, button.IsDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsTrue_When_ButtonIsDisabled_WinForms()
    {
        var button = App.Components.CreateByName<Button>("D Button");

        Assert.AreEqual(true, button.IsDisabled);
    }
}
