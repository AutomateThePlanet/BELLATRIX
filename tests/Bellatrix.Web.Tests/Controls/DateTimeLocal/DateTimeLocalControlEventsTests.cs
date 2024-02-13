// <copyright file="DateTimeLocalControlEventsTests.cs" company="Automate The Planet Ltd.">
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
using System;
using Bellatrix.Web.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("DateTimeLocal Control")]
[AllureFeature("ControlEvents")]
public class DateTimeLocalControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().DateTimeLocalLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingTimeCalled_BeforeActuallySetTime()
    {
        DateTimeLocal.SettingTime += AssertValueAttributeEmpty;

        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        timeElement.SetTime(new DateTime(1989, 10, 28, 23, 23, 0));

        Assert.AreEqual("1989-10-28T23:23", timeElement.GetTime());

        DateTimeLocal.SettingTime -= AssertValueAttributeEmpty;

        void AssertValueAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingTimeCalled_AfterSetTime()
    {
        DateTimeLocal.TimeSet += AssertValueAttributeContainsNewValue;

        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime");

        timeElement.SetTime(new DateTime(1989, 10, 28, 23, 23, 0));

        DateTimeLocal.TimeSet -= AssertValueAttributeContainsNewValue;

        void AssertValueAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("1989-10-28T23:23", args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        DateTimeLocal.Hovering += AssertStyleAttributeEmpty;

        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime7");

        timeElement.Hover();

        Assert.AreEqual("color: red;", timeElement.GetStyle());

        DateTimeLocal.Hovering -= AssertStyleAttributeEmpty;

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
        DateTimeLocal.Hovered += AssertStyleAttributeContainsNewValue;

        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime7");

        timeElement.Hover();

        DateTimeLocal.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<DateTimeLocal>("myTime7").ValidateStyleIs("color: red;");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusingCalled_BeforeActuallyFocus()
    {
        DateTimeLocal.Focusing += AssertStyleAttributeEmpty;

        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime8");

        timeElement.Focus();

        Assert.AreEqual("color: blue;", timeElement.GetStyle());

        DateTimeLocal.Focusing -= AssertStyleAttributeEmpty;

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
        DateTimeLocal.Focused += AssertStyleAttributeContainsNewValue;

        var timeElement = App.Components.CreateById<DateTimeLocal>("myTime8");

        timeElement.Focus();

        DateTimeLocal.Focused -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
        }
    }
}