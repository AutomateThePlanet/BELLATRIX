// <copyright file="CheckBoxControlEventsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("CheckBox Control")]
[AllureFeature("ControlEvents")]
public class CheckBoxControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().CheckBoxLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CheckingCalled_BeforeActuallyCheck()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox");
        CheckBox.Checking += AssertIsCheckedFalse;

        checkBoxElement.Check(false);

        Assert.IsFalse(checkBoxElement.IsChecked);

        CheckBox.Checking -= AssertIsCheckedFalse;

        void AssertIsCheckedFalse(object sender, ComponentActionEventArgs args)
        {
            Assert.IsTrue(checkBoxElement.IsChecked);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void CheckedCalled_AfterCheck()
    {
        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox");
        CheckBox.Checked += AssertIsCheckedFalse;

        checkBoxElement.Check(false);

        CheckBox.Checked -= AssertIsCheckedFalse;

        void AssertIsCheckedFalse(object sender, ComponentActionEventArgs args)
        {
            Assert.IsFalse(checkBoxElement.IsChecked);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        CheckBox.Hovering += AssertStyleAttributeEmpty;

        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox1");

        checkBoxElement.Hover();

        Assert.AreEqual("color: red;", checkBoxElement.GetStyle());

        CheckBox.Hovering -= AssertStyleAttributeEmpty;

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
        CheckBox.Hovered += AssertStyleAttributeContainsNewValue;

        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox1");

        checkBoxElement.Hover();

        CheckBox.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<CheckBox>("myCheckbox1").ValidateStyleIs("color: red;");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusingCalled_BeforeActuallyFocus()
    {
        CheckBox.Focusing += AssertStyleAttributeEmpty;

        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox2");

        checkBoxElement.Focus();

        Assert.AreEqual("color: blue;", checkBoxElement.GetStyle());

        CheckBox.Focusing -= AssertStyleAttributeEmpty;

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
        CheckBox.Focused += AssertStyleAttributeContainsNewValue;

        var checkBoxElement = App.Components.CreateById<CheckBox>("myCheckbox2");

        checkBoxElement.Focus();

        CheckBox.Focused -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
        }
    }
}