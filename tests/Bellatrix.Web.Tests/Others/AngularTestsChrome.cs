// <copyright file="AngularTestsChrome.cs" company="Automate The Planet Ltd.">
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

using Bellatrix.Web.Angular;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
    [Browser(OS.OSX, BrowserType.Safari, Lifecycle.ReuseIfStarted)]
    [ScreenshotOnFail(true)]
    public class AngularTestsChrome : MSTest.WebTest
    {
        [TestMethod]
        [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
        public void ShouldGreetUsingBinding()
        {
            App.NavigationService.Navigate("http://www.angularjs.org");
            var textField = App.ElementCreateService.CreateByNgModel<TextField>("yourName");

            textField.SetText("Julie");

            var heading = App.ElementCreateService.CreateByNgBinding<Heading>("yourName");

            heading.ValidateInnerTextIs("Hello Julie!");
        }

        [TestMethod]
        [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
        public void ShouldListTodos()
        {
            App.NavigationService.Navigate("http://www.angularjs.org");
            var labels = App.ElementCreateService.CreateAllByNgRepeater<Label>("todo in todoList.todos");

            Assert.AreEqual("build an AngularJS app", labels[1].InnerText.Trim());
        }

        [TestMethod]
        [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
        public void Angular2Test()
        {
            App.NavigationService.Navigate("https://material.angular.io/");
            var button = App.ElementCreateService.CreateByXpath<Button>("//a[@routerlink='/guide/getting-started']");
            button.Click();

            Assert.AreEqual("https://material.angular.io/guide/getting-started", App.BrowserService.Url.ToString());
        }
    }
}