﻿// <copyright file="ValidatedSpellCheckIsHandler.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.LoadTesting.Model.Results;

namespace Bellatrix.LoadTesting.Model.Validates
{
    public class ValidatedSpellCheckIsHandler : LoadTestValidateHandler
    {
        public override string ValidateType => "ValidatedSpellCheckIs";

        public override ResponseAssertionResults Execute(LoadTestElement loadTestElement, string expectedValue)
        {
            var responseAssertionResults = new ResponseAssertionResults();
            if (loadTestElement.HtmlNode.GetAttributeValue("spellcheck", string.Empty) != expectedValue)
            {
                responseAssertionResults.AssertionType = $"{ValidateType}- {loadTestElement.Locator}={loadTestElement.LocatorValue} Expected = {expectedValue}";
                responseAssertionResults.Passed = false;
                responseAssertionResults.FailedMessage = $"Element with locator {loadTestElement.Locator}={loadTestElement.LocatorValue} spellcheck wasn't equal to {expectedValue}.";
            }

            return responseAssertionResults;
        }
    }
}