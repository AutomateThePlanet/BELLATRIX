// <copyright file="LoadTestEngine.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Api;
using Bellatrix.Configuration;
using Bellatrix.Infrastructure;
using Bellatrix.LoadTesting.Configuration;
using Bellatrix.LoadTesting.Model;
using Bellatrix.LoadTesting.Model.Assertions;
using Bellatrix.LoadTesting.Model.Locators;
using Bellatrix.LoadTesting.Model.Results;
using Bellatrix.LoadTesting.Model.Validates;
using Bellatrix.LoadTesting.Report;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Bellatrix.LoadTesting
{
    public class LoadTestEngine
    {
        private readonly TestScenarioExecutor _testScenarioExecutor;
        private readonly ConcurrentDictionary<string, ConcurrentBag<HttpRequestDto>> _testsRequests;
        private TestScenarioMixtureDeterminer _testScenarioMixtureDeterminer;
        private List<ITestScenarioMixtureDeterminer> _testScenarioMixtureDeterminers;
        private List<LoadTestValidateHandler> _loadTestValidateHandler;
        private ApiClientService _apiClientService;
        private List<LoadTestLocator> _loadTestLocators;
        private ConcurrentBag<TestScenario> _testScenarios;

        public LoadTestEngine()
        {
            _testsRequests = new ConcurrentDictionary<string, ConcurrentBag<HttpRequestDto>>();
            var loadTestAssertionHandlers = new List<LoadTestAssertionHandler>();
            InitializeValidateHandlers();
            InitializeLoadTestLocators();
            Customizations = new LoadTestCustomizations(_loadTestLocators, _loadTestValidateHandler, _testScenarioMixtureDeterminers);
            Assertions = new LoadTestAssertions(loadTestAssertionHandlers, _loadTestLocators, _loadTestValidateHandler);
            Settings = new LoadTestSettings();
            InitializeApiClientService();
            LoadTestsRequestsFromFiles();

            _testScenarioExecutor = new TestScenarioExecutor(loadTestAssertionHandlers, _apiClientService);
        }

        public LoadTestSettings Settings { get; }
        public LoadTestCustomizations Customizations { get; }
        public LoadTestAssertions Assertions { get; }

        public void Execute(string resultsFilePath)
        {
            InitializeTestScenarios();
            InitializeTestScenarioMixtureDeterminers();
            var loadTestExecutionWatch = Stopwatch.StartNew();
            var loadTestRunResults = new LoadTestRunResults();
            var loadTestService = new LoadTestService();
            var testScenarioMixtureDeterminer = _testScenarioMixtureDeterminer.GetTestScenarioMixtureDeterminer();

            if (Settings.LoadTestType == LoadTestType.ExecuteForTime)
            {
                loadTestService.ExecuteForTime(Settings.NumberOfProcesses, Settings.PauseBetweenStartSeconds, Settings.SecondsToBeExecuted, ExecuteTestScenario);
            }
            else if (Settings.LoadTestType == LoadTestType.ExecuteNumberOfTimes)
            {
                loadTestService.ExecuteNumberOfTimes(Settings.NumberOfProcesses, Settings.PauseBetweenStartSeconds, Settings.TimesToBeExecuted, ExecuteTestScenario);
            }

            void ExecuteTestScenario()
            {
                var testScenarioResults = new TestScenarioResults();
                var testScenario = testScenarioMixtureDeterminer.GetTestScenario();

                if (loadTestRunResults.TestScenarioResults.ContainsKey(testScenario.TestName))
                {
                    testScenarioResults = loadTestRunResults.TestScenarioResults[testScenario.TestName];
                }
                else
                {
                    testScenarioResults.TestName = testScenario.TestName;
                    loadTestRunResults.TestScenarioResults.GetOrAdd(testScenario.TestName, testScenarioResults);
                }

                _testScenarioExecutor.Execute(testScenario, testScenarioResults, Settings.ShouldExecuteRecordedRequestPauses, Settings.IgnoreUrlRequestsPatterns);
            }

            loadTestExecutionWatch.Stop();

            loadTestRunResults.TotalExecutionTime = loadTestExecutionWatch.Elapsed;

            var loadTestReportGenerator = new LoadTestReportGenerator();
            loadTestReportGenerator.GenerateReport(loadTestRunResults, resultsFilePath);
        }

        private void InitializeTestScenarioMixtureDeterminers()
        {
            _testScenarioMixtureDeterminers = new List<ITestScenarioMixtureDeterminer>()
                                              {
                                                    new WeightTimesExecutedTestScenarioMixtureDeterminer(),
                                                    new EqualTimesExecutedTestScenarioMixtureDeterminer(),
                                              };

            _testScenarioMixtureDeterminer = new TestScenarioMixtureDeterminer(_testScenarioMixtureDeterminers, Settings, _testScenarios);
        }

        private void InitializeTestScenarios()
        {
            _testScenarios = new ConcurrentBag<TestScenario>();

            foreach (var currentTestName in _testsRequests.Keys)
            {
                if (!ShouldFilterTestScenario(currentTestName))
                {
                    var testScenario = new TestScenario(currentTestName, _testsRequests[currentTestName]);
                    if (Settings.TestScenariosWeights.ContainsKey(currentTestName))
                    {
                        testScenario.Weight = Settings.TestScenariosWeights[currentTestName];
                    }

                    _testScenarios.Add(testScenario);
                }
            }
        }

        private bool ShouldFilterTestScenario(string testScenarioName)
        {
            bool shouldFilter = false;
            if (Settings.TestScenariosToBeExecutedPatterns.Count > 0)
            {
                shouldFilter = true;
                foreach (var currentPattern in Settings.TestScenariosToBeExecutedPatterns)
                {
                    var m = Regex.Match(testScenarioName, currentPattern, RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        shouldFilter = false;
                        break;
                    }
                }
            }
            else
            {
                foreach (var currentPattern in Settings.TestScenariosNotToBeExecutedPatterns)
                {
                    var m = Regex.Match(testScenarioName, currentPattern, RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        shouldFilter = true;
                        break;
                    }
                }
            }

            return shouldFilter;
        }

        private void InitializeValidateHandlers()
        {
            _loadTestValidateHandler = new List<LoadTestValidateHandler>()
                                     {
                                          new ValidatedAcceptIsHandler(),
                                          new ValidatedAcceptIsNullHandler(),
                                          new ValidatedAccessKeyIsHandler(),
                                          new ValidatedAccessKeyIsNullHandler(),
                                          new ValidatedAltIsHandler(),
                                          new ValidatedAltIsNullHandler(),
                                          new ValidatedAutoCompleteIsOffHandler(),
                                          new ValidatedAutoCompleteIsOnHandler(),
                                          new ValidatedColorIsHandler(),
                                          new ValidatedColsIsHandler(),
                                          new ValidatedCssClassIsHandler(),
                                          new ValidatedCssClassIsNullHandler(),
                                          new ValidatedDateIsHandler(),
                                          new ValidatedDirIsHandler(),
                                          new ValidatedDirIsNullHandler(),
                                          new ValidatedDisabledIsHandler(),
                                          new ValidatedEmailIsHandler(),
                                          new ValidatedForIsHandler(),
                                          new ValidatedForIsNullHandler(),
                                          new ValidatedHeightIsHandler(),
                                          new ValidatedHeightIsNotNullHandler(),
                                          new ValidatedHeightIsNullHandler(),
                                          new ValidatedHrefIsHandler(),
                                          new ValidatedHrefIsSetHandler(),
                                          new ValidatedIsCheckedHandler(),
                                          new ValidatedIsNotCheckedHandler(),
                                          new ValidatedIsNotDisabledHandler(),
                                          new ValidatedIsNotSelectedHandler(),
                                          new ValidatedIsNotVisibleHandler(),
                                          new ValidatedIsSelectedHandler(),
                                          new ValidatedIsVisibleHandler(),
                                          new ValidatedLangIsHandler(),
                                          new ValidatedLangIsNullHandler(),
                                          new ValidatedListIsHandler(),
                                          new ValidatedListIsNullHandler(),
                                          new ValidatedLongDescIsHandler(),
                                          new ValidatedLongDescIsNullHandler(),
                                          new ValidatedMaxIsHandler(),
                                          new ValidatedMaxIsNullHandler(),
                                          new ValidatedMaxLengthIsHandler(),
                                          new ValidatedMaxLengthIsNullHandler(),
                                          new ValidatedMinIsHandler(),
                                          new ValidatedMinIsNullHandler(),
                                          new ValidatedMinLengthIsHandler(),
                                          new ValidatedMinLengthIsNullHandler(),
                                          new ValidatedMinTextIsHandler(),
                                          new ValidatedMinTextIsNullHandler(),
                                          new ValidatedMonthIsHandler(),
                                          new ValidatedMultipleIsHandler(),
                                          new ValidatedMultipleIsNullHandler(),
                                          new ValidatedNumberIsHandler(),
                                          new ValidatedPasswordIsHandler(),
                                          new ValidatedPhoneIsHandler(),
                                          new ValidatedPlaceholderIsHandler(),
                                          new ValidatedPlaceholderIsNullHandler(),
                                          new ValidatedRangeIsHandler(),
                                          new ValidatedReadonlyIsHandler(),
                                          new ValidatedReadonlyIsNotHandler(),
                                          new ValidatedRelIsHandler(),
                                          new ValidatedRelIsNullHandler(),
                                          new ValidatedRequiredIsHandler(),
                                          new ValidatedRequiredIsNotHandler(),
                                          new ValidatedRowIsHandler(),
                                          new ValidatedRowIsNullHandler(),
                                          new ValidatedSearchIsHandler(),
                                          new ValidatedSizeIsHandler(),
                                          new ValidatedSizeIsNullHandler(),
                                          new ValidatedSizesIsHandler(),
                                          new ValidatedSizesIsNullHandler(),
                                          new ValidatedSpellCheckIsHandler(),
                                          new ValidatedSpellCheckIsNullHandler(),
                                          new ValidatedSrcIsEventHandler(),
                                          new ValidatedSrcIsNullHandler(),
                                          new ValidatedSrcIsNotNullHandler(),
                                          new ValidatedSrcSetIsHandler(),
                                          new ValidatedSrcSetIsNullHandler(),
                                          new ValidatedStepIsEventHandler(),
                                          new ValidatedStepIsNullHandler(),
                                          new ValidatedStyleIsHandler(),
                                          new ValidatedStyleIsNullHandler(),
                                          new ValidatedTabIndexIsHandler(),
                                          new ValidatedTabIndexIsNullHandler(),
                                          new ValidatedTargetIsHandler(),
                                          new ValidatedTargetIsNullHandler(),
                                          new ValidatedTextIsHandler(),
                                          new ValidatedTextContainsHandler(),
                                          new ValidatedTextIsNullHandler(),
                                          new ValidatedTimeIsHandler(),
                                          new ValidatedTimeIsNullHandler(),
                                          new ValidatedTitleIsHandler(),
                                          new ValidatedTitleIsNullHandler(),
                                          new ValidatedTitleIsNotNullHandler(),
                                          new ValidatedUrlIsHandler(),
                                          new ValidatedValueIsHandler(),
                                          new ValidatedValueIsNullHandler(),
                                          new ValidatedWeekIsHandler(),
                                          new ValidatedWidthIsHandler(),
                                          new ValidatedWidthIsNotNullHandler(),
                                          new ValidatedWidthIsNullHandler(),
                                          new ValidatedWrapIsHandler(),
                                          new ValidatedWrapIsNullHandler(),
                                          new ValidateInnerHtmlIsHandler(),
                                          new ValidatedStyleContainsHandler(),
                                          new ValidatedStyleNotContainsHandler(),
                                          new ValidateInnerTextContainsHandler(),
                                          new ValidateIsVisibleHandler(),
                                          new ValidateInnerTextIsHandler(),
                                          new ValidateIsVisibleHandler(),
                                     };
        }

        private void InitializeLoadTestLocators()
        {
            _loadTestLocators = new List<LoadTestLocator>()
                               {
                                   new ByClassContainingLoadTestLocator(),
                                   new ByClassLoadTestLocator(),
                                   new ByCssLoadTestLocator(),
                                   new ByIdLoadTestLocator(),
                                   new ByIdEndingWithLoadTestLocator(),
                                   new ByIdContainingLoadTestLocator(),
                                   new ByIdEndingWithLoadTestLocator(),
                                   new ByInnerTextContainsLoadTestLocator(),
                                   new ByLinkTextLoadTestLocator(),
                                   new ByLinkTextContainsLoadTestLocator(),
                                   new ByNameLoadTestLocator(),
                                   new ByNameEndingWithLoadTestLocator(),
                                   new ByTagLoadTestLocator(),
                                   new ByValueContainingLoadTestLocator(),
                                   new ByXPathLoadTestLocator(),
                               };
        }

        private void InitializeApiClientService()
        {
            var apiApp = new Api.App();
            _apiClientService = apiApp.GetApiClientService();
            apiApp.ShouldReuseRestClient = true;

            SetCertificates();
        }

        private void SetCertificates()
        {
#pragma warning disable CA5359 // Do Not Disable Certificate Validation
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
#pragma warning restore CA5359 // Do Not Disable Certificate Validation
        }

        private void LoadTestsRequestsFromFiles()
        {
            string allRequestsFilePath = ConfigurationService.GetSection<LoadTestingConfiguration>().RequestsFileLocation.NormalizeAppPath();
            if (!string.IsNullOrEmpty(allRequestsFilePath) || !Directory.Exists(allRequestsFilePath))
            {
                var jsonSerializer = new JsonSerializer();
                string[] allTestRequestFilePaths = Directory.GetFiles(allRequestsFilePath, "*.json", SearchOption.TopDirectoryOnly);
                foreach (var testRequestFilePath in allTestRequestFilePaths)
                {
                    string content = File.ReadAllText(testRequestFilePath);
                    string testName = Path.GetFileName(testRequestFilePath);
                    try
                    {
                        var bagOfHttpRequestDtos = jsonSerializer.Deserialize<ConcurrentBag<HttpRequestDto>>(content);
                        _testsRequests.GetOrAdd(testName, bagOfHttpRequestDtos);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException("There was a problem loading your requests file.", ex);
                    }
                }
            }
            else
            {
                throw new ArgumentException($"The requests files directory was not found- {allRequestsFilePath}");
            }
        }
    }
}