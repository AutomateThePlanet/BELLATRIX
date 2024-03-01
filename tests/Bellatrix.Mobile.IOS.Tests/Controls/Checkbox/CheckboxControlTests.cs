// <copyright file="CheckboxControlTests.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.Tests;

[TestClass]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
[AllureSuite("CheckBox Control")]
public class CheckboxControlTests : MSTest.IOSTest
{
    [TestMethod]
    [Timeout(180000)]
    public void IsCheckedTrue_When_CheckBoxUncheckedAndCheckIt()
    {
        var addButton = App.Components.CreateById<Button>("Add");
        addButton.Click();

        var checkBox = App.Components.CreateByIOSNsPredicate<CheckBox>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

        checkBox.Check();

        Assert.IsTrue(checkBox.IsChecked);
    }

    [TestMethod]
    [Timeout(180000)]
    public void IsCheckedFalse_When_CheckBoxCheckedAndUncheckIt()
    {
        var addButton = App.Components.CreateById<Button>("Add");
        addButton.Click();

        var checkBox = App.Components.CreateByIOSNsPredicate<CheckBox>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

        checkBox.Check();
        checkBox.Uncheck();

        Assert.IsFalse(checkBox.IsChecked);
    }

    [TestMethod]
    [Timeout(180000)]
    public void IsDisabledReturnsFalse_When_CheckBoxIsNotDisabled()
    {
        var addButton = App.Components.CreateById<Button>("Add");
        addButton.Click();

        var checkBox = App.Components.CreateByIOSNsPredicate<CheckBox>("type == \"XCUIElementTypeSwitch\" AND name == \"All-day\"");

        Assert.AreEqual(false, checkBox.IsDisabled);
    }
}
