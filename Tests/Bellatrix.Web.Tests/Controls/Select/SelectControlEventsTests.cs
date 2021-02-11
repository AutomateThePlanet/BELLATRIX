// <copyright file="SelectControlEventsTests.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls
{
    [TestClass]
    [Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
    [AllureSuite("Select Control")]
    [AllureFeature("ControlEvents")]
    public class SelectControlEventsTests : MSTest.WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(SettingsService.GetSection<TestPagesSettings>().SelectLocalPage);

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void SelectingCalled_BeforeActuallySelectByText()
        {
            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect");
            Select.Selecting += AssertIsCheckedFalse;

            selectElement.SelectByText("Awesome");

            Assert.AreEqual("bella2", selectElement.GetSelected().Value);

            Select.Selecting -= AssertIsCheckedFalse;

            void AssertIsCheckedFalse(object sender, ElementActionEventArgs args)
            {
                Assert.AreEqual("bella", selectElement.GetSelected().Value);
            }
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void SelectingCalled_BeforeActuallySelectByIndex()
        {
            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect");
            Select.Selecting += AssertIsCheckedFalse;

            selectElement.SelectByIndex(2);

            Assert.AreEqual("bella2", selectElement.GetSelected().Value);

            Select.Selecting -= AssertIsCheckedFalse;

            void AssertIsCheckedFalse(object sender, ElementActionEventArgs args)
            {
                Assert.AreEqual("bella", selectElement.GetSelected().Value);
            }
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void SelectedCalled_AfterSelectByText()
        {
            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect");
            Select.Selected += AssertIsCheckedFalse;

            selectElement.SelectByText("Awesome");

            Select.Selected -= AssertIsCheckedFalse;

            void AssertIsCheckedFalse(object sender, ElementActionEventArgs args)
            {
                Assert.AreEqual("bella2", selectElement.GetSelected().Value);
            }
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void SelectedCalled_AfterSelectByIndex()
        {
            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect");
            Select.Selected += AssertIsCheckedFalse;

            selectElement.SelectByIndex(2);

            Select.Selected -= AssertIsCheckedFalse;

            void AssertIsCheckedFalse(object sender, ElementActionEventArgs args)
            {
                Assert.AreEqual("bella2", selectElement.GetSelected().Value);
            }
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void HoveringCalled_BeforeActuallyHover()
        {
            Select.Hovering += AssertStyleAttributeEmpty;

            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect1");

            selectElement.Hover();

            Assert.AreEqual("color: red;", selectElement.GetStyle());

            Select.Hovering -= AssertStyleAttributeEmpty;

            void AssertStyleAttributeEmpty(object sender, ElementActionEventArgs args)
            {
                Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
            }
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void HoveredCalled_AfterHover()
        {
            Select.Hovered += AssertStyleAttributeContainsNewValue;

            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect1");

            selectElement.Hover();

            Select.Hovered -= AssertStyleAttributeContainsNewValue;

            void AssertStyleAttributeContainsNewValue(object sender, ElementActionEventArgs args)
            {
                App.ElementCreateService.CreateById<Select>("mySelect1").ValidateStyleIs("color: red;");
            }
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void FocusingCalled_BeforeActuallyHover()
        {
            Select.Focusing += AssertStyleAttributeEmpty;

            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect2");

            selectElement.Focus();

            Assert.AreEqual("color: blue;", selectElement.GetStyle());

            Select.Focusing -= AssertStyleAttributeEmpty;

            void AssertStyleAttributeEmpty(object sender, ElementActionEventArgs args)
            {
                Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
            }
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void FocusedCalled_AfterHover()
        {
            Select.Focused += AssertStyleAttributeContainsNewValue;

            var selectElement = App.ElementCreateService.CreateById<Select>("mySelect2");

            selectElement.Focus();

            Select.Focused -= AssertStyleAttributeContainsNewValue;

            void AssertStyleAttributeContainsNewValue(object sender, ElementActionEventArgs args)
            {
                Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
            }
        }
    }
}