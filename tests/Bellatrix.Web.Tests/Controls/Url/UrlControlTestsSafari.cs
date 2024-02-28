// <copyright file="UrlControlTestsSafari.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserType.Safari, Lifecycle.ReuseIfStarted)]
[AllureSuite("Url Control")]
public class UrlControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().UrlLocalPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void UrlSet_When_UseSetUrlMethod_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        urlElement.SetUrl("bellatrix.solutions");

        Assert.AreEqual("bellatrix.solutions", urlElement.GetUrl());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetUrlReturnsCorrectUrl_When_DefaultUrlIsSet_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL3");

        Assert.AreEqual("http://www.example.com", urlElement.GetUrl());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_NoAutoCompleteAttributeIsPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        Assert.AreEqual(false, urlElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsFalse_When_AutoCompleteAttributeExistsAndIsSetToOff_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL5");

        Assert.AreEqual(false, urlElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void AutoCompleteReturnsTrue_When_AutoCompleteAttributeExistsAndIsSetToOn_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL4");

        Assert.AreEqual(true, urlElement.IsAutoComplete);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsFalse_When_ReadonlyAttributeIsNotPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL4");

        Assert.AreEqual(false, urlElement.IsReadonly);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetReadonlyReturnsTrue_When_ReadonlyAttributeIsPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL6");

        Assert.AreEqual(true, urlElement.IsReadonly);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMaxLengthReturnsNull_When_MaxLengthAttributeIsNotPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        var maxLength = urlElement.MaxLength;

        Assert.IsNull(maxLength);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMinLengthReturnsNull_When_MinLengthAttributeIsNotPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        Assert.IsNull(urlElement.MinLength);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetSizeReturnsDefault20_When_SizeAttributeIsNotPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        // Specifies the width of an <input> element, in characters. Default value is 20
        Assert.AreEqual(20, urlElement.Size);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMaxLengthReturns80_When_MaxLengthAttributeIsPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL2");

        Assert.AreEqual(80, urlElement.MaxLength);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetMinLengthReturns10_When_MinLengthAttributeIsPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL2");

        Assert.AreEqual(10, urlElement.MinLength);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetSizeReturns30_When_SizeAttributeIsNotPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL2");

        Assert.AreEqual(30, urlElement.Size);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsFalse_When_RequiredAttributeIsNotPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL4");

        Assert.AreEqual(false, urlElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetRequiredReturnsTrue_When_RequiredAttributeIsPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL7");

        Assert.AreEqual(true, urlElement.IsRequired);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetPlaceholder_When_PlaceholderAttributeIsSet_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        Assert.AreEqual("http://www.example.com", urlElement.Placeholder);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void GetPlaceholderReturnsNull_When_PlaceholderAttributeIsNotPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL1");

        Assert.IsNull(urlElement.Placeholder);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL8");

        urlElement.Hover();

        Assert.AreEqual("color: red;", urlElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL9");

        urlElement.Focus();

        Assert.AreEqual("color: blue;", urlElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL9");

        bool isDisabled = urlElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Safari()
    {
        var urlElement = App.Components.CreateById<Url>("myURL10");

        bool isDisabled = urlElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }
}