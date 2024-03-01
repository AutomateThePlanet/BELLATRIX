// <copyright file="ToggleButtonControlTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("ToggleButton Control")]
public class ToggleButtonControlTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsOnTrue_When_ToggleButtonTurnedOffAndTurnOn()
    {
        var toggleButton = App.Components.CreateByIdContaining<ToggleButton>("toggle1");

        Assert.IsFalse(toggleButton.IsOn);

        toggleButton.TurnOn();

        Assert.IsTrue(toggleButton.IsOn);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsOnFalse_When_ToggleButtonTurnedOnAndTurnOff()
    {
        var toggleButton = App.Components.CreateByIdContaining<ToggleButton>("toggle1");

        toggleButton.TurnOn();
        toggleButton.TurnOff();

        Assert.IsFalse(toggleButton.IsOn);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void OffTextReturned_When_CallGetTextMethod()
    {
        var toggleButton = App.Components.CreateByIdContaining<ToggleButton>("toggle1");

        string text = toggleButton.GetText();

        Assert.AreEqual("OFF", text);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsCheckedFalse_When_ToggleButtonIsTurnedOff()
    {
        var toggleButton = App.Components.CreateByIdContaining<ToggleButton>("toggle2");

        Assert.IsFalse(toggleButton.IsOn);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsDisabledReturnsFalse_When_ToggleButtonIsNotDisabled()
    {
        var toggleButton = App.Components.CreateByIdContaining<ToggleButton>("toggle2");

        Assert.AreEqual(false, toggleButton.IsDisabled);
    }
}