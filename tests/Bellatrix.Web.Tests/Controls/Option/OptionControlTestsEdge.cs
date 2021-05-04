﻿// <copyright file="OptionControlTestsEdge.cs" company="Automate The Planet Ltd.">
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
    [Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
    [AllureSuite("Option Control")]
    [AllureFeature("Edge Browser")]
    public class OptionControlTestsEdge : MSTest.WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().OptionLocalPage);

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ReturnBellatrix_When_UseGetInnerTextMethod_Edge()
        {
            var selectComponent = App.ComponentCreateService.CreateById<Select>("mySelect");

            Assert.AreEqual("Bellatrix", selectComponent.GetSelected().InnerText);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ReturnBella_When_UseGetValueMethod_Edge()
        {
            var selectComponent = App.ComponentCreateService.CreateById<Select>("mySelect2");

            Assert.AreEqual("bella2", selectComponent.GetSelected().Value);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ReturnTrue_When_OptionSelectedAndCallGetIsSelectedMethod_Edge()
        {
            var selectComponent = App.ComponentCreateService.CreateById<Select>("mySelect");

            Assert.IsTrue(selectComponent.GetAllOptions()[0].IsSelected);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ReturnFalse_When_OptionNotSelectedAndCallGetIsSelectedMethod_Edge()
        {
            var selectComponent = App.ComponentCreateService.CreateById<Select>("mySelect");

            Assert.IsFalse(selectComponent.GetAllOptions()[1].IsSelected);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ReturnFalse_When_DisabledAttributeNotPresent_Edge()
        {
            var selectComponent = App.ComponentCreateService.CreateById<Select>("mySelect");

            Assert.IsFalse(selectComponent.GetSelected().IsDisabled);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ReturnTrue_When_DisabledAttributeIsPresent_Edge()
        {
            var selectComponent = App.ComponentCreateService.CreateById<Select>("mySelect4");

            Assert.IsFalse(selectComponent.GetAllOptions()[2].IsDisabled);
        }
    }
}