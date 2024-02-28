// <copyright file="TextAreaControlEventsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("TextArea Control")]
[AllureFeature("ControlEvents")]
public class TextAreaControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().TextAreaLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SettingTextCalled_BeforeActuallySetText()
    {
        TextArea.SettingText += SettingTextCalled;

        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");
        bool setTextCalled = false;

        textAreaElement.SetText("bellatrix@solutions.com");

        Assert.IsTrue(setTextCalled);

        TextArea.SettingText -= SettingTextCalled;

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
        TextArea.TextSet += AssertValueAttributeContainsNewValue;

        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea");

        textAreaElement.SetText("bellatrix@solutions.com");

        TextArea.TextSet -= AssertValueAttributeContainsNewValue;

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
        TextArea.Hovering += AssertStyleAttributeEmpty;

        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea8");

        textAreaElement.Hover();

        Assert.AreEqual("color: red;", textAreaElement.GetStyle());

        TextArea.Hovering -= AssertStyleAttributeEmpty;

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
        TextArea.Hovered += AssertStyleAttributeContainsNewValue;

        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea8");

        textAreaElement.Hover();

        TextArea.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<TextArea>("myTextArea8").ValidateStyleIs("color: red;");
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FocusingCalled_BeforeActuallyFocus()
    {
        TextArea.Focusing += AssertStyleAttributeEmpty;

        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea9");

        textAreaElement.Focus();

        Assert.AreEqual("color: blue;", textAreaElement.GetStyle());

        TextArea.Focusing -= AssertStyleAttributeEmpty;

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
        TextArea.Focused += AssertStyleAttributeContainsNewValue;

        var textAreaElement = App.Components.CreateById<TextArea>("myTextArea9");

        textAreaElement.Focus();

        TextArea.Focused -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual("color: blue;", args.Element.WrappedElement.GetAttribute("style"));
        }
    }
}