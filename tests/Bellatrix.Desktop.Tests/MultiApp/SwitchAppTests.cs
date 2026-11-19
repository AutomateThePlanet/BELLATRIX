// <copyright file="SwitchAppTests.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.Tests;

// Needs Appium and the WPF and WinForms demo apps. App names come from desktopSettings.applicationsSettings.
[TestClass]
[App("WpfSample", Lifecycle.RestartEveryTime)]
[TestCategory(Categories.Desktop)]
public class SwitchAppTests : MSTest.DesktopTest
{
    [TestMethod]
    public void SwitchApp_StartsSecondApp_And_SwitchingBackReusesPrimarySession()
    {
        var primarySessionId = App.AppService.WrappedDriver.SessionId;

        App.SwitchApp("WinFormsSample");
        App.Components.CreateByName<Button>("E Button").Click();
        Assert.IsTrue(App.Components.CreateByAutomationId<Label>("resultLabel").IsVisible);
        Assert.AreNotEqual(primarySessionId, App.AppService.WrappedDriver.SessionId);

        App.SwitchApp("WpfSample");
        Assert.AreEqual(primarySessionId, App.AppService.WrappedDriver.SessionId);
        App.Components.CreateByName<Button>("E Button").Click();
        Assert.AreEqual("ebuttonClicked", App.Components.CreateByAutomationId<Label>("ResultLabelId").InnerText);
    }
}
