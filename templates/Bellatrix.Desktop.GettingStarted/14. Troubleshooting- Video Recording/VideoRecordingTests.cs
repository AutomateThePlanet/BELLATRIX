﻿using Bellatrix.Desktop.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    // If you open the testFrameworkSettings file, you find the videoRecordingSettings section that controls this lifecycle.
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
    [App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
    public class VideoRecordingTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonHovered_Wpf()
        {
            var button = App.ComponentCreateService.CreateByName<Button>("E Button");

            button.Hover();

            var label = App.ComponentCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }
    }
}