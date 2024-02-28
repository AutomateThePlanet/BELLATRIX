// <copyright file="CheckBoxControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("CheckBox Control")]
[AllureFeature("ValidateExtensions")]
public class CheckBoxControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().CheckBoxLocalPage);

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void ValidateChecked_DoesNotThrowException_Checkbox_When_Checked()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox");

        checkBoxElement.Check();

        checkBoxElement.ValidateIsChecked();
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void ValidateNotChecked_DoesNotThrowException_Checkbox_When_Unchecked()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox");

        checkBoxElement.Uncheck();

        checkBoxElement.ValidateIsNotChecked();
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void ValidateStyleIs_DoesNotThrowException_Checkbox_When_StyleIsExact()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox1");

        checkBoxElement.Hover();

        checkBoxElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void ValidateIsNotDisabled_DoesNotThrowException_Checkbox_When_DisabledAttributeNotPresent()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox");

        checkBoxElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void ValidateIsDisabled_DoesNotThrowException_Checkbox_When_DisabledAttributePresent()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox3");

        checkBoxElement.ValidateIsDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void ValidateIsDisabled_DoesNotThrowException_Checkbox_When_ValueAttributeNotPresent()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox1");

        checkBoxElement.ValidateValueIs("on");
    }
}