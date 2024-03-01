using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class KeyboardServiceTests : NUnit.IOSTest
{
    // 1. BELLATRIX gives you an interface for easier work with device's keyboard through KeyboardService class.
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void TestHideKeyBoard()
    {
        var textField = App.Components.CreateById<TextField>("IntegerA");
        textField.SetText(string.Empty);

        // Hides the keyboard.
        App.Keyboard.HideKeyboard();
    }
}