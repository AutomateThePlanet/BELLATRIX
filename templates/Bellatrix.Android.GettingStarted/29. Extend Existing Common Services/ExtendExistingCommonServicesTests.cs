// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Mobile.Android.GettingStarted.CommonServicesExtensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        Lifecycle.ReuseIfStarted)]
    public class ExtendExistingCommonServicesTests : MSTest.AndroidTest
    {
        [TestMethod]
        [Ignore]
        public void ButtonClicked_When_CallClickMethod()
        {
            // 2. Use newly added login method which is not part of the original implementation of the common service.
            App.AppService.LoginToApp("bellatrix", "topSecret");

            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }
    }
}