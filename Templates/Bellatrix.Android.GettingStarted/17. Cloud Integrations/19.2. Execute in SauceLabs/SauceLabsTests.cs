using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]

    // 1. To execute BELLATRIX tests in SauceLabs cloud you should use the AndroidSauceLabs attribute instead of Android.
    // SauceLabs has the same parameters as Android but adds to additional ones-
    // device name, recordVideo and recordScreenshots.
    // As with the Android attribute you can override the class behavior on Test level.
    //
    // 2. You can find a dedicated section about SauceLabs in testFrameworkSettings file under the mobileSettings section.
    // "sauceLabs": {
    //         "gridUri":  "http://ondemand.saucelabs.com:80/wd/hub",
    //         "user": "aangelov",
    //         "key":  "mySecretKey"
    //     }
    //
    // There you can set the grid URL and credentials.
    [AndroidSauceLabs("sauce-storage:ApiDemos.apk",
        "7.1",
        "Android GoogleAPI Emulator",
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.ControlsMaterialDark",
        AppBehavior.RestartEveryTime)]
    public class SauceLabsTests : AndroidTest
    {
        [TestMethod]
        [Ignore]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }

        // 2. As mentioned if you use the SauceLabs attribute on method level it overrides the class settings.
        [TestMethod]
        [AndroidSauceLabs("sauce-storage:ApiDemos.apk",
            "7.1",
            "Android GoogleAPI Emulator",
            Constants.AndroidNativeAppAppExamplePackage,
            ".view.ControlsMaterialDark",
            AppBehavior.ReuseIfStarted)]
        [Ignore]
        public void ButtonClicked_When_CallClickMethodSecond()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }
    }
}