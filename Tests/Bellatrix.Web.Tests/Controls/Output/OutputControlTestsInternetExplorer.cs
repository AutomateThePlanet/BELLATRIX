// <copyright file="OutputControlTestsInternetExplorer.cs" company="Automate The Planet Ltd.">
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
    [Browser(BrowserType.InternetExplorer, BrowserBehavior.ReuseIfStarted)]
    [AllureSuite("Output Control")]
    public class OutputControlTestsInternetExplorer : WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().OutputLocalPage);

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnRed_When_Hover_InternetExplorer()
        {
            var outputElement = App.ElementCreateService.CreateById<Output>("myOutput");

            outputElement.Hover();

            Assert.AreEqual("color: red;", outputElement.GetStyle());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void Return10_When_InnerText_InternetExplorer()
        {
            var outputElement = App.ElementCreateService.CreateById<Output>("myOutput");

            Assert.AreEqual("10", outputElement.InnerText);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnNull_When_InnerTextNotSet_InternetExplorer()
        {
            var outputElement = App.ElementCreateService.CreateById<Output>("myOutput2");

            Assert.IsNotNull(outputElement.InnerText);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnNull_When_InnerHtmlNotSet_InternetExplorer()
        {
            var outputElement = App.ElementCreateService.CreateById<Output>("myOutput2");

            Assert.IsNotNull(outputElement.InnerHtml);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnButtonHtml_When_InnerHtmlSet_InternetExplorer()
        {
            var outputElement = App.ElementCreateService.CreateById<Output>("myOutput1");

            Assert.AreEqual("<button name=\"button\">Click me</button>", outputElement.InnerHtml);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnNull_When_ForNotSet_InternetExplorer()
        {
            var outputElement = App.ElementCreateService.CreateById<Output>("myOutput2");

            Assert.IsNull(outputElement.For);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnOutputFor_When_ForSet_InternetExplorer()
        {
            var outputElement = App.ElementCreateService.CreateById<Output>("myOutput");

            Assert.AreEqual("myOutput", outputElement.For);
        }
    }
}