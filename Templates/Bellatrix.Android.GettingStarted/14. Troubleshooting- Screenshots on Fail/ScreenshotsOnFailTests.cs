using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]

    // 1. This is the attribute for automatic generation of app screenshots by Bellatrix.
    // The engine checks after each test, its result, if failed, makes the screenshots.
    //
    // If you place attribute over the class all tests inherit the behaviour.
    // It is possible to put it over each test and this way you override the class behaviour only for this particular test.
    [ScreenshotOnFail(true)]
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

        // 2. As mentioned above we can override the screenshot behaviour for a particular test.
        // The global behaviour for all tests in the class is to make screenshots on fail.
        // Only for this particular test, we tell BELLATRIX not to make screenshots.
        [TestMethod]
        [ScreenshotOnFail(false)]
        [TestCategory(Categories.CI)]
        public void ButtonClicked_When_CallClickMethodSecond()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }

        // 3. If you open the testFrameworkSettings file, you find the screenshotsSettings section that controls this behaviour.
        // "screenshotsSettings": {
        //     "isEnabled": "true",
        //     "filePath": "ApplicationData\\Troubleshooting\\Screenshots"
        // }
        //
        // You can turn off the making of screenshots for all tests and specify where the screenshots to be saved.
        // In the extensibility chapters read more about how you can create different screenshots engine or change the saving strategy.
    }
}