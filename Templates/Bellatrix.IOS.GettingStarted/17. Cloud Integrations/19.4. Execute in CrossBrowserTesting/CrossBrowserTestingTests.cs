using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]

    // 1. To execute BELLATRIX tests in CrossBrowserTesting cloud, you should use the CrossBrowserTesting attribute instead of IOS.
    // CrossBrowserTesting has the same parameters as IOS but adds to additional ones-
    // deviceName, recordVideo, recordNetwork and build. The last three are optional and have default values.
    // As with the IOS attribute you can override the class behaviour on Test level.
    //
    // 2. You can find a dedicated section about SauceLabs in testFrameworkSettings file under the mobileSettings section.
    //     "crossBrowserTesting": {
    //     "gridUri":  "http://hub.crossbrowsertesting.com:80/wd/hub",
    //     "user": "aangelov",
    //     "key":  "mySecretKey"
    // }
    //
    // There you can set the grid URL and credentials.
    [IOSCrossBrowserTesting("crossBrowser-storage:TestApp.app.zip",
        "11.3",
        "iPhone 6",
        Lifecycle.RestartEveryTime,
        recordVideo: true,
        recordNetwork: true,
        build: "CI Execution")]
    public class CrossBrowserTesting : MSTest.IOSTest
    {
        [TestMethod]
        [Timeout(180000)]
        [Ignore]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ElementCreateService.CreateByName<Button>("ComputeSumButton");

            button.Click();
        }

        // 2. As mentioned if you use the CrossBrowserTesting attribute on method level it overrides the class settings.
        [TestMethod]
        [Timeout(180000)]
        [Ignore]
        [IOSCrossBrowserTesting("crossBrowser-storage:TestApp.app.zip",
            "11.3",
            "iPhone 6",
            Lifecycle.ReuseIfStarted,
            recordVideo: true,
            recordNetwork: true,
            build: "CI Execution")]
        public void ButtonClicked_When_CallClickMethodSecond()
        {
            var button = App.ElementCreateService.CreateByName<Button>("ComputeSumButton");

            button.Click();
        }
    }
}