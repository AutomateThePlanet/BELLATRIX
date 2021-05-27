using Bellatrix;
using Bellatrix.Mobile;
using NUnit.Framework;

[SetUpFixture]
public class TestsInitialize
{
    [OneTimeSetUp]
    public void AssemblyInitialize()
    {
        AndroidApp.StartAppiumLocalService();
    }

    [OneTimeTearDown]
    public void AssemblyCleanUp()
    {
        var app = ServicesCollection.Current.Resolve<AndroidApp>();
        app?.Dispose();
        app?.StopAppiumLocalService();
    }
}