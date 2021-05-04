using Bellatrix.Desktop.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    // If you open the testFrameworkSettings file, you find the screenshotsSettings section that controls this lifecycle.
    // "screenshotsSettings": {
    //     "isEnabled": "true",
    //     "filePath": "ApplicationData\\Troubleshooting\\Screenshots"
    // }
    //
    // You can turn off the making of screenshots for all tests and specify where the screenshots to be saved.
    // In the extensibility chapters read more about how you can create different screenshots engine or change the saving strategy.
    [TestClass]
    [App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
    public class AppScreenshotsOnFailTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonHovered_Wpf()
        {
            var button = App.ComponentCreateService.CreateByName<Button>("E Button");

            button.Hover();

            var label = App.ComponentCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }
    }
}