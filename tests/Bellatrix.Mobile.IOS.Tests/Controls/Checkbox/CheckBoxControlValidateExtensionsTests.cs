// <copyright file="CheckBoxControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
    [IOS(Constants.AppleCalendarBundleId,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    [AllureSuite("CheckBox Control")]
    [AllureFeature("ValidateExtensions")]
    public class CheckBoxControlValidateExtensionsTests : MSTest.IOSTest
    {
        [TestMethod]
        [Timeout(180000)]
        public void ValidateIsChecked_DoesNotThrowException_When_CheckBoxIsChecked()
        {
            var addButton = App.ElementCreateService.CreateById<Button>("Add");
            addButton.Click();

            var checkBox = App.ElementCreateService.CreateByIOSNsPredicate<CheckBox>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");
            checkBox.Check();

            checkBox.ValidateIsChecked();
        }

        [TestMethod]
        [Timeout(180000)]
        public void ValidateIsNotChecked_DoesNotThrowException_When_CheckBoxIsNotChecked()
        {
            var addButton = App.ElementCreateService.CreateById<Button>("Add");
            addButton.Click();

            var checkBox = App.ElementCreateService.CreateByIOSNsPredicate<CheckBox>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

            checkBox.Check();
            checkBox.Uncheck();

            checkBox.ValidateIsNotChecked();
        }

        [TestMethod]
        [Timeout(180000)]
        public void ValidateIsDisabled_DoesNotThrowException_When_CheckBoxIsNotDisabled()
        {
            var addButton = App.ElementCreateService.CreateById<Button>("Add");
            addButton.Click();

            var checkBox = App.ElementCreateService.CreateByIOSNsPredicate<CheckBox>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

            checkBox.ValidateIsNotDisabled();
        }
    }
}
