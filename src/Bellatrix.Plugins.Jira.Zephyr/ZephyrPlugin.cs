// <copyright file="ZephyrPlugin.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
    private static ZephyrSettings Settings => ConfigurationService.GetSection<ZephyrSettings>();
    private static bool IsEnabled => Settings.IsEnabled;
    private readonly ZephyrPluginProvider _zephyrPluginProvider = new ZephyrPluginProvider();

    private ZephyrTestCycle _testCycle = new();

    public static void Add()
    {
        ServicesCollection.Current.RegisterType<Plugin, ZephyrPlugin>(Guid.NewGuid().ToString());
    }

    protected override void PostTestsArrange(object sender, PluginEventArgs e)
    {
        if (IsEnabled && !Settings.IsExistingCycle)
        {
            _testCycle.ProjectKey = GetProjectId(e.TestClassType);
            _testCycle.Name = $"{DateTimeUtilities.GetUtcNow()} {Settings.TestCycleName}";
            _testCycle.StatusName = TestCycleStatus.InProgress.GetValue();
            _testCycle.PlannedStartDate = DateTimeUtilities.GetUtcNow();

            if (ZephyrApiService.TryCreateTestCycle(ref _testCycle))
            {
                _zephyrPluginProvider.ZephyrCycleCreated(e, _testCycle);
                e.Container.RegisterInstance(_testCycle);
            }
            else
            {
                _zephyrPluginProvider.ZephyrCycleCreationFailed(e, _testCycle);
            }
        }

        base.PostTestsArrange(sender, e);
    }

    protected override void PostTestCleanup(object sender, PluginEventArgs e)
    {
        if (IsEnabled)
        {
            var testCase = new ZephyrTestCase();
            testCase.ProjectKey = GetProjectKey(e.TestMethodMemberInfo);
            testCase.CycleKey = GetCycleKey(e);
            testCase.Id = GetExecutionId(e.TestMethodMemberInfo);
            testCase.Status = GetTestStatus(e.TestOutcome);
            testCase.Exception = GetActualException(e.Exception);


            if (string.IsNullOrEmpty(testCase.Id) || string.IsNullOrEmpty(testCase.Status) || string.IsNullOrEmpty(testCase.ProjectKey) || string.IsNullOrEmpty(testCase.CycleKey))
            {
                _zephyrPluginProvider.ZephyrTestCaseExecutionFailed(e, testCase);
            }
            else
            {
                if (ZephyrApiService.TryExecuteTestCase(testCase))
                {
                    _zephyrPluginProvider.ZephyrTestCaseExecuted(e, testCase);
                }
                else
                {
                    _zephyrPluginProvider.ZephyrTestCaseExecutionFailed(e, testCase);
                }
            }
        }

        base.PostTestCleanup(sender, e);    
    }

    protected override void PostTestsCleanup(object sender, PluginEventArgs e)
    {
        if (IsEnabled && !Settings.IsExistingCycle)
        {
            var testCycle = e.Container.Resolve<ZephyrTestCycle>();

            testCycle.PlannedEndDate = DateTimeUtilities.GetUtcNow();

            if (ZephyrApiService.TryMarkTestCycleDone(_testCycle))
            {
                _zephyrPluginProvider.ZephyrCycleStatuseUpdated(e, testCycle);
            }
            else
            {
                _zephyrPluginProvider.ZephyrCycleStatusUpdateFailed(e, testCycle);
            }
        }

        base.PostTestsCleanup(sender, e);
    }

    private string? GetExecutionId(MemberInfo memberInfo)
    {
        return memberInfo.GetCustomAttribute<ZephyrTestCaseAttribute>()?.Id;
    }

    private string? GetProjectKey(MemberInfo memberInfo)
    {
        if (memberInfo.GetCustomAttribute<ZephyrTestCaseAttribute>()?.ProjectId != null) 
            return memberInfo.GetCustomAttribute<ZephyrTestCaseAttribute>()?.ProjectId;

        if (memberInfo.DeclaringType?.GetCustomAttribute<ZephyrProjectIdAttribute>() != null) 
            return memberInfo.DeclaringType?.GetCustomAttribute<ZephyrProjectIdAttribute>()?.Value;

        else 
            return Settings.DefaultProjectKey;
    }

    private string? GetProjectId(Type? classType)
    {
        if (classType?.GetCustomAttribute<ZephyrProjectIdAttribute>() != null)
            return classType.GetCustomAttribute<ZephyrProjectIdAttribute>()?.Value;

        else
            return Settings.DefaultProjectKey;
    }

    private string? GetCycleKey(PluginEventArgs e)
    {
        if (!Settings.IsExistingCycle) return e.Container.Resolve<ZephyrTestCycle>().Key;

        var methodInfo = e.TestMethodMemberInfo;

        if (methodInfo.GetCustomAttribute<ZephyrTestCaseAttribute>()?.CycleId != null)
        {
            return methodInfo.GetCustomAttribute<ZephyrTestCaseAttribute>()?.CycleId;
        }

        if (methodInfo.DeclaringType?.GetCustomAttribute<ZephyrCycleIdAttribute>() != null)
        {
            return methodInfo.DeclaringType?.GetCustomAttribute<ZephyrCycleIdAttribute>()?.Value;
        }

        else return null;
    }

    private string GetTestStatus(TestOutcome testOutcome)
    {
        switch(testOutcome)
        {
            case TestOutcome.Failed:
            case TestOutcome.Aborted:
            case TestOutcome.Error:
                return TestExecutionStatus.Fail.GetValue();
            case TestOutcome.Passed:
                return TestExecutionStatus.Pass.GetValue();
            default: 
                return TestExecutionStatus.InProgress.GetValue();
        }
    }

    private Exception GetActualException(Exception exception)
    {
        while (exception.Message.Equals("Rethrown"))
        {
            exception = exception.InnerException;
        }

        return exception;
    }
}