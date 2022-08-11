using Bellatrix.Api;
using Bellatrix.API.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Bellatrix.API.GettingStarted;

[TestClass]
[JwtAuthenticationStrategy(GlobalConstants.JwtToken)]
public class ApiLoadTests : APITest
{
    private ApiClientService _apiClientService;

    public override void TestInit()
    {
        FixtureFactory.Create();
        _apiClientService = App.GetApiClientService();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void LoadTest_ExecuteForTime()
    {
        var request = new RestRequest("api/Albums");

        // 1. BELLATRIX offers a module for making load tests.
        // The first type of load tests is time-based.
        // 10 parallel processes will run for 30 seconds, and the engine makes a pause of 1 sec between requests.
        // As an anonymous method or existing method you can specify the code to be executed in each thread.
        App.LoadTestService.ExecuteForTime(
            numberOfProcesses: 10,
            pauseBetweenStartSeconds: 1,
            secondsToBeExecuted: 30,
            testBody: () =>
        {
            var response = _apiClientService.Get(request);

            response.AssertSuccessStatusCode();
            response.AssertExecutionTimeUnder(1);
        });
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void LoadTest_ExecuteNumberOfTimes()
    {
        var request = new RestRequest("api/Albums");

        // 2. The second type of load tests is number-of-times-based
        // Your code executes the specified number of times between the specified number of processes.
        App.LoadTestService.ExecuteNumberOfTimes(5, 1, 5, () =>
        {
            var response = _apiClientService.Get(request);

            response.AssertSuccessStatusCode();
            response.AssertExecutionTimeUnder(1);
        });
    }
}
