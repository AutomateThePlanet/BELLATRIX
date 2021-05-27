using Bellatrix;
using Bellatrix.Mobile;
using NUnit.Framework;

[SetUpFixture]
public class TestsInitialize
{
    [OneTimeSetUp]
    public static void AssemblyInitialize()
    {
        IOSApp.StartAppiumLocalService();
    }

    [OneTimeTearDown]
    public void AssemblyCleanUp()
    {
        var app = ServicesCollection.Current.Resolve<IOSApp>();
        app?.Dispose();
        app?.StopAppiumLocalService();
    }
}