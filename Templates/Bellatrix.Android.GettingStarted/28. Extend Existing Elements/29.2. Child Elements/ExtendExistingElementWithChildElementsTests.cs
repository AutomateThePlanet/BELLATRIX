using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        AppBehavior.ReuseIfStarted)]
    public class ExtendExistingElementWithChildElementsTests : AndroidTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ButtonClicked_When_CallClickMethod()
        {
            // 1. Instead of the regular button, we create the ExtendedButton, this way we can use its new methods.
            var button = App.ElementCreateService.CreateByIdContaining<ExtendedButton>("button");

            // 2. Use the new custom method provided by the ExtendedButton class.
            button.SubmitButtonWithScroll();
        }
    }
}