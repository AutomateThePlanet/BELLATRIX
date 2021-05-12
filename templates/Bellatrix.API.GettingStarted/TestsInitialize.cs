using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Bellatrix.Api;
using Bellatrix.API;
using Bellatrix.Utilities;
using NUnit.Framework;

namespace Bellatrix.GettingStarted
{
    [SetUpFixture]
    public class TestsInitialize
    {
        private static Process _testApiProcess;

        [OneTimeSetUp]
        public void AssemblyInitialize()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string workingDir = Path.Combine(ProcessProvider.GetEntryProcessApplicationPath(), "Demos", "TestAPI");
                _testApiProcess = ProcessProvider.StartProcess("dotnet", workingDir, " run", true);
                ProcessProvider.WaitPortToGetBusy(55215);
            }
        }

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
