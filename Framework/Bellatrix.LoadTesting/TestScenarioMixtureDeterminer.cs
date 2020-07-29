// <copyright file="TestScenarioMixtureDeterminer.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Linq;
using Bellatrix.LoadTesting.Model;

namespace Bellatrix.LoadTesting
{
    public class TestScenarioMixtureDeterminer
    {
        private readonly List<ITestScenarioMixtureDeterminer> _testScenarioMixtureDeterminers;
        private readonly LoadTestSettings _settings;
        private readonly ConcurrentBag<TestScenario> _testScenarios;

        public TestScenarioMixtureDeterminer(List<ITestScenarioMixtureDeterminer> testScenarioMixtureDeterminers, LoadTestSettings settings, ConcurrentBag<TestScenario> testScenarios)
        {
            _testScenarioMixtureDeterminers = testScenarioMixtureDeterminers;
            _settings = settings;
            _testScenarios = testScenarios;
        }

        public ITestScenarioMixtureDeterminer GetTestScenarioMixtureDeterminer()
        {
            var chosenTestScenarioMixtureDeterminer = _testScenarioMixtureDeterminers.First();

            foreach (var testScenarioMixtureDeterminer in _testScenarioMixtureDeterminers)
            {
                if (testScenarioMixtureDeterminer.ShouldUse(_settings))
                {
                    chosenTestScenarioMixtureDeterminer = testScenarioMixtureDeterminer;
                    break;
                }
            }

            chosenTestScenarioMixtureDeterminer.InitializeTestScenarioMixtureDeterminer(_settings, _testScenarios);

            return chosenTestScenarioMixtureDeterminer;
        }
    }
}
