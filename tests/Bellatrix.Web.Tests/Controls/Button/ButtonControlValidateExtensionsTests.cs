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

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Button Control")]
[AllureFeature("ValidateExtensions")]
public class ButtonControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ButtonLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateInnerTextIs_DoesNotThrowException_Button_When_InnerTextIsAsExpected()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton8");

        buttonElement.Click();

        buttonElement.ValidateInnerTextIs("Stop");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_Button_When_Hovered()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton9");

        buttonElement.Hover();

        buttonElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleContains_DoesNotThrowException_Button_When_Hovered()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton9");

        buttonElement.Hover();

        buttonElement.ValidateStyleContains("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleNotContains_DoesNotThrowException_Button_When_Hovered()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton9");

        buttonElement.Hover();

        buttonElement.ValidateStyleNotContains("color: blue;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_Button_When_DisabledAttributeNotPresent()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton8");

        buttonElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_Button_When_DisabledAttributePresent()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton11");

        buttonElement.ValidateIsDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateValueIs_DoesNotThrowException_Button_When_ValueAttributePresent()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton8");

        buttonElement.ValidateValueIs("Start");
    }
}