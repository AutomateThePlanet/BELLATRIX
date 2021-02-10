using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Bellatrix.Api;
using Bellatrix.Utilities;
using NUnit.Framework;

namespace Bellatrix.API.NUnit.Tests
{
    [SetUpFixture]
    public class TestsInitialize
    {
        private static Process _testApiProcess;

        [OneTimeSetUp]
        public void AssemblyInitialize()
        {
            var app = new App();

            app.UseExceptionLogger();
            app.UseNUnitSettings();
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
        }

        // TODO: Remove this code once you use your own web service! It is needed only to run the sample tests.
        [OneTimeTearDown]
        public void AssemblyCleanUp()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ProcessProvider.CloseProcess(_testApiProcess);
            }
        }
    }
}