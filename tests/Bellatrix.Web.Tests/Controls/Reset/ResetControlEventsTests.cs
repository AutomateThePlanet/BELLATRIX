// <copyright file="ResetControlEventsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Reset Control")]
[AllureFeature("ControlEvents")]
public class ResetControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ResetLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ClickingCalled_BeforeActuallyClick()
    {
        var buttonElement = App.Components.CreateById<Reset>("myButton");
        Reset.Clicking += AssertIsCheckedFalse;

        buttonElement.Click();

        Assert.AreEqual("Stop", buttonElement.Value);

        Reset.Clicking -= AssertIsCheckedFalse;

        void AssertIsCheckedFalse(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("Start", buttonElement.Value);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ClickedCalled_AfterClick()
    {
        var buttonElement = App.Components.CreateById<Reset>("myButton");
        Reset.Clicked += AssertIsCheckedFalse;

        buttonElement.Click();

        Reset.Clicked -= AssertIsCheckedFalse;

        void AssertIsCheckedFalse(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("Stop", buttonElement.Value);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveringCalled_BeforeActuallyHover()
    {
       Reset.Hovering += AssertStyleAttributeEmpty;

       var buttonElement = App.Components.CreateById<Reset>("myButton1");

       buttonElement.Hover();

       Assert.AreEqual("color: red;", buttonElement.GetStyle());

       Reset.Hovering -= AssertStyleAttributeEmpty;

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
       Reset.Hovered += AssertStyleAttributeContainsNewValue;

       var buttonElement = App.Components.CreateById<Reset>("myButton1");

       buttonElement.Hover();

       Reset.Hovered -= AssertStyleAttributeContainsNewValue;

       void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<Reset>("myButton1").ValidateStyleIs("color: red;");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusingCalled_BeforeActuallyHover()
    {
       Reset.Focusing += AssertStyleAttributeEmpty;

       var buttonElement = App.Components.CreateById<Reset>("myButton2");

       buttonElement.Focus();

       Assert.AreEqual("color: blue;", buttonElement.GetStyle());

       Reset.Focusing -= AssertStyleAttributeEmpty;

       void AssertStyleAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusedCalled_AfterHover()
    {
       Reset.Focused += AssertStyleAttributeContainsNewValue;

       var buttonElement = App.Components.CreateById<Reset>("myButton2");

       buttonElement.Focus();

       Reset.Focused -= AssertStyleAttributeContainsNewValue;

       void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
        }
    }
}