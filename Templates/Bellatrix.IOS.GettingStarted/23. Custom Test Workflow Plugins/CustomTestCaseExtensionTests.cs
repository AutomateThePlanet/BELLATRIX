using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    [IOS(Constants.IOSNativeAppPath,
        Constants.IOSDefaultVersion,
        Constants.IOSDefaultDeviceName,
        AppBehavior.RestartEveryTime)]
    public class CustomTestCaseExtensionTests : IOSTest
    {
        // 1. Once we created the test workflow plugin, we need to add it to the existing test workflow.
        // It is done using the App service's method AddTestWorkflowPlugin.
        // It doesn't need to be added multiple times as will happen here with the TestInit method.
        // Usually this is done in the TestsInitialize file in the AssemblyInitialize method.
        //
        //  public static void AssemblyInitialize(TestContext testContext)
        //  {
        //      App.AddTestWorkflowPlugin<AssociatedTestWorkflowPlugin>();
        //  }
        public override void TestInit()
        {
            App.AddTestWorkflowPlugin<AssociatedTestWorkflowPlugin>();
        }

        [TestMethod]
        [Timeout(180000)]
        [ManualTestCase(1532)]
        [Ignore]
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ElementCreateService.CreateByName<Button>("ComputeSumButton");

            button.Click();
        }
    }
}