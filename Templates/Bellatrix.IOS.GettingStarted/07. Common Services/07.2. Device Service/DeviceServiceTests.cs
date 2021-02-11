using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using BA = Bellatrix.Assertions;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    public class DeviceServiceTests : MSTest.IOSTest
    {
        // 1. BELLATRIX gives you an interface to most common operations for controlling the device through the DeviceService class.
        [TestMethod]
        [Timeout(180000)]
        [Ignore]
        public void OrientationSetToLandscape_When_CallRotateWithLandscape()
        {
            // Rotates the device horizontally.
            App.DeviceService.Rotate(ScreenOrientation.Landscape);

            // Gets the current device orientation.
            Assert.AreEqual(ScreenOrientation.Landscape, App.DeviceService.Orientation);

            App.DeviceService.Rotate(ScreenOrientation.Portrait);
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void CorrectTimeReturned_When_CallDeviceTime()
        {
            // Gets current device time.
            BA.DateTimeAssert.AreEqual(DateTime.Now, App.DeviceService.DeviceTime, BA.DateTimeDeltaType.Minutes, 5);
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void DeviceIsLockedTrue_When_CallLock()
        {
            // Locks the device
            App.DeviceService.Lock(1);
        }

        [TestMethod]
        [Timeout(180000)]
        [Ignore]
        public void TestShakeDevice()
        {
            // Shakes the device
            App.DeviceService.ShakeDevice();
        }
    }
}