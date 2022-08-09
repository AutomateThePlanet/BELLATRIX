using Bellatrix.Desktop.GettingStarted.Elements.ChildElements;
using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class ExtendExistingElementWithChildElementsTests : DesktopTest
{
    [Test]
    [Category(Categories.CI)]
    public void MessageChanged_When_ButtonClicked_Wpf()
    {
        // 1. Instead of the regular button, we create the ExtendedButton, this way we can use its new methods.
        var button = App.Components.CreateByName<ExtendedButton>("E Button");

        // 2. Use the new custom method provided by the ExtendedButton class.
        button.SubmitButtonWithEnter();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("ebuttonHovered", label.InnerText);
    }
}