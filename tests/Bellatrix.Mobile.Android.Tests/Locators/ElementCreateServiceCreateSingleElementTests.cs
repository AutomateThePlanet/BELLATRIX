// <copyright file="ComponentCreateServiceCreateSingleElementTests.cs" company="Automate The Planet Ltd.">
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
    public class ComponentCreateServiceCreateSingleElementTests : MSTest.AndroidTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByIdContaining_And_ElementIsOnScreen()
        {
            var button = App.ComponentCreateService.CreateByIdContaining<Button>("button");

            button.ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByIdContaining_And_ElementIsNotOnScreen()
        {
            var textField = App.ComponentCreateService.CreateByIdContaining<TextField>("edit");

            textField.ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateById_And_ElementIsOnScreen()
        {
            var button = App.ComponentCreateService.CreateById<Button>("com.example.android.apis:id/button");

            button.ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateById_And_ElementIsNotOnScreen()
        {
            var textField = App.ComponentCreateService.CreateById<TextField>("com.example.android.apis:id/edit");

            textField.ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByClass()
        {
            var checkBox = App.ComponentCreateService.CreateByClass<CheckBox>("android.widget.CheckBox");

            checkBox.ValidateIsNotDisabled();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByText_And_ElementIsOnScreen()
        {
            var button = App.ComponentCreateService.CreateByText<Button>("BUTTON");

            button.ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByText_And_ElementIsNotOnScreen()
        {
            var textField = App.ComponentCreateService.CreateByText<TextField>("Text appearances");

            textField.ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByTextContaining_And_ElementIsOnScreen()
        {
            var button = App.ComponentCreateService.CreateByTextContaining<Button>("BUTTO");

            button.ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByTextContaining_And_ElementIsNotOnScreen()
        {
            var textField = App.ComponentCreateService.CreateByTextContaining<TextField>("Text appearanc");

            textField.ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByAndroidUIAutomator_And_ElementIsOnScreen()
        {
            var button = App.ComponentCreateService.CreateByAndroidUIAutomator<Button>("new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().resourceId(\"com.example.android.apis:id/button\"));");

            button.ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByAndroidUIAutomator_And_ElementIsNotOnScreen()
        {
            var textField = App.ComponentCreateService.CreateByAndroidUIAutomator<TextField>("new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().resourceId(\"com.example.android.apis:id/edit\"));");

            textField.ValidateIsVisible();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ElementFound_When_CreateByXPath_And_ElementIsOnScreen()
        {
            var button = App.ComponentCreateService.CreateByXPath<Button>("//*[@resource-id='com.example.android.apis:id/button']");

            button.ValidateIsVisible();
        }
    }
}
