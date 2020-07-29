// <copyright file="TestScenarioResults.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Bellatrix.LoadTesting.Model.Results
{
    public class TestScenarioResults
    {
        public TestScenarioResults() => TestScenarioRunResults = new ConcurrentDictionary<Guid, TestScenarioRunResult>();

        public string TestName { get; set; }
        public double AverageExecutionTimeSeconds => TestScenarioRunResults.Values.Average(x => x.ExecutionTime.TotalSeconds);
        public double MaxExecutionTimeSeconds => TestScenarioRunResults.Values.Max(x => x.ExecutionTime.TotalSeconds);
        public double MinExecutionTimeSeconds => TestScenarioRunResults.Values.Min(x => x.ExecutionTime.TotalSeconds);
        public int TimesExecuted { get; set; }
        public int TimesFailed { get; set; }
        public int TimesPassed { get; set; }
        public int TotalAssertionsCount => TestScenarioRunResults.Values.Sum(x => x.TotalAssertionsCount);
        public int FailedAssertionsCount => TestScenarioRunResults.Values.Sum(x => x.FailedAssertionsCount);
        public int PassedAssertionsCount => TestScenarioRunResults.Values.Sum(x => x.PassedAssertionsCount);
        public int Weight { get; set; }

        public ConcurrentDictionary<Guid, TestScenarioRunResult> TestScenarioRunResults { get; set; }
    }
}
