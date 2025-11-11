using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

// If you open the testFrameworkSettings file, you find the screenshotsSettings section that controls this lifecycle.
// "screenshotsSettings": {
//     "isEnabled": "true",
//     "filePath": "ApplicationData\\Troubleshooting\\Screenshots"
// }
//
// You can turn off the making of screenshots for all tests and specify where the screenshots to be saved.
// In the extensibility chapters read more about how you can create different screenshots engine or change the saving strategy.
[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class ScreenshotsOnFailTests : NUnit.IOSTest
{
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ButtonClicked_When_CallClickMethod()
    {
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        button.Click();
    }
}