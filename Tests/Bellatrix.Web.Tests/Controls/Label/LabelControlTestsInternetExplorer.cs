// <copyright file="LabelControlTestsInternetExplorer.cs" company="Automate The Planet Ltd.">
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
    [AllureSuite("Label Control")]
    public class LabelControlTestsInternetExplorer : MSTest.WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(SettingsService.GetSection<TestPagesSettings>().LabelLocalPage);

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnRed_When_Hover_InternetExplorer()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel");

            labelElement.Hover();

            Assert.AreEqual("color: red;", labelElement.GetStyle());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnAutomateThePlanet_When_InnerText_InternetExplorer()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel");

            Assert.AreEqual("Hover", labelElement.InnerText);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnNull_When_InnerTextNotSet_InternetExplorer()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel2");

            Assert.IsNotNull(labelElement.InnerText);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnHover_When_InnerTextSet_InternetExplorer()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel");

            Assert.AreEqual("Hover", labelElement.InnerText);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnNull_When_InnerHtmlNotSet_InternetExplorer()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel2");

            Assert.IsNotNull(labelElement.InnerHtml);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnButtonHtml_When_InnerHtmlSet_InternetExplorer()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel1");

            Assert.IsTrue(labelElement.InnerHtml.Contains("<button name=\"button\">Click me</button>"));
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnNull_When_ForNotSet_InternetExplorer()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel2");

            Assert.IsNull(labelElement.For);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnLabelFor_When_ForSet_InternetExplorer()
        {
            var labelElement = App.ElementCreateService.CreateById<Label>("myLabel");

            Assert.AreEqual("myLabel", labelElement.For);
        }
    }
}