using Bellatrix.Desktop.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]
    public class AddCustomWebDriverCapabilitiesTests : DesktopTest
    {
        // 1. BELLATRIX hides the complexity of initialisation of WebDriver and all related services.
        // In some cases, you need to customise the set up of the app with adding driver capabilities.
        // Using the App service methods you can add all of these with ease. Make sure to call them in the TestsArrange which is called before the
        // execution of the tests placed in the test class. These options are used only for the tests in this particular class.
        public override void TestsArrange()
        {
            // 2. Add custom WebDriver capability.
            App.AddAdditionalCapability("appArguments", @"MyTestFile.txt");
            App.AddAdditionalCapability("appWorkingDir", @"C:\MyTestFolder\");
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [Ignore]
        public void MessageChanged_When_ButtonHovered_Wpf()
        {
            var button = App.Components.CreateByName<Button>("E Button");

            button.Hover();

            var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        [App(Constants.WpfAppPath, Lifecycle.RestartOnFail)]
        [Ignore]
        public void MessageChanged_When_ButtonClicked_Wpf()
        {
            var button = App.Components.CreateByName<Button>("E Button");

            button.Click();

            var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonClicked", label.InnerText);
        }
    }
}