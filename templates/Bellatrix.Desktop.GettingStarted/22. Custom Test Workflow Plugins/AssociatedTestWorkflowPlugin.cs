using System;
using System.Linq;
using System.Reflection;
using Bellatrix.Plugins;

namespace Bellatrix.Desktop.GettingStarted;

// 1. To create a custom test workflow plugin:
// 1.1. Create a new class that derives from the 'Plugin' base class.
// 1.2. Then override some of the workflow's protected methods adding there your logic.
// 1.3. Register the workflow plugin using the AddPlugin method of the App service.
public class AssociatedPlugin : Plugin
{
    // 2. You can override all mentioned test workflow method hooks in your custom handlers.
    // The method uses reflection to find out if the ManualTestCase attribute is set to the run test.
    // If the attribute is not set or is set more than once an exception is thrown.
    // The logic executes before the actual test run, during the PreTestInit phase.
    //
    // 3. Your plug-ins can plug in the screenshots and video generation on fail.
    // 3.1. To do a post-screenshot generation action, implement the IScreenshotPlugin interface and add your logic to ScreenshotGenerated method.
    // 3.2. To do a post-video generation action, implement the IVideoPlugin interface and add your logic to VideoGenerated method.
    protected override void PreTestInit(object sender, PluginEventArgs e)
    {
        base.PreTestInit(sender, e);
        ValidateManualTestCaseAttribute(e.TestMethodMemberInfo);
    }

    private void ValidateManualTestCaseAttribute(MemberInfo memberInfo)
    {
        if (memberInfo == null)
        {
            throw new ArgumentNullException();
        }

        var methodBrowserAttributes = memberInfo.GetCustomAttributes<ManualTestCaseAttribute>(true).ToList();
        if (methodBrowserAttributes.Count == 0)
        {
            throw new ArgumentException("No manual test case is associated with the BELLATRIX test.");
        }
        else if (methodBrowserAttributes.Count > 1)
        {
            throw new ArgumentException("You cannot associate two manual test cases with a single BELLATRIX test.");
        }
        else if (methodBrowserAttributes.First().TestCaseId <= 0)
        {
            throw new ArgumentException("The associated manual test case ID cannot be <= 0.");
        }
    }
}
