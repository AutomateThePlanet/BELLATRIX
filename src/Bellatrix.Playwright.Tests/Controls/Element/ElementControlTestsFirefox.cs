﻿// <copyright file="ElementControlTestsFirefox.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Playwright.Tests.Controls.Element;

[TestClass]
[Browser(BrowserTypes.Firefox, Lifecycle.ReuseIfStarted)]
[AllureSuite("Element Control")]
public class ElementControlTestsFirefox : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ElementPage);

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void IsVisibleReturnsTrue_When_ElementIsPresent_Firefox()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        Assert.IsTrue(urlElement.IsVisible);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void IsVisibleReturnsFalse_When_ElementIsHidden_Firefox()
    {
        var urlElement = App.Components.CreateById<Url>("myURL11");

        Assert.IsFalse(urlElement.IsVisible);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void SetAttributeChangesAttributeValue_Firefox()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        urlElement.SetAttribute("class", "myTestClass1");
        var cssClass = urlElement.GetAttribute("class");

        Assert.AreEqual("myTestClass1", cssClass);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetAttributeReturnsName_When_NameAttributeIsSet_Firefox()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        var nameValue = urlElement.GetAttribute("name");

        Assert.AreEqual("myURL", nameValue);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetAttributeReturnsEmpty_When_NameAttributeIsNotPresent_Firefox()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        var nameValue = urlElement.GetAttribute("style");

        Assert.AreEqual(string.Empty, nameValue);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void CssClassReturnsMyTestClass_When_ClassAttributeIsSet_Firefox()
    {
        var urlElement = App.Components.CreateById<Url>("myURL");

        var cssClass = urlElement.CssClass;

        Assert.AreEqual("myTestClass", cssClass);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void CssClassReturnsNull_When_ClassAttributeIsNotPresent_Firefox()
    {
        var urlElement = App.Components.CreateById<Url>("myURL1");

        var cssClass = urlElement.CssClass;

        Assert.IsNull(cssClass);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ElementVisible_AfterCallingScrollToVisible_Firefox()
    {
        var urlElement = App.Components.CreateById<Url>("myURL12");

        urlElement.ScrollToVisible();

        Assert.AreEqual("color: red;", urlElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void CreateElement_When_InsideAnotherElementAndIsPresent_Firefox()
    {
        var wrapperDiv = App.Components.CreateById<Div>("myURL10Wrapper");

        var urlElement = wrapperDiv.CreateById<Url>("myURL10");

        Assert.IsTrue(urlElement.IsDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetTitle_When_TitleAttributeIsPresent_Firefox()
    {
        var element = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL13");

        string title = element.GetTitle();

        Assert.AreEqual("bellatrix.solutions", title);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void RetunsNull_When_TitleAttributeIsNotPresent_Firefox()
    {
        var element = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL12");

        string title = element.GetTitle();

        Assert.IsNull(title);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetTabIndexOne_When_TabIndexAttributeIsPresent_Firefox()
    {
        var element = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL14");

        string tabIndex = element.GetTabIndex();

        Assert.AreEqual("1", tabIndex);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnsNull_When_TabIndexAttributeIsNotPresent_Firefox()
    {
        var element = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL12");

        string tabIndex = element.GetTabIndex();

        Assert.IsNull(tabIndex);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetStyle_When_StyleAttributeIsPresent_Firefox()
    {
        var element = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL16");

        var style = element.GetStyle();

        Assert.AreEqual("color: green;", style);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnsNull_When_StyleAttributeIsNotPresent_Firefox()
    {
        var element = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL");

        string style = element.GetStyle();

        Assert.AreEqual(null, style);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetDir_When_DirAttributeIsPresent_Firefox()
    {
        var element = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL19");

        var dir = element.GetDir();

        Assert.AreEqual("rtl", dir);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnsNull_When_DirAttributeIsNotPresent_Firefox()
    {
        var element = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL12");

        string dir = element.GetDir();

        Assert.IsNull(dir);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void GetLang_When_LangAttributeIsPresent_Firefox()
    {
        var element = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL20");

        var lang = element.GetLang();

        Assert.AreEqual("en", lang);
    }

    [TestMethod]
    [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnsNull_When_LangAttributeIsNotPresent_Firefox()
    {
        var element = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL12");

        string lang = element.GetLang();

        Assert.IsNull(lang);
    }
}