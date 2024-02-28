// <copyright file="ButtonControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Button Control")]
[AllureFeature("ValidateExtensions")]
[AllureTag("WPF")]
public class ButtonControlValidateExtensionsTests : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void ValidateInnerTextIs_DoesNotThrowException_Button_When_InnerTextIsAsExpected()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        button.Hover();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        label.ValidateInnerTextIs("ebuttonHovered");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void ValidateIsNotDisabled_DoesNotThrowException_Button_When_DisabledAttributeNotPresent()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        button.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void ValidateIsDisabled_DoesNotThrowException_When_ButtonIsNotDisabled()
    {
        var button = App.Components.CreateByName<Button>("D Button");

        button.ValidateIsDisabled();
    }
}
