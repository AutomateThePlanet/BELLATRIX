using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        AppBehavior.RestartEveryTime)]
    public class KeyboardServiceTests : IOSTest
    {
        // 1. BELLATRIX gives you an interface for easier work with device's keyboard through KeyboardService class.
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void TestHideKeyBoard()
        {
            var textField = App.ElementCreateService.CreateById<TextField>("IntegerA");
            textField.SetText(string.Empty);

            // Hides the keyboard.
            App.KeyboardService.HideKeyboard();
        }
    }
}