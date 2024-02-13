// <copyright file="ExpanderControlTestsWpf.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using Bellatrix.Desktop.Locators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.Tests;

[TestClass]
[App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
[AllureSuite("Expander Control")]
[AllureTag("WPF")]
public class ExpanderControlTestsWpf : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_ExpanderHovered_Wpf()
    {
        var expander = App.Components.CreateByAutomationId<Expander>("HeaderSite");

        expander.Hover();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("expanderHovered", label.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void LabelVisible_When_ExpanderClicked_Wpf()
    {
        var expander = App.Components.CreateByAutomationId<Expander>("HeaderSite");

        expander.Click();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.IsTrue(label.IsVisible);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsFalse_When_ExpanderIsNotDisabled_Wpf()
    {
        var expander = App.Components.CreateByAutomationId<Expander>("HeaderSite");

        Assert.AreEqual(false, expander.IsDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void IsDisabledReturnsTrue_When_ExpanderIsDisabled_Wpf()
    {
        var expander = App.Components.CreateAll<Expander, FindAutomationIdStrategy>(Find.By.AutomationId("HeaderSite")).Last();

        Assert.AreEqual(true, expander.IsDisabled);
    }
}
