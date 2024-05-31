// <copyright file="ZephyrPlugin.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using System.Reflection;
using Bellatrix.Plugins.Jira.Zephyr.Attributes;
using Bellatrix.Plugins.Jira.Zephyr.Data;
using Bellatrix.Plugins.Jira.Zephyr.Eventargs;
using Bellatrix.Plugins.Jira.Zephyr.Services;
using Plugins.Jira.Zephyr.Services;

namespace Bellatrix.Plugins.Jira.Zephyr;

public class ZephyrPlugin : Plugin
{
    internal static ZephyrSettings Settings => ConfigurationService.GetSection<ZephyrSettings>();

    internal static ZephyrLocalData Data = new ZephyrLocalData();

    private readonly ZephyrPluginProvider _zephyrPluginProvider = new ZephyrPluginProvider();
    private static bool IsEnabled => Settings.IsEnabled;

    protected override void PostTestsArrange(object sender, PluginEventArgs e)
    {
        if (IsEnabled)
        {
            var testCycleName = $"{DateTimeUtilities.GetUtcNow()} {Settings.TestCycleName}";

            var testCycle = new ZephyrTestCycle(Settings.DefaultProjectKey, testCycleName, "In Progress");
            testCycle.PlannedStartDate = DateTimeUtilities.GetUtcNow();
            Data.TestCycle = testCycle;

            Data.TestCycleResponse = ZephyrApiService.CreateTestCycle(Data);

            e.Container.RegisterInstance(Data);

            _zephyrPluginProvider.ZephyrCycleCreated(e, Data.TestCycle);
        }

        base.PostTestsArrange(sender, e);
    }

    protected override void PostTestCleanup(object sender, PluginEventArgs e)
    {
        if (IsEnabled)
        {
            var data = e.Container.Resolve<ZephyrLocalData>();
            var testCase = new ZephyrTestCase(GetProjectId(e.TestMethodMemberInfo), data.TestCycleResponse.key, GetExecutionId(e.TestMethodMemberInfo), GetTestStatus(e.TestOutcome));

            if (string.IsNullOrEmpty(testCase.TestCaseId) || string.IsNullOrEmpty(testCase.Status) || string.IsNullOrEmpty(testCase.ProjectId))
            {
                _zephyrPluginProvider.ZephyrTestCaseExecutionFailed(e, testCase);
            }
            else
            {
                var response = ZephyrApiService.ExecuteTestCase(testCase);

                if (!response.IsSuccessful)
                    _zephyrPluginProvider.ZephyrTestCaseExecutionFailed(e, testCase);
                else
                    _zephyrPluginProvider.ZephyrTestCaseExecuted(e, testCase);
            }
        }

        base.PostTestCleanup(sender, e);    
    }

    protected override void PostTestsCleanup(object sender, PluginEventArgs e)
    {
        if (IsEnabled)
        {
            var data = e.Container.Resolve<ZephyrLocalData>();

            data.TestCycle.PlannedEndDate = DateTimeUtilities.GetUtcNow();
            var response = ZephyrApiService.MarkTestCycleDone(data);

            if (!response.IsSuccessful)
            {
                _zephyrPluginProvider.ZephyrCycleStatusUpdateFailed(e, data.TestCycle);
            }
        }

        base.PostTestsCleanup(sender, e);
    }

    internal class ZephyrLocalData
    {
        internal ZephyrTestCycleResponse TestCycleResponse { get; set; }
        internal ZephyrTestCycle TestCycle { get; set; }
    }

    private string GetExecutionId(MemberInfo memberInfo)
    {
        var zephyrTestCaseAttribute = memberInfo.GetCustomAttribute<ZephyrTestCaseAttribute>();
        return zephyrTestCaseAttribute != null ? zephyrTestCaseAttribute.Id : string.Empty;
    }

    private string GetProjectId(MemberInfo memberInfo)
    {
        var zephyrProjectAttribute = memberInfo.DeclaringType.GetCustomAttribute<ZephyrProjectAttribute>();
        if (zephyrProjectAttribute != null)
        {
            return zephyrProjectAttribute.Id;
        }
        else
        {
            return Settings.DefaultProjectKey != null ? Settings.DefaultProjectKey : string.Empty;
        }
    }

    private string GetTestStatus(TestOutcome testOutcome)
    {
        switch(testOutcome)
        {
            case TestOutcome.Failed:
            case TestOutcome.Aborted:
            case TestOutcome.Error:
                return "Fail";
            case TestOutcome.Passed:
                return "Pass";
            default: 
                return "In Progress";
        }
    }
}