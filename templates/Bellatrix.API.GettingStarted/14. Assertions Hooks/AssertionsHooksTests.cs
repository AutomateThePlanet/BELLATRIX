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
////    public class AssertionsHooksTests : APITest
////    {
////        private ApiClientService _apiClientService;

////        // 1. Another way to extend BELLATRIX is to use the assertions hooks. This is how the BDD logging is implemented.
////        // For example, some of the available hooks are:
////        // AssertExecutionTimeUnderEvent - an event executed before AssertExecutionTimeUnder method
////        // AssertContentContainsEvent - an event executed before AssertContentContains method
////        // AssertContentTypeEvent - an event executed before AssertContentType method
////        //
////        // 2. You need to implement the event handlers for these events and subscribe them.
////        // 3. BELLATRIX gives you again a shortcut- you need to create a class and inherit the AssertExtensionsEventHandlers class
////        // In the example, DebugLogger is called for each assertion event printing to Debug window what the method is verifying.
////        // You can call external logging provider, modify the response, etc. The options are limitless.
////        //
////        // 4. Once you have created the EventHandlers class, you need to tell BELLATRIX to use it. To do so call the App service method
////        // Note: Usually, we add the assertions event handlers in the AssemblyInitialize method which is called once for a test run.
////        public override void TestsArrange()
////        {
////            App.AddAssertionsEventHandler<DebugLoggerAssertExtensions>();

////            // If you need to remove it during the run you can use the method bellow.
////            ////App.RemoveAssertionsEventHandler<DebugLoggerAssertExtensions>();
////        }

////        public override void TestInit()
////        {
////            _apiClientService = App.GetApiClientService();
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public void AssertSuccessStatusCode()
////        {
////            var request = new RestRequest("api/Albums");

////            var response = _apiClientService.Get(request);

////            response.AssertSuccessStatusCode();
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public void AssertStatusCodeOk()
////        {
////            var request = new RestRequest("api/Albums");

////            var response = _apiClientService.Get(request);

////            response.AssertStatusCode(HttpStatusCode.OK);
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public void AssertResponseHeaderServerIsEqualToKestrel()
////        {
////            var request = new RestRequest("api/Albums");

////            var response = _apiClientService.Get(request);

////            response.AssertResponseHeader("server", "Kestrel");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public void AssertExecutionTimeUnderIsUnderOneSecond()
////        {
////            var request = new RestRequest("api/Albums");

////            var response = _apiClientService.Get(request);

////            response.AssertExecutionTimeUnder(1);
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertContentTypeJson()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            response.AssertContentType("application/json; charset=utf-8");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertContentContainsAudioslave()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            response.AssertContentContains("Audioslave");
////        }

////        [TestMethod]
////        [Ignore("API example purposes only. No need to run.")]
////        public async Task AssertContentEncodingUtf8()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            response.AssertContentEncoding("gzip");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertContentEquals()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            response.AssertContentEquals("{\"albumId\":10,\"title\":\"Audioslave\",\"artistId\":8,\"artist\":null,\"tracks\":[]}");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertContentNotContainsRammstein()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            response.AssertContentNotContains("Rammstein");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertContentNotEqualsRammstein()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

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

////            response.AssertCookieExists("whoIs");
////        }

////        [TestMethod]
////        [TestCategory(Categories.CI)]
////        public async Task AssertCookieWhoIsBella()
////        {
////            var request = new RestRequest("api/Albums/10");

////            var response = await _apiClientService.GetAsync<Albums>(request);

////            response.AssertCookie("whoIs", "Bella");
////        }
////    }
////}
