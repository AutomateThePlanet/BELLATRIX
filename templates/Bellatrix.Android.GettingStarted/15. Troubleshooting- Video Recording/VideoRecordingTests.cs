using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    // If you open the testFrameworkSettings file, you find the videoRecordingSettings section that controls this behaviour.
    //  "videoRecordingSettings": {
    //      "isEnabled": "true",
    //      "waitAfterFinishRecordingMilliseconds": "500",
    //      "filePath": "ApplicationData\\Troubleshooting\\Videos"
    //  }
    //
    // You can turn off the making of videos for all tests and specify where the videos to be saved.
    // waitAfterFinishRecordingMilliseconds adds some time to the end of the test, making the video not going black immediately.
    // In the extensibility chapters read more about how you can create custom video recorder or change the saving strategy.
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        Lifecycle.ReuseIfStarted)]
    public class VideoRecordingTests : MSTest.AndroidTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ButtonClicked_When_CallClickMethodSecond()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }
    }
}