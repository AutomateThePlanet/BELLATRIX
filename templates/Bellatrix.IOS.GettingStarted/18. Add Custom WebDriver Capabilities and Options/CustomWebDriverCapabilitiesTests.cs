using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class CustomWebDriverCapabilitiesTests : NUnit.IOSTest
{
    // 1. BELLATRIX hides the complexity of initialisation of WebDriver/Appium and all related services.
    // In some cases, you need to customise the set up of a Appium with using custom Appium options.
    // Using the App service methods you can add all of these with ease. Make sure to call them in the TestsArrange which is called before the
    // execution of the tests placed in the test class. These options are used only for the tests in this particular class.
    // Note: You can use all of these methods no matter which attributes you use- IOS, IOSSauceLabs, IOSBrowserStack or IOSCrossBrowserTesting.
    public override void TestsArrange()
    {
        // 2. Add custom iOS Appium options.
        App.AddAdditionalAppiumOption("autoAcceptAlerts", "true");
        App.AddAdditionalAppiumOption("showIOSLog", "true");
        App.AddAdditionalAppiumOption("remoteDebugProxy", "12000");
    }

    [Test]
    [CancelAfter(180000)]
    [Ignore("API example purposes only. No need to run.")]
    public void ButtonClicked_When_CallClickMethod()
    {
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        button.Click();
    }
}