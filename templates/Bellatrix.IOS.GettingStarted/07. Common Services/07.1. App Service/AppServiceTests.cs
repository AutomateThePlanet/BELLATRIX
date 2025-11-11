using System.IO;
using Bellatrix.Utilities;
using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class AppServiceTests : NUnit.IOSTest
{
    // 1. BELLATRIX gives you an interface to most common operations for controlling the iOS app through the AppService class.
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void TestBackgroundApp()
    {
        // Backgrounds the app for the specified number of seconds.
        App.AppService.BackgroundApp(1);
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void TestResetApp()
    {
        // Resets the app.
        App.AppService.ResetApp();
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void InstallAppInstalledTrue_When_AppIsInstalled()
    {
        // Checks whether the app with the specified bundleId is installed.
        Assert.That(App.AppService.IsAppInstalled("com.apple.mobilecal"));
    }

    [Test]
    [CancelAfter(180000)]
    [Ignore("API example purposes only. No need to run.")]
    public void InstallAppInstalledFalse_When_AppIsUninstalled()
    {
        string appPath = Path.Combine(ProcessProvider.GetExecutingAssemblyFolder(), "Demos/TestApp.app.zip");

        // Installs the app file on the device.
        App.AppService.InstallApp(appPath);

        // Uninstalls the app with the specified bundleId. You can get your app's bundleId from XCode.
        App.AppService.RemoveApp("com.apple.mobilecal");

        Assert.That(App.AppService.IsAppInstalled("com.apple.mobilecal"));

        App.AppService.InstallApp(appPath);
    }
}