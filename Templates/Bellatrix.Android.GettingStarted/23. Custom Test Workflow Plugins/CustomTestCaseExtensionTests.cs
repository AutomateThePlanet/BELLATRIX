using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        AppBehavior.ReuseIfStarted)]
    public class CustomTestCaseExtensionTests : AndroidTest
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
        public void ButtonClicked_When_CallClickMethod()
        {
            var button = App.ElementCreateService.CreateByIdContaining<Button>("button");

            button.Click();
        }
    }
}