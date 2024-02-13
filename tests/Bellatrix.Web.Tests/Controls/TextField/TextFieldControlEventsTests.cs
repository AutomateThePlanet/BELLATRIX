// <copyright file="TextFieldControlEventsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("TextField Control")]
[AllureFeature("ControlEvents")]
public class TextFieldControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().TextFieldLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingTextCalled_BeforeActuallySetText()
    {
        TextField.SettingText += SettingTextCalled;

        var textFieldElement = App.Components.CreateById<TextField>("myText");
        bool setTextCalled = false;

        textFieldElement.SetText("bellatrix@solutions.com");

        Assert.IsTrue(setTextCalled);

        TextField.SettingText -= SettingTextCalled;

        void SettingTextCalled(object sender, ComponentActionEventArgs args)
        {
            setTextCalled = true;
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingTextCalled_AfterSetText()
    {
        TextField.TextSet += AssertValueAttributeContainsNewValue;

        var textFieldElement = App.Components.CreateById<TextField>("myText");

        textFieldElement.SetText("bellatrix@solutions.com");

        TextField.TextSet -= AssertValueAttributeContainsNewValue;

        void AssertValueAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("bellatrix@solutions.com", args.Element.WrappedElement.GetAttribute("value"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        TextField.Hovering += AssertStyleAttributeEmpty;

        var textFieldElement = App.Components.CreateById<TextField>("myText8");

        textFieldElement.Hover();

        Assert.AreEqual("color: red;", textFieldElement.GetStyle());

        TextField.Hovering -= AssertStyleAttributeEmpty;

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
        TextField.Hovered += AssertStyleAttributeContainsNewValue;

        var textFieldElement = App.Components.CreateById<TextField>("myText8");

        textFieldElement.Hover();

        TextField.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<TextField>("myText8").ValidateStyleIs("color: red;");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusingCalled_BeforeActuallyFocus()
    {
        TextField.Focusing += AssertStyleAttributeEmpty;

        var textFieldElement = App.Components.CreateById<TextField>("myText9");

        textFieldElement.Focus();

        Assert.AreEqual("color: blue;", textFieldElement.GetStyle());

        TextField.Focusing -= AssertStyleAttributeEmpty;

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
        TextField.Focused += AssertStyleAttributeContainsNewValue;

        var textFieldElement = App.Components.CreateById<TextField>("myText9");

        textFieldElement.Focus();

        TextField.Focused -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
        }
    }
}