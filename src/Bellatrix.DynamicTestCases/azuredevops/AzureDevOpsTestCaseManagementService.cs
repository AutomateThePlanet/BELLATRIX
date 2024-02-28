// <copyright file="AzureDevOpsTestCaseManagementService.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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
using Bellatrix.DynamicTestCases;
using Bellatrix.DynamicTestCases.AzureDevOps;
using Bellatrix.DynamicTestCases.Contracts;

namespace Bellatrix.DynamicTestCases.AzureDevOps;

public class AzureDevOpsTestCaseManagementService : ITestCaseManagementService
{
    private readonly AzureTestCasesService _azureTestCasesService;
    public AzureDevOpsTestCaseManagementService()
    {
        _azureTestCasesService = new AzureTestCasesService();
    }

    public TestCase UpdateTestCaseStepAndCreateANewTestCase(TestCasesContext testCasesContext)
    {
        var createdTestCase = new AzureTestCase();
        var existingTestCase = testCasesContext.TestCase != null ? ServicesCollection.Current.Resolve<AzureTestCase>() : null;

        string requirementUrl = string.Empty;
        if (!string.IsNullOrEmpty(testCasesContext.RequirementId))
        {
            requirementUrl = AzureQueryExecutor.GetWorkItems(testCasesContext.RequirementId).First().Url;
        }

        // If it is an existing manual test case- just set the associated automation and the relate the requirements.
        if (testCasesContext.TestCaseId != null)
        {
            existingTestCase = _azureTestCasesService.AssociateAutomationToExistingTestCase(testCasesContext.TestCaseId, testCasesContext.TestFullName, testCasesContext.TestProjectName, requirementUrl);

            return ConvertAzureTestCaseToTestCase(existingTestCase);
        }

        if (existingTestCase == null)
        {
            existingTestCase = _azureTestCasesService.FindTestCasesByAssociatedAutomation(testCasesContext.TestFullName, testCasesContext.TestProjectName).FirstOrDefault();
        }

        if (existingTestCase != null)
        {
            bool shouldUpdate = false;

            // Existing test case was found. Update the test cases if a collection is provided
            if (testCasesContext.TestSteps != null && testCasesContext.TestSteps.Any())
            {
                // Compare the existing test cases and the New ones using Serialization. Update if they are different.
                if (TestStepsService.GenerateTestStepsHtml(testCasesContext.TestSteps) != existingTestCase.TestStepsHtml)
                {
                    existingTestCase.TestStepsHtml = TestStepsService.GenerateTestStepsHtml(testCasesContext.TestSteps);
                    shouldUpdate = true;
                }
            }

            if (testCasesContext.TestCaseName != existingTestCase.Title)
            {
                existingTestCase.Title = testCasesContext.TestCaseName;

                shouldUpdate = true;
            }

            if (requirementUrl != existingTestCase.RequirementUrl)
            {
                existingTestCase.RequirementUrl = requirementUrl;

                shouldUpdate = true;
            }

            if (testCasesContext.TestCaseDescription != existingTestCase.Description ||
                (string.IsNullOrEmpty(testCasesContext.TestCaseDescription) && !string.IsNullOrEmpty(testCasesContext.Precondition)))
            {
                if (!string.IsNullOrEmpty(testCasesContext.Precondition))
                {
                    existingTestCase.Description = $"{testCasesContext.TestCaseDescription}{Environment.NewLine}Precondition:{Environment.NewLine}{testCasesContext.Precondition}";
                }
                else if (!string.IsNullOrEmpty(existingTestCase.Description))
                {
                    existingTestCase.Description = testCasesContext.TestCaseDescription;
                }

                shouldUpdate = true;
            }

            if (shouldUpdate)
            {
                existingTestCase = _azureTestCasesService.UpdateTestCase(existingTestCase);
            }

            createdTestCase = existingTestCase;
        }
        else
        {
            var testCase = new AzureTestCase()
            {
                Title = testCasesContext.TestCaseName,
                Description = testCasesContext.TestCaseDescription,
                AutomatedTestName = testCasesContext.TestFullName,
                AutomatedTestStorage = testCasesContext.TestProjectName,
                TestStepsHtml = TestStepsService.GenerateTestStepsHtml(testCasesContext.TestSteps),
                RequirementUrl = requirementUrl,
                AreaPath = testCasesContext.GetAdditionalPropertyByKey("AreaPath"),
                IterationPath = testCasesContext.GetAdditionalPropertyByKey("IterationPath"),
                Priority = testCasesContext.GetAdditionalPropertyByKey("Priority"),
            };

            if (!string.IsNullOrEmpty(testCasesContext.Precondition))
            {
                testCase.Description = $"{testCasesContext.TestCaseDescription}{Environment.NewLine}Precondition:{Environment.NewLine}{testCasesContext.Precondition}";
            }

            createdTestCase = _azureTestCasesService.CreatTestCase(testCase);
        }

        ServicesCollection.Current.RegisterInstance(createdTestCase);

        return ConvertAzureTestCaseToTestCase(createdTestCase);
    }

    private TestCase ConvertAzureTestCaseToTestCase(AzureTestCase azureTestCase)
    {
        // NOTE: Since in Azure DevOps test steps are used as HTML we don't convert them back.
        var testCase = new TestCase(azureTestCase.Id.ToString(), azureTestCase.Title, azureTestCase.Description, string.Empty);

        return testCase;
    }
}