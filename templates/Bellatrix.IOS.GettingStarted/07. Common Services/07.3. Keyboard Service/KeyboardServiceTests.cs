using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        Lifecycle.RestartEveryTime)]
    public class KeyboardServiceTests : MSTest.IOSTest
    {
        // 1. BELLATRIX gives you an interface for easier work with device's keyboard through KeyboardService class.
        [TestMethod]
        [Timeout(180000)]
        [TestCategory(Categories.CI)]
        public void TestHideKeyBoard()
        {
            var textField = App.Components.CreateById<TextField>("IntegerA");
            textField.SetText(string.Empty);

            // Hides the keyboard.
            App.KeyboardService.HideKeyboard();
        }
    }
}