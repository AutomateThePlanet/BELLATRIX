﻿// <copyright file="LabelControlTestsUniversal.cs" company="Automate The Planet Ltd.">
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
    [App(Constants.UniversalAppPath, AppBehavior.RestartEveryTime)]
    [AllureSuite("Label Control")]
    [AllureTag("Universal")]
    public class LabelControlTestsUniversal : BellatrixBaseTest
    {
        [TestMethod]
        [TestCategory(Categories.Desktop)]
        public void MessageChanged_When_LabelHovered_Universal()
        {
            var label = App.ElementCreateService.CreateByAutomationId<Label>("resultTextBlock");

            label.Hover();

            Assert.AreEqual("labelHovered", label.InnerText);
        }
    }
}
