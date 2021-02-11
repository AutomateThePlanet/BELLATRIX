using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.LoadTesting.Tests
{
    [TestClass]
    public class LoadTestDemo : LoadTest
    {
        public override void TestInit()
        {
        }

        [TestMethod]
        public void PurchasingDemoWebSiteLoadTest()
        {
            LoadTestEngine.Settings.LoadTestType = LoadTestType.ExecuteForTime;
            LoadTestEngine.Settings.MixtureMode = MixtureMode.Equal;
            LoadTestEngine.Settings.NumberOfProcesses = 5;
            LoadTestEngine.Settings.PauseBetweenStartSeconds = 0;
            LoadTestEngine.Settings.SecondsToBeExecuted = 60;
            LoadTestEngine.Settings.ShouldExecuteRecordedRequestPauses = true;
            LoadTestEngine.Settings.IgnoreUrlRequestsPatterns.Add(".*theming.js.*");
            LoadTestEngine.Settings.IgnoreUrlRequestsPatterns.Add(".*loginHash.*");
            LoadTestEngine.Settings.TestScenariosToBeExecutedPatterns.Add("Bellatrix.Web.GettingStarted.LoadTestingReuseWebTests.PurchaseRocketWithoutPageObjects");
            LoadTestEngine.Settings.TestScenariosToBeExecutedPatterns.Add("Bellatrix.Web.GettingStarted.LoadTestingReuseWebTests.PurchaseRocketWithPageObjects");
            LoadTestEngine.Assertions.AssertAllRequestStatusesAreSuccessful();
            LoadTestEngine.Assertions.AssertAllRecordedEnsureAssertions();
            LoadTestEngine.Execute("loadTestResults.html");
        }
    }
}