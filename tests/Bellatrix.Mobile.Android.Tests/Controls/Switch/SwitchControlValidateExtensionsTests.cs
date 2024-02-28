// <copyright file="SwitchControlValidateExtensionsTests.cs" company="Automate The Planet Ltd.">
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
[AllureFeature("ValidateExtensions")]
public class SwitchControlValidateExtensionsTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ValidateIsOn_DoesNotThrowException_When_SwitchIsTurnedOn()
    {
        var switchControl = App.Components.CreateByXPath<Switch>("//*[@class='android.widget.Switch'][2]");

        switchControl.TurnOn();

        switchControl.ValidateIsOn();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ValidateIsOff_DoesNotThrowException_When_SwitchIsTurnedoff()
    {
        var switchControl = App.Components.CreateByXPath<Switch>("//*[@class='android.widget.Switch'][2]");

        switchControl.TurnOff();

        switchControl.ValidateIsOff();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ValidateTextIs_DoesNotThrowException_When_CorrectTextIsSet()
    {
        var switchControl = App.Components.CreateByXPath<Switch>("//*[@class='android.widget.Switch'][2]");

        switchControl.TurnOn();

        switchControl.ValidateTextIs("Default is on ON");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ValidateIsDisabled_DoesNotThrowException_When_SwitchIsNotDisabled()
    {
        var switchControl = App.Components.CreateByXPath<Switch>("//*[@class='android.widget.Switch'][4]");

        switchControl.ValidateIsNotDisabled();
    }
}