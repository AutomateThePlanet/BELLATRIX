using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

[TestFixture]
public class CustomTestCaseExtensionTests : NUnit.AndroidTest
{
    // 1. Once we created the test workflow plugin, we need to add it to the existing test workflow.
    // It is done using the App service's method AddPlugin.
    // It doesn't need to be added multiple times as will happen here with the TestInit method.
    // Usually this is done in the TestsInitialize file in the AssemblyInitialize method.
    //
    //  public static void AssemblyInitialize(TestContext testContext)
    //  {
    //      App.AddPlugin<AssociatedTestCaseExtension>();
    //  }
    public override void TestInit()
    {
        // App.AddPlugin<AssociatedTestCaseExtension>();
    }

    [Test]
    [ManualTestCase(1532)]
    [Category(Categories.CI)]
    public void ButtonClicked_When_CallClickMethod()
    {
        var button = App.Components.CreateByIdContaining<Button>("button");

        button.Click();
    }
}