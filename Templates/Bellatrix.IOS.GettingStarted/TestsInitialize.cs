using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    public class TestsInitialize : IOSTest
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            var app = new IOSApp();

            app.UseExceptionLogger();
            app.UseMsTestSettings();
            app.UseAppBehavior();
            app.UseLogExecutionBehavior();
            app.UseLogExecutionBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseIOSDriverScreenshotsOnFail();
            app.UseElementsBddLogging();
            app.UseValidateExtensionsBddLogging();
            app.UseLayoutAssertionExtensionsBddLogging();
            app.StartAppiumLocalService();
            app.Initialize();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<IOSApp>();
            app?.Dispose();
            app?.StopAppiumLocalService();
        }
    }
}