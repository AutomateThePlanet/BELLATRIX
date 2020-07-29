using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    [VideoRecording(VideoRecordingMode.OnlyFail)]
    [App(Constants.WpfAppPath, AppBehavior.RestartEveryTime)]
    public class CustomTestCaseExtensionTests : DesktopTest
    {
        // 1. Once we created the test workflow plugin, we need to add it to the existing test workflow.
        // It is done using the App service's method AddTestWorkflowPlugin.
        // It doesn't need to be added multiple times as will happen here with the TestInit method.
        // Usually this is done in the TestsInitialize file in the AssemblyInitialize method.
        //
        //  public static void AssemblyInitialize(TestContext testContext)
        //  {
        //      App.AddTestWorkflowPlugin<AssociatedTestCaseExtension>();
        //  }
        public override void TestInit()
        {
            // App.AddTestWorkflowPlugin<AssociatedTestCaseExtension>();
        }

        [TestMethod]
        [ManualTestCase(1532)]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonHovered_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            button.Hover();

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }

        [TestMethod]
        [App(Constants.WpfAppPath, AppBehavior.RestartOnFail)]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonClicked_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            button.Click();

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonClicked", label.InnerText);
        }
    }
}