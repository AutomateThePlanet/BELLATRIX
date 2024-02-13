// <copyright file="QTestTestCaseManagementService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.DynamicTestCases.Contracts;
using Bellatrix.KeyVault;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DTC = Bellatrix.DynamicTestCases;
using QT = QASymphony.QTest;

namespace Bellatrix.DynamicTestCases.QTest;

/// <summary>
/// Class used for communication with qTest.
/// Contains all logic needed for authentication, init and update.
/// </summary>
public class QTestTestCaseManagementService : ITestCaseManagementService
{
    private QT.TestDesignService _testDesignService;
    private QT.ProjectService _projectService;

    public QTestTestCaseManagementService()
    {
        // we must login first to be able to make any request to the server
        // or we will get 401 error
        try
        {
            var serviceAddress = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<QTestDynamicTestCasesSettings>().ServiceAddress);
            LoginToService(_testDesignService = new QT.TestDesignService(serviceAddress));
            LoginToService(_projectService = new QT.ProjectService(serviceAddress));
        }
        catch (Exception ex)
        {
            Logger.LogError($"qTest Login was unsuccesful, {ex.Message}");
        }
    }

    public DTC.TestCase UpdateTestCaseStepAndCreateANewTestCase(TestCasesContext testCasesContext)
    {
        QT.ServiceResponse<QT.TestCase> result = default;
        QT.TestCase qTestCreatedTestCase = new QT.TestCase();
        QT.TestCase qTestExistingTestCase = testCasesContext.TestCase != null ? ServicesCollection.Current.Resolve<QT.TestCase>() : null;

        if (string.IsNullOrEmpty(testCasesContext.SuiteId))
        {
            throw new ArgumentException("You must specify a qTest SuiteId.");
        }

        // Generate the identification token placed in the beginning of the description.
        string formattedAutomationId = $"#QE-{testCasesContext.TestCaseId}";
        if (!string.IsNullOrEmpty(testCasesContext.TestCaseDescription))
        {
            testCasesContext.TestCaseDescription = $"{formattedAutomationId}-{testCasesContext.TestFullName} \r\n{testCasesContext.TestCaseDescription}";
        }
        else
        {
            testCasesContext.TestCaseDescription = $"{formattedAutomationId}-{testCasesContext.TestFullName} \r\n";
        }

        long projectId = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<QTestDynamicTestCasesSettings>().ProjectId).ToLong();
        if (!string.IsNullOrEmpty(testCasesContext.TestCaseId))
        {
            // Check if a test case with this ID exists in qTest
            if (qTestExistingTestCase == null)
            {
                qTestExistingTestCase = _testDesignService.ListTestCase(projectId, testCasesContext.SuiteId.ToLong(), expandSteps: true).Data?.FindLast(tcase => tcase.Description.Trim().StartsWith(formattedAutomationId));
            }

            if (qTestExistingTestCase != null)
            {
                // Existing test case was found. Update the test cases if a collection is provided
                if (testCasesContext.TestSteps != null && testCasesContext.TestSteps.Any())
                {
                    QT.TestCase updateCase = new QT.TestCase();

                    bool shouldUpdate = false;

                    // Compare the existing test cases and the New ones using Serialization. Update if they are different.
                    if (testCasesContext.TestSteps.Select(x => x.Description).Stringify() != qTestExistingTestCase.TestSteps.Select(x => x.Description).Stringify())
                    {
                        updateCase.TestSteps = new List<QT.TestStep>();
                        foreach (var testStep in testCasesContext.TestSteps)
                        {
                            string description = IsValidJson(testStep.Description) ? testStep.Description : JsonConvert.ToString(testStep.Description).Trim('"');
                            string expected = IsValidJson(testStep.Expected) ? testStep.Expected : JsonConvert.ToString(testStep.Expected).Trim('"');
                            updateCase.TestSteps.Add(new QT.TestStep() { Description = description, Expected = expected });
                        }

                        shouldUpdate = true;
                    }

                    // Set precondition
                    if (testCasesContext.Precondition != qTestExistingTestCase.Precondition)
                    {
                        updateCase.Precondition = testCasesContext.Precondition;
                        shouldUpdate = true;
                    }

                    // Update using the SDK if diffs are found
                    if (shouldUpdate)
                    {
                        result = _testDesignService.UpdateTestCase(projectId, qTestExistingTestCase.Id, updateCase);
                    }
                }

                qTestCreatedTestCase = qTestExistingTestCase;
            }
            else
            {
                // Create brand-new test case
                // The specific here is that we need to create a very basic test case, and then update it with the details
                QT.TestCase testCase = new QT.TestCase()
                {
                    Name = testCasesContext.TestCaseName,
                    ParentId = testCasesContext.SuiteId.ToLong(),
                    Description = testCasesContext.TestCaseDescription,
                };

                // Call SDK testDesign service
                result = _testDesignService.CreateTestCase(projectId, testCase);

                if (result.Data != null)
                {
                    result = UpdateBasicTestCase(result.Data.Id, testCasesContext.Precondition);
                }
            }
        }

        if (result != null && !result.IsSuccess)
        {
            throw new InvalidOperationException(string.Format("Cannnot create test case [{0}] - {1}", result.Error.Code, result.Error.Message));
        }
        else if (result != null)
        {
            qTestCreatedTestCase = result.Data;
        }

        // Set requirement relation once the test case has been created
        if (!string.IsNullOrEmpty(testCasesContext.RequirementId))
        {
            IList<long> testCaseList = new List<long> { qTestCreatedTestCase.Id };
            var requirementResponse = _projectService.LinkTestCaseRequirement(projectId, new QT.LinkTestCaseRequirement() { RequirementId = testCasesContext.RequirementId.ToLong(), TestCases = testCaseList });
            if (requirementResponse != null && !requirementResponse.IsSuccess)
            {
                throw new InvalidOperationException($"Cannnot link test case {qTestCreatedTestCase.Id} to requirement {testCasesContext.RequirementId}. {requirementResponse.Error}");
            }
        }

        ServicesCollection.Current.RegisterInstance(qTestCreatedTestCase);

        TestCase createdTestCase = new TestCase(testCasesContext.TestCaseId, qTestCreatedTestCase.Name, qTestCreatedTestCase.Description, qTestCreatedTestCase.Precondition);
        createdTestCase.TestSteps = testCasesContext.TestSteps;

        return createdTestCase;
    }

    private QT.ServiceResponse<QT.TestCase> UpdateBasicTestCase(long? testCaseId, string precondition)
    {
        QT.ServiceResponse<QT.TestCase> result;
        QT.TestCase existingTestCase = new QT.TestCase();
        existingTestCase.Precondition = precondition;
        existingTestCase.Properties = ConfigurationService.GetSection<QTestDynamicTestCasesSettings>().FieldValues;
        long projectId = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<QTestDynamicTestCasesSettings>().ProjectId).ToLong();
        result = _testDesignService.UpdateTestCase(projectId, (long)testCaseId, existingTestCase);

        return result;
    }

    private void LoginToService(QT.QTestService serviceToLogin)
    {
        string token = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<QTestDynamicTestCasesSettings>().Token);
        if (!string.IsNullOrEmpty(token))
        {
            serviceToLogin.SetAuthorizationToken(token);
        }

        string userName = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<QTestDynamicTestCasesSettings>().UserName);
        string password = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<QTestDynamicTestCasesSettings>().Password);

        if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
        {
            serviceToLogin.SetUserCredentials(userName, password);
        }

        if (string.IsNullOrEmpty(token) && string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException("Please set qTest token or username & password in the testFrameworkSettings.json");
        }

        QT.ServiceResponse<string> response = serviceToLogin.Login();
        if (!response.IsSuccess)
        {
            throw new InvalidOperationException($"Unable to login to service: {serviceToLogin.GetType()}");
        }
    }

    private bool IsValidJson(string strInput)
    {
        if (string.IsNullOrEmpty(strInput))
        {
            return false;
        }

        strInput = strInput.Trim();
        if ((strInput.StartsWith("{") || strInput.EndsWith("}")) ||
            (strInput.StartsWith("[") || strInput.EndsWith("]")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}