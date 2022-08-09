using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Bellatrix.Api;
using Bellatrix.API;
using Bellatrix.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.GettingStarted;

[TestClass]
public class TestsInitialize
{
    private static Process _testApiProcess;

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext testContext)
    {
        string workingDir = Path.Combine(ProcessProvider.GetEntryProcessApplicationPath(), "Demos", "TestAPI");
        _testApiProcess = ProcessProvider.StartProcess("dotnet", workingDir, " run", true);
        ProcessProvider.WaitPortToGetBusy(55215);
    }

    [AssemblyCleanup]
    public static void AssemblyCleanUp()
    {
        ProcessProvider.CloseProcess(_testApiProcess);
    }
}
