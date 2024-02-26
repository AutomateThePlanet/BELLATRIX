// <copyright file="ButtonControlTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Button Control")]
public class ButtonControlTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ButtonClicked_When_CallClickMethod()
    {
        var button = App.Components.CreateByIdContaining<Button>("button");

        button.Click();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ReturnsSave_When_GetText()
    {
        var button = App.Components.CreateByIdContaining<Button>("button");

        Assert.AreEqual("Save", button.GetText());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsDisabledReturnsFalse_When_ButtonIsNotDisabled()
    {
        var button = App.Components.CreateByIdContaining<Button>("button");

        Assert.AreEqual(false, button.IsDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsDisabledReturnsTrue_When_ButtonIsDisabled()
    {
        var button = App.Components.CreateByIdContaining<Button>("button_disabled");

        Assert.AreEqual(true, button.IsDisabled);
    }
}