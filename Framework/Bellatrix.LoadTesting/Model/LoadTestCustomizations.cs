// <copyright file="LoadTestCustomizations.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Linq;
using Bellatrix.LoadTesting.Model.Ensures;
using Bellatrix.LoadTesting.Model.Locators;

namespace Bellatrix.LoadTesting.Model
{
    public class LoadTestCustomizations
    {
        private readonly List<LoadTestLocator> _loadTestLocators;
        private readonly List<LoadTestEnsureHandler> _loadTestEnsureHandler;
        private readonly List<ITestScenarioMixtureDeterminer> _testScenarioMixtureDeterminers;

        public LoadTestCustomizations(List<LoadTestLocator> loadTestLocators, List<LoadTestEnsureHandler> loadTestEnsureHandler, List<ITestScenarioMixtureDeterminer> testScenarioMixtureDeterminers)
        {
            _loadTestLocators = loadTestLocators;
            _loadTestEnsureHandler = loadTestEnsureHandler;
            _testScenarioMixtureDeterminers = testScenarioMixtureDeterminers;
        }

        public void AddCustomTestScenarioMixtureDeterminer(ITestScenarioMixtureDeterminer testScenarioMixtureDeterminer)
        {
            if (!_testScenarioMixtureDeterminers.Contains(testScenarioMixtureDeterminer))
            {
                _testScenarioMixtureDeterminers.Add(testScenarioMixtureDeterminer);
            }
        }

        public void AddCustomLoadTestLocator(LoadTestLocator loadTestLocator)
        {
            if (!_loadTestLocators.Any(x => x.LocatorType.Equals(loadTestLocator.LocatorType)))
            {
                _loadTestLocators.Add(loadTestLocator);
            }
        }

        public void AddCustomLoadTestEnsureHandler(LoadTestEnsureHandler loadTestEnsureHandler)
        {
            if (!_loadTestEnsureHandler.Any(x => x.EnsureType.Equals(loadTestEnsureHandler.EnsureType)))
            {
                _loadTestEnsureHandler.Add(loadTestEnsureHandler);
            }
        }
    }
}
