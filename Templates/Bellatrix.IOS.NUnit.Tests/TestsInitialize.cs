using Bellatrix.Mobile;
using Bellatrix.Mobile.IOS;
using NUnit.Framework;

namespace Bellatrix.Web.NUnit.Tests
{
    [SetUpFixture]
    public class TestsInitialize
    {
        [OneTimeSetUpAttribute]
        public void AssemblyInitialize()
        {
            var app = new IOSApp();
            app.UseExceptionLogger();
            app.UseNUnitSettings();
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

        [OneTimeTearDown]
        public void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<IOSApp>();
            app?.Dispose();
            app?.StopAppiumLocalService();
        }
    }
}
