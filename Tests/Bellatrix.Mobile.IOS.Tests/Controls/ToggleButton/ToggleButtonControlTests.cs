// <copyright file="ToggleButtonControlTests.cs" company="Automate The Planet Ltd.">
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
        AppBehavior.RestartEveryTime)]
    [AllureSuite("ToggleButton Control")]
    public class ToggleButtonControlTests : IOSTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.KnownIssue)]
        [Timeout(180000)]
        public void IsOnTrue_When_ToggleButtonTurnedOffAndTurnOn()
        {
            var addButton = App.ElementCreateService.CreateById<Button>("Add");
            addButton.Click();

            var toggleButton = App.ElementCreateService.CreateByXPath<ToggleButton>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

            Assert.IsFalse(toggleButton.IsOn);

            toggleButton.TurnOn();

            Assert.IsTrue(toggleButton.IsOn);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.KnownIssue)]
        [Timeout(180000)]
        public void IsOnFalse_When_ToggleButtonTurnedOnAndTurnOff()
        {
            var addButton = App.ElementCreateService.CreateById<Button>("Add");
            addButton.Click();

            var toggleButton = App.ElementCreateService.CreateByIOSNsPredicate<ToggleButton>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

            toggleButton.TurnOn();
            toggleButton.TurnOff();

            Assert.IsFalse(toggleButton.IsOn);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [TestCategory(Categories.KnownIssue)]
        [Timeout(180000)]
        public void IsDisabledReturnsFalse_When_ToggleButtonIsNotDisabled()
        {
            var addButton = App.ElementCreateService.CreateById<Button>("Add");
            addButton.Click();

            var toggleButton = App.ElementCreateService.CreateByIOSNsPredicate<ToggleButton>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

            Assert.AreEqual(false, toggleButton.IsDisabled);
        }
    }
}
