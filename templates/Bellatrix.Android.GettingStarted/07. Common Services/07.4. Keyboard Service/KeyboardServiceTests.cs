using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Enums;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        Lifecycle.ReuseIfStarted)]
    public class KeyboardServiceTests : MSTest.AndroidTest
    {
        // 1. BELLATRIX gives you an interface for easier work with device's keyboard through KeyboardService class.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void TestHideKeyBoard()
        {
            var textField = App.ComponentCreateService.CreateByIdContaining<TextField>("edit");
            textField.SetText(string.Empty);

            // Hides the keyboard.
            App.KeyboardService.HideKeyboard();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PressKeyCodeTest()
        {
            App.KeyboardService.PressKeyCode(AndroidKeyCode.Home);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PressKeyCodeWithMetaStateTest()
        {
            // Press Space key simulating that the Shift key is ON.
            App.KeyboardService.PressKeyCode(AndroidKeyCode.Space, AndroidKeyMetastate.Meta_Shift_On);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void LongPressKeyCodeTest()
        {
            // Long press the Home button.
            App.KeyboardService.LongPressKeyCode(AndroidKeyCode.Home);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void LongPressKeyCodeWithMetaStateTest()
        {
            // Long press Space key simulating that the Shift key is ON.
            App.KeyboardService.LongPressKeyCode(AndroidKeyCode.Space, AndroidKeyMetastate.Meta_Shift_On);
        }
    }
}