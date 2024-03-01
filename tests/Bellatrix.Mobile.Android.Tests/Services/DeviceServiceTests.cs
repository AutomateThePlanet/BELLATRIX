// <copyright file="DeviceServiceTests.cs" company="Automate The Planet Ltd.">
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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using BA = Bellatrix.Assertions;

namespace Bellatrix.Mobile.Android.Tests;

[TestClass]
[Android(Constants.AndroidNativeAppPath,
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".view.ControlsMaterialDark",
    Lifecycle.RestartEveryTime)]
[AllureSuite("Services")]
[AllureFeature("DeviceService")]
public class DeviceServiceTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void OrientationSetToLandscape_When_CallRotateWithLandscape()
    {
        App.Device.Rotate(ScreenOrientation.Landscape);

        Assert.AreEqual(ScreenOrientation.Landscape, App.Device.Orientation);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.KnownIssue)]
    public void CorrectTimeReturned_When_CallDeviceTime()
    {
        BA.DateTimeAssert.AreEqual(DateTime.Now, App.Device.DeviceTime, BA.DateTimeDeltaType.Minutes, 5);
    }

    ////[TestMethod]
    ////[TestCategory(Categories.CI)]
    ////[TestCategory(Categories.Mobile)]
    ////////[Ignore]
    ////public void SetLocation()
    ////{
    ////    Console.WriteLine(App.DeviceService.Location);
    ////    var location = new Location();
    ////    location.Altitude = 10;
    ////    location.Longitude = 11;
    ////    location.Latitude = 12;
    ////    App.DeviceService.Location = location;

    ////    Assert.AreEqual(10, App.DeviceService.Location.Altitude);
    ////    Assert.AreEqual(11, App.DeviceService.Location.Longitude);
    ////    Assert.AreEqual(12, App.DeviceService.Location.Latitude);
    ////}

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.KnownIssue)]
    public void DeviceIsLockedFalse_When_DeviceIsUnlocked()
    {
        App.Device.Unlock();

        Assert.IsTrue(App.Device.IsLocked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.KnownIssue)]
    public void DeviceIsLockedTrue_When_CallLock()
    {
        App.Device.Lock();

        Assert.IsTrue(App.Device.IsLocked);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.KnownIssue)]
    public void ConnectionTypeAirplaneMode_When_SetConnectionTypeToAirplaneMode()
    {
        try
        {
            App.Device.ConnectionType = ConnectionType.AirplaneMode;
            Assert.AreEqual(ConnectionType.AirplaneMode, App.Device.ConnectionType);

            App.Device.ConnectionType = ConnectionType.AllNetworkOn;
            Assert.AreEqual(ConnectionType.AllNetworkOn, App.Device.ConnectionType);
        }
        finally
        {
            App.Device.ConnectionType = ConnectionType.AllNetworkOn;
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void TestTurnOnLocationService()
    {
        App.Device.TurnOnLocationService();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void TestOpenNotifications()
    {
        App.Device.OpenNotifications();
    }
}