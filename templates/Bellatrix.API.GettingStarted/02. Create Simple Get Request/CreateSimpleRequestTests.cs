using Bellatrix.API.GettingStarted.Models;
using Bellatrix.API.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.GettingStarted;

// 1. This is the main attribute that you need to mark each class that contains NUnit tests.
[TestClass]
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]

// 2.2. All API BELLATRIX test classes should inherit from the APItest base class. This way you can use all built-in BELLATRIX tools and functionality.
public class CreateSimpleRequestTests : APITest
{
    // 2.3. All NUnit tests should be marked with the Test attribute.
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void GetAlbumById()
    {
        // 2.4. The base URL of your application is set in testFrameworkSettings.json
        // under apiSettings sections- baseUrl
        // For creating a request you need to point the second part of the URL.
        var request = new RestRequest("api/Albums/10");

        // 2.5. Use BELLATRIX App class to get a web client instance. We use it to make requests.
        var client = App.GetApiClientService();

        // 2.6. Use generic Get method to make a GET request. In the <> brackets we place the type of the response.
        // BELLATRIX will automatically convert the JSON or XML response to the specified type.
        var response = client.Get<Albums>(request);

        // 2.7. After you have the response object, you can make all kinds of assertions.
        Assert.AreEqual(10, response.Data.AlbumId);
    }
}
