using Bellatrix.API.GettingStarted.Models;
using Bellatrix.API.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.GettingStarted;

[TestClass]
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]
public class LoggingTests : APITest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void GetAlbumById()
    {
        var request = new RestRequest("api/Albums/10");

        var client = App.GetApiClientService();

        // Sometimes is useful to add information to the generated test log.
        // To do it you can use the BELLATRIX built-in logger through accessing it via App service.
        Logger.LogInformation("Before GET request. CUSTOM MESSAGE ###");
        var response = client.Get<Albums>(request);

        Assert.AreEqual(10, response.Data.AlbumId);

        // Generated Log, as you can see the above custom message is added to the log.
        // [11:14:08] Start Test
        // [11:14:08] Class = LoggingTests Name = GetAlbumById
        // [11:14:09] Before GET request. CUSTOM MESSAGE ###
        // [11:14:09] Making GET request against resource api/Albums/10
        // [11:14:09] Response of request GET against resource api/Albums/10 - Completed
    }
}
