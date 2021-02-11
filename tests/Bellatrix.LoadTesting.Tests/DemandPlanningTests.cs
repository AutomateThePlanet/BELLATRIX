// <copyright file="DemandPlanningTests.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
// Licensed under the Royalty-free End-user License Agreement, Version 1.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://bellatrix.solutions/licensing-royalty-free/
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.LoadTesting.Tests
{
    [TestClass]
    public class DemandPlanningTests : LoadTest
    {
        public override void TestInit()
        {
        }

        [TestMethod]
        public void NavigateToDemandPlanning()
        {
            LoadTestEngine.Settings.LoadTestType = LoadTestType.ExecuteForTime;
            LoadTestEngine.Settings.MixtureMode = MixtureMode.Equal;
            LoadTestEngine.Settings.NumberOfProcesses = 5;
            LoadTestEngine.Settings.PauseBetweenStartSeconds = 0;
            LoadTestEngine.Settings.SecondsToBeExecuted = 60;
            LoadTestEngine.Settings.ShouldExecuteRecordedRequestPauses = true;
            LoadTestEngine.Settings.IgnoreUrlRequestsPatterns.Add(".*theming.js.*");
            LoadTestEngine.Settings.IgnoreUrlRequestsPatterns.Add(".*loginHash.*");
            LoadTestEngine.Settings.TestScenariosToBeExecutedPatterns.Add("Bellatrix.Web.Tests.Controls.DemandPlanningTests.RunForecast");
            LoadTestEngine.Assertions.AssertAllRequestStatusesAreSuccessful();
            LoadTestEngine.Assertions.AssertAllRecordedEnsureAssertions();
            LoadTestEngine.Execute("loadTestResults.html");
        }
    }
}