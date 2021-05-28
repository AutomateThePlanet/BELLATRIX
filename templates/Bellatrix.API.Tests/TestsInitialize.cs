using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Bellatrix.Utilities;
////using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

[SetUpFixture]
public class TestsInitialize
{
    private static Process _testApiProcess;

    [OneTimeSetUp]
    public void AssemblyInitialize()
    {
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

    // Uncomment if you want to use MSTest
    ////[TestClass]
    ////public class TestsInitialize
    ////{
    ////    private static Process _testApiProcess;

    ////    [AssemblyInitialize]
    ////    public static void AssemblyInitialize(TestContext testContext)
    ////    {
    ////        // TODO: Remove this code once you use your own web service! It is needed only to run the sample tests.
    ////        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    ////        {
    ////            string workingDir = Path.Combine(ProcessProvider.GetEntryProcessApplicationPath(), "Demos", "TestAPI");
    ////            _testApiProcess = ProcessProvider.StartProcess("dotnet", workingDir, " run", true);
    ////            ProcessProvider.WaitPortToGetBusy(55215);
    ////        }
    ////    }

    ////    // TODO: Remove this code once you use your own web service! It is needed only to run the sample tests.
    ////    [AssemblyCleanup]
    ////    public static void AssemblyCleanUp()
    ////    {
    ////        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    ////        {
    ////            ProcessProvider.CloseProcess(_testApiProcess);
    ////        }
    ////    }
    ////}
}