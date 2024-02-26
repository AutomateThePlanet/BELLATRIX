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

namespace Bellatrix.Mobile.Android.Tests;

[TestClass]
[Android(Constants.AndroidNativeAppPath,
    "",
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".view.Controls1",
    Lifecycle.ReuseIfStarted)]
[AllureSuite("ComboBox Control")]
[AllureFeature("ValidateExtensions")]
public class ComboBoxControlValidateExtensionsTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ValidateTextIs_DoesNotThrowException_When_ComboBoxTextIsAsExpected()
    {
        var comboBox = App.Components.CreateByIdContaining<ComboBox>("spinner1");

        comboBox.SelectByText("Jupiter");

        comboBox.ValidateTextIs("Jupiter");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ValidateIsNotDisabled_DoesNotThrowException_When_ComboBoxIsNotDisabled()
    {
        var comboBox = App.Components.CreateByIdContaining<ComboBox>("spinner1");

        Assert.AreEqual(false, comboBox.IsDisabled);
    }
}