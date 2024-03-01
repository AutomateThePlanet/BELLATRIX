// <copyright file="ComboBoxControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("ComboBox Control")]
[AllureFeature("ValidateExtensions")]
public class ComboBoxControlValidateExtensionsTests : MSTest.IOSTest
{
    [TestMethod]
    [Ignore]
    public void ValidateTextIs_DoesNotThrowException_When_ComboBoxTextIsAsExpected()
    {
        var comboBox = App.Components.CreateById<ComboBox>("spinner1");

        comboBox.SelectByText("Jupiter");

        comboBox.ValidateTextIs("Jupiter");
    }

    [TestMethod]
    [Ignore]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_ComboBoxIsNotDisabled()
    {
        var comboBox = App.Components.CreateById<ComboBox>("spinner1");

        Assert.AreEqual(false, comboBox.IsDisabled);
    }
}
