using Bellatrix.TestExecutionExtensions.Common.ExecutionTime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]

    // 1. Sometimes it is useful to use your functional tests to measure performance. Or to just make sure that your app
    // is not slow. To do that BELLATRIX libraries offer the ExecutionTimeUnder attribute. You specify a timeout and if the
    // test is executed over it the test will fail.
    //
    // 1.1. You need to add the NuGet package- Bellatrix.TestExecutionExtensions.Common
    // 1.2. After that you need to add a using statement to Bellatrix.TestExecutionExtensions.Common.ExecutionTime
    [ExecutionTimeUnder(2000, TimeUnit.Milliseconds)]
    [App(Constants.WpfAppPath, AppBehavior.RestartEveryTime)]
    public class MeasureTestExecutionTimesTests : DesktopTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void MessageChanged_When_ButtonHovered_Wpf()
        {
            var button = App.ElementCreateService.CreateByName<Button>("E Button");

            button.Hover();

            var label = App.ElementCreateService.CreateByAutomationId<Label>("ResultLabelId");
            Assert.AreEqual("ebuttonHovered", label.InnerText);
        }
    }
}