using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    // If you open the testFrameworkSettings file, you find the screenshotsSettings section that controls this behaviour.
    // "screenshotsSettings": {
    //     "isEnabled": "true",
    //     "filePath": "ApplicationData\\Troubleshooting\\Screenshots"
    // }
    //
    // You can turn off the making of screenshots for all tests and specify where the screenshots to be saved.
    // In the extensibility chapters read more about how you can create different screenshots engine or change the saving strategy.
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        Lifecycle.ReuseIfStarted)]
    public class ScreenshotsOnFailTests : MSTest.AndroidTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }
    }
}