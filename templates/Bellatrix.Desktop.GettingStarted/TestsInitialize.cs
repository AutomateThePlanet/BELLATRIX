global using Assert = Bellatrix.Assertions.Assert;
global using Bellatrix;
global using Bellatrix.Desktop;
global using NUnit.Framework;

[SetUpFixture]
public class TestsInitialize
{
    [OneTimeSetUp]
    public static void AssemblyInitialize()
    {
        ////App.StartWinAppDriver();
    }

    [OneTimeTearDown]
    public void AssemblyCleanUp()
    {
        var app = ServicesCollection.Current.Resolve<App>();
        app?.Dispose();
        ////App.StopWinAppDriver();
    }
}