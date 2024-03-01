// <copyright file="SelectControlEventsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Select Control")]
[AllureFeature("ControlEvents")]
public class SelectControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().SelectLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SelectingCalled_BeforeActuallySelectByText()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");
        Select.Selecting += AssertIsCheckedFalse;

        selectComponent.SelectByText("Awesome");

        Assert.AreEqual("bella2", selectComponent.GetSelected().Value);

        Select.Selecting -= AssertIsCheckedFalse;

        void AssertIsCheckedFalse(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("bella", selectComponent.GetSelected().Value);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SelectingCalled_BeforeActuallySelectByIndex()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");
        Select.Selecting += AssertIsCheckedFalse;

        selectComponent.SelectByIndex(2);

        Assert.AreEqual("bella2", selectComponent.GetSelected().Value);

        Select.Selecting -= AssertIsCheckedFalse;

        void AssertIsCheckedFalse(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("bella", selectComponent.GetSelected().Value);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SelectedCalled_AfterSelectByText()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");
        Select.Selected += AssertIsCheckedFalse;

        selectComponent.SelectByText("Awesome");

        Select.Selected -= AssertIsCheckedFalse;

        void AssertIsCheckedFalse(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("bella2", selectComponent.GetSelected().Value);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SelectedCalled_AfterSelectByIndex()
    {
        var selectComponent = App.Components.CreateById<Select>("mySelect");
        Select.Selected += AssertIsCheckedFalse;

        selectComponent.SelectByIndex(2);

        Select.Selected -= AssertIsCheckedFalse;

        void AssertIsCheckedFalse(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("bella2", selectComponent.GetSelected().Value);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        Select.Hovering += AssertStyleAttributeEmpty;

        var selectComponent = App.Components.CreateById<Select>("mySelect1");

        selectComponent.Hover();

        Assert.AreEqual("color: red;", selectComponent.GetStyle());

        Select.Hovering -= AssertStyleAttributeEmpty;

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
        Select.Hovered += AssertStyleAttributeContainsNewValue;

        var selectComponent = App.Components.CreateById<Select>("mySelect1");

        selectComponent.Hover();

        Select.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<Select>("mySelect1").ValidateStyleIs("color: red;");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusingCalled_BeforeActuallyHover()
    {
        Select.Focusing += AssertStyleAttributeEmpty;

        var selectComponent = App.Components.CreateById<Select>("mySelect2");

        selectComponent.Focus();

        Assert.AreEqual("color: blue;", selectComponent.GetStyle());

        Select.Focusing -= AssertStyleAttributeEmpty;

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
        Select.Focused += AssertStyleAttributeContainsNewValue;

        var selectComponent = App.Components.CreateById<Select>("mySelect2");

        selectComponent.Focus();

        Select.Focused -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
        }
    }
}