using Bellatrix.Web;
using NUnit.Framework;

namespace Bellatrix.GettingStarted
{
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
}