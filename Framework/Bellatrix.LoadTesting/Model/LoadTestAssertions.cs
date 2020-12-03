// <copyright file="LoadTestAssertions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.LoadTesting.Model.Assertions;
using Bellatrix.LoadTesting.Model.Locators;
using Bellatrix.LoadTesting.Model.Validates;
using System.Collections.Generic;

namespace Bellatrix.LoadTesting.Model
{
    public class LoadTestAssertions
    {
        private readonly List<LoadTestAssertionHandler> _loadTestAssertionHandlers;
        private readonly List<LoadTestLocator> _loadTestLocators;
        private readonly List<LoadTestValidateHandler> _loadTestValidateHandler;

        public LoadTestAssertions(List<LoadTestAssertionHandler> loadTestAssertionHandlers, List<LoadTestLocator> loadTestLocators, List<LoadTestValidateHandler> loadTestValidateHandler)
        {
            _loadTestAssertionHandlers = loadTestAssertionHandlers;
            _loadTestLocators = loadTestLocators;
            _loadTestValidateHandler = loadTestValidateHandler;
        }

        public void AssertAllRequestStatusesAreSuccessful()
        {
            _loadTestAssertionHandlers.Add(new SuccessStatusLoadTestAssertionHandler());
        }

        public void AssertAllRecordedValidateAssertions()
        {
            _loadTestAssertionHandlers.Add(new ValidatesLoadTestAssertionHandler(_loadTestLocators, _loadTestValidateHandler));
        }
    }
}
