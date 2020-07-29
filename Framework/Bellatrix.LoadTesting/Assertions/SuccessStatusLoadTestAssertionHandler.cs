// <copyright file="SuccessStatusLoadTestAssertionHandler.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Api.Contracts;
using Bellatrix.LoadTesting.Model.Results;

namespace Bellatrix.LoadTesting.Model.Assertions
{
    public class SuccessStatusLoadTestAssertionHandler : LoadTestAssertionHandler
    {
        public override List<ResponseAssertionResults> Execute(HttpRequestDto httpRequestDto, IMeasuredResponse response)
        {
            ResponseAssertionResultsCollection.Clear();
            var responseAssertionResults = new ResponseAssertionResults
            {
                AssertionType = "StatusCode is SUCCESS",
                Passed = true,
            };

            if ((int)response.StatusCode <= 200 && (int)response.StatusCode >= 299)
            {
                responseAssertionResults.FailedMessage = $"Request's status code was not successful - {response.StatusCode} {response.ResponseUri}.";
            }

            ResponseAssertionResultsCollection.Add(responseAssertionResults);

            return ResponseAssertionResultsCollection;
        }
    }
}