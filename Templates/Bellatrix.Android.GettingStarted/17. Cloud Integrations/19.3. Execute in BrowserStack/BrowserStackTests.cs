using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]

    // 1. To execute BELLATRIX tests in BrowserStack cloud, you should use the BrowserStack attribute instead of Android.
    // BrowserStack has the same parameters as Android but adds to additional ones-
    // device name, captureVideo, captureNetworkLogs, consoleLogType, build and debug. The last five are optional and have default values.
    // As with the Android attribute you can override the class behaviour on Test level.
    //
    // 2. You can find a dedicated section about SauceLabs in testFrameworkSettings file under the mobileSettings section.
    //     "browserStack": {
    //     "gridUri":  "http://hub-cloud.browserstack.com/wd/hub/",
    //     "user": "soioa1",
    //     "key":  "pnFG3Ky2yLZ5muB1p46P"
    // }
    //
    // There you can set the grid URL and credentials.
    [AndroidBrowserStack("pngG38y26LZ5muB1p46P",
        "7.1",
        "Android GoogleAPI Emulator",
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.ControlsMaterialDark",
        AppBehavior.RestartEveryTime,
        captureVideo: true,
        captureNetworkLogs: true,
        consoleLogType: BrowserStackConsoleLogType.Disable,
        debug: false,
        build: "CI Execution")]
    public class BrowserStackTests : AndroidTest
    {
        [TestMethod]
        [Ignore]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }

        // 2. As mentioned if you use the BrowserStack attribute on method level it overrides the class settings.
        [TestMethod]
        [Ignore]
        [AndroidBrowserStack("pngG38y26LZ5muB1p46P",
            "7.1",
            "Android GoogleAPI Emulator",
            Constants.AndroidNativeAppAppExamplePackage,
            ".view.ControlsMaterialDark",
            AppBehavior.ReuseIfStarted,
            captureVideo: true,
            captureNetworkLogs: true,
            consoleLogType: BrowserStackConsoleLogType.Disable,
            debug: false,
            build: "CI Execution")]
        public void ButtonClicked_When_CallClickMethodSecond()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }
    }
}