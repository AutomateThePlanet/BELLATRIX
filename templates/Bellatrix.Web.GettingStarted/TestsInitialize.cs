using Bellatrix;
using Bellatrix.Web;
using NUnit.Framework;

[SetUpFixture]
public class TestsInitialize
{
    [OneTimeTearDown]
    public void AssemblyCleanUp()
    {
        var app = ServicesCollection.Current.Resolve<App>();
        app?.Dispose();
    }
}