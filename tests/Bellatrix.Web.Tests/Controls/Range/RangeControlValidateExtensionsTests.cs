// <copyright file="RangeControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Range Control")]
[AllureFeature("ValidateExtensions")]
public class RangeControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().RangeLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateRangeIs_DoesNotThrowException_When_UseSetRangeMethod()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        rangeElement.SetRange(4);

        rangeElement.ValidateRangeIs(4);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOff_DoesNotThrowException_When_NoAutoCompleteAttributeIsPresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        rangeElement.ValidateAutoCompleteOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOn_DoesNotThrowException_When_AutoCompleteAttributeExistsAndIsSetToOn()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange3");

        rangeElement.ValidateAutoCompleteOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxIsNull_DoesNotThrowException_When_MaxAttributeIsNotPresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        rangeElement.ValidateMaxIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinIsNull_DoesNotThrowException_When_MinAttributeIsNotPresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        rangeElement.ValidateMinIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStepIsNull_DoesNotThrowException_When_StepAttributeIsNotPresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        rangeElement.ValidateStepIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxIs_DoesNotThrowException_When_MaxAttributeIsPresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange1");

        rangeElement.ValidateMaxIs(10);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinIs_DoesNotThrowException_When_MinAttributeIsPresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange1");

        rangeElement.ValidateMinIs(2);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStepIs_DoesNotThrowException_When_StepAttributeIsNotPresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange1");

        rangeElement.ValidateStepIs(2);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotRequired_DoesNotThrowException_When_RequiredAttributeIsNotPresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange4");

        rangeElement.ValidateIsNotRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsRequired_DoesNotThrowException_When_RequiredAttributeIsPresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange6");

        rangeElement.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange7");

        rangeElement.Hover();

        rangeElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        rangeElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange9");

        rangeElement.ValidateIsDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateListIsNull_DoesNotThrowException_When_ListAttributeIsNotPresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange");

        rangeElement.ValidateListIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateListIs_DoesNotThrowException_When_MaxAttributeIsPresent()
    {
        var rangeElement = App.Components.CreateById<Range>("myRange10");

        rangeElement.ValidateListIs("tickmarks");
    }
}