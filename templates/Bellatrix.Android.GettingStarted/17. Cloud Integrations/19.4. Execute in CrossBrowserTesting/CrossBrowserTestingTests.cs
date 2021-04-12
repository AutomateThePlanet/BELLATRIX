﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]

    // 1. To execute BELLATRIX tests in CrossBrowserTesting cloud, you should use the CrossBrowserTesting attribute instead of Android.
    // CrossBrowserTesting has the same parameters as Android but adds to additional ones-
    // deviceName, recordVideo, recordNetwork and build. The last three are optional and have default values.
    // As with the Android attribute you can override the class lifecycle on Test level.
    //
    // 2. You can find a dedicated section about SauceLabs in testFrameworkSettings file under the mobileSettings section.
    //     "crossBrowserTesting": {
    //     "gridUri":  "http://hub.crossbrowsertesting.com:80/wd/hub",
    //     "user": "aangelov",
    //     "key":  "mySecretKey"
    // }
    //
    // There you can set the grid URL and credentials.
    [AndroidCrossBrowserTesting("crossBrowser-storage:ApiDemos.apk",
        "7.1",
        "Android GoogleAPI Emulator",
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.ControlsMaterialDark",
        Lifecycle.RestartEveryTime,
        recordVideo: true,
        recordNetwork: true,
        build: "CI Execution")]
    public class CrossBrowserTesting : MSTest.AndroidTest
    {
        [TestMethod]
        [Ignore]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }

        // 2. As mentioned if you use the CrossBrowserTesting attribute on method level it overrides the class settings.
        [TestMethod]
        [Ignore]
        [AndroidCrossBrowserTesting("crossBrowser-storage:ApiDemos.apk",
            "7.1",
            "Android GoogleAPI Emulator",
            Constants.AndroidNativeAppAppExamplePackage,
            ".view.ControlsMaterialDark",
            Lifecycle.ReuseIfStarted,
            recordVideo: true,
            recordNetwork: true,
            build: "CI Execution")]
        public void ButtonClicked_When_CallClickMethodSecond()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }
    }
}