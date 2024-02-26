// <copyright file="TextFieldControlTests.cs" company="Automate The Planet Ltd.">
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
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".view.Controls1",
    Lifecycle.ReuseIfStarted)]
[AllureSuite("TextField Control")]
public class TextFieldControlTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void BellatrixTextSet_When_CallSetTextMethod()
    {
        var textField = App.Components.CreateByIdContaining<TextField>("edit");

        textField.SetText("Bellatrix");

        Assert.AreEqual("Bellatrix", textField.GetText());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ReturnsEmpty_When_CallGetTextMethodAndNoTextSet()
    {
        var textField = App.Components.CreateByIdContaining<TextField>("edit");

        textField.SetText(string.Empty);

        Assert.AreEqual(string.Empty, textField.GetText());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsDisabledReturnsFalse_When_TextFieldIsNotDisabled()
    {
        var textField = App.Components.CreateByIdContaining<TextField>("edit");

        Assert.AreEqual(false, textField.IsDisabled);
    }
}