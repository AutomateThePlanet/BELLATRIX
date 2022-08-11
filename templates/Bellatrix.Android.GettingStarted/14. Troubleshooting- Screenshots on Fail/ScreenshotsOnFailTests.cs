using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

// If you open the testFrameworkSettings file, you find the screenshotsSettings section that controls this lifecycle.
// "screenshotsSettings": {
//     "isEnabled": "true",
//     "filePath": "ApplicationData\\Troubleshooting\\Screenshots"
// }
//
// You can turn off the making of screenshots for all tests and specify where the screenshots to be saved.
// In the extensibility chapters read more about how you can create different screenshots engine or change the saving strategy.
[TestFixture]
public class ScreenshotsOnFailTests : NUnit.AndroidTest
{
    [Test]
    [Category(Categories.CI)]
    public void ButtonClicked_When_CallClickMethod()
    {
        var button = App.Components.CreateByIdContaining<Button>("button");

        button.Click();
    }
}