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
using BA = Bellatrix.Assertions;

namespace Bellatrix.Mobile.IOS.Tests;

[TestClass]
[IOS(Constants.IOSNativeAppPath,
    Constants.IOSAppBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.ReuseIfStarted)]
[AllureSuite("Services")]
[AllureFeature("DeviceService")]
public class DeviceServiceTests : MSTest.IOSTest
{
    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void OrientationSetToLandscape_When_CallRotateWithLandscape()
    {
        App.Device.Rotate(ScreenOrientation.Landscape);

        Assert.AreEqual(ScreenOrientation.Landscape, App.Device.Orientation);
    }

    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void CorrectTimeReturned_When_CallDeviceTime()
    {
        BA.DateTimeAssert.AreEqual(DateTime.Now, App.Device.DeviceTime, BA.DateTimeDeltaType.Minutes, 5);
    }

    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    public void DeviceIsLockedTrue_When_CallLock()
    {
        App.Device.Lock(1);
    }

    [TestMethod]
    [Timeout(180000)]
    [TestCategory(Categories.CI)]
    [Ignore]
    public void TestShakeDevice()
    {
        App.Device.ShakeDevice();
    }
}
