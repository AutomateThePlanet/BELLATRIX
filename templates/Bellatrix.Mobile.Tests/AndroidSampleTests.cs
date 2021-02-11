using Bellatrix.Mobile.Android;
////using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Bellatrix.Mobile.Tests
{
    // uncomment to use MSTest
    ////[TestClass]
    [TestFixture]
    [Android("pathToApk",
        "7.1",
        "yourTestDeviceName",
        "testPackageName",
        "testActivityName",
        Lifecycle.ReuseIfStarted)]
    [VideoRecording(VideoRecordingMode.OnlyFail)]
    [ScreenshotOnFail(true)]
    public class AndroidSampleTests : NUnit.AndroidTest
    {
        ////[TestMethod]
        [Test]
        public void CorrectTextDisplayed_When_ClickSubscribeButton()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }
    }
}