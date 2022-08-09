// 1. You need to add a using statement to the namespace where the new wait extension methods are situated.
using Bellatrix.Desktop.GettingStarted.ExtensionMethodsWaitMethods;
using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class AddNewElementWaitMethodsTests : DesktopTest
{
    [Test]
    [Category(Categories.CI)]
    public void MessageChanged_When_ButtonHovered_Wpf()
    {
        // 2. After that, you can use the new wait method as it was originally part of Bellatrix.
        var button = App.Components.CreateByName<Button>("E Button").ToHaveSpecificContent("E Button");

        button.Hover();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("ebuttonHovered", label.InnerText);
    }
}
