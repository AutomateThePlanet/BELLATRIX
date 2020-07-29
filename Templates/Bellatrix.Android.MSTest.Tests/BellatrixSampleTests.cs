using Bellatrix.Mobile.Android;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.MSTest.Tests
{
    [TestClass]
    [Android("pathToApk",
        "7.1",
        "yourTestDeviceName",
        "testPackageName",
        "testActivityName",
        AppBehavior.ReuseIfStarted)]
    [VideoRecording(VideoRecordingMode.OnlyFail)]
    [ScreenshotOnFail(true)]
    public class BellatrixSampleTests : AndroidTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        [VideoRecording(VideoRecordingMode.Ignore)]
        [ScreenshotOnFail(false)]
        public void CorrectTextDisplayed_When_ClickSubscribeButton()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }
    }
}