// <copyright file="DateTimeLocalControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("DateTimeLocal Control")]
[AllureFeature("ValidateExtensions")]
public class DateTimeLocalControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().DateTimeLocalLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateTimeIs_DoesNotThrowException_When_UseSetDateTimeLocalMethod()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        timeElement.SetTime(new DateTime(1989, 10, 28, 23, 23, 0));

        timeElement.ValidateTimeIs("1989-10-28T23:23");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOff_DoesNotThrowException_When_NoAutoCompleteAttributeIsPresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        timeElement.ValidateAutoCompleteOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime4");

        Assert.IsFalse(timeElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOn_DoesNotThrowException_When_AutoCompleteAttributeExistsAndIsSetToOn()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime3");

        timeElement.ValidateAutoCompleteOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateNotReadonly_DoesNotThrowException_When_ReadonlyAttributeIsNotPresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime4");

        timeElement.ValidateIsNotReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateReadonly_DoesNotThrowException_When_ReadonlyAttributeIsPresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime5");

        timeElement.ValidateIsReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxTextIsNull_DoesNotThrowException_When_MaxAttributeIsNotPresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        timeElement.ValidateMaxTextIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinTextIsNull_DoesNotThrowException_When_MinAttributeIsNotPresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        timeElement.ValidateMinTextIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStepIsNull_DoesNotThrowException_When_StepAttributeIsNotPresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        timeElement.ValidateStepIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxTextIs_DoesNotThrowException_MaxAttributeIsPresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime1");

        timeElement.ValidateMaxTextIs("2017-06-30T16:30");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinTextIs_DoesNotThrowException_When_MinAttributeIsPresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime1");

        timeElement.ValidateMinTextIs("2017-06-01T08:30");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStepIs_DoesNotThrowException_When_StepAttributeIsNotPresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime1");

        timeElement.ValidateStepIs(10);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateNotRequired_DoesNotThrowException_When_RequiredAttributeIsNotPresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime4");

        timeElement.ValidateIsNotRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsRequired_DoesNotThrowException_When_RequiredAttributeIsPresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime6");

        timeElement.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime7");

        timeElement.Hover();

        timeElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException__When_DisabledAttributeNotPresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        timeElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent()
    {
        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime9");

        timeElement.ValidateIsDisabled();
    }
}