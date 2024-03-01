// <copyright file="PasswordControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Password Control")]
[AllureFeature("ValidateExtensions")]
public class PasswordControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().PasswordLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePasswordIs_DoesNotThrowException_When_UseSetPasswordMethod()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword");

        passwordElement.SetPassword("bellatrix");

        passwordElement.ValidatePasswordIs("bellatrix");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOff_DoesNotThrowException_When_NoAutoCompleteAttributeIsPresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword");

        passwordElement.ValidateAutoCompleteOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOn_DoesNotThrowException_When_AutoCompleteAttributeExistsAndIsSetToOn()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword4");

        passwordElement.ValidateAutoCompleteOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotReadonly_DoesNotThrowException_When_ReadonlyAttributeIsNotPresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword4");

        passwordElement.ValidateIsNotReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsReadonly_DoesNotThrowException_When_ReadonlyAttributeIsPresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword6");

        passwordElement.ValidateIsReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxLengthIsNull_DoesNotThrowException_When_MaxLengthAttributeIsNotPresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword");

        passwordElement.ValidateMaxLengthIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinLengthIsNull_DoesNotThrowException_When_MinLengthAttributeIsNotPresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword");

        passwordElement.ValidateMinLengthIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateSizeIs_DoesNotThrowException_When_SizeAttributeIsNotPresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword");

        passwordElement.ValidateSizeIs(20);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxLengthIs_DoesNotThrowException_When_MaxLengthAttributeIsPresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword2");

        passwordElement.ValidateMaxLengthIs(80);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinLengthIs_DoesNotThrowException_When_MinLengthAttributeIsPresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword2");

        passwordElement.ValidateMinLengthIs(10);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotRequired_DoesNotThrowException_When_RequiredAttributeIsNotPresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword4");

        passwordElement.ValidateIsNotRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsRequired_DoesNotThrowException_When_RequiredAttributeIsPresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword7");

        passwordElement.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePlaceholderIs_DoesNotThrowException_When_PlaceholderAttributeIsSet()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword");

        passwordElement.ValidatePlaceholderIs("your password term goes here");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePlaceholderIsNull_DoesNotThrowException_When_PlaceholderAttributeIsNotPresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword1");

        passwordElement.ValidatePlaceholderIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword8");

        passwordElement.Hover();

        passwordElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword9");

        passwordElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent()
    {
        var passwordElement = App.Components.CreateById<Password>("myPassword10");

        passwordElement.ValidateIsDisabled();
    }
}