// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Mobile.Android.GettingStarted.Custom;

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
    public class ExtendExistingElementWithExtensionMethodsTests : MSTest.AndroidTest
    {
        [TestMethod]
        [Ignore]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ComponentCreateService.CreateByIdContaining<Button>("button");

            // 2. Use the custom added submit button  with scroll-to-visible lifecycle.
            button.SubmitButtonWithScroll();
        }
    }
}