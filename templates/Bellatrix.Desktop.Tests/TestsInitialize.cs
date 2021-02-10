////using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Bellatrix.Desktop.MSTest.Tests
{
    [SetUpFixture]
    public class TestsInitialize
    {
        [OneTimeSetUp]
        public void AssemblyInitialize()
        {
            App.StartWinAppDriver();
        }

        [OneTimeTearDown]
        public void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<App>();
            app?.Dispose();
            App.StopWinAppDriver();
        }
    }

    // Uncomment if you want to use MSTest
    ////[TestClass]
    ////public class TestsInitialize
    ////{
    ////    [AssemblyInitialize]
    ////    public static void AssemblyInitialize(TestContext testContext)
    ////    {
    ////        App.StartWinAppDriver();
    ////    }

    ////    [AssemblyCleanup]
    ////    public static void AssemblyCleanUp()
    ////    {
    ////        var app = ServicesCollection.Current.Resolve<App>();
    ////        app?.Dispose();
    ////        App.StopWinAppDriver();
    ////    }
    ////}
}