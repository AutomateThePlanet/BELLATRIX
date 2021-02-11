using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Desktop.GettingStarted
{
    [TestClass]
    public class TestsInitialize
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            App.StartWinAppDriver();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<App>();
            app?.Dispose();
            App.StopWinAppDriver();
        }
    }
}
