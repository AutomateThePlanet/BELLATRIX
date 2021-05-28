////using Bellatrix.Api;
////using Bellatrix.API.GettingStarted.Models;
////using Bellatrix.API.MSTest;
////using Microsoft.VisualStudio.TestTools.UnitTesting;
////using RestSharp;
////using System.Net;
////using System.Threading.Tasks;

////namespace Bellatrix.API.GettingStarted
////{
////    [TestClass]
////    public class ApiAssertionsTests : APITest
////    {
////        private ApiClientService _apiClientService;

////        public override void TestInit()
////        {
////            _apiClientService = App.GetApiClientService();
////        }

////        // 0. BELLATRIX API library brings many convenient assertion methods on top of RestSharp.
////        // Of course, you can write similar methods yourself using MSTest or NUnit.
////        // All BELLATRIX assertions comes with full BDD logging and extensibility hooks.
////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public void AssertSuccessStatusCode()
////        {
////            var request = new RestRequest("api/Albums");

////            var response = _apiClientService.Get(request);

////            // 1. Assert that the status code is successful.
////            response.AssertSuccessStatusCode();
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public void AssertStatusCodeOk()
////        {
////            var request = new RestRequest("api/Albums");

////            var response = _apiClientService.Get(request);

////            // 2. Assert that the status code is OK.
////            response.AssertStatusCode(HttpStatusCode.OK);
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public void AssertResponseHeaderServerIsEqualToKestrel()
////        {
////            var request = new RestRequest("api/Albums");

////            var response = _apiClientService.Get(request);

////            // 3. Assert that the header named 'server' has the value 'Kestrel'.
////            response.AssertResponseHeader("server", "Kestrel");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public void AssertExecutionTimeUnderIsUnderOneSecond()
////        {
////            var request = new RestRequest("api/Albums");

////            var response = _apiClientService.Get(request);

////            // 4. Assert that the execution time of the GET request is under 1 second.
////            response.AssertExecutionTimeUnder(1);
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertContentTypeJson()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            // 5. Assert that the content type is of the specified type.
////            response.AssertContentType("application/json; charset=utf-8");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertContentContainsAudioslave()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            // 6. Assert that the native text content (JSON or XML) contains the specified value.
////            response.AssertContentContains("Audioslave");
////        }

////        [TestMethod]
////        [Ignore("API example purposes only. No need to run.")]
////        public async Task AssertContentEncodingUtf8()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            // 7. Assert the response's content encoding.
////            response.AssertContentEncoding("gzip");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertContentEquals()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            // 8. Assert the native text content.
////            response.AssertContentEquals("{\"albumId\":10,\"title\":\"Audioslave\",\"artistId\":8,\"artist\":null,\"tracks\":[]}");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertContentNotContainsRammstein()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            // 9. Assert that the native text content doesn't contain specific text.
////            response.AssertContentNotContains("Rammstein");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertContentNotEqualsRammstein()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            // 10. Assert that the native text content is not equal to a specific text.
////            response.AssertContentNotEquals("Rammstein");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertResultEquals()
////        {
////            var expectedAlbum = new Albums
////                                {
////                                    AlbumId = 10,
////                                };
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            // 11. Assert C# collections directly.
////            response.AssertResultEquals(expectedAlbum);
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertResultNotEquals()
////        {
////            var expectedAlbum = new Albums
////                                {
////                                    AlbumId = 11,
////                                };
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            response.AssertResultNotEquals(expectedAlbum);
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertCookieExists()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            // 12. Assert that a specific cookie exists.
////            response.AssertCookieExists("whoIs");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertCookieWhoIsBella()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            // 13. Assert that a cookie's value is equal to a specific value.
////            response.AssertCookie("whoIs", "Bella");
////        }

////        [TestMethod]
////        public async Task AssertMultiple()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            // 14. You can execute multiple assertions failing only once viewing all results.
////            Bellatrix.Assertions.Assert.Multiple(
////                () => response.AssertCookie("whoIs", "Bella"),
////                () => response.AssertCookieExists("whoIs"));
////        }
////    }
////}
