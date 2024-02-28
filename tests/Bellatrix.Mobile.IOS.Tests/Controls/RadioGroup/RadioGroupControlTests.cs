// <copyright file="RadioGroupControlTests.cs" company="Automate The Planet Ltd.">
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
    Constants.IOSAppBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
[AllureSuite("RadioGroup Control")]
public class RadioGroupControlTests : MSTest.IOSTest
{
    [TestMethod]
    [Timeout(180000)]
    public void Return1RadioButtons_When_CallGetAllMethod()
    {
        var radioGroup = App.Components.CreateByIOSNsPredicate<RadioGroup>("type == \"XCUIElementTypeApplication\" AND name == \"TestApp\"");

        var radioButtons = radioGroup.GetAll();

        Assert.AreEqual(1, radioButtons.Count());
    }

    [TestMethod]
    [Timeout(180000)]
    public void ClickFirstRadioButton_When_CallClickByIndex()
    {
        var radioGroup = App.Components.CreateByIOSNsPredicate<RadioGroup>("type == \"XCUIElementTypeApplication\" AND name == \"TestApp\"");

        radioGroup.ClickByIndex(0);
        var clickedRadioButton = radioGroup.GetChecked();

        clickedRadioButton.ValidateIsChecked();
    }
}
