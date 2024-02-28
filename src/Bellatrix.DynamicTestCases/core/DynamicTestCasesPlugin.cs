// <copyright file="DynamicTestCasesPlugin.cs" company="Automate The Planet Ltd.">
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
using System.Text.RegularExpressions;
using Bellatrix.DynamicTestCases.AzureDevOps;
using Bellatrix.DynamicTestCases.Contracts;
using Bellatrix.DynamicTestCases.QTest;
using Bellatrix.Plugins;

namespace Bellatrix.DynamicTestCases.Core;

public class DynamicTestCasesPlugin : Plugin
{
    private readonly ITestCaseManagementService _testCaseManagementService;
    private readonly DynamicTestCasesService _dynamicTestCasesService;

    public DynamicTestCasesPlugin(ITestCaseManagementService testCaseManagementService, DynamicTestCasesService dynamicTestCasesService)
    {
        _testCaseManagementService = testCaseManagementService;
        _dynamicTestCasesService = dynamicTestCasesService;
    }

    protected override void PreTestsArrange(object sender, PluginEventArgs e)
    {
        if (!AreDynamicTestCasesEnabled() || e.TestMethodMemberInfo == null)
        {
            return;
        }

        base.PreTestsArrange(sender, e);

        InitializeTestCase(e);
    }

    protected override void PreTestInit(object sender, PluginEventArgs e)
    {
        if (!AreDynamicTestCasesEnabled())
        {
            return;
        }

        base.PreTestInit(sender, e);
        InitializeTestCase(e);
    }

    protected override void PostTestCleanup(object sender, PluginEventArgs e)
    {
        if (!AreDynamicTestCasesEnabled())
        {
            return;
        }

        base.PostTestCleanup(sender, e);

        try
        {
            // Update the test case only upon test pass. In case of failure, only the basic test case - name, description, etc will remain - without the test steps
            if (e.TestOutcome == TestOutcome.Passed && _dynamicTestCasesService?.Context != null)
            {
                _dynamicTestCasesService.Context.Value.TestCase = _testCaseManagementService.UpdateTestCaseStepAndCreateANewTestCase(_dynamicTestCasesService.Context.Value);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Test case failed to update, {ex.Message}");
        }

        _dynamicTestCasesService?.ResetContext();
    }

    private void InitializeTestCase(PluginEventArgs args)
    {
        var methodInfo = args.TestMethodMemberInfo;
        var methodAttribute = GetMethodAttribute<DynamicTestCaseAttribute>(methodInfo);
        var classAttribute = GetClassAttribute<DynamicTestCaseAttribute>(args.TestClassType);

        classAttribute?.SetCustomProperties();
        methodAttribute?.SetCustomProperties();

        string suiteId = methodAttribute?.SuiteId ?? classAttribute?.SuiteId ?? null;
        string testCaseName = methodAttribute?.TestName ?? null;
        string testCaseId = methodAttribute?.TestCaseId ?? null;
        string requirementId = methodAttribute?.RequirementId ?? classAttribute?.RequirementId ?? null;

        var testCaseDescription = methodAttribute?.Description ?? null;

        if (string.IsNullOrEmpty(testCaseName))
        {
            // test case name from the test method name
            testCaseName = TestNameToDesciption(args.TestName);
        }

        _dynamicTestCasesService.Context.Value.SuiteId = suiteId;
        _dynamicTestCasesService.Context.Value.TestCaseName = testCaseName;
        _dynamicTestCasesService.Context.Value.TestCaseDescription = testCaseDescription;
        _dynamicTestCasesService.Context.Value.TestCaseId = testCaseId;
        _dynamicTestCasesService.Context.Value.RequirementId = requirementId;
        _dynamicTestCasesService.Context.Value.TestFullName = $"{args.TestClassName}.{args.TestName}";
        _dynamicTestCasesService.Context.Value.TestProjectName = args.TestClassType.Assembly.GetName().Name;
    }

    private string TestNameToDesciption(string name)
    {
        var noUnderScoreName = Regex.Replace(name, "_", string.Empty);
        var properName = Regex.Replace(noUnderScoreName, "([a-z])([A-Z])", "$1 $2");
        return properName;
    }

    private bool AreDynamicTestCasesEnabled()
    {
        return ConfigurationService.GetSection<QTestDynamicTestCasesSettings>().IsEnabled ||
            ConfigurationService.GetSection<AzureDevOpsDynamicTestCasesSettings>().IsEnabled;
    }
}