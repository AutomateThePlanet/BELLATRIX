using System.IO;
using Bellatrix.Utilities;
using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

[TestFixture]
public class AppServiceTests : NUnit.AndroidTest
{
    // 1. BELLATRIX gives you an interface to most common operations for controlling the Android app through the AppService class.
    // We already saw one of them StartActivity for opening a particular initial activity.
    [Test]
    [Category(Categories.CI)]
    public void TestBackgroundApp()
    {
        // Backgrounds the app for the specified number of seconds.
        App.AppService.BackgroundApp(1);
    }

    [Test]
    [Category(Categories.CI)]
    public void TestResetApp()
    {
        // Resets the app.
        App.AppService.ResetApp();
    }

    [Test]
    [Category(Categories.CI)]
    public void InstallAppInstalledFalse_When_AppIsUninstalled()
    {
        // Uninstalls the app with the specified app package.
        App.AppService.RemoveApp("com.example.android.apis");

        // Checks whether the app with the specified app package is installed.
        Assert.That(!App.AppService.IsAppInstalled("com.example.android.apis"));

        string appPath = Path.Combine(ProcessProvider.GetExecutingAssemblyFolder(), "Demos\\ApiDemos.apk");

        // Installs the APK file on the device.
        App.AppService.InstallApp(appPath);
    }
}