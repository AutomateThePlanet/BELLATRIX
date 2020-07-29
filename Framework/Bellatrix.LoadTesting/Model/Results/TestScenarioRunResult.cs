// <copyright file="TestScenarioRunResult.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.LoadTesting.Model.Results
{
    public class TestScenarioRunResult
    {
        public TestScenarioRunResult()
        {
            RequestResults = new List<RequestResults>();
            RunId = Guid.NewGuid();
        }

        public List<RequestResults> RequestResults { get; set; }
        public Guid RunId { get; set; }
        public bool Passed => HaveAllResponseAssertionResultsPassed() && RequestResults.Any(x => !x.IsSuccessful);
        public TimeSpan ExecutionTime { get; set; }
        public int TotalAssertionsCount => GetTotalAssertionsCount();
        public int FailedAssertionsCount => GetFailedAssertionsCount();
        public int PassedAssertionsCount => GetPassedAssertionsCount();

        private int GetTotalAssertionsCount()
        {
            int totalAssertionsCount = 0;
            foreach (var requestResults in RequestResults)
            {
                totalAssertionsCount += requestResults.ResponseAssertionResults.Count;
            }

            return totalAssertionsCount;
        }

        private int GetFailedAssertionsCount()
        {
            int totalAssertionsCount = 0;
            foreach (var requestResults in RequestResults)
            {
                totalAssertionsCount += requestResults.ResponseAssertionResults.Count(x => x != null && !x.Passed);
            }

            return totalAssertionsCount;
        }

        private int GetPassedAssertionsCount()
        {
            int totalAssertionsCount = 0;
            foreach (var requestResults in RequestResults)
            {
                totalAssertionsCount += requestResults.ResponseAssertionResults.Count(x => x != null && x.Passed);
            }

            return totalAssertionsCount;
        }

        private bool HaveAllResponseAssertionResultsPassed()
        {
            foreach (var requestResults in RequestResults)
            {
                if (requestResults.ResponseAssertionResults.Any(x => x != null && !x.Passed))
                {
                    return false;
                }
            }

            return true;
        }
    }
}