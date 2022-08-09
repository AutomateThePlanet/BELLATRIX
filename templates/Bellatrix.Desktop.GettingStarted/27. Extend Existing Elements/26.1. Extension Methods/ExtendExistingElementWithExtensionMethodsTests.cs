// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Desktop.GettingStarted.Advanced.Elements.Extension.Methods;
using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class ExtendExistingElementWithExtensionMethodsTests : DesktopTest
{
    [Test]
    [Category(Categories.CI)]
    public void MessageChanged_When_ButtonClicked_Wpf()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        // 2. Use the custom added submit button lifecycle through 'Enter' key.
        button.SubmitButtonWithEnter();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("ebuttonHovered", label.InnerText);
    }
}