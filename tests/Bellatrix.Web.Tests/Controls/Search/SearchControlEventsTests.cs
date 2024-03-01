// <copyright file="SearchControlEventsTests.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Search Control")]
[AllureFeature("ControlEvents")]
public class SearchControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().SearchLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingSearchCalled_BeforeActuallySetSearch()
    {
        Search.SettingSearch += AssertValueAttributeEmpty;

        var searchElement = App.Components.CreateById<Search>("mySearch");

        searchElement.SetSearch("bellatrix test framework");

        Assert.AreEqual("bellatrix test framework", searchElement.GetSearch());

        Search.SettingSearch -= AssertValueAttributeEmpty;

        void AssertValueAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingSearchCalled_AfterSetSearch()
    {
        Search.SearchSet += AssertValueAttributeContainsNewValue;

        var searchElement = App.Components.CreateById<Search>("mySearch");

        searchElement.SetSearch("bellatrix test framework");

        Search.SearchSet -= AssertValueAttributeContainsNewValue;

        void AssertValueAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("bellatrix test framework", args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        Search.Hovering += AssertStyleAttributeEmpty;

        var searchElement = App.Components.CreateById<Search>("mySearch8");

        searchElement.Hover();

        Assert.AreEqual("color: red;", searchElement.GetStyle());

        Search.Hovering -= AssertStyleAttributeEmpty;

        void AssertStyleAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveredCalled_AfterHover()
    {
        Search.Hovered += AssertStyleAttributeContainsNewValue;

        var searchElement = App.Components.CreateById<Search>("mySearch8");

        searchElement.Hover();

        Search.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<Search>("mySearch8").ValidateStyleIs("color: red;");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusingCalled_BeforeActuallyFocus()
    {
        Search.Focusing += AssertStyleAttributeEmpty;

        var searchElement = App.Components.CreateById<Search>("mySearch9");

        searchElement.Focus();

        Assert.AreEqual("color: blue;", searchElement.GetStyle());

        Search.Focusing -= AssertStyleAttributeEmpty;

        void AssertStyleAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusedCalled_AfterFocus()
    {
        Search.Focused += AssertStyleAttributeContainsNewValue;

        var searchElement = App.Components.CreateById<Search>("mySearch9");

        searchElement.Focus();

        Search.Focused -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
        }
    }
}