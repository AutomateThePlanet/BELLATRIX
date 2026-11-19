using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]

// 1. Instead of a path, you can pass an app name to the App attribute. BELLATRIX looks the name up under
// desktopSettings.applicationsSettings in the configuration file and starts the app from its appPath:
// "applicationsSettings": {
//   "WpfSample": { "appPath": "AssemblyFolder\\Demos\\Wpf\\WPFSampleApp.exe" },
//   "WinFormsSample": { "appPath": "AssemblyFolder\\Demos\\WinForms\\WindowsFormsSampleApp.exe" }
// }
// If the name is not found there, it is used as a path, as before.
[App("WpfSample", Lifecycle.RestartEveryTime)]
public class MultipleAppsTests : DesktopTest
{
    [Test]
    public void ButtonsClicked_When_SwitchingBetweenApps()
    {
        App.Components.CreateByName<Button>("E Button").Click();
        Assert.AreEqual("ebuttonClicked", App.Components.CreateByAutomationId<Label>("ResultLabelId").InnerText);

        // 2. App.SwitchApp starts the second app if it is not open yet and makes it the active one.
        // All components created after the switch are searched in the active app.
        App.SwitchApp("WinFormsSample");
        App.Components.CreateByName<Button>("E Button").Click();
        Assert.IsTrue(App.Components.CreateByAutomationId<Label>("resultLabel").IsVisible);

        // 3. Switching back to an app that is already open reuses its session.
        App.SwitchApp("WpfSample");
        Assert.AreEqual("ebuttonClicked", App.Components.CreateByAutomationId<Label>("ResultLabelId").InnerText);

        // 4. After the test, BELLATRIX switches back to the app from the App attribute and closes the apps opened with SwitchApp.
        // To close an app yourself and make sure its process is gone, use App.CloseOrKill("WinFormsSample", () => { /* close through the UI */ }).
    }
}
