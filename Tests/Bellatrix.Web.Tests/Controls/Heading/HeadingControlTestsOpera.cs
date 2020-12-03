// <copyright file="HeadingControlTestsOpera.cs" company="Automate The Planet Ltd.">
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
    [Browser(BrowserType.Opera, BrowserBehavior.ReuseIfStarted)]
    [AllureSuite("Heading Control")]
    public class HeadingControlTestsOpera : WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().HeadingLocalPage);

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnRed_When_Hover_Opera()
        {
            var headingElement = App.ElementCreateService.CreateById<Heading>("myHeading");

            headingElement.Hover();

            Assert.AreEqual("color: red;", headingElement.GetStyle());
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnAutomateThePlanet_When_InnerText_Opera()
        {
            var headingElement = App.ElementCreateService.CreateById<Heading>("myHeading2");

            Assert.AreEqual("Automate The Planet", headingElement.InnerText);
        }

        [TestMethod]
        [TestCategory(Categories.Opera)]
        public void ReturnEmpty_When_InnerTextNotSet_Opera()
        {
            var headingElement = App.ElementCreateService.CreateById<Heading>("myHeading4");

            Assert.AreEqual(string.Empty, headingElement.InnerText);
        }
    }
}