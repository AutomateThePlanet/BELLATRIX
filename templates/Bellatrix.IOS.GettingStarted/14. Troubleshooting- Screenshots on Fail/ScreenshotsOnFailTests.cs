using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    // If you open the testFrameworkSettings file, you find the screenshotsSettings section that controls this lifecycle.
    // "screenshotsSettings": {
    //     "isEnabled": "true",
    //     "filePath": "ApplicationData\\Troubleshooting\\Screenshots"
    // }
    //
    // You can turn off the making of screenshots for all tests and specify where the screenshots to be saved.
    // In the extensibility chapters read more about how you can create different screenshots engine or change the saving strategy.
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    public class ScreenshotsOnFailTests : MSTest.IOSTest
    {
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ElementCreateService.CreateByName<Button>("ComputeSumButton");

            button.Click();
        }
    }
}