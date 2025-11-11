global using Assert = Bellatrix.Assertions.Assert;
global using Bellatrix;
global using Bellatrix.Mobile;
global using Bellatrix.Mobile.Android;
global using NUnit.Framework;
////using Microsoft.VisualStudio.TestTools.UnitTesting;


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