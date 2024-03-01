// <copyright file="NumberControlEventsTests.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Number Control")]
[AllureFeature("ControlEvents")]
public class NumberControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().NumberLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingNumberCalled_BeforeActuallySetNumber()
    {
        Number.SettingNumber += AssertValueAttributeEmpty;

        var numberElement = App.Components.CreateById<Number>("myNumber");

        numberElement.SetNumber(13);

        Assert.AreEqual(13, numberElement.GetNumber());

        Number.SettingNumber -= AssertValueAttributeEmpty;

        void AssertValueAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingNumberCalled_AfterSetNumber()
    {
        Number.NumberSet += AssertValueAttributeContainsNewValue;

        var numberElement = App.Components.CreateById<Number>("myNumber");

        numberElement.SetNumber(13);

        Number.NumberSet -= AssertValueAttributeContainsNewValue;

        void AssertValueAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("13", args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        Number.Hovering += AssertStyleAttributeEmpty;

        var numberElement = App.Components.CreateById<Number>("myNumber7");

        numberElement.Hover();

        Assert.AreEqual("color: red;", numberElement.GetStyle());

        Number.Hovering -= AssertStyleAttributeEmpty;

        void AssertStyleAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveredCalled_AfterHover()
    {
        Number.Hovered += AssertStyleAttributeContainsNewValue;

        var numberElement = App.Components.CreateById<Number>("myNumber7");

        numberElement.Hover();

        Number.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<Number>("myNumber7").ValidateStyleIs("color: red;");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusingCalled_BeforeActuallyFocus()
    {
        Number.Focusing += AssertStyleAttributeEmpty;

        var numberElement = App.Components.CreateById<Number>("myNumber8");

        numberElement.Focus();

        Assert.AreEqual("color: blue;", numberElement.GetStyle());

        Number.Focusing -= AssertStyleAttributeEmpty;

        void AssertStyleAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusedCalled_AfterFocus()
    {
        Number.Focused += AssertStyleAttributeContainsNewValue;

        var numberElement = App.Components.CreateById<Number>("myNumber8");

        numberElement.Focus();

        Number.Focused -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
        }
    }
}