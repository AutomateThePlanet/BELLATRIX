// <copyright file="SearchControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Search Control")]
[AllureFeature("ValidateExtensions")]
public class SearchControlValidateExtensionsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().SearchLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateSearchIs_DoesNotThrowException_When_UseSetSearchMethod_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch");

        searchElement.SetSearch("bellatrix test framework");

        searchElement.ValidateSearchIs("bellatrix test framework");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOff_DoesNotThrowException_When_NoAutoCompleteAttributeIsPresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch");

        searchElement.ValidateAutoCompleteOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateAutoCompleteOn_DoesNotThrowException_When_AutoCompleteAttributeExistsAndIsSetToOn_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch4");

        searchElement.ValidateAutoCompleteOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotReadonly_DoesNotThrowException_When_ReadonlyAttributeIsNotPresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch4");

        searchElement.ValidateIsNotReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsReadonly_DoesNotThrowException_When_ReadonlyAttributeIsPresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch6");

        searchElement.ValidateIsReadonly();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxLengthIsNull_DoesNotThrowException_When_MaxLengthAttributeIsNotPresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch");

        searchElement.ValidateMaxLengthIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinLengthIsNull_DoesNotThrowException_When_MinLengthAttributeIsNotPresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch");

        searchElement.ValidateMinLengthIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateSizeIs_DoesNotThrowException_When_SizeAttributeIsNotPresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch");

        searchElement.ValidateSizeIs(20);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMaxLengthIs_DoesNotThrowException_When_MaxLengthAttributeIsPresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch2");

        searchElement.ValidateMaxLengthIs(80);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateMinLengthIs_DoesNotThrowException_When_MinLengthAttributeIsPresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch2");

        searchElement.ValidateMinLengthIs(10);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotRequired_DoesNotThrowException_When_RequiredAttributeIsNotPresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch4");

        searchElement.ValidateIsNotRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsRequired_DoesNotThrowException_When_RequiredAttributeIsPresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch7");

        searchElement.ValidateIsRequired();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePlaceholderIs_DoesNotThrowException_When_PlaceholderAttributeIsSet_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch");

        searchElement.ValidatePlaceholderIs("your search term goes here");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidatePlaceholderIsNull_DoesNotThrowException_When_PlaceholderAttributeIsNotPresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch1");

        searchElement.ValidatePlaceholderIsNull();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateStyleIs_DoesNotThrowException_When_Hover_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch8");

        searchElement.Hover();

        searchElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_DisabledAttributeNotPresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch9");

        searchElement.ValidateIsNotDisabled();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ValidateIsDisabled_DoesNotThrowException_When_DisabledAttributePresent_Edge()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch10");

        searchElement.ValidateIsDisabled();
    }
}