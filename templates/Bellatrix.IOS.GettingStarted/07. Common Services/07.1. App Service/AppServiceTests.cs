using System.IO;
using Bellatrix.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    public class AppServiceTests : MSTest.IOSTest
    {
        // 1. BELLATRIX gives you an interface to most common operations for controlling the iOS app through the AppService class.
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void TestBackgroundApp()
        {
            // Backgrounds the app for the specified number of seconds.
            App.AppService.BackgroundApp(1);
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void TestResetApp()
        {
            // Resets the app.
            App.AppService.ResetApp();
        }

        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void InstallAppInstalledTrue_When_AppIsInstalled()
        {
            // Checks whether the app with the specified bundleId is installed.
            Assert.IsTrue(App.AppService.IsAppInstalled("com.apple.mobilecal"));
        }

        [TestMethod]
        [Timeout(180000)]
        [Ignore]
        public void InstallAppInstalledFalse_When_AppIsUninstalled()
        {
            string appPath = Path.Combine(ProcessProvider.GetExecutingAssemblyFolder(), "Demos/TestApp.app.zip");

            // Installs the app file on the device.
            App.AppService.InstallApp(appPath);

            // Uninstalls the app with the specified bundleId. You can get your app's bundleId from XCode.
            App.AppService.RemoveApp("com.apple.mobilecal");

            Assert.IsFalse(App.AppService.IsAppInstalled("com.apple.mobilecal"));

            App.AppService.InstallApp(appPath);
        }
    }
}