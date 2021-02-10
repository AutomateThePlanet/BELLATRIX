using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.MSTest.Tests
{
    [TestClass]
    public class TestsInitialize : DesktopTest
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            var app = new App();

            app.UseExceptionLogger();
            app.UseMsTestSettings();
            app.UseAppBehavior();
            app.UseLogExecutionBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseVanillaWebDriverScreenshotsOnFail();
            app.UseElementsBddLogging();
            app.UseValidateExtensionsBddLogging();
            app.UseLayoutAssertionExtensionsBddLogging();
            app.StartWinAppDriver();
            app.Initialize();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<App>();
            app?.Dispose();
            app?.StopWinAppDriver();
        }
    }
}