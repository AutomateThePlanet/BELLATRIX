// <copyright file="MonthControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Month Control")]
[AllureFeature("ValidateExtensions")]
public class MonthControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().MonthLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMonthIs_DoesNotThrowException_When_UseSetMonthMethodWithMonthLessThan10()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        monthElement.SetMonth(2017, 7);

        monthElement.ValidateMonthIs("2017-07");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOff_DoesNotThrowException_When_NoAutoCompleteAttributeIsPresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        Assert.IsFalse(monthElement.IsAutoComplete);
        monthElement.ValidateAutoCompleteOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOn_DoesNotThrowException_When_AutoCompleteAttributeExistsAndIsSetToOn()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth3");

        monthElement.ValidateAutoCompleteOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotReadonly_DoesNotThrowException_When_ReadonlyAttributeIsNotPresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth4");

        monthElement.ValidateIsNotReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsReadonly_DoesNotThrowException_When_ReadonlyAttributeIsPresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth5");

        monthElement.ValidateIsReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxTextIsNull_DoesNotThrowException_When_MaxAttributeIsNotPresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        monthElement.ValidateMaxTextIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinTextIsNull_DoesNotThrowException_When_MinAttributeIsNotPresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        monthElement.ValidateMinTextIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStepIsNull_DoesNotThrowException_When_StepAttributeIsNotPresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        monthElement.ValidateStepIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxTextIs_DoesNotThrowException_When_MaxAttributeIsPresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth1");

        monthElement.ValidateMaxTextIs("2032-12");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinTextIs_DoesNotThrowException_When_MinAttributeIsPresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth1");

        monthElement.ValidateMinTextIs("1900-01");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStepIs_DoesNotThrowException_When_StepAttributeIsNotPresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth1");

        monthElement.ValidateStepIs(2);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotRequired_DoesNotThrowException_When_RequiredAttributeIsNotPresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth4");

        monthElement.ValidateIsNotRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsRequired_DoesNotThrowException_When_RequiredAttributeIsPresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth6");

        monthElement.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth7");

        monthElement.Hover();

        monthElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        monthElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth9");

        monthElement.ValidateIsDisabled();
    }
}