using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class CustomTestCaseExtensionTests : DesktopTest
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
    public void MessageChanged_When_ButtonHovered_Wpf()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        button.Hover();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("ebuttonHovered", label.InnerText);
    }

    [Test]
    [App(Constants.WpfAppPath, Lifecycle.RestartOnFail)]
    [Category(Categories.CI)]
    public void MessageChanged_When_ButtonClicked_Wpf()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        button.Click();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("ebuttonClicked", label.InnerText);
    }
}