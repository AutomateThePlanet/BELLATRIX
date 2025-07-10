﻿// <copyright file="RadioButtonControlEventsTests.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using Bellatrix.Web.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Radio Control")]
[AllureFeature("ControlEvents")]
public class RadioButtonControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().RadioPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ClickingCalled_BeforeActuallyClick()
    {
        var radioButtonElement = App.Components.CreateById<RadioButton>("myRadio");
        RadioButton.Clicking += AssertIsCheckedFalse;

        radioButtonElement.Click();

        Assert.IsTrue(radioButtonElement.IsChecked);

        RadioButton.Clicking -= AssertIsCheckedFalse;

        void AssertIsCheckedFalse(object sender, ComponentActionEventArgs args)
        {
            Assert.IsFalse(radioButtonElement.IsChecked);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ClickedCalled_AfterClick()
    {
        var radioButtonElement = App.Components.CreateById<RadioButton>("myRadio");
        RadioButton.Clicked += AssertIsCheckedFalse;

        radioButtonElement.Click();

        RadioButton.Clicked -= AssertIsCheckedFalse;

        void AssertIsCheckedFalse(object sender, ComponentActionEventArgs args)
        {
            Assert.IsTrue(radioButtonElement.IsChecked);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        RadioButton.Hovering += AssertStyleAttributeEmpty;

        var radioButtonElement = App.Components.CreateById<RadioButton>("myRadio1");

        radioButtonElement.Hover();

        Assert.AreEqual("color: red;", radioButtonElement.GetStyle());

        RadioButton.Hovering -= AssertStyleAttributeEmpty;

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
        RadioButton.Hovered += AssertStyleAttributeContainsNewValue;

        var radioButtonElement = App.Components.CreateById<RadioButton>("myRadio1");

        radioButtonElement.Hover();

        RadioButton.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<RadioButton>("myRadio1").ValidateStyleIs("color: red;");
        }
    }
}