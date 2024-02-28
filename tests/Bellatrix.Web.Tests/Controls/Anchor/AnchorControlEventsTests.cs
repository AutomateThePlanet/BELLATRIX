// <copyright file="AnchorControlEventsTests.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
[AllureSuite("Anchor Control")]
[AllureFeature("ControlEvents")]
public class AnchorControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().AnchorLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ClickingCalled_BeforeActuallyClick()
    {
        Anchor.Clicking += AssertUrlNotAutomateThePlanet;

        var anchorElement = App.Components.CreateById<Anchor>("myAnchor");

        anchorElement.Click();

        App.Navigation.WaitForPartialUrl("automatetheplanet");

        Assert.AreEqual("https://www.automatetheplanet.com/", App.Browser.Url.ToString());

        Anchor.Clicking -= AssertUrlNotAutomateThePlanet;

        void AssertUrlNotAutomateThePlanet(object sender, ComponentActionEventArgs args)
        {
            Assert.AreNotEqual("https://automatetheplanet.com/", App.Browser.Url.ToString());
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ClickedCalled_AfterClick()
    {
        Anchor.Clicked += AssertUrlAutomateThePlanet;

        var anchorElement = App.Components.CreateById<Anchor>("myAnchor");

        anchorElement.Click();

        Anchor.Clicked -= AssertUrlAutomateThePlanet;

        void AssertUrlAutomateThePlanet(object sender, ComponentActionEventArgs args)
        {
            App.Navigation.WaitForPartialUrl("automatetheplanet");
            Assert.AreEqual("https://www.automatetheplanet.com/", App.Browser.Url.ToString());
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        Anchor.Hovering += AssertStyleAttributeEmpty;

        var anchorElement = App.Components.CreateById<Anchor>("myAnchor1");

        anchorElement.Hover();

        Assert.AreEqual("color: red;", anchorElement.GetStyle());

        Anchor.Hovering -= AssertStyleAttributeEmpty;

        void AssertStyleAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void HoveredCalled_AfterHover()
    {
        Anchor.Hovered += AssertStyleAttributeContainsNewValue;

        var anchorElement = App.Components.CreateById<Anchor>("myAnchor1");

        anchorElement.Hover();

        Anchor.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<Anchor>("myAnchor1").ValidateStyleIs("color: red;");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void FocusingCalled_BeforeActuallyFocus()
    {
        Anchor.Focusing += AssertStyleAttributeEmpty;

        var anchorElement = App.Components.CreateById<Anchor>("myAnchor1");

        anchorElement.Focus();

        Anchor.Focusing -= AssertStyleAttributeEmpty;

        void AssertStyleAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void FocusedCalled_AfterFocus()
    {
        Anchor.Focused += AssertStyleAttributeContainsNewValue;

        var anchorElement = App.Components.CreateById<Anchor>("myAnchor2");

        anchorElement.Focus();

        Anchor.Focused -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
        }
    }
}