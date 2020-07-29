using Bellatrix.Mobile.IOS;
using NUnit.Framework;

namespace Bellatrix.Mobile.NUnit.Tests
{
    [TestFixture]
    [IOS("pathToApk",
        "7.1",
        "yourTestDeviceName",
        AppBehavior.ReuseIfStarted)]
    [VideoRecording(VideoRecordingMode.OnlyFail)]
    [ScreenshotOnFail(true)]
    public class BellatrixSampleTests : IOSTest
    {
        [Test]
        [Category(Categories.CI)]
        [VideoRecording(VideoRecordingMode.Ignore)]
        [ScreenshotOnFail(false)]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ElementCreateService.CreateById<Button>("button");

            button.Click();
        }
    }
}