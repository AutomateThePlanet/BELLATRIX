// <copyright file="AppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
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
using System;
using Bellatrix.Api;
using Bellatrix.Api.Extensions;
using Bellatrix.Application;

namespace Bellatrix
{
    public static class AppRegistrationExtensions
    {
        public static BaseApp UseApiExtensionsBddLogging(this BaseApp baseApp)
        {
            baseApp.RegisterType<ApiClientExecutionPlugin, BddApiClientExecutionPlugin>(Guid.NewGuid().ToString());

            return baseApp;
        }

        public static BaseApp UseAssertExtensionsBddLogging(this BaseApp baseApp)
        {
            var bddLoggingAssertExtensions = new BDDLoggingAssertExtensions();
            bddLoggingAssertExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseApiAssertExtensionsDynamicTestCases(this BaseApp baseApp)
        {
            var dynamicTestCasesAssertExtensions = new DynamicTestCasesAssertExtensions();
            dynamicTestCasesAssertExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseAssertExtensionsBugReporting(this BaseApp baseApp)
        {
            var dynamicTestCasesAssertExtensions = new BugReportingAssertExtensions();
            dynamicTestCasesAssertExtensions.SubscribeToAll();

            return baseApp;
        }
    }
}
