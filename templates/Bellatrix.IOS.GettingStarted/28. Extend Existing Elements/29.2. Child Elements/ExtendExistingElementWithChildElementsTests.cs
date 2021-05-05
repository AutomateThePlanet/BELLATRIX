using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    public class ExtendExistingElementWithChildElementsTests : MSTest.IOSTest
    {
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void ButtonClicked_When_CallClickMethod()
        {
            // 1. Instead of the regular button, we create the ExtendedButton, this way we can use its new methods.
            var button = App.Components.CreateByName<ExtendedButton>("ComputeSumButton");

            // 2. Use the new custom method provided by the ExtendedButton class.
            button.SubmitButtonWithScroll();
        }
    }
}