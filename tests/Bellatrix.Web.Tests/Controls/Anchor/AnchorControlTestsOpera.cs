// <copyright file="AnchorControlTestsOpera.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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

namespace Bellatrix.Web.Tests.Controls
{
    [TestClass]
    [Browser(BrowserType.Opera, Lifecycle.ReuseIfStarted)]
    [AllureSuite("Anchor Control")]
    public class AnchorControlTestsOpera : MSTest.WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().AnchorLocalPage);

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ClickOpensAutomateThePlanetUrl_When_DefaultClickIsSet_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor");

            anchorElement.Click();

            App.NavigationService.WaitForPartialUrl("automatetheplanet");

            Assert.IsTrue(App.BrowserService.Url.ToString().Contains("automatetheplanet.com"));
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnRed_When_Hover_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor1");

            anchorElement.Hover();

            Assert.AreEqual("color: red;", anchorElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnBlue_When_Focus_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor2");

            anchorElement.Focus();
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnAutomateThePlanet_When_InnerText_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor");

            Assert.AreEqual("Automate The Planet", anchorElement.InnerText);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnButtonHtml_When_InnerHtml_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor4");

            Assert.IsTrue(anchorElement.InnerHtml.Contains("<button name=\"button\">Click me</button>"));
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnEmpty_When_InnerTextNotSet_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor6");

            Assert.AreEqual(string.Empty, anchorElement.InnerText);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnEmpty_When_InnerHtmlNotSet_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor6");

            string actualInnerHtml = anchorElement.InnerHtml;

            Assert.AreEqual(string.Empty, actualInnerHtml);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnEmpty_When_RelNotSet_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor");

            string actualRel = anchorElement.Rel;

            Assert.AreEqual(string.Empty, actualRel);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnCanonical_When_RelRel_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor5");

            Assert.AreEqual("canonical", anchorElement.Rel);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnEmpty_When_TargetNotSet_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor1");

            Assert.AreEqual(string.Empty, anchorElement.Target);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnSelf_When_RelRel_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor");

            Assert.AreEqual("_self", anchorElement.Target);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnNull_When_HrefNotSet_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor5");

            Assert.IsNull(anchorElement.Href);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnAutomateThePlanetUrl_When_Href_Opera()
        {
            var anchorElement = App.ElementCreateService.CreateById<Anchor>("myAnchor");

            Assert.AreEqual("https://automatetheplanet.com/", anchorElement.Href);
        }
    }
}