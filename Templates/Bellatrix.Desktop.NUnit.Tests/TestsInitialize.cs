using NUnit.Framework;

namespace Bellatrix.Desktop.NUnit.Tests
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
            app.UseAppBehavior();
            app.UseLogExecutionBehavior();
            app.UseLogExecutionBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseVanillaWebDriverScreenshotsOnFail();
            app.UseElementsBddLogging();
            app.UseValidateExtensionsBddLogging();
            app.UseLayoutAssertionExtensionsBddLogging();
            app.StartWinAppDriver();
            app.Initialize();
        }

        [OneTimeTearDown]
        public void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<App>();
            app?.Dispose();
            app?.StopWinAppDriver();
        }
    }
}