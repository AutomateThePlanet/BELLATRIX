using Bellatrix.Desktop.NUnit;
using Bellatrix.Plugins.Common.ExecutionTime;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]

// 1. Sometimes it is useful to use your functional tests to measure performance. Or to just make sure that your app
// is not slow. To do that BELLATRIX libraries offer the ExecutionTimeUnder attribute. You specify a timeout and if the
// test is executed over it the test will fail.
//
// 1.1. You need to add the NuGet package- Bellatrix.Plugins.Common
// 1.2. After that you need to add a using statement to Bellatrix.Plugins.Common.ExecutionTime
[ExecutionTimeUnder(2000, TimeUnit.Milliseconds)]
public class MeasureTestExecutionTimesTests : DesktopTest
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