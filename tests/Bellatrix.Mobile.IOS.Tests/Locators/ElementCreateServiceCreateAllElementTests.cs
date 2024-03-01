// <copyright file="ComponentCreateServiceCreateAllElementTests.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Mobile.IOS.Tests;

[TestClass]
[IOS(Constants.IOSNativeAppPath,
    Constants.IOSAppBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
[AllureSuite("Services")]
[AllureFeature("ComponentCreateService")]
public class ComponentCreateServiceCreateAllElementTests : MSTest.IOSTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    [Timeout(180000)]
    public void ElementFound_When_CreateAllById_And_ElementIsOnScreen()
    {
        var textFields = App.Components.CreateAllById<TextField>("IntegerA");

        textFields[0].ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [Timeout(180000)]
    public void ElementFound_When_CreateAllByClass()
    {
        var textFields = App.Components.CreateAllByClass<CheckBox>("XCUIElementTypeTextField");

        textFields[0].ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [Timeout(180000)]
    public void ElementFound_When_CreateAllByvalueContaining_And_ElementIsOnScreen()
    {
        var labels = App.Components.CreateAllByValueContaining<Label>("SumLabel");

        labels[0].ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [Timeout(180000)]
    public void ElementFound_When_CreateAllByIOSNsPredicate_And_ElementIsOnScreen()
    {
        var buttons = App.Components.CreateAllByIOSNsPredicate<Button>("type == \"XCUIElementTypeButton\" AND name == \"ComputeSumButton\"");

        buttons[0].ValidateIsVisible();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [Timeout(180000)]
    public void ElementFound_When_CreateAllByXPath_And_ElementIsOnScreen()
    {
        var buttons = App.Components.CreateAllByXPath<Button>("//XCUIElementTypeButton[@name=\"ComputeSumButton\"]");

        buttons[0].ValidateIsVisible();
    }
}
