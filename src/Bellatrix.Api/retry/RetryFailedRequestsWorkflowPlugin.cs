// <copyright file="RetryFailedRequestsWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using System.Reflection;
using Bellatrix.Api;
using Bellatrix.Plugins.Api.Retry;
using Bellatrix.Utilities;

namespace Bellatrix.Plugins.Api;

public class RetryFailedRequestsWorkflowPlugin : Plugin
{
    protected override void PostTestInit(object sender, PluginEventArgs e)
    {
        RetryFailedRequestsInfo retryFailedRequestsInfo = GetRetryFailedRequestsInfo(e.TestMethodMemberInfo);

        if (retryFailedRequestsInfo != null)
        {
            var client = e.Container.Resolve<ApiClientService>();
            client.PauseBetweenFailures = TimeSpanConverter.Convert(retryFailedRequestsInfo.PauseBetweenFailures, retryFailedRequestsInfo.TimeUnit);
            client.MaxRetryAttempts = retryFailedRequestsInfo.MaxRetryAttempts;
        }
    }

    private RetryFailedRequestsInfo GetRetryFailedRequestsInfo(MemberInfo memberInfo)
    {
        RetryFailedRequestsInfo methodRetryFailedRequestsInfo = GetRetryFailedRequestsInfoByMethodInfo(memberInfo);
        RetryFailedRequestsInfo classRetryFailedRequestsInfo = GetRetryFailedRequestsInfoByType(memberInfo.DeclaringType);

        if (methodRetryFailedRequestsInfo != null)
        {
            return methodRetryFailedRequestsInfo;
        }
        else if (classRetryFailedRequestsInfo != null)
        {
            return classRetryFailedRequestsInfo;
        }

        return null;
    }

    private RetryFailedRequestsInfo GetRetryFailedRequestsInfoByType(Type currentType)
    {
        if (currentType == null)
        {
            throw new ArgumentNullException();
        }

        var retryFailedRequestsClassAttribute = currentType.GetCustomAttribute<RetryFailedRequestsAttribute>(true);
        return retryFailedRequestsClassAttribute?.RetryFailedRequestsInfo;
    }

    private RetryFailedRequestsInfo GetRetryFailedRequestsInfoByMethodInfo(MemberInfo memberInfo)
    {
        if (memberInfo == null)
        {
            throw new ArgumentNullException();
        }

        var retryFailedRequestsMethodAttribute = memberInfo.GetCustomAttribute<RetryFailedRequestsAttribute>(true);
        return retryFailedRequestsMethodAttribute?.RetryFailedRequestsInfo;
    }
}