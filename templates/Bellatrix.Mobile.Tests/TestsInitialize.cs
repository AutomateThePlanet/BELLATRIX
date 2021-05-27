using Bellatrix;
using Bellatrix.Mobile;
using Bellatrix.Mobile.Android;
////using Microsoft.VisualStudio.TestTools.UnitTesting;
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

// Uncomment if you want to use MSTest
////[TestClass]
////public class TestsInitialize
////{
////    [AssemblyInitialize]
////    public static void AssemblyInitialize(TestContext testContext)
////    {
////        AndroidApp.StartAppiumLocalService();
////    }

////    [AssemblyCleanup]
////    public static void AssemblyCleanUp()
////    {
////        var app = ServicesCollection.Current.Resolve<AndroidApp>();
////        app?.Dispose();
////        App.StopAppiumLocalService();
////    }
////}