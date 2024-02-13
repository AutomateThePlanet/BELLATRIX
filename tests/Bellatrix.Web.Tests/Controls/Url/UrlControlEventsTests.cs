// <copyright file="UrlControlEventsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Url Control")]
[AllureFeature("ControlEvents")]
public class UrlControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().UrlLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingUrlCalled_BeforeActuallySetUrl()
    {
        Url.SettingUrl += AssertValueAttributeEmpty;

        var urlElement = App.Components.CreateById<Url>("myURL");

        urlElement.SetUrl("bellatrix.solutions");

        Assert.AreEqual("bellatrix.solutions", urlElement.GetUrl());

        Url.SettingUrl -= AssertValueAttributeEmpty;

        void AssertValueAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingUrlCalled_AfterSetUrl()
    {
        Url.UrlSet += AssertValueAttributeContainsNewValue;

        var urlElement = App.Components.CreateById<Url>("myURL");

        urlElement.SetUrl("bellatrix.solutions");

        Url.UrlSet -= AssertValueAttributeContainsNewValue;

        void AssertValueAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("bellatrix.solutions", args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        Url.Hovering += AssertStyleAttributeEmpty;

        var urlElement = App.Components.CreateById<Url>("myURL8");

        urlElement.Hover();

        Assert.AreEqual("color: red;", urlElement.GetStyle());

        Url.Hovering -= AssertStyleAttributeEmpty;

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
        Url.Hovered += AssertStyleAttributeContainsNewValue;

        var urlElement = App.Components.CreateById<Url>("myURL8");

        urlElement.Hover();

        Url.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<Url>("myURL8").ValidateStyleIs("color: red;");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusingCalled_BeforeActuallyFocus()
    {
        Url.Focusing += AssertStyleAttributeEmpty;

        var urlElement = App.Components.CreateById<Url>("myURL9");

        urlElement.Focus();

        Assert.AreEqual("color: blue;", urlElement.GetStyle());

        Url.Focusing -= AssertStyleAttributeEmpty;

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
        Url.Focused += AssertStyleAttributeContainsNewValue;

        var urlElement = App.Components.CreateById<Url>("myURL9");

        urlElement.Focus();

        Url.Focused -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
        }
    }
}