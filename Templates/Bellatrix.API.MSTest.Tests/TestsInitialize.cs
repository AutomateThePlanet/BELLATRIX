using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Bellatrix.Api;
using Bellatrix.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.API.MSTest.Tests
{
    [TestClass]
    public class TestsInitialize
    {
        private static Process _testApiProcess;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            var app = new App();

            app.UseExceptionLogger();
            app.UseMsTestSettings();
            app.UseLogger();
            app.UseExecutionTimeUnderExtensions();
            app.UseApiAuthenticationStrategies();
            app.UseApiExtensionsBddLogging();
            app.UseAssertExtensionsBddLogging();
            app.UseLogExecution();
            app.UseRetryFailedRequests();
            app.Initialize();

            // TODO: Remove this code once you use your own web service! It is needed only to run the sample tests.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string workingDir = Path.Combine(ProcessProvider.GetEntryProcessApplicationPath(), "Demos", "TestAPI");
                _testApiProcess = ProcessProvider.StartProcess("dotnet", workingDir, " run", true);
                ProcessProvider.WaitPortToGetBusy(55215);
            }

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
            // To use the machine automation setup- install Bellatrix.MachineAutomation NuGet package.
            // SoftwareAutomationService.InstallRequiredSoftware();
        }

        // TODO: Remove this code once you use your own web service! It is needed only to run the sample tests.
        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ProcessProvider.CloseProcess(_testApiProcess);
            }
        }
    }
}