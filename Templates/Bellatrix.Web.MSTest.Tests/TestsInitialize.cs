using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.MSTest.Tests
{
    [TestClass]
    public class TestsInitialize : WebTest
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            var app = new App();

            app.UseMsTestSettings();
            app.UseControlDataHandlers();
            app.UseBrowserBehavior();
            app.UseLogExecutionBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseVanillaWebDriverScreenshotsOnFail();
            app.UseElementsBddLogging();
            app.UseHighlightElements();
            app.UseValidateExtensionsBddLogging();
            app.UseLayoutAssertionExtensionsBddLogging();
            app.UseMSTestResults();
            app.Initialize();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<App>();
            app.Dispose();
        }
    }
}
