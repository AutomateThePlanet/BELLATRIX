// <copyright file="ElementControlEventsTests.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Web.Tests.Controls.Element
{
    [TestClass]
    [Browser(BrowserType.Edge, BrowserBehavior.ReuseIfStarted)]
    [AllureSuite("Element Control")]
    [AllureFeature("ControlEvents")]
    public class ElementControlEventsTests : WebTest
    {
        public override void TestInit() => App.NavigationService.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ElementLocalPage);

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void SettingAttributeCalled_BeforeActuallySetAttribute()
        {
            Bellatrix.Web.Element.SettingAttribute += AssertClassAttributeEmpty;

            var urlElement = App.ElementCreateService.CreateById<Bellatrix.Web.Element>("myURL");

            urlElement.SetAttribute("class", "myTestClass1");
            var cssClass = urlElement.GetAttribute("class");

            Assert.AreEqual("myTestClass1", cssClass);

            Bellatrix.Web.Element.SettingAttribute -= AssertClassAttributeEmpty;

            void AssertClassAttributeEmpty(object sender, ElementActionEventArgs args)
            {
                Assert.AreEqual("myTestClass", args.Element.WrappedElement.GetAttribute("class"));
            }
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void AttributeSetCalled_AfterSetAttribute()
        {
            Bellatrix.Web.Element.AttributeSet += AssertClassAttributeContainsNewValue;

            var urlElement = App.ElementCreateService.CreateById<Bellatrix.Web.Element>("myURL");

            urlElement.SetAttribute("class", "myTestClass1");
            var cssClass = urlElement.GetAttribute("class");

            Assert.AreEqual("myTestClass1", cssClass);

            Bellatrix.Web.Element.SettingAttribute -= AssertClassAttributeContainsNewValue;

            Bellatrix.Web.Element.AttributeSet -= AssertClassAttributeContainsNewValue;

            void AssertClassAttributeContainsNewValue(object sender, ElementActionEventArgs args)
            {
                Assert.AreEqual("myTestClass1", args.Element.WrappedElement.GetAttribute("class"));
            }
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ReturningWrappedElementCalled_AfterElementCreated()
        {
            Bellatrix.Web.Element.ReturningWrappedElement += AssertNativeElementNotNullAfterCallingAction;

            var urlElement = App.ElementCreateService.CreateById<Bellatrix.Web.Element>("myURL");

            var cssClass = urlElement.GetAttribute("class");

            Assert.AreEqual("myTestClass", cssClass);

            Bellatrix.Web.Element.ReturningWrappedElement -= AssertNativeElementNotNullAfterCallingAction;

            void AssertNativeElementNotNullAfterCallingAction(object sender, NativeElementActionEventArgs args)
            {
                Assert.IsNotNull(args.Element);
            }
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        public void ScrollingToVisibleCalled_BeforeActuallyScrollingToVisible()
        {
            Bellatrix.Web.Element.ScrollingToVisible += AssertStyleAttributeEmpty;

            var element = App.ElementCreateService.CreateById<Bellatrix.Web.Element>("myURL12");

            element.ScrollToVisible();

            Assert.AreEqual("color: red;", element.ToHasStyle("color: red;").GetStyle());

            Bellatrix.Web.Element.ScrollingToVisible -= AssertStyleAttributeEmpty;

            void AssertStyleAttributeEmpty(object sender, ElementActionEventArgs args)
            {
                Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
            }
        }

        ////[TestMethod]
        ////[TestCategory(Categories.CI)]
        ////[TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
        ////public void ScrollingToVisible_BeforeActuallyScrollingToVisible()
        ////{
        ////    Bellatrix.Web.Element.ScrolledToVisible += AssertStyleAttributeContainsNewValue;

        ////    var element = App.ElementCreateService.CreateById<Bellatrix.Web.Element>("myURL12");

        ////    Assert.IsNull(element.GetStyle());

        ////    element.ScrollToVisible();

        ////    Assert.AreEqual("color: red;", element.ToHasStyle("color: red;").GetStyle());

        ////    Bellatrix.Web.Element.ScrolledToVisible -= AssertStyleAttributeContainsNewValue;

        ////    void AssertStyleAttributeContainsNewValue(object sender, ElementActionEventArgs args)
        ////    {
        ////        Assert.AreEqual("color: red;", args.Element.ToHasStyle("color: red;").WrappedElement.GetAttribute("style"));
        ////    }
        ////}

        // TODO: add tests for CreatingElement, CreatedElement, CreatingElements, CreatedElements when have way to mock some of the internal services.
        // TODO: add tests for WaitForExists, WaitForNotExists when have way to mock _elementWaiter
    }
}