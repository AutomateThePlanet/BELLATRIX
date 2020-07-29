using Bellatrix.Mobile.IOS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.MSTest.Tests
{
    [TestClass]
    [IOS("pathToApk",
        "7.1",
        "yourTestDeviceName",
        AppBehavior.ReuseIfStarted)]
    [VideoRecording(VideoRecordingMode.OnlyFail)]
    [ScreenshotOnFail(true)]
    public class BellatrixSampleTests : IOSTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        [VideoRecording(VideoRecordingMode.Ignore)]
        [ScreenshotOnFail(false)]
        public void CorrectTextDisplayed_When_ClickSubscribeButton()
        {
            var button = App.ElementCreateService.CreateById<Button>("button");

            button.Click();
        }
    }
}