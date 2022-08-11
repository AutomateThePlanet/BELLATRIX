using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

// If you open the testFrameworkSettings file, you find the videoRecordingSettings section that controls this lifecycle.
//  "videoRecordingSettings": {
//      "isEnabled": "true",
//      "waitAfterFinishRecordingMilliseconds": "500",
//      "filePath": "ApplicationData\\Troubleshooting\\Videos"
//  }
//
// You can turn off the making of videos for all tests and specify where the videos to be saved.
// waitAfterFinishRecordingMilliseconds adds some time to the end of the test, making the video not going black immediately.
// In the extensibility chapters read more about how you can create custom video recorder or change the saving strategy.
[TestFixture]
public class VideoRecordingTests : DesktopTest
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