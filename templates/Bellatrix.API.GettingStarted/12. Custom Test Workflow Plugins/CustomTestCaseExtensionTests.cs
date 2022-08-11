using Bellatrix.API.GettingStarted.Models;
using Bellatrix.API.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.GettingStarted;

[TestClass]
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]
public class CustomTestCaseExtensionTests : APITest
{
    // 1. Once we created the test workflow plugin, we need to add it to the existing test workflow.
    // It is done using the App service's method AddPlugin.
    // It doesn't need to be added multiple times as will happen here with the TestInit method.
    // Usually this is done in the TestsInitialize file in the AssemblyInitialize method.
    //
    //  public static void AssemblyInitialize(TestContext testContext)
    //  {
    //      App.AddPlugin<AssociatedTestCaseExtension>();
    //  }
    public override void TestInit()
    {
        // 2. If you uncomment the bellow code the extension is added and all tests without ManualTestCase attribute will fail.
        // App.AddPlugin<AssociatedTestCaseExtension>();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [ManualTestCase(1532)]
    public void GetAlbumById()
    {
        var request = new RestRequest("api/Albums/10");

        var client = App.GetApiClientService();

        var response = client.Get<Albums>(request);

        Assert.AreEqual(10, response.Data.AlbumId);
    }
}
