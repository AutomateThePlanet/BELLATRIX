// <copyright file="EqualTimesExecutedTestScenarioMixtureDeterminer.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Concurrent;
using System.Linq;
using Bellatrix.LoadTesting.Model;

namespace Bellatrix.LoadTesting
{
    public class EqualTimesExecutedTestScenarioMixtureDeterminer : ITestScenarioMixtureDeterminer
    {
        private readonly object _lockObj = string.Empty;
        private ConcurrentBag<TestScenario> _testScenarios;

        public TestScenario GetTestScenario()
        {
            var testScenario = default(TestScenario);
            lock (_lockObj)
            {
                testScenario = _testScenarios.OrderBy(x => x.TimesExecuted - x.TimesToBeExecuted).First();
                testScenario.TimesExecuted++;
            }

            return testScenario;
        }

        public bool ShouldUse(LoadTestSettings settings) => settings.MixtureMode == MixtureMode.Equal;

        public void InitializeTestScenarioMixtureDeterminer(LoadTestSettings settings, ConcurrentBag<TestScenario> testScenarios)
        {
            _testScenarios = testScenarios;
            if (settings.LoadTestType == LoadTestType.ExecuteForTime)
            {
                foreach (var testScenario in _testScenarios)
                {
                    testScenario.TimesToBeExecuted = int.MaxValue;
                }
            }
        }
    }
}