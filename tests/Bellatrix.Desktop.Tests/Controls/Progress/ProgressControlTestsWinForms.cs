﻿// <copyright file="ProgressControlTestsWinForms.cs" company="Automate The Planet Ltd.">
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

[TestClass]
[App(Constants.WinFormsAppPath, Lifecycle.RestartEveryTime)]
[AllureSuite("Progress Control")]
[AllureTag("WinForms")]
public class ProgressControlTestsWinForms : MSTest.DesktopTest
{
    [TestMethod]
    [TestCategory(Categories.Desktop)]
    public void MessageChanged_When_ProgressHovered_WinForms()
    {
        var progress = App.Components.CreateByAutomationId<Calendar>("progress");

        progress.Hover();

        var label = App.Components.CreateByAutomationId<Label>("resultLabel");
        Assert.IsTrue(label.IsVisible);
    }
}
