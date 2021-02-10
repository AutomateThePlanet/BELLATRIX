using System;
using NUnit.Framework;

namespace Bellatrix.Web.NUnit.Tests
{
    [SetUpFixture]
    public class TestsInitialize
    {
        [OneTimeSetUp]
        public void AssemblyInitialize()
        {
            var app = new App();

            app.UseExceptionLogger();
            app.UseNUnitSettings();
            app.UseControlDataHandlers();
            app.UseBrowserBehavior();
            app.UseLogExecutionBehavior();
            app.UseNUnitResults();
            app.UseFFmpegVideoRecorder();
            app.UseVanillaWebDriverScreenshotsOnFail();
            app.UseElementsBddLogging();
            app.UseHighlightElements();
            app.UseValidateExtensionsBddLogging();
            app.UseLayoutAssertionExtensionsBddLogging();
            ////app.AssemblyInitialize();
            app.Initialize();
        }

        [OneTimeTearDown]
        public void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<App>();
            app?.Dispose();
        }
    }
}