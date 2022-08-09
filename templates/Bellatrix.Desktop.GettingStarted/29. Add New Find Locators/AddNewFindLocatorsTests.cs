// 1. You need to add a using statement to the namespace where the extension methods for new locator are situated.
using Bellatrix.Desktop.GettingStarted.ExtensionMethodsLocators;
using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class AddNewFindLocatorsTests : DesktopTest
{
    [Test]
    [Category(Categories.CI)]
    public void MessageChanged_When_ButtonHovered_Wpf()
    {
        // 2. After that, you can use the new locator as it was originally part of Bellatrix.
        var button = App.Components.CreateByNameStartingWith<Button>("E Butto");

        button.Hover();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("ebuttonHovered", label.InnerText);
    }
}