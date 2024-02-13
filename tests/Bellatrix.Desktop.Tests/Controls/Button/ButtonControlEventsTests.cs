// <copyright file="ButtonControlEventsTests.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.Tests;

[TestClass]
[App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
[AllureSuite("Button Control")]
[AllureFeature("Control Events")]
[AllureTag("WPF")]
public class ButtonControlEventsTests : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        Button.Hovering += AssertTextResultLabel;

        var button = App.Components.CreateByName<Button>("E Button");

        button.Hover();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("ebuttonHovered", label.InnerText);

        Button.Hovering -= AssertTextResultLabel;

        label.Hover();

        void AssertTextResultLabel(object sender, ComponentActionEventArgs args)
        {
            var label1 = App.Components.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("Result Label", label1.InnerText);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void HoveredCalled_AfterHover()
    {
        Button.Hovered += AssertTextResultLabel;

        var button = App.Components.CreateByName<Button>("E Button");

        button.Hover();

        Button.Hovered -= AssertTextResultLabel;

        void AssertTextResultLabel(object sender, ComponentActionEventArgs args)
        {
            var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);

            label.Hover();
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void ClickingCalled_BeforeActuallyClick()
    {
        Button.Clicking += AssertTextResultLabel;

        var button = App.Components.CreateByName<Button>("E Button");

        button.Click();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("ebuttonClicked", label.InnerText);

        Button.Clicking -= AssertTextResultLabel;

        label.Hover();

        void AssertTextResultLabel(object sender, ComponentActionEventArgs args)
        {
            var label1 = App.Components.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("Result Label", label1.InnerText);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void ClickedCalled_AfterClick()
    {
        Button.Clicked += AssertTextResultLabel;

        var button = App.Components.CreateByName<Button>("E Button");

        button.Click();

        Button.Clicked -= AssertTextResultLabel;

        void AssertTextResultLabel(object sender, ComponentActionEventArgs args)
        {
            var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonClicked", label.InnerText);

            label.Hover();
        }
    }
}
