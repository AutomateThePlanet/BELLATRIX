// <copyright file="DynamicTestCasesPlugin.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using Bellatrix.DynamicTestCases.AzureDevOps;
using Bellatrix.DynamicTestCases.Contracts;
using Bellatrix.DynamicTestCases.QTest;
using Bellatrix.Plugins;

namespace Bellatrix.DynamicTestCases.Core
{
    public class QTestIdPlugin : Plugin
    {
        private readonly ITestCaseManagementService _testCaseManagementService;
        private readonly DynamicTestCasesService _dynamicTestCasesService;

        public QTestIdPlugin(ITestCaseManagementService testCaseManagementService, DynamicTestCasesService dynamicTestCasesService)
        {
            _testCaseManagementService = testCaseManagementService;
            _dynamicTestCasesService = dynamicTestCasesService;
        }

        protected override void PreTestsArrange(object sender, PluginEventArgs e)
        {
            if (!IsTestCaseLinkingEnabled() || e.TestMethodMemberInfo == null)
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
                    _testCaseManagementService.UpdateTestCaseStepAndCreateANewTestCase(_dynamicTestCasesService.Context.Value);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Test case failed to update, {ex.Message}");
            }

        }

        private void InitializeTestCase(PluginEventArgs args)
        {
            var methodInfo = args.TestMethodMemberInfo;
            var methodAttribute = GetMethodAttribute<QTestIdAttribute>(methodInfo);
            var classAttribute = GetClassAttribute<QTestIdAttribute>(args.TestClassType);

            if (methodAttribute != null && classAttribute != null)
            {
                string testCaseId = methodAttribute?.TestCaseId ?? null;
                QTestModules suiteId = methodAttribute?.SuiteId ?? null;
                QTestProjects project = classAttribute?.Project ?? null;
                _dynamicTestCasesService.Context.Value.TestCaseId = testCaseId;
                _dynamicTestCasesService.Context.Value.ProjectId = (int)project;
                _dynamicTestCasesService.Context.Value.SuiteId = ((int)suiteId).ToString();
                _dynamicTestCasesService.Context.Value.TestFullName = args.TestName;
                _dynamicTestCasesService.Context.Value.TestProjectName = args.TestClassType.Assembly.GetName().Name;
            }
        }

        private bool AreDynamicTestCasesEnabled()
        {
            return ConfigurationService.GetSection<QTestDynamicTestCasesSettings>().IsEnabled ||
                ConfigurationService.GetSection<AzureDevOpsDynamicTestCasesSettings>().IsEnabled;
        }

        private bool IsTestCaseLinkingEnabled()
        {
            return ConfigurationService.GetSection<QTestDynamicTestCasesSettings>().TestCaseLinking;
        }
    }
}