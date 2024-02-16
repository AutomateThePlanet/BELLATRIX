using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class CustomTestCaseExtensionTests : NUnit.IOSTest
{
    // 1. Once we created the test workflow plugin, we need to add it to the existing test workflow.
    // It is done using the App service's method AddPlugin.
    // It doesn't need to be added multiple times as will happen here with the TestInit method.
    // Usually this is done in the TestsInitialize file in the AssemblyInitialize method.
    //
    //  public static void AssemblyInitialize(TestContext testContext)
    //  {
    //      App.AddPlugin<AssociatedPlugin>();
    //  }
    public override void TestInit()
    {
        App.AddPlugin<AssociatedPlugin>();
    }

    [Test]
    [CancelAfter(180000)]
    [ManualTestCase(1532)]
    [Ignore("API example purposes only. No need to run.")]
    public void ButtonClicked_When_CallClickMethod()
    {
        var button = App.Components.CreateByName<Button>("ComputeSumButton");

        button.Click();
    }
}