using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

// If you open the testFrameworkSettings file, you find the screenshotsSettings section that controls this lifecycle.
// "screenshotsSettings": {
//     "isEnabled": "true",
//     "filePath": "ApplicationData\\Troubleshooting\\Screenshots"
// }
//
// You can turn off the making of screenshots for all tests and specify where the screenshots to be saved.
// In the extensibility chapters read more about how you can create different screenshots engine or change the saving strategy.
[TestFixture]
public class AppScreenshotsOnFailTests : DesktopTest
{
    [Test]
    [Category(Categories.CI)]
    public void MessageChanged_When_ButtonHovered_Wpf()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        button.Hover();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        Assert.AreEqual("ebuttonHovered", label.InnerText);
    }
}