// <copyright file="ElementControlEventsTests.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>
using Bellatrix.Playwright.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Playwright.Tests.Controls.Element;

[TestClass]
[Browser(BrowserTypes.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Element Control")]
[AllureFeature("ControlEvents")]
public class ElementControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ElementPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingAttributeCalled_BeforeActuallySetAttribute()
    {
        Bellatrix.Playwright.Component.SettingAttribute += AssertClassAttributeEmpty;

        var urlElement = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL");

        urlElement.SetAttribute("class", "myTestClass1");
        var cssClass = urlElement.GetAttribute("class");

        Assert.AreEqual("myTestClass1", cssClass);

        Bellatrix.Playwright.Component.SettingAttribute -= AssertClassAttributeEmpty;

        void AssertClassAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("myTestClass", args.Element.WrappedElement.GetAttribute("class"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void AttributeSetCalled_AfterSetAttribute()
    {
        Bellatrix.Playwright.Component.AttributeSet += AssertClassAttributeContainsNewValue;

        var urlElement = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL");

        urlElement.SetAttribute("class", "myTestClass1");
        var cssClass = urlElement.GetAttribute("class");

        Assert.AreEqual("myTestClass1", cssClass);

        Bellatrix.Playwright.Component.SettingAttribute -= AssertClassAttributeContainsNewValue;

        Bellatrix.Playwright.Component.AttributeSet -= AssertClassAttributeContainsNewValue;

        void AssertClassAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("myTestClass1", args.Element.WrappedElement.GetAttribute("class"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturningWrappedElementCalled_AfterElementCreated()
    {
        Bellatrix.Playwright.Component.ReturningWrappedElement += AssertNativeElementNotNullAfterCallingAction;

        var urlElement = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL");

        var cssClass = urlElement.GetAttribute("class");

        Assert.AreEqual("myTestClass", cssClass);

        Bellatrix.Playwright.Component.ReturningWrappedElement -= AssertNativeElementNotNullAfterCallingAction;

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
        Bellatrix.Playwright.Component.ScrollingToVisible += AssertStyleAttributeEmpty;

        var element = App.Components.CreateById<Bellatrix.Playwright.Component>("myURL12");

        element.ScrollToVisible();

        Assert.AreEqual("color: red;", element.ToHasStyle("color: red;").GetStyle());

        Bellatrix.Playwright.Component.ScrollingToVisible -= AssertStyleAttributeEmpty;

        void AssertStyleAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
        }
    }

    ////[TestMethod]
    ////[TestCategory(Categories.CI)]
    ////[TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    ////public void ScrollingToVisible_BeforeActuallyScrollingToVisible()
    ////{
    ////    Bellatrix.Playwright.Element.ScrolledToVisible += AssertStyleAttributeContainsNewValue;

    ////    var element = App.Components.CreateById<Bellatrix.Playwright.Element>("myURL12");

    ////    Assert.IsNull(element.GetStyle());

    ////    element.ScrollToVisible();

    ////    Assert.AreEqual("color: red;", element.ToHasStyle("color: red;").GetStyle());

    ////    Bellatrix.Playwright.Element.ScrolledToVisible -= AssertStyleAttributeContainsNewValue;

    ////    void AssertStyleAttributeContainsNewValue(object sender, ElementActionEventArgs args)
    ////    {
    ////        Assert.AreEqual("color: red;", args.Element.ToHasStyle("color: red;").WrappedElement.GetAttribute("style"));
    ////    }
    ////}

    // TODO: add tests for CreatingElement, CreatedElement, CreatingElements, CreatedElements when have way to mock some of the internal services.
    // TODO: add tests for WaitForExists, WaitForNotExists when have way to mock _elementWaiter
}