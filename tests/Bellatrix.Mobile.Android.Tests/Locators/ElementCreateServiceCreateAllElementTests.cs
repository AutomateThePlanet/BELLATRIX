﻿// <copyright file="ComponentCreateServiceCreateAllElementTests.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Mobile.Android.Tests
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.ControlsMaterialDark",
        Lifecycle.RestartEveryTime)]
    [AllureSuite("Services")]
    [AllureFeature("ComponentCreateService")]
    public class ComponentCreateServiceCreateAllElementTests : MSTest.AndroidTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllByIdContaining_And_ElementIsOnScreen()
        {
            var buttons = App.ComponentCreateService.CreateAllByIdContaining<Button>("button");

            buttons[0].ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllByIdContaining_And_ElementIsNotOnScreen()
        {
            var textFields = App.ComponentCreateService.CreateAllByIdContaining<TextField>("edit");

            textFields[0].ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllById_And_ElementIsOnScreen()
        {
            var buttons = App.ComponentCreateService.CreateAllById<Button>("com.example.android.apis:id/button");

            buttons[0].ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllById_And_ElementIsNotOnScreen()
        {
            var textFields = App.ComponentCreateService.CreateAllById<TextField>("com.example.android.apis:id/edit");

            textFields[0].ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllByClass()
        {
            var checkBoxes = App.ComponentCreateService.CreateAllByClass<CheckBox>("android.widget.CheckBox");

            checkBoxes[0].ValidateIsNotDisabled();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllByText_And_ElementIsOnScreen()
        {
            var buttons = App.ComponentCreateService.CreateAllByText<Button>("BUTTON");

            buttons[0].ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllByText_And_ElementIsNotOnScreen()
        {
            var textFields = App.ComponentCreateService.CreateAllByText<TextField>("Text appearances");

            textFields[0].ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllByTextContaining_And_ElementIsOnScreen()
        {
            var buttons = App.ComponentCreateService.CreateAllByTextContaining<Button>("BUTTO");

            buttons[0].ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllByTextContaining_And_ElementIsNotOnScreen()
        {
            var textFields = App.ComponentCreateService.CreateAllByTextContaining<TextField>("Text appearanc");

            textFields[0].ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllByAndroidUIAutomator_And_ElementIsOnScreen()
        {
            var buttons = App.ComponentCreateService.CreateAllByAndroidUIAutomator<Button>("new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().resourceId(\"com.example.android.apis:id/button\"));");

            buttons[0].ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllByAndroidUIAutomator_And_ElementIsNotOnScreen()
        {
            var textFields = App.ComponentCreateService.CreateAllByAndroidUIAutomator<TextField>("new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().resourceId(\"com.example.android.apis:id/edit\"));");

            textFields[0].ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateAllByXPath_And_ElementIsOnScreen()
        {
            var buttons = App.ComponentCreateService.CreateAllByXPath<Button>("//*[@resource-id='com.example.android.apis:id/button']");

            buttons[0].ValidateIsVisible();
        }
    }
}
