// <copyright file="RadioButtonControlTests.cs" company="Automate The Planet Ltd.">
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
    Lifecycle.RestartEveryTime)]
[AllureSuite("RadioButton Control")]
public class RadioButtonControlTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ButtonClicked_When_ClickMethodCalled()
    {
        var radioButton = App.Components.CreateByIdContaining<RadioButton>("radio2");

        Assert.IsFalse(radioButton.IsChecked);

        radioButton.Click();

        Assert.IsTrue(radioButton.IsChecked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsDisabledReturnsFalse_When_RadioButtonIsNotDisabled()
    {
        var radioButton = App.Components.CreateByIdContaining<RadioButton>("radio2");

        Assert.IsFalse(radioButton.IsDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void RadioButton1TextReturned_When_RadioButtonGetTextMethodCalled()
    {
        var radioButton = App.Components.CreateByIdContaining<RadioButton>("radio1");

        string text = radioButton.GetText();

        Assert.AreEqual("RadioButton 1", text);
    }
}