using Bellatrix.API.GettingStarted.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.GettingStarted
{
    [TestClass]
    public class LoggingTests : APITest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void GetAlbumById()
        {
            var request = new RestRequest("api/Albums/10");

            var client = App.GetApiClientService();

            // 1. Sometimes is useful to add information to the generated test log.
            // To do it you can use the BELLATRIX built-in logger through accessing it via App service.
            App.Logger.LogInformation("Before GET request. CUSTOM MESSAGE ###");
            var response = client.Get<Albums>(request);

            Assert.AreEqual(10, response.Data.AlbumId);

            // 2. In the testFrameworkSettings.json file find a section called logging, responsible for controlling the logs generation.
            //  "logging": {
            //      "isEnabled": "true",
            //      "isConsoleLoggingEnabled": "true",
            //      "isDebugLoggingEnabled": "true",
            //      "isEventLoggingEnabled": "false",
            //      "isFileLoggingEnabled": "true",
            //      "outputTemplate":  "{Message:lj}{NewLine}",
            //      "addUrlToBddLogging": "false"
            //  }
            //
            // You can disable the logs entirely. There are different places where the logs are populated.
            // By default, you can see the logs in the output window of each test.
            // Also, a file called logs.txt is generated in the folder with the DLLs of your tests.
            // If you execute your tests in CI with some CLI test runner the logs are printed there as well.
            // outputTemplate - controls how the message is formatted. You can add additional info such as timestamp and much more.
            // for more info visit- https://github.com/serilog/serilog/wiki/Formatting-Output
            //
            // 3. Generated Log, as you can see the above custom message is added to the log.
            // [11:14:08] Start Test
            // [11:14:08] Class = LoggingTests Name = GetAlbumById
            // [11:14:09] Before GET request. CUSTOM MESSAGE ###
            // [11:14:09] Making GET request against resource api/Albums/10
            // [11:14:09] Response of request GET against resource api/Albums/10 - Completed
        }
    }
}
