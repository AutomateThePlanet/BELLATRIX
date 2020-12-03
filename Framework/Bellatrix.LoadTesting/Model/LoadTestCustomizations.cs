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
using Bellatrix.LoadTesting.Model.Locators;
using Bellatrix.LoadTesting.Model.Validates;
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.LoadTesting.Model
{
    public class LoadTestCustomizations
    {
        private readonly List<LoadTestLocator> _loadTestLocators;
        private readonly List<LoadTestValidateHandler> _loadTestValidateHandler;
        private readonly List<ITestScenarioMixtureDeterminer> _testScenarioMixtureDeterminers;

        public LoadTestCustomizations(List<LoadTestLocator> loadTestLocators, List<LoadTestValidateHandler> loadTestValidateHandler, List<ITestScenarioMixtureDeterminer> testScenarioMixtureDeterminers)
        {
            _loadTestLocators = loadTestLocators;
            _loadTestValidateHandler = loadTestValidateHandler;
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

        public void AddCustomLoadTestValidateHandler(LoadTestValidateHandler loadTestValidateHandler)
        {
            if (!_loadTestValidateHandler.Any(x => x.ValidateType.Equals(loadTestValidateHandler.ValidateType)))
            {
                _loadTestValidateHandler.Add(loadTestValidateHandler);
            }
        }
    }
}
