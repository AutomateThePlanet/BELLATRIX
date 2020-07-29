using Bellatrix.Mobile.Android;
using NUnit.Framework;

namespace Bellatrix.Mobile.NUnit.Tests
{
    [TestFixture]
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
        [Test]
        [Category(Categories.CI)]
        [VideoRecording(VideoRecordingMode.Ignore)]
        [ScreenshotOnFail(false)]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }
    }
}