// <copyright file="TimeControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Time Control")]
[AllureFeature("ValidateExtensions")]
public class TimeControlValidateExtensionsTests : MSTest.WebTest
{
    private string _url = ConfigurationService.GetSection<TestPagesSettings>().TimeLocalPage;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(_url);
        ////_url = App.Browser.Url.ToString();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateTitleIs_DoesNotThrowException_When_UseSetTimeMethod()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        timeElement.SetTime(11, 11);

        timeElement.ValidateTimeIs("11:11:00");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOff_DoesNotThrowException_When_NoAutoCompleteAttributeIsPresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        Assert.IsFalse(timeElement.IsAutoComplete);
        timeElement.ValidateAutoCompleteOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOn_DoesNotThrowException_When_AutoCompleteAttributeExistsAndIsSetToOn()
    {
        var timeElement = App.Components.CreateById<Time>("myTime3");

        timeElement.ValidateAutoCompleteOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotReadonly_DoesNotThrowException_When_ReadonlyAttributeIsNotPresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime4");

        timeElement.ValidateIsNotReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsReadonly_DoesNotThrowException_When_ReadonlyAttributeIsPresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime5");

        timeElement.ValidateIsReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxTextIsNull_DoesNotThrowException_When_MaxAttributeIsNotPresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        timeElement.ValidateMaxTextIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinTextIsNull_DoesNotThrowException_When_MinAttributeIsNotPresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        timeElement.ValidateMinTextIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStepIsNull_DoesNotThrowException_When_StepAttributeIsNotPresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        timeElement.ValidateStepIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxTextIs_DoesNotThrowException_When_MaxAttributeIsPresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime1");

        timeElement.ValidateMaxTextIs("11:11");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinTextIs_DoesNotThrowException_When_MinAttributeIsPresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime1");

        timeElement.ValidateMinTextIs("00:01");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStepIs_DoesNotThrowException_When_StepAttributeIsNotPresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime1");

        timeElement.ValidateStepIs(10);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotRequired_DoesNotThrowException_When_RequiredAttributeIsNotPresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime4");

        timeElement.ValidateIsNotRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsRequired_DoesNotThrowException_When_RequiredAttributeIsPresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime6");

        timeElement.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover()
    {
        var timeElement = App.Components.CreateById<Time>("myTime7");

        timeElement.Hover();

        timeElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime");

        timeElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent()
    {
        var timeElement = App.Components.CreateById<Time>("myTime9");

        timeElement.ValidateIsDisabled();
    }
}