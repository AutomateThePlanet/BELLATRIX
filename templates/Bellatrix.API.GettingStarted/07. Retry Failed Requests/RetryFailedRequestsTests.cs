using Bellatrix.API.GettingStarted.Models;
using Bellatrix.API.MSTest;
using Bellatrix.Plugins.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.GettingStarted;

[TestClass]
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]
// 1. BELLATRIX provides an easy way to retry failed request through the RetryFailedRequests.
// If you place it over you class the rules will be applied to all tests in it.
// Provide how many times your tests to be retried and what should be the pause between retries and the time unit.
[RetryFailedRequests(3, 200, TimeUnit.Milliseconds)]
public class RetryFailedRequestsTests : APITest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void GetAlbumById()
    {
        var request = new RestRequest("api/Albums/10");

        var client = App.GetApiClientService();

        var response = client.Get<Albums>(request);

        Assert.AreEqual(10, response.Data.AlbumId);
    }
}
