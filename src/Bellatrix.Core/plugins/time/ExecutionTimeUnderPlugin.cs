// <copyright file="ExecutionTimeUnderPlugin.cs" company="Automate The Planet Ltd.">
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
using System.Reflection;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Common.ExecutionTime;

namespace Bellatrix.Plugins.Common;

public class ExecutionTimeUnderPlugin : Plugin
{
    private static readonly Dictionary<string, DateTime> _testsExecutionTimes = new Dictionary<string, DateTime>();

    protected override void PostTestInit(object sender, PluginEventArgs e)
    {
        TimeSpan executionTimeout = GetExecutionTimeout(e.TestMethodMemberInfo);
        string testFullName = GetTestFullName(e);
        if (executionTimeout != TimeSpan.MaxValue)
        {
            DateTime startTime = DateTime.Now;
            if (!_testsExecutionTimes.ContainsKey(testFullName))
            {
                _testsExecutionTimes.Add(testFullName, startTime);
            }
            else
            {
                _testsExecutionTimes[testFullName] = startTime;
            }
        }
    }

    protected override void PostTestCleanup(object sender, PluginEventArgs e)
    {
        TimeSpan executionTimeout = GetExecutionTimeout(e.TestMethodMemberInfo);
        string testFullName = GetTestFullName(e);
        if (executionTimeout != TimeSpan.MaxValue)
        {
            DateTime endTime = DateTime.Now;
            if (_testsExecutionTimes.ContainsKey(testFullName))
            {
                var startTime = _testsExecutionTimes[testFullName];
                var totalExecutionTime = endTime - startTime;
                _testsExecutionTimes.Remove(testFullName);
                if (totalExecutionTime > executionTimeout)
                {
                    throw new ExecutionTimeoutException($"The test {testFullName} was executed for {totalExecutionTime}. The specified limit was {executionTimeout}.");
                }
            }
        }
    }

    private string GetTestFullName(PluginEventArgs e) => $"{e.TestMethodMemberInfo.DeclaringType.FullName}.{e.TestMethodMemberInfo.Name}";

    private TimeSpan GetExecutionTimeout(MemberInfo memberInfo)
    {
        TimeSpan methodTimeout = GetTimeoutByMethodInfo(memberInfo);
        TimeSpan classTimeout = GetTimeoutInfoByType(memberInfo.DeclaringType);

        if (methodTimeout != TimeSpan.MaxValue)
        {
            return methodTimeout;
        }

        if (classTimeout != TimeSpan.MaxValue)
        {
            return classTimeout;
        }

        return TimeSpan.MaxValue;
    }

    private TimeSpan GetTimeoutInfoByType(Type currentType)
    {
        if (currentType == null)
        {
            throw new ArgumentNullException();
        }

        var executionTimeUnderAttribute = currentType.GetCustomAttribute<ExecutionTimeUnderAttribute>(true);
        if (executionTimeUnderAttribute != null)
        {
            return executionTimeUnderAttribute.Timeout;
        }

        return TimeSpan.MaxValue;
    }

    private TimeSpan GetTimeoutByMethodInfo(MemberInfo memberInfo)
    {
        if (memberInfo == null)
        {
            throw new ArgumentNullException();
        }

        var executionTimeUnderAttribute = memberInfo.GetCustomAttribute<ExecutionTimeUnderAttribute>(true);
        if (executionTimeUnderAttribute != null)
        {
            return executionTimeUnderAttribute.Timeout;
        }

        return TimeSpan.MaxValue;
    }
}