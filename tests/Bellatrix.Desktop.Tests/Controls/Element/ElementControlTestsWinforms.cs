// <copyright file="ElementControlTestsWinforms.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Element Control")]
[AllureTag("WinForms")]
public class ElementControlTestsWinForms : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsVisibleReturnsTrue_When_ElementIsVisible_WinForms()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        Assert.IsTrue(button.IsVisible);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsPresentReturnsTrue_When_ElementIsPresent_WinForms()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        Assert.IsTrue(button.IsPresent);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsPresentReturnsFalse_When_ElementIsNotPresent_WinForms()
    {
        var button = App.Components.CreateByName<Button>("E1 Button");

        Assert.IsFalse(button.IsPresent);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsVisibleReturnsFalse_When_ElementIsNotVisible_WinForms()
    {
        var button = App.Components.CreateByAutomationId<Button>("btnShowAfter");

        Assert.IsFalse(button.IsVisible);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void GetAttributeReturnsName_When_NameAttributeIsSet_WinForms()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        var nameValue = button.GetAttribute("Name");

        Assert.AreEqual("E Button", nameValue);
    }

    [TestMethod]
    [TestCategory(Categories.Desktop)]
    public void ElementVisible_AfterCallingScrollToVisible_WinForms()
    {
        var label = App.Components.CreateByAccessibilityId<Button>("labelScrolledTo");

        label.ScrollToVisible();

        Assert.AreEqual("scrolledSuccessful", label.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void ReturnNestedElement_When_ElementContainsOneChildElement_WinForms()
    {
        var comboBox = App.Components.CreateByAccessibilityId<Button>("clBox");
        var comboBoxItem = comboBox.CreateByName<Button>("First Item 1");

        Assert.AreEqual("First Item 1", comboBoxItem.GetAttribute("Name"));
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    [App(Constants.WinFormsAppPath, Lifecycle.RestartEveryTime)]
    public void WaitForElementToExists_When_ElementIsNotVisibleInitially_WinForms()
    {
        var disappearBtn = App.Components.CreateByAutomationId<Button>("btnDisappear");
        var button = App.Components.CreateByAutomationId<Button>("btnShowAfter");
        disappearBtn.Click();

        button.ToExists().WaitToBe();

        Assert.IsTrue(button.IsVisible);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    [App(Constants.WinFormsAppPath, Lifecycle.RestartEveryTime)]
    public void WaitForElementToNotExists_When_ElementIsVisibleInitially_WinForms()
    {
        var button = App.Components.CreateByAutomationId<Button>("btnDisappear");

        button.Click();

        button.ToNotExists().WaitToBe();

        Assert.IsFalse(button.IsPresent);
    }
}
