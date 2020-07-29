// <copyright file="EnsuresLoadTestAssertionHandler.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Api.Contracts;
using Bellatrix.LoadTesting.Model.Ensures;
using Bellatrix.LoadTesting.Model.Locators;
using Bellatrix.LoadTesting.Model.Results;
using HtmlAgilityPack;

namespace Bellatrix.LoadTesting.Model.Assertions
{
    public class EnsuresLoadTestAssertionHandler : LoadTestAssertionHandler
    {
        private readonly List<LoadTestLocator> _loadTestLocators;
        private readonly List<LoadTestEnsureHandler> _loadTestEnsureHandler;

        public EnsuresLoadTestAssertionHandler(List<LoadTestLocator> loadTestLocators, List<LoadTestEnsureHandler> loadTestEnsureHandler)
        {
            _loadTestLocators = loadTestLocators;
            _loadTestEnsureHandler = loadTestEnsureHandler;
        }

        public override List<ResponseAssertionResults> Execute(HttpRequestDto httpRequestDto, IMeasuredResponse response)
        {
            ResponseAssertionResultsCollection.Clear();
            if (httpRequestDto.ResponseAssertions.Count > 0)
            {
                foreach (var responseAssertion in httpRequestDto.ResponseAssertions)
                {
                    var responseAssertionResults = new ResponseAssertionResults
                    {
                        AssertionType = responseAssertion.ToString(),
                    };
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(response.Content);
                    if (_loadTestLocators.Any(x => x.LocatorType.Equals(responseAssertion.Locator)))
                    {
                        var currentLocator = _loadTestLocators.First(x => x.LocatorType.Equals(responseAssertion.Locator));
                        try
                        {
                            LoadTestElement htmlElement = currentLocator.LocateElement(htmlDocument, responseAssertion.LocatorValue);
                            if (_loadTestEnsureHandler.Any(x => x.EnsureType.Equals(responseAssertion.AssertionType)))
                            {
                                var currentEnsureHandler = _loadTestEnsureHandler.First(x => x.EnsureType.Equals(responseAssertion.AssertionType));
                                responseAssertionResults = currentEnsureHandler.Execute(htmlElement, responseAssertion.ExpectedValue);
                            }
                            else
                            {
                                responseAssertionResults.FailedMessage = $"AssertionType {responseAssertion.AssertionType} is not supported.";
                                responseAssertionResults.Passed = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            responseAssertionResults.FailedMessage = ex.Message;
                            responseAssertionResults.Passed = false;
                        }
                    }
                    else
                    {
                        responseAssertionResults.FailedMessage = $"Locator {responseAssertion.Locator} is not supported.";
                        responseAssertionResults.Passed = false;
                    }

                    ResponseAssertionResultsCollection.Add(responseAssertionResults);
                }
            }

            return ResponseAssertionResultsCollection;
        }
    }
}