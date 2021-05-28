// <copyright file="DivControlTestsInternetExlorer.cs" company="Automate The Planet Ltd.">
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
    [Browser(BrowserType.InternetExplorer, Lifecycle.ReuseIfStarted)]
    [AllureSuite("Div Control")]
    public class DivControlTestsInternetExplorer : MSTest.WebTest
    {
        public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().DivLocalPage);

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnRed_When_Hover_InternetExplorer()
        {
            var divElement = App.Components.CreateById<Div>("myDiv");

            divElement.Hover();

            Assert.AreEqual("color: red;", divElement.GetStyle());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnAutomateThePlanet_When_InnerText_InternetExplorer()
        {
            var divElement = App.Components.CreateById<Div>("myDiv1");

            Assert.AreEqual("Automate The Planet", divElement.InnerText);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnNull_When_InnerTextNotSet_InternetExplorer()
        {
            var divElement = App.Components.CreateById<Div>("myDiv3");

            Assert.IsNotNull(divElement.InnerText);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnNull_When_InnerHtmlNotSet_InternetExplorer()
        {
            var divElement = App.Components.CreateById<Div>("myDiv3");

            Assert.IsNotNull(divElement.InnerHtml);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnButtonHtml_When_InnerHtmlSet_InternetExplorer()
        {
            var divElement = App.Components.CreateById<Div>("myDiv2");

            Assert.AreEqual("<button name=\"button\">Click me</button>", divElement.InnerHtml);
        }
    }
}