using Bellatrix.API.GettingStarted.Models;
using Bellatrix.API.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.GettingStarted;

[TestClass]
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]
public class ApiClientHooksTests : APITest
{
    // 1. Another way to execute BELLATRIX is to create an API client plugin.
    // You can execute your logic in various points such as
    // OnClientInitialized - executed after the API client is initialized. Here you can fine-tune the client.
    // OnRequestTimeout - executed if some of the requests timeout.
    // OnMakingRequest - executed before making request.
    // OnRequestMade - executed after the request is made.
    // OnRequestFailed - executed in case of request failure.
    //
    // To create a custom plugin, you need to derive the class- ApiClientExecutionPlugin
    // Then you can override some of its protected methods. In the example, the request execution time is measured and logged.
    // The plugin needs to be registered through App's method AddApiClientExecutionPlugin. Usually, this happens in the AssemblyInitialize method.
    //
    // You can create plugins for logging the request failures, modifying the requests. The possibilities are limitless.
    public override void TestsArrange() => App.AddApiClientExecutionPlugin<LogRequestTimeApiClientExecutionPlugin>();

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void GetAlbumById()
    {
        var request = new RestRequest("api/Albums/10");

        var client = App.GetApiClientService();

        var response = client.Get<Albums>(request);

        Assert.AreEqual(10, response.Data.AlbumId);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void SecondGetAlbumById()
    {
        var request = new RestRequest("api/Albums/10");

        var client = App.GetApiClientService();

        var response = client.Get<Albums>(request);

        Assert.AreEqual(10, response.Data.AlbumId);
    }
}
