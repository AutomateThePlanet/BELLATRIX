// <copyright file="AnchorControlTestsSafari.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Anchor Control")]
public class AnchorControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().AnchorLocalPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ClickOpensAutomateThePlanetUrl_When_DefaultClickIsSet_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor");

        anchorElement.Click();

        App.Navigation.WaitForPartialUrl("automatetheplanet");

        Assert.IsTrue(App.Browser.Url.ToString().Contains("automatetheplanet.com"));
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor1");

        anchorElement.Hover();

        Assert.AreEqual("color: red;", anchorElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor2");

        anchorElement.Focus();
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnAutomateThePlanet_When_InnerText_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor");

        Assert.AreEqual("Automate The Planet", anchorElement.InnerText);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnButtonHtml_When_InnerHtml_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor4");

        Assert.IsTrue(anchorElement.InnerHtml.Contains("<button name=\"button\">Click me</button>"));
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnEmpty_When_InnerTextNotSet_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor6");

        Assert.AreEqual(string.Empty, anchorElement.InnerText);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnEmpty_When_InnerHtmlNotSet_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor6");

        string actualInnerHtml = anchorElement.InnerHtml;

        Assert.AreEqual(string.Empty, actualInnerHtml);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnEmpty_When_RelNotSet_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor");

        string actualRel = anchorElement.Rel;

        Assert.AreEqual(string.Empty, actualRel);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnCanonical_When_RelRel_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor5");

        Assert.AreEqual("canonical", anchorElement.Rel);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnEmpty_When_TargetNotSet_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor1");

        Assert.AreEqual(string.Empty, anchorElement.Target);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnSelf_When_RelRel_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor");

        Assert.AreEqual("_self", anchorElement.Target);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnNull_When_HrefNotSet_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor5");

        Assert.IsNull(anchorElement.Href);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnAutomateThePlanetUrl_When_Href_Safari()
    {
        var anchorElement = App.Components.CreateById<Anchor>("myAnchor");

        Assert.AreEqual("https://automatetheplanet.com/", anchorElement.Href);
    }
}