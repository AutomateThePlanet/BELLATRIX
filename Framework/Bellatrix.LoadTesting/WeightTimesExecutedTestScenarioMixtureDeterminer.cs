// <copyright file="WeightTimesExecutedTestScenarioMixtureDeterminer.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.LoadTesting.Model;

namespace Bellatrix.LoadTesting
{
    public class WeightTimesExecutedTestScenarioMixtureDeterminer : ITestScenarioMixtureDeterminer
    {
        private readonly object _lockObj = string.Empty;
        private int _totalWeight;
        private ConcurrentBag<TestScenario> _testScenarios;

        public TestScenario GetTestScenario()
        {
            lock (_lockObj)
            {
                var rand = new Random();
                int choice = rand.Next(_totalWeight);
                int sum = 0;
                foreach (var testScenario in _testScenarios.OrderBy(x => x.TimesExecuted - x.TimesToBeExecuted))
                {
                    for (int i = sum; i < testScenario.Weight + sum; i++)
                    {
                        if (i >= choice)
                        {
                            return testScenario;
                        }
                    }

                    sum += testScenario.Weight;
                }
            }

            return _testScenarios.First();
        }

        public bool ShouldUse(LoadTestSettings settings) => settings.MixtureMode == MixtureMode.Weights;

        public void InitializeTestScenarioMixtureDeterminer(LoadTestSettings settings, ConcurrentBag<TestScenario> testScenarios)
        {
            _testScenarios = testScenarios;
            _totalWeight = _testScenarios.Sum(c => c.Weight);
        }
    }
}