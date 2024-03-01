// <copyright file="SearchControlTestsFirefox.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserType.Firefox, Lifecycle.ReuseIfStarted)]
[AllureSuite("Search Control")]
public class SearchControlTestsFirefox : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().SearchLocalPage);

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void SearchSet_When_UseSetSearchMethod_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch");

        searchElement.SetSearch("bellatrix test framework");

        Assert.AreEqual("bellatrix test framework", searchElement.GetSearch());
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetSearchReturnsCorrectSearch_When_DefaultSearchIsSet_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch3");

        Assert.AreEqual("search for stars", searchElement.GetSearch());
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch");

        Assert.AreEqual(false, searchElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch5");

        Assert.AreEqual(false, searchElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch4");

        Assert.AreEqual(true, searchElement.IsAutoComplete);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch4");

        Assert.AreEqual(false, searchElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch6");

        Assert.AreEqual(true, searchElement.IsReadonly);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMaxLengthReturnsNull_When_MaxLengthAttributeIsNotPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch");

        var maxLength = searchElement.MaxLength;

        Assert.IsNull(maxLength);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMinLengthReturnsNull_When_MinLengthAttributeIsNotPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch");

        Assert.IsNull(searchElement.MinLength);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetSizeReturnsDefault20_When_SizeAttributeIsNotPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch");

        // Specifies the width of an <input> element, in characters. Default value is 20
        Assert.AreEqual(20, searchElement.Size);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMaxLengthReturns80_When_MaxLengthAttributeIsPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch2");

        Assert.AreEqual(80, searchElement.MaxLength);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetMinLengthReturns10_When_MinLengthAttributeIsPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch2");

        Assert.AreEqual(10, searchElement.MinLength);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetSizeReturns30_When_SizeAttributeIsNotPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch2");

        Assert.AreEqual(30, searchElement.Size);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch4");

        Assert.AreEqual(false, searchElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch7");

        Assert.AreEqual(true, searchElement.IsRequired);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetPlaceholder_When_PlaceholderAttributeIsSet_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch");

        Assert.AreEqual("your search term goes here", searchElement.Placeholder);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetPlaceholderReturnsNull_When_PlaceholderAttributeIsNotPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch1");

        Assert.IsNull(searchElement.Placeholder);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch8");

        searchElement.Hover();

        searchElement.ValidateStyleIs("color: red;");
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch9");

        searchElement.Focus();

        searchElement.ValidateStyleIs("color: blue;");
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch9");

        bool isDisabled = searchElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Firefox()
    {
        var searchElement = App.Components.CreateById<Search>("mySearch10");

        bool isDisabled = searchElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}