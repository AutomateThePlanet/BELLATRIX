using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
    [TestClass]
    public class TestsInitialize
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            IOSApp.StartAppiumLocalService();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<IOSApp>();
            app?.Dispose();
            app?.StopAppiumLocalService();
        }
    }
}