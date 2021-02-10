using Bellatrix.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.GettingStarted
{
    [TestClass]
    public class TestsInitialize
    {
        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<App>();
            app?.Dispose();
        }
    }
}