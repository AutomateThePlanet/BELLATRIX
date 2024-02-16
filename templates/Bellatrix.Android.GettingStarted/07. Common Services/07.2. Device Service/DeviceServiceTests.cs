using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using BA = Bellatrix.Assertions;

namespace Bellatrix.Mobile.Android.GettingStarted;

[TestFixture]
public class DeviceServiceTests : NUnit.AndroidTest
{
    // 1. BELLATRIX gives you an interface to most common operations for controlling the device through the DeviceService class.
    [Test]
    [Ignore("API example purposes only. No need to run.")]
    [Category(Categories.KnownIssue)]
    public void OrientationSetToLandscape_When_CallRotateWithLandscape()
    {
        // Rotates the device horizontally.
        App.Device.Rotate(ScreenOrientation.Landscape);

        // Gets the current device orientation.
        Assert.That(ScreenOrientation.Landscape.Equals(App.Device.Orientation));
    }

    [Test]
    [Ignore("API example purposes only. No need to run.")]
    [Category(Categories.KnownIssue)]
    public void CorrectTimeReturned_When_CallDeviceTime()
    {
        // Gets current device time.
        BA.DateTimeAssert.AreEqual(DateTime.Now, App.Device.DeviceTime, BA.DateTimeDeltaType.Minutes, 5);
    }

    [Test]
    [Ignore("API example purposes only. No need to run.")]
    [Category(Categories.KnownIssue)]
    public void DeviceIsLockedFalse_When_DeviceIsUnlocked()
    {
        // Unlocks the device.
        App.Device.Unlock();

        // Checks if the device is locked or not.
        Assert.That(App.Device.IsLocked);
    }

    [Test]
    [Ignore("API example purposes only. No need to run.")]
    [Category(Categories.KnownIssue)]
    public void DeviceIsLockedTrue_When_CallLock()
    {
        // Locks the device.
        App.Device.Lock();

        Assert.That(App.Device.IsLocked);
    }

    [Test]
    [Ignore("API example purposes only. No need to run.")]
    public void ConnectionTypeAirplaneMode_When_SetConnectionTypeToAirplaneMode()
    {
        try
        {
            // Changes the connection to Airplane mode.
            App.Device.ConnectionType = ConnectionType.AirplaneMode;

            // Checks whether the current connection type is airplane mode.
            Assert.That(ConnectionType.AirplaneMode.Equals(App.Device.ConnectionType));

            App.Device.ConnectionType = ConnectionType.AllNetworkOn;
            Assert.That(ConnectionType.AllNetworkOn.Equals(App.Device.ConnectionType));
        }
        finally
        {
            App.Device.ConnectionType = ConnectionType.AllNetworkOn;
        }
    }

    [Test]
    [Category(Categories.CI)]
    public void TestTurnOnLocationService()
    {
        // Turns on the location service.
        App.Device.TurnOnLocationService();
    }

    [Test]
    [Category(Categories.CI)]
    public void TestOpenNotifications()
    {
        // Opens notifications.
        App.Device.OpenNotifications();
    }
}