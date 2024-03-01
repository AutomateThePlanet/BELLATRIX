// <copyright file="SwitchControlTests.cs" company="Automate The Planet Ltd.">
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
    ".view.Switches",
    Lifecycle.ReuseIfStarted)]
[AllureSuite("Switch Control")]
public class SwitchControlTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsOnTrue_When_SwitchTurnedOffAndTurnOn()
    {
        var switchControl = App.Components.CreateByXPath<Switch>("//*[@class='android.widget.Switch'][1]");

        Assert.IsFalse(switchControl.IsOn);

        switchControl.TurnOn();

        Assert.IsTrue(switchControl.IsOn);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsOnFalse_When_SwitchTurnedOnAndTurnOff()
    {
        var switchControl = App.Components.CreateByXPath<Switch>("//*[@class='android.widget.Switch'][2]");

        switchControl.TurnOff();

        Assert.IsFalse(switchControl.IsOn);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void DefaultIsOnTextReturned_When_CallGetTextMethod()
    {
        var switchControl = App.Components.CreateByXPath<Switch>("//*[@class='android.widget.Switch'][2]");

        switchControl.TurnOn();
        string text = switchControl.GetText();

        Assert.AreEqual("Default is on ON", text);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsCheckedFalse_When_ToggleButtonIsTurnedOff()
    {
        var switchControl = App.Components.CreateByXPath<Switch>("//*[@class='android.widget.Switch'][4]");

        Assert.IsFalse(switchControl.IsOn);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void IsDisabledReturnsFalse_When_ToggleButtonIsNotDisabled()
    {
        var switchControl = App.Components.CreateByXPath<Switch>("//*[@class='android.widget.Switch'][4]");

        Assert.AreEqual(false, switchControl.IsDisabled);
    }
}