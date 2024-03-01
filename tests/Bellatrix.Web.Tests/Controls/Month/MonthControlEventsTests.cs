// <copyright file="MonthControlEventsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Month Control")]
[AllureFeature("ControlEvents")]
public class MonthControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().MonthLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingMonthCalled_BeforeActuallySetMonth()
    {
        Month.SettingMonth += AssertValueAttributeEmpty;

        var monthElement = App.Components.CreateById<Month>("myMonth");

        monthElement.SetMonth(2017, 7);

        Assert.AreEqual("2017-07", monthElement.GetMonth());

        Month.SettingMonth -= AssertValueAttributeEmpty;

        void AssertValueAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingMonthCalled_AfterSetMonth()
    {
        Month.MonthSet += AssertValueAttributeContainsNewValue;

        var monthElement = App.Components.CreateById<Month>("myMonth");

        monthElement.SetMonth(2017, 7);

        Month.MonthSet -= AssertValueAttributeContainsNewValue;

        void AssertValueAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("2017-07", args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        Month.Hovering += AssertStyleAttributeEmpty;

        var monthElement = App.Components.CreateById<Month>("myMonth7");

        monthElement.Hover();

        Assert.AreEqual("color: red;", monthElement.GetStyle());

        Month.Hovering -= AssertStyleAttributeEmpty;

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
        Month.Hovered += AssertStyleAttributeContainsNewValue;

        var monthElement = App.Components.CreateById<Month>("myMonth7");

        monthElement.Hover();

        Month.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<Month>("myMonth7").ValidateStyleIs("color: red;");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusingCalled_BeforeActuallyFocus()
    {
        Month.Focusing += AssertStyleAttributeEmpty;

        var monthElement = App.Components.CreateById<Month>("myMonth8");

        monthElement.Focus();

        Assert.AreEqual("color: blue;", monthElement.GetStyle());

        Month.Focusing -= AssertStyleAttributeEmpty;

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
        Month.Focused += AssertStyleAttributeContainsNewValue;

        var monthElement = App.Components.CreateById<Month>("myMonth8");

        monthElement.Focus();

        Month.Focused -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
        }
    }
}