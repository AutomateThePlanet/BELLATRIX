using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]

    // 1. This is the attribute for automatic generation of app screenshots by Bellatrix.
    // The engine checks after each test, its result, if failed, makes the screenshots.
    //
    // If you place attribute over the class all tests inherit the behaviour.
    // It is possible to put it over each test and this way you override the class behaviour only for this particular test.
    [ScreenshotOnFail(true)]
    [App(Constants.WpfAppPath, AppBehavior.RestartEveryTime)]
    public class AppScreenshotsOnFailTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonHovered_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            button.Hover();

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }

        // 2. As mentioned above we can override the screenshot behaviour for a particular test.
        // The global behaviour for all tests in the class is to make screenshots on fail.
        // Only for this particular test, we tell BELLATRIX not to make screenshots.
        [TestMethod]
        [ScreenshotOnFail(false)]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonClicked_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            button.Click();

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonClicked", label.InnerText);
        }

        // 3. If you open the testFrameworkSettings file, you find the screenshotsSettings section that controls this behaviour.
        // "screenshotsSettings": {
        //     "isEnabled": "true",
        //     "filePath": "ApplicationData\\Troubleshooting\\Screenshots"
        // }
        //
        // You can turn off the making of screenshots for all tests and specify where the screenshots to be saved.
        // In the extensibility chapters read more about how you can create different screenshots engine or change the saving strategy.
    }
}