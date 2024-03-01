// <copyright file="NumberControlValidateExtensionTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Number Control")]
public class NumberControlValidateExtensionTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().NumberLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateNumberIs_DoesNotThrowException_When_UseSetNumberMethod()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber");

        numberElement.SetNumber(12.1);

        numberElement.ValidateNumberIs(12.1);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOff_DoesNotThrowException_When_NoAutoCompleteAttributeIsPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber");

        numberElement.ValidateAutoCompleteOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOn_DoesNotThrowException_When_AutoCompleteAttributeExistsAndIsSetToOn()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber3");

        numberElement.ValidateAutoCompleteOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotReadonly_DoesNotThrowException_When_ReadonlyAttributeIsNotPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber4");

        numberElement.ValidateIsNotReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotReadonly_DoesNotThrowException_When_ReadonlyAttributeIsPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber5");

        numberElement.ValidateIsReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxIsNull_DoesNotThrowException_When_MaxAttributeIsNotPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber");

        numberElement.ValidateMaxIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinIsNull_DoesNotThrowException_When_MinAttributeIsNotPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber");

        numberElement.ValidateMinIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStepIsNull_DoesNotThrowException_When_StepAttributeIsNotPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber");

        numberElement.ValidateStepIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxIs_DoesNotThrowException_When_MaxAttributeIsPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber1");

        numberElement.ValidateMaxIs(20);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinIs_DoesNotThrowException_When_MinAttributeIsPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber1");

        numberElement.ValidateMinIs(10);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStepIs_DoesNotThrowException_When_StepAttributeIsNotPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber1");

        numberElement.ValidateStepIs(2);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotRequired_DoesNotThrowException_When_RequiredAttributeIsNotPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber4");

        numberElement.ValidateIsNotRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsRequired_DoesNotThrowException_When_RequiredAttributeIsPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber6");

        numberElement.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePlaceholderIs_DoesNotThrowException_When_PlaceholderAttributeIsSet()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber");

        numberElement.ValidatePlaceholderIs("Multiple of 10");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePlaceholderIsNull_DoesNotThrowException_When_PlaceholderAttributeIsNotPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber1");

        numberElement.ValidatePlaceholderIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber7");

        numberElement.Hover();

        numberElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber");

        numberElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent()
    {
        var numberElement = App.Components.CreateById<Number>("myNumber9");

        numberElement.ValidateIsDisabled();
    }
}