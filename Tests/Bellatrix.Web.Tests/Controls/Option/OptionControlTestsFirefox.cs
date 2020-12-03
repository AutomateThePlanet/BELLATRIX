// <copyright file="OptionControlTestsFirefox.cs" company="Automate The Planet Ltd.">
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
    [Browser(BrowserType.Firefox, BrowserBehavior.ReuseIfStarted)]
    [AllureSuite("Option Control")]
    public class OptionControlTestsFirefox : WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().OptionLocalPage);

        [TestMethod]
        [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
        public void ReturnBellatrix_When_UseGetInnerTextMethod_Firefox()
        {
            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect");

            Assert.AreEqual("Bellatrix", selectElement.GetSelected().InnerText);
        }

        [TestMethod]
        [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
        public void ReturnBella_When_UseGetValueMethod_Firefox()
        {
            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect2");

            Assert.AreEqual("bella2", selectElement.GetSelected().Value);
        }

        [TestMethod]
        [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
        public void ReturnTrue_When_OptionSelectedAndCallGetIsSelectedMethod_Firefox()
        {
            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect");

            Assert.IsTrue(selectElement.GetAllOptions()[0].IsSelected);
        }

        [TestMethod]
        [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
        public void ReturnFalse_When_OptionNotSelectedAndCallGetIsSelectedMethod_Firefox()
        {
            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect");

            Assert.IsFalse(selectElement.GetAllOptions()[1].IsSelected);
        }

        [TestMethod]
        [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
        public void ReturnFalse_When_DisabledAttributeNotPresent_Firefox()
        {
            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect");

            Assert.IsFalse(selectElement.GetSelected().IsDisabled);
        }

        [TestMethod]
        [TestCategory(Categories.Firefox), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
        public void ReturnTrue_When_DisabledAttributeIsPresent_Firefox()
        {
            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect4");

            Assert.IsFalse(selectElement.GetAllOptions()[2].IsDisabled);
        }
    }
}