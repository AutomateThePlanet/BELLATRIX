using Bellatrix.Logging;
using Bellatrix.Mobile;
using Bellatrix.Mobile.IOS;
using Bellatrix.Mobile.TestExecutionExtensions;
using NUnit.Framework;

namespace Bellatrix.Web.NUnit.Tests
{
    [SetUpFixture]
    public class TestsInitialize
    {
        [OneTimeSetUpAttribute]
        public void AssemblyInitialize()
        {
            var app = new AndroidApp();

            app.UseExceptionLogger();
            app.UseNUnitSettings();
            app.UseAppBehavior();
            app.UseLogExecutionBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseIOSDriverScreenshotsOnFail();
            app.UseElementsBddLogging();
            app.UseValidateExtensionsBddLogging();
            app.UseLayoutAssertionExtensionsBddLogging();
            app.StartAppiumLocalService();
            app.Initialize();
        }

        [OneTimeTearDown]
        public void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<AndroidApp>();
            app?.Dispose();
            app?.StopAppiumLocalService();
        }
    }
}
