// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Mobile.Android.GettingStarted.CommonServicesExtensions;

using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestFixture]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        Lifecycle.ReuseIfStarted)]
    public class ExtendExistingCommonServicesTests : MSTest.AndroidTest
    {
        [Test]
        [Ignore("API example purposes only. No need to run.")]
        public void ButtonClicked_When_CallClickMethod()
        {
            // 2. Use newly added login method which is not part of the original implementation of the common service.
            App.AppService.LoginToApp("bellatrix", "topSecret");

            var button = App.Components.CreateByIdContaining<Button>("button");

            button.Click();
        }
    }
}