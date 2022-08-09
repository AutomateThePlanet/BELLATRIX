using Bellatrix.Api;
using Bellatrix.API.GettingStarted.Models;
using Bellatrix.API.MSTest;
using Bellatrix.Plugins.Common.ExecutionTime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Collections.Generic;

namespace Bellatrix.API.GettingStarted;

[TestClass]
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]

// 1. Sometimes it is useful to use your functional tests to measure performance. Or to just make sure that your app
// is not slow. To do that BELLATRIX libraries offer the ExecutionTimeUnder attribute. You specify a timeout and if the
// test is executed over it the test will fail.
[ExecutionTimeUnder(2)]
public class MeasuredResponseTimesTests : APITest
{
    private ApiClientService _apiClientService;

    public override void TestInit()
    {
        _apiClientService = App.GetApiClientService();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ContentPopulated_When_GetAlbums()
    {
        var request = new RestRequest("api/Albums");

        var response = _apiClientService.Get(request);

        Assert.IsNotNull(response.Content);

        // 2. Another way to measure performance is to use the AssertExecutionTimeUnder method.
        // if the request took longer to execute the test will fail again.
        // You can use the method approach in case the speed of all request is not so important.
        // In all other cases, you can use the global attribute.
        response.AssertExecutionTimeUnder(2);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void DataPopulatedAsList_When_GetGenericAlbums()
    {
        var request = new RestRequest("api/Albums");

        var response = _apiClientService.Get<List<Albums>>(request);

        Assert.AreEqual(347, response.Data.Count);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void DataPopulatedAsList_When_GetGenericAlbumsById()
    {
        var request = new RestRequest("api/Albums/10");

        var response = _apiClientService.Get<Albums>(request);

        Assert.AreEqual(10, response.Data.AlbumId);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void ContentPopulated_When_GetGenericAlbumsById()
    {
        var request = new RestRequest("api/Albums/10");

        var response = _apiClientService.Get<Albums>(request);

        Assert.IsNotNull(response.Content);
    }
}
