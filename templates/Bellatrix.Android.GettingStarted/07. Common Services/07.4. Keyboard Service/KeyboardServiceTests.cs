using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Enums;

namespace Bellatrix.Mobile.Android.GettingStarted;

[TestFixture]
public class KeyboardServiceTests : NUnit.AndroidTest
{
    // 1. BELLATRIX gives you an interface for easier work with device's keyboard through KeyboardService class.
    [Test]
    [Category(Categories.CI)]
    public void TestHideKeyBoard()
    {
        var textField = App.Components.CreateByIdContaining<TextField>("edit");
        textField.SetText(string.Empty);

        // Hides the keyboard.
        App.Keyboard.HideKeyboard();
    }

    [Test]
    [Category(Categories.CI)]
    public void PressKeyCodeTest()
    {
        App.Keyboard.PressKeyCode(AndroidKeyCode.Home);
    }

    [Test]
    [Category(Categories.CI)]
    public void PressKeyCodeWithMetaStateTest()
    {
        // Press Space key simulating that the Shift key is ON.
        App.Keyboard.PressKeyCode(AndroidKeyCode.Space, AndroidKeyMetastate.Meta_Shift_On);
    }

    [Test]
    [Category(Categories.CI)]
    public void LongPressKeyCodeTest()
    {
        // Long press the Home button.
        App.Keyboard.LongPressKeyCode(AndroidKeyCode.Home);
    }

    [Test]
    [Category(Categories.CI)]
    public void LongPressKeyCodeWithMetaStateTest()
    {
        // Long press Space key simulating that the Shift key is ON.
        App.Keyboard.LongPressKeyCode(AndroidKeyCode.Space, AndroidKeyMetastate.Meta_Shift_On);
    }
}