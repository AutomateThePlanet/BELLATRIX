// <copyright file="ElementControlTestsWpf.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Desktop.Tests
{
    [TestClass]
    [App(Constants.WpfAppPath, AppBehavior.RestartEveryTime)]
    [AllureSuite("Element Control")]
    [AllureTag("WPF")]
    public class ElementControlTestsWpf : BellatrixBaseTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Desktop)]
        public void IsVisibleReturnsTrue_When_ElementIsVisible_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            Assert.IsTrue(button.IsVisible);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Desktop)]
        public void IsPresentReturnsTrue_When_ElementIsPresent_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            Assert.IsTrue(button.IsPresent);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Desktop)]
        public void IsPresentReturnsFalse_When_ElementIsNotPresent_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E1 Button");

            Assert.IsFalse(button.IsPresent);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Desktop)]
        public void IsVisibleReturnsFalse_When_ElementIsNotVisible_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("V Button");

            Assert.IsFalse(button.IsVisible);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Desktop)]
        public void GetAttributeReturnsName_When_NameAttributeIsSet_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            var nameValue = button.GetAttribute("Name");

            Assert.AreEqual("E Button", nameValue);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Desktop)]
        public void ReturnNestedElement_When_ElementContainsOneChildElement_Wpf()
        {
            var comboBox = App.ElementCreateService.CreateByAutomationId<Button>("listBoxEnabled");
            var comboBoxItem = comboBox.CreateByAutomationId<Button>("lb2");

            Assert.AreEqual("ListBox Item #2", comboBoxItem.InnerText);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Desktop)]
        [App(Constants.WpfAppPath, AppBehavior.RestartEveryTime)]
        public void WaitForElementToExists_When_ElementIsNotVisibleInitially_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("ShowAfterButton");

            button.ToExists().WaitToBe();

            Assert.IsTrue(button.IsVisible);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Desktop)]
        [App(Constants.WpfAppPath, AppBehavior.RestartEveryTime)]
        public void WaitForElementToNotExists_When_ElementIsVisibleInitially_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("DisappearAfterButton1");

            button.ToNotExists().WaitToBe();

            Assert.IsFalse(button.IsPresent);
        }
    }
}
