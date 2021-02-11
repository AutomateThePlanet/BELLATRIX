using Bellatrix.Desktop.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]

    // 1. This is the attribute for cross-platform video recording by Bellatrix.
    // The engine checks after each test, its result, depending on the specified video saves the video.
    // All video recording modes:
    // Always - records and save video for all tests.
    // DoNotRecord - wont' record any videos.
    // Ignore - ignores the tests.
    // OnlyPass - saves the videos only for pass tests.
    // OnlyFail - saves the videos only for failed tests.
    // If you place attribute over the class all tests inherit the behaviour.
    // It is possible to put it over each test and this way you override the class behaviour only for this particular test.
    [VideoRecording(VideoRecordingMode.OnlyFail)]
    [App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
    public class VideoRecordingTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonHovered_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            button.Hover();

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }

        // 2. As mentioned above we can override the video behaviour for a particular test.
        // The global behaviour for all tests in the class is to save the videos only for failed tests.
        // Only for this particular test, we tell BELLATRIX not to make a video.
        [TestMethod]
        [App(Constants.WpfAppPath, Lifecycle.RestartOnFail)]
        [VideoRecording(VideoRecordingMode.DoNotRecord)]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonClicked_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            button.Click();

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonClicked", label.InnerText);
        }

        // 3. If you open the testFrameworkSettings file, you find the videoRecordingSettings section that controls this behaviour.
        //  "videoRecordingSettings": {
        //      "isEnabled": "true",
        //      "waitAfterFinishRecordingMilliseconds": "500",
        //      "filePath": "ApplicationData\\Troubleshooting\\Videos"
        //  }
        //
        // You can turn off the making of videos for all tests and specify where the videos to be saved.
        // waitAfterFinishRecordingMilliseconds adds some time to the end of the test, making the video not going black immediately.
        // In the extensibility chapters read more about how you can create custom video recorder or change the saving strategy.
    }
}