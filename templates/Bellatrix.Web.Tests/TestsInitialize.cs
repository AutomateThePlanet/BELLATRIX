////using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Bellatrix.Web.Tests
{
    ////[TestClass]
    [SetUpFixture]
    public class TestsInitialize
    {
        // Uncomment if you want to use MSTest
        ////[AssemblyCleanup]
        [OneTimeTearDown]
        public static void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<App>();
            app.Dispose();
        }
    }
}
