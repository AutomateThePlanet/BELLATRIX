﻿// <copyright file="TabsControlTestsWpf.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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

namespace Bellatrix.Desktop.Tests
{
    [TestClass]
    [App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
    [AllureSuite("Tabs Control")]
    [AllureTag("WPF")]
    public class TabsControlTestsWpf : BellatrixBaseTest
    {
        [TestMethod]
        [TestCategory(Categories.Desktop)]
        public void MessageChanged_When_TabsHovered_Wpf()
        {
            var listBox = App.ComponentCreateService.CreateByAutomationId<Tabs>("tabs");

            listBox.Hover();

            var label = App.ComponentCreateService.CreateByAutomationId<Label>("ResultLabelId");
            label.ValidateInnerTextIs("tabsHovered");
        }
    }
}
