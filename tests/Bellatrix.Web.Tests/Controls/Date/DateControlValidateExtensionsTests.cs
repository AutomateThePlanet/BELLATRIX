// <copyright file="DateControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Date Control")]
[AllureFeature("ValidateExtensions")]
public class DateControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().DateLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateDateIs_DoesNotThrowException_When_UseSetDateMethodWithDateLessThan10()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        dateElement.SetDate(2017, 7, 6);

        dateElement.ValidateDateIs("2017-07-06");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOff_DoesNotThrowException_When_NoAutoCompleteAttributeIsPresent()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        dateElement.ValidateAutoCompleteOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOn_DoesNotThrowException_When_AutoCompleteAttributeExistsAndIsSetToOn()
    {
        var dateElement = App.Components.CreateById<Date>("myDate3");

        dateElement.ValidateAutoCompleteOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateNotReadonly_DoesNotThrowException_When_ReadonlyAttributeIsNotPresent()
    {
        var dateElement = App.Components.CreateById<Date>("myDate4");

        dateElement.ValidateIsNotReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateReadonly_DoesNotThrowException_When_ReadonlyAttributeIsPresent()
    {
        var dateElement = App.Components.CreateById<Date>("myDate5");

        dateElement.ValidateIsReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxTextIsNull_DoesNotThrowException_When_MaxAttributeIsNotPresent()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        dateElement.ValidateMaxTextIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinTextIsNull_DoesNotThrowException_When_MinAttributeIsNotPresent()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        dateElement.ValidateMinTextIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStepIsNull_DoesNotThrowException_When_StepAttributeIsNotPresent()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        dateElement.ValidateStepIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxTextIs_DoesNotThrowException_When_MaxAttributeIsPresent()
    {
        var dateElement = App.Components.CreateById<Date>("myDate1");

        dateElement.ValidateMaxTextIs("2032-12-01");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinTextIs_DoesNotThrowException_MinAttributeIsPresent()
    {
        var dateElement = App.Components.CreateById<Date>("myDate1");

        dateElement.ValidateMinTextIs("1900-01-01");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStepIs_DoesNotThrowException_When_StepAttributeIsNotPresent()
    {
        var dateElement = App.Components.CreateById<Date>("myDate1");

        dateElement.ValidateStepIs(2);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateNotRequired_DoesNotThrowException_When_RequiredAttributeIsNotPresent()
    {
        var dateElement = App.Components.CreateById<Date>("myDate4");

        dateElement.ValidateIsNotRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsRequired_DoesNotThrowException_When_RequiredAttributeIsPresent_Edge()
    {
        var dateElement = App.Components.CreateById<Date>("myDate6");

        dateElement.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover()
    {
        var dateElement = App.Components.CreateById<Date>("myDate7");

        dateElement.Hover();

        dateElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        dateElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent()
    {
        var dateElement = App.Components.CreateById<Date>("myDate9");

        dateElement.ValidateIsDisabled();
    }
}