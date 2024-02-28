// <copyright file="ElementControlTestsUniversal.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.Tests;

[TestClass]
[App(Constants.UniversalAppPath, Lifecycle.RestartEveryTime)]
[AllureSuite("Element Control")]
[AllureTag("Universal")]
public class ElementControlTestsUniversal : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsVisibleReturnsTrue_When_ElementIsVisible_Universal()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        Assert.IsTrue(button.IsVisible);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsPresentReturnsTrue_When_ElementIsPresent_Universal()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        Assert.IsTrue(button.IsPresent);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsPresentReturnsFalse_When_ElementIsNotPresent_Universal()
    {
        var button = App.Components.CreateByName<Button>("E1 Button");

        Assert.IsFalse(button.IsPresent);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsVisibleReturnsFalse_When_ElementIsNotVisible_Universal()
    {
        var button = App.Components.CreateByAutomationId<Button>("ShowAfterButton");

        Assert.IsFalse(button.IsVisible);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void GetAttributeReturnsName_When_NameAttributeIsSet_Universal()
    {
        var button = App.Components.CreateByAutomationId<Button>("EnabledButton");

        var nameValue = button.GetAttribute("Name");

        Assert.AreEqual("E Button", nameValue);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void ReturnNestedElement_When_ElementContainsOneChildElement_Universal()
    {
        var comboBox = App.Components.CreateByClass<Button>("CalendarView");
        var comboBoxItem = comboBox.CreateByName<Button>("25");

        Assert.AreEqual("25", comboBoxItem.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void ReturnNestedElements_When_ElementContainsMoreThanOneChildElement_Universal()
    {
        var comboBox = App.Components.CreateByClass<Button>("CalendarView");
        var comboBoxItems = comboBox.CreateAllByName<Button>("2");

        Assert.AreEqual(2, comboBoxItems.ToList().Count);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    [App(Constants.UniversalAppPath, Lifecycle.RestartEveryTime)]
    public void WaitForElementToExists_When_ElementIsNotVisibleInitially_Universal()
    {
        var disappearButton = App.Components.CreateByAutomationId<Button>("DisappearAfter");
        disappearButton.Click();
        var button = App.Components.CreateByAutomationId<Button>("ShowAfterButton");

        button.ToExists().WaitToBe();

        Assert.IsTrue(button.IsVisible);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    [App(Constants.UniversalAppPath, Lifecycle.RestartEveryTime)]
    public void WaitForElementToNotExists_When_ElementIsVisibleInitially_Universal()
    {
        var button = App.Components.CreateByAutomationId<Button>("DisappearAfter");
        button.Click();
        button.ToNotExists().WaitToBe();

        Assert.IsFalse(button.IsPresent);
    }
}
