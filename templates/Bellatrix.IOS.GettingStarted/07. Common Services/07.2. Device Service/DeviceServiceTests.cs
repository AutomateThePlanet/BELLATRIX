using NUnit.Framework;
using OpenQA.Selenium;
using System;
using BA = Bellatrix.Assertions;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class DeviceServiceTests : NUnit.IOSTest
{
    // 1. BELLATRIX gives you an interface to most common operations for controlling the device through the DeviceService class.
    [Test]
    [CancelAfter(180000)]
    [Ignore("API example purposes only. No need to run.")]
    public void OrientationSetToLandscape_When_CallRotateWithLandscape()
    {
        // Rotates the device horizontally.
        App.Device.Rotate(ScreenOrientation.Landscape);

        // Gets the current device orientation.
        Assert.That(ScreenOrientation.Landscape.Equals(App.Device.Orientation));

        App.Device.Rotate(ScreenOrientation.Portrait);
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void CorrectTimeReturned_When_CallDeviceTime()
    {
        // Gets current device time.
        BA.DateTimeAssert.AreEqual(DateTime.Now, App.Device.DeviceTime, BA.DateTimeDeltaType.Minutes, 5);
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void DeviceIsLockedTrue_When_CallLock()
    {
        // Locks the device
        App.Device.Lock(1);
    }

    [Test]
    [CancelAfter(180000)]
    [Ignore("API example purposes only. No need to run.")]
    public void TestShakeDevice()
    {
        // Shakes the device
        App.Device.ShakeDevice();
    }
}