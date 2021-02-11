using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    public class TestsInitialize
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            AndroidApp.StartAppiumLocalService();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<AndroidApp>();
            app?.Dispose();
            app?.StopAppiumLocalService();
        }
    }
}