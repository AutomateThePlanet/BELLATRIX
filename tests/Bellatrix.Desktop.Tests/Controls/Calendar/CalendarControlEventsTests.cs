// <copyright file="CalendarControlEventsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Calendar Control")]
[AllureFeature("Control Events")]
[AllureTag("WPF")]
public class CalendarControlEventsTests : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        Calendar.Hovering += AssertTextResultLabel;

        var calendar = App.Components.CreateByAutomationId<Calendar>("calendar");

        calendar.Hover();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.IsTrue(label.IsVisible);

        Calendar.Hovering -= AssertTextResultLabel;

        void AssertTextResultLabel(object sender, ComponentActionEventArgs args)
        {
            var label1 = App.Components.CreateByAutomationId<Label>("ResultLabelId");
            Assert.IsTrue(label1.IsVisible);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Desktop)]
    public void HoveredCalled_AfterHover()
    {
        Calendar.Hovered += AssertTextResultLabel;

        var calendar = App.Components.CreateByAutomationId<Calendar>("calendar");

        calendar.Hover();

        Calendar.Hovered -= AssertTextResultLabel;

        void AssertTextResultLabel(object sender, ComponentActionEventArgs args)
        {
            var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
            Assert.IsTrue(label.IsVisible);
        }
    }
}
