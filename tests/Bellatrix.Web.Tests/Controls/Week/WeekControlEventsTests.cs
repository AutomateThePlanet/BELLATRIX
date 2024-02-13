// <copyright file="WeekControlEventsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Week Control")]
[AllureFeature("ControlEvents")]
public class WeekControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().WeekLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingWeekCalled_BeforeActuallySetWeek()
    {
        Week.SettingWeek += AssertValueAttributeEmpty;

        var weekElement = App.Components.CreateById<Week>("myWeek");

        weekElement.SetWeek(2017, 7);

        Assert.AreEqual("2017-W07", weekElement.GetWeek());

        Week.SettingWeek -= AssertValueAttributeEmpty;

        void AssertValueAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingWeekCalled_AfterSetWeek()
    {
        Week.WeekSet += AssertValueAttributeContainsNewValue;

        var weekElement = App.Components.CreateById<Week>("myWeek");

        weekElement.SetWeek(2017, 7);

        Week.WeekSet -= AssertValueAttributeContainsNewValue;

        void AssertValueAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("2017-W07", args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        Week.Hovering += AssertStyleAttributeEmpty;

        var weekElement = App.Components.CreateById<Week>("myWeek7");

        weekElement.Hover();

        Assert.AreEqual("color: red;", weekElement.GetStyle());

        Week.Hovering -= AssertStyleAttributeEmpty;

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
        Week.Hovered += AssertStyleAttributeContainsNewValue;

        var weekElement = App.Components.CreateById<Week>("myWeek7");

        weekElement.Hover();

        Week.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<Week>("myWeek7").ValidateStyleIs("color: red;");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusingCalled_BeforeActuallyFocus()
    {
        Week.Focusing += AssertStyleAttributeEmpty;

        var weekElement = App.Components.CreateById<Week>("myWeek8");

        weekElement.Focus();

        Assert.AreEqual("color: blue;", weekElement.GetStyle());

        Week.Focusing -= AssertStyleAttributeEmpty;

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
        Week.Focused += AssertStyleAttributeContainsNewValue;

        var weekElement = App.Components.CreateById<Week>("myWeek8");

        weekElement.Focus();

        Week.Focused -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
        }
    }
}