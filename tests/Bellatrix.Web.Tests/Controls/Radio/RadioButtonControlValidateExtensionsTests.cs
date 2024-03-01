// <copyright file="RadioButtonControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Radio Control")]
[AllureFeature("ValidateExtensions")]
public class RadioButtonControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().RadioLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsChecked_DoesNotThrowException_When_UseClickMethod()
    {
        var radioElement = App.Components.CreateById<RadioButton>("myRadio");

        radioElement.Click();

        radioElement.ValidateIsChecked();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover()
    {
        var radioElement = App.Components.CreateById<RadioButton>("myRadio1");

        radioElement.Hover();

        radioElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent()
    {
        var radioElement = App.Components.CreateById<RadioButton>("myRadio");

        radioElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent()
    {
        var radioElement = App.Components.CreateById<RadioButton>("myRadio3");

        radioElement.ValidateIsDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateValueIs_DoesNotThrowException_When_ValueAttributeNotPresent()
    {
        var radioElement = App.Components.CreateById<RadioButton>("myRadio1");

        radioElement.ValidateValueIs("on");
    }
}