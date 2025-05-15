﻿// <copyright file="TextFieldControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".view.Controls1",
    Lifecycle.ReuseIfStarted)]
[AllureSuite("TextField Control")]
[AllureFeature("ValidateExtensions")]
public class TextFieldControlValidateExtensionsTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ValidateTextIs_DoesNotThrowException_When_TextIsSet()
    {
        var textField = App.Components.CreateByIdContaining<TextField>("edit");

        textField.SetText("Bellatrix");

        textField.ValidateTextIs("Bellatrix");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ValidateTextIsNotSet_DoesNotThrowException_When_TextFieldIsNotSet()
    {
        var textField = App.Components.CreateByIdContaining<TextField>("edit");

        textField.SetText(string.Empty);

        textField.ValidateTextIsNotSet();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ValidateIsDisabled_DoesNotThrowException_When_TextFieldIsNotDisabled()
    {
        var textField = App.Components.CreateByIdContaining<TextField>("edit");

        Assert.AreEqual(false, textField.IsDisabled);
    }
}