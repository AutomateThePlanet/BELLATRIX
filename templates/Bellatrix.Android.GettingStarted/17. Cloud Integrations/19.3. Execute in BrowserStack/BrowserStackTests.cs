using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

[TestFixture]

// 1. To execute BELLATRIX tests in BrowserStack cloud, you should use the BrowserStack attribute instead of Android.
// BrowserStack has the same parameters as Android but adds to additional ones-
// device name, captureVideo, captureNetworkLogs, consoleLogType, build and debug. The last five are optional and have default values.
// As with the Android attribute you can override the class lifecycle on Test level.
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
    Constants.AndroidNativeAppId,
    "7.1",
    "Android GoogleAPI Emulator",
    ".view.ControlsMaterialDark",
    Lifecycle.RestartEveryTime,
    captureVideo: true,
    captureNetworkLogs: true,
    consoleLogType: BrowserStackConsoleLogType.Disable,
    debug: false,
    build: "CI Execution")]
public class BrowserStackTests : NUnit.AndroidTest
{
    [Test]
    [Ignore("API example purposes only. No need to run.")]
    public void ButtonClicked_When_CallClickMethod()
    {
        var button = App.Components.CreateByIdContaining<Button>("button");

        button.Click();
    }

    // 2. As mentioned if you use the BrowserStack attribute on method level it overrides the class settings.
    [Test]
    [Ignore("API example purposes only. No need to run.")]
    [AndroidBrowserStack("pngG38y26LZ5muB1p46P",
        Constants.AndroidNativeAppId,
        "7.1",
        "Android GoogleAPI Emulator",
        ".view.ControlsMaterialDark",
        Lifecycle.ReuseIfStarted,
        captureVideo: true,
        captureNetworkLogs: true,
        consoleLogType: BrowserStackConsoleLogType.Disable,
        debug: false,
        build: "CI Execution")]
    public void ButtonClicked_When_CallClickMethodSecond()
    {
        var button = App.Components.CreateByIdContaining<Button>("button");

        button.Click();
    }
}