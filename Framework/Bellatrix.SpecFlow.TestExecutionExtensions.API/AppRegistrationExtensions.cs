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
using Bellatrix.Application;
using Bellatrix.SpecFlow.TestExecutionExtensions.API;
using Bellatrix.SpecFlow.TestWorkflowPlugins;

namespace Bellatrix.SpecFlow
{
    public static class AppRegistrationExtensions
    {
        public static BaseApp UseRetryFailedRequests(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            baseApp.RegisterType<TestWorkflowPlugin, RetryFailedRequestsWorkflowPlugin>(Guid.NewGuid().ToString());

            return baseApp;
        }

        public static BaseApp UseLogExecution(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            baseApp.RegisterType<TestWorkflowPlugin, LogWorkflowPlugin>(Guid.NewGuid().ToString());

            return baseApp;
        }
    }
}
