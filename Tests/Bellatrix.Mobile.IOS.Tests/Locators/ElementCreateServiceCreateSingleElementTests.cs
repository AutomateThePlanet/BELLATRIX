﻿// <copyright file="ElementCreateServiceCreateSingleElementTests.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Mobile.IOS.Tests
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        AppBehavior.RestartEveryTime)]
    [AllureSuite("Services")]
    [AllureFeature("ElementCreateService")]
    public class ElementCreateServiceCreateSingleElementTests : IOSTest
    {
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateById_And_ElementIsOnScreen()
        {
            var textField = App.ElementCreateService.CreateById<TextField>("IntegerA");

            textField.ValidateIsVisible();
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByClass()
        {
            var textFields = App.ElementCreateService.CreateByClass<TextField>("XCUIElementTypeTextField");

            textFields.ValidateIsNotDisabled();
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByValueContaining_And_ElementIsOnScreen()
        {
            var label = App.ElementCreateService.CreateByValueContaining<Label>("SumLabel");

            label.ValidateIsVisible();
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByIOSNsPredicate_And_ElementIsOnScreen()
        {
            var button = App.ElementCreateService.CreateByIOSNsPredicate<Button>("type == \"XCUIElementTypeButton\" AND name == \"ComputeSumButton\"");

            button.ValidateIsVisible();
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByXPath_And_ElementIsOnScreen()
        {
            var button = App.ElementCreateService.CreateByXPath<Button>("//XCUIElementTypeButton[@name=\"ComputeSumButton\"]");

            button.ValidateIsVisible();
        }
    }
}
