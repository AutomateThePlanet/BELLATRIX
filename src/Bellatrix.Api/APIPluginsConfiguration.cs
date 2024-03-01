// <copyright file="AppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Api;
using Bellatrix.Api.Extensions;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Api;
using Bellatrix.Plugins.API;

namespace Bellatrix;

public static class APIPluginsConfiguration
{
    public static void AddApiExtensionsBddLogging()
    {
        ServicesCollection.Current.RegisterType<ApiClientExecutionPlugin, BddApiClientExecutionPlugin>(Guid.NewGuid().ToString());
    }

    public static void AddAssertExtensionsBddLogging()
    {
        var bddLoggingAssertExtensions = new BDDLoggingAssertExtensions();
        bddLoggingAssertExtensions.SubscribeToAll();
    }

    public static void AddApiAssertExtensionsDynamicTestCases()
    {
        var dynamicTestCasesAssertExtensions = new DynamicTestCasesAssertExtensions();
        dynamicTestCasesAssertExtensions.SubscribeToAll();
    }

    public static void AddAssertExtensionsBugReporting()
    {
        var dynamicTestCasesAssertExtensions = new BugReportingAssertExtensions();
        dynamicTestCasesAssertExtensions.SubscribeToAll();
    }

    public static void AddApiAuthenticationStrategies()
    {
        ServicesCollection.Current.RegisterType<Plugin, ApiAuthenticationWorkflowPlugin>(Guid.NewGuid().ToString());
    }

    public static void AddRetryFailedRequests()
    {
        ServicesCollection.Current.RegisterType<Plugin, RetryFailedRequestsWorkflowPlugin>(Guid.NewGuid().ToString());
    }

    public static void AddLogExecution()
    {
        ServicesCollection.Current.RegisterType<Plugin, LogWorkflowPlugin>(Guid.NewGuid().ToString());
    }
}
