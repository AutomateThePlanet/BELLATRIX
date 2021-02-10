using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    public class TestsInitialize : AndroidTest
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            var app = new AndroidApp();

            app.UseExceptionLogger();
            app.UseMsTestSettings();
            app.UseAppBehavior();
            app.UseLogExecutionBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseAndroidDriverScreenshotsOnFail();
            app.UseElementsBddLogging();
            app.UseValidateExtensionsBddLogging();
            app.UseLayoutAssertionExtensionsBddLogging();
            app.StartAppiumLocalService();
            app.Initialize();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<AndroidApp>();
            app?.Dispose();
            app?.StopAppiumLocalService();
        }
    }
}