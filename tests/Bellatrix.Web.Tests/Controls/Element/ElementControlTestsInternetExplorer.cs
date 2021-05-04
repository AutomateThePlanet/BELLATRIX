// <copyright file="ElementControlTestsInternetExplorer.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Web.Tests.Controls.Element
{
    [TestClass]
    [Browser(BrowserType.InternetExplorer, Lifecycle.ReuseIfStarted)]
    [AllureSuite("Element Control")]
    public class ElementControlTestsInternetExplorer : MSTest.WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ElementLocalPage);

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void IsVisibleReturnsTrue_When_ElementIsPresent_InternetExplorer()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL");

            Assert.IsTrue(urlElement.IsVisible);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void IsVisibleReturnsFalse_When_ElementIsHidden_InternetExplorer()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL11");

            Assert.IsFalse(urlElement.IsVisible);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void SetAttributeChangesAttributeValue_InternetExplorer()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL");

            urlElement.SetAttribute("class", "myTestClass1");
            var cssClass = urlElement.GetAttribute("class");

            Assert.AreEqual("myTestClass1", cssClass);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetAttributeReturnsName_When_NameAttributeIsSet_InternetExplorer()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL");

            var nameValue = urlElement.GetAttribute("name");

            Assert.AreEqual("myURL", nameValue);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetAttributeReturnsEmpty_When_NameAttributeIsNotPresent_InternetExplorer()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL");

            var nameValue = urlElement.GetAttribute("style");

            Assert.AreEqual(string.Empty, nameValue);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void CssClassReturnsMyTestClass_When_ClassAttributeIsSet_InternetExplorer()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL");

            var cssClass = urlElement.CssClass;

            Assert.AreEqual("myTestClass", cssClass);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void CssClassReturnsNull_When_ClassAttributeIsNotPresent_InternetExplorer()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL1");

            var cssClass = urlElement.CssClass;

            Assert.IsNull(cssClass);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ElementVisible_AfterCallingScrollToVisible_InternetExplorer()
        {
            var urlElement = App.ComponentCreateService.CreateById<Url>("myURL12");

            urlElement.ScrollToVisible();

            Assert.AreEqual("color: red;", urlElement.GetStyle());
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void CreateElement_When_InsideAnotherElementAndIsPresent_InternetExplorer()
        {
            var wrapperDiv = App.ComponentCreateService.CreateById<Div>("myURL10Wrapper");

            var urlElement = wrapperDiv.CreateById<Url>("myURL10");

            Assert.IsTrue(urlElement.IsDisabled);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetTitle_When_TitleAttributeIsPresent_InternetExplorer()
        {
            var element = App.ComponentCreateService.CreateById<Bellatrix.Web.Component>("myURL13");

            string title = element.GetTitle();

            Assert.AreEqual("bellatrix.solutions", title);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnsNull_When_TitleAttributeIsNotPresent_InternetExplorer()
        {
            var element = App.ComponentCreateService.CreateById<Bellatrix.Web.Component>("myURL12");

            string title = element.GetTitle();

            Assert.IsNull(title);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetTabIndexOne_When_TabIndexAttributeIsPresent_InternetExplorer()
        {
            var element = App.ComponentCreateService.CreateById<Bellatrix.Web.Component>("myURL14");

            string tabIndex = element.GetTabIndex();

            Assert.AreEqual("1", tabIndex);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnsNull_When_TabIndexAttributeIsNotPresent_InternetExplorer()
        {
            var element = App.ComponentCreateService.CreateById<Bellatrix.Web.Component>("myURL12");

            string tabIndex = element.GetTabIndex();

            Assert.IsNull(tabIndex);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetStyle_When_StyleAttributeIsPresent_InternetExplorer()
        {
            var element = App.ComponentCreateService.CreateById<Bellatrix.Web.Component>("myURL16");

            var style = element.GetStyle();

            Assert.AreEqual("color: green;", style);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnsNull_When_StyleAttributeIsNotPresent_InternetExplorer()
        {
            var element = App.ComponentCreateService.CreateById<Bellatrix.Web.Component>("myURL");

            string style = element.GetStyle();

            Assert.AreEqual(null, style);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetDir_When_DirAttributeIsPresent_InternetExplorer()
        {
            var element = App.ComponentCreateService.CreateById<Bellatrix.Web.Component>("myURL19");

            var dir = element.GetDir();

            Assert.AreEqual("rtl", dir);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnsNull_When_DirAttributeIsNotPresent_InternetExplorer()
        {
            var element = App.ComponentCreateService.CreateById<Bellatrix.Web.Component>("myURL12");

            string dir = element.GetDir();

            Assert.IsNull(dir);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void GetLang_When_LangAttributeIsPresent_InternetExplorer()
        {
            var element = App.ComponentCreateService.CreateById<Bellatrix.Web.Component>("myURL20");

            var lang = element.GetLang();

            Assert.AreEqual("en", lang);
        }

        [TestMethod]
        [Ignore, TestCategory(Categories.InternetExplorer), TestCategory(Categories.Windows)]
        public void ReturnsNull_When_LangAttributeIsNotPresent_InternetExplorer()
        {
            var element = App.ComponentCreateService.CreateById<Bellatrix.Web.Component>("myURL12");

            string lang = element.GetLang();

            Assert.IsNull(lang);
        }
    }
}