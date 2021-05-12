using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted
{
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
}
