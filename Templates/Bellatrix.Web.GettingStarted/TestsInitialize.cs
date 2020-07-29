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
            app.UseControlLocalOverridesCleanBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseFullPageScreenshotsOnFail();
            app.UseLogger();
            app.UseElementsBddLogging();
            app.UseHighlightElements();
            app.UseEnsureExtensionsBddLogging();
            app.UseLayoutAssertionExtensionsBddLogging();
            app.UseExceptionAnalysation();
            app.UseLoadTesting();
            app.Initialize();

            // Software machine automation module helps you to install the required software to the developer's machine
            // such as a specific version of the browsers, browser extensions, and any other required software.
            // You can configure it from BELLATRIX configuration file testFrameworkSettings.json
            //  "machineAutomationSettings": {
            //      "isEnabled": "true",
            //      "packagesToBeInstalled": [ "googlechrome", "firefox --version=65.0.2", "opera" ]
            //  }
            //
            // You need to specify the packages to be installed in the packagesToBeInstalled array. You can search for packages in the
            // public community repository- https://chocolatey.org/
            //
            // To use the service you need to start Visual Studio in Administrative Mode. The service supports currently only Windows.
            // In the future BELLATRIX releases we will support OSX and Linux as well.
            //
            // To use the machine automation setup- uncomment.
            // SoftwareAutomationService.InstallRequiredSoftware();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<App>();
            app?.Dispose();
        }
    }
}