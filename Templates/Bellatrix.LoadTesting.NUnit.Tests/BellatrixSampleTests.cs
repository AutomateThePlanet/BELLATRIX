using NUnit.Framework;

namespace Bellatrix.LoadTesting.NUnit.Tests
{
    [TestFixture]
    public class BellatrixSampleTests : LoadTest
    {
        public override void TestInit()
        {
        }

        [Test]
        public void MyFirstLoadTest()
        {
            LoadTestEngine.Settings.LoadTestType = LoadTestType.ExecuteForTime;
            LoadTestEngine.Settings.MixtureMode = MixtureMode.Equal;
            LoadTestEngine.Settings.NumberOfProcesses = 5;
            LoadTestEngine.Settings.PauseBetweenStartSeconds = 0;
            LoadTestEngine.Settings.SecondsToBeExecuted = 60;
            LoadTestEngine.Settings.ShouldExecuteRecordedRequestPauses = true;
            LoadTestEngine.Settings.IgnoreUrlRequestsPatterns.Add(".*theming.js.*");
            LoadTestEngine.Settings.IgnoreUrlRequestsPatterns.Add(".*loginHash.*");
            LoadTestEngine.Settings.TestScenariosToBeExecutedPatterns.Add("Bellatrix.Web.Tests.Controls");
            LoadTestEngine.Assertions.AssertAllRequestStatusesAreSuccessful();
            LoadTestEngine.Assertions.AssertAllRecordedEnsureAssertions();
            LoadTestEngine.Execute("loadTestResults.html");
        }
    }
}