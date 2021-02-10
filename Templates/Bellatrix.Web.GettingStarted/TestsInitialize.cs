using Bellatrix.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.GettingStarted
{
    [TestClass]
    public class TestsInitialize : WebTest
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            var app = new App();

            app.UseExceptionLogger();
            app.UseMsTestSettings();
            app.UseControlDataHandlers();
            app.UseBrowserBehavior();
            app.UseLogExecutionBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseFullPageScreenshotsOnFail();
            app.UseElementsBddLogging();
            app.UseHighlightElements();
            app.UseValidateExtensionsBddLogging();
            app.UseLayoutAssertionExtensionsBddLogging();
            app.UseLoadTesting();
            app.Initialize();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<App>();
            app?.Dispose();
        }
    }
}