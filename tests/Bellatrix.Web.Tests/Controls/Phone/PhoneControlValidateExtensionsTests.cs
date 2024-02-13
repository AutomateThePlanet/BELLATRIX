// <copyright file="PhoneControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Phone Control")]
[AllureFeature("ValidateExtensions")]
public class PhoneControlValidateExtensionsExceptionMessagesTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().PhoneLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePhoneIs_DoesNotThrowException_When_UseSetPhoneMethod()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone");

        phoneElement.SetPhone("123-4567-8901");

        phoneElement.ValidatePhoneIs("123-4567-8901");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOff_DoesNotThrowException_When_NoAutoCompleteAttributeIsPresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone");

        phoneElement.ValidateAutoCompleteOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOn_DoesNotThrowException_When_AutoCompleteAttributeExistsAndIsSetToOn()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone4");

        phoneElement.ValidateAutoCompleteOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotReadonly_DoesNotThrowException_When_ReadonlyAttributeIsNotPresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone4");

        phoneElement.ValidateIsNotReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsReadonly_DoesNotThrowException_When_ReadonlyAttributeIsPresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone6");

        phoneElement.ValidateIsReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxLengthIsNull_DoesNotThrowException_When_MaxLengthAttributeIsNotPresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone");

        phoneElement.ValidateMaxLengthIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinLengthIsNull_DoesNotThrowException_When_MinLengthAttributeIsNotPresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone");

        phoneElement.ValidateMinLengthIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateSizeIs_DoesNotThrowException_When_SizeAttributeIsNotPresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone");

        phoneElement.ValidateSizeIs(20);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxLengthIs_DoesNotThrowException_When_MaxLengthAttributeIsPresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone2");

        phoneElement.ValidateMaxLengthIs(80);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinLengthIs_DoesNotThrowException_When_MinLengthAttributeIsPresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone2");

        phoneElement.ValidateMinLengthIs(10);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotRequired_DoesNotThrowException_When_RequiredAttributeIsNotPresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone4");

        phoneElement.ValidateIsNotRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsRequired_DoesNotThrowException_When_RequiredAttributeIsPresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone7");

        phoneElement.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePlaceholderIs_DoesNotThrowException_When_PlaceholderAttributeIsSet()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone");

        phoneElement.ValidatePlaceholderIs("123-4567-8901");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePlaceholderIsNull_DoesNotThrowException_When_PlaceholderAttributeIsNotPresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone1");

        phoneElement.ValidatePlaceholderIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone8");

        phoneElement.Hover();

        phoneElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone9");

        phoneElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent()
    {
        var phoneElement = App.Components.CreateById<Phone>("myPhone10");

        phoneElement.ValidateIsDisabled();
    }
}