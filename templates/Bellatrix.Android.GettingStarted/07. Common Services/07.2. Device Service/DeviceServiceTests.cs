using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using BA = Bellatrix.Assertions;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        Lifecycle.RestartEveryTime)]
    public class DeviceServiceTests : MSTest.AndroidTest
    {
        // 1. BELLATRIX gives you an interface to most common operations for controlling the device through the DeviceService class.
        [TestMethod]
        [Ignore]
        [TestCategory(Categories.KnownIssue)]
        public void OrientationSetToLandscape_When_CallRotateWithLandscape()
        {
            // Rotates the device horizontally.
            App.DeviceService.Rotate(ScreenOrientation.Landscape);

            // Gets the current device orientation.
            Assert.AreEqual(ScreenOrientation.Landscape, App.DeviceService.Orientation);
        }

        [TestMethod]
        [Ignore]
        [TestCategory(Categories.KnownIssue)]
        public void CorrectTimeReturned_When_CallDeviceTime()
        {
            // Gets current device time.
            BA.DateTimeAssert.AreEqual(DateTime.Now, App.DeviceService.DeviceTime, BA.DateTimeDeltaType.Minutes, 5);
        }

        [TestMethod]
        [Ignore]
        [TestCategory(Categories.KnownIssue)]
        public void DeviceIsLockedFalse_When_DeviceIsUnlocked()
        {
            // Unlocks the device.
            App.DeviceService.Unlock();

            // Checks if the device is locked or not.
            Assert.IsTrue(App.DeviceService.IsLocked);
        }

        [TestMethod]
        [Ignore]
        [TestCategory(Categories.KnownIssue)]
        public void DeviceIsLockedTrue_When_CallLock()
        {
            // Locks the device.
            App.DeviceService.Lock();

            Assert.IsTrue(App.DeviceService.IsLocked);
        }

        [TestMethod]
        [Ignore]
        public void ConnectionTypeAirplaneMode_When_SetConnectionTypeToAirplaneMode()
        {
            try
            {
                // Changes the connection to Airplane mode.
                App.DeviceService.ConnectionType = ConnectionType.AirplaneMode;

                // Checks whether the current connection type is airplane mode.
                Assert.AreEqual(ConnectionType.AirplaneMode, App.DeviceService.ConnectionType);

                App.DeviceService.ConnectionType = ConnectionType.AllNetworkOn;
                Assert.AreEqual(ConnectionType.AllNetworkOn, App.DeviceService.ConnectionType);
            }
            finally
            {
                App.DeviceService.ConnectionType = ConnectionType.AllNetworkOn;
            }
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void TestTurnOnLocationService()
        {
            // Turns on the location service.
            App.DeviceService.TurnOnLocationService();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void TestOpenNotifications()
        {
            // Opens notifications.
            App.DeviceService.OpenNotifications();
        }
    }
}