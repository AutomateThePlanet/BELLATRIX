using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]

// 1. To execute BELLATRIX tests in SauceLabs cloud you should use the IOSSauceLabs attribute instead of IOS.
// SauceLabs has the same parameters as IOS but adds to additional ones-
// device name, recordVideo and recordScreenshots.
// As with the IOS attribute you can override the class behavior on Test level.
//
// 2. You can find a dedicated section about SauceLabs in testFrameworkSettings file under the mobileSettings section.
// "sauceLabs": {
//         "gridUri":  "http://ondemand.saucelabs.com:80/wd/hub",
//         "user": "aangelov",
//         "key":  "mySecretKey"
//     }
//
// There you can set the grid URL and credentials.
[IOSSauceLabs("sauce-storage:TestApp.app.zip",
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class SauceLabsTests : NUnit.IOSTest
{
    [Test]
    [CancelAfter(180000)]
    [Ignore("API example purposes only. No need to run.")]
    public void ButtonClicked_When_CallClickMethod()
    {
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        button.Click();
    }

    // 2. As mentioned if you use the SauceLabs attribute on method level it overrides the class settings.
    [Test]
    [CancelAfter(180000)]
    [IOSSauceLabs("sauce-storage:TestApp.app.zip",
        Constants.AppleCalendarBundleId,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.ReuseIfStarted)]
    [Ignore("API example purposes only. No need to run.")]
    public void ButtonClicked_When_CallClickMethodSecond()
    {
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        button.Click();
    }
}