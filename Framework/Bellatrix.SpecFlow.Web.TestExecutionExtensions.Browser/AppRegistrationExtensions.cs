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
using Bellatrix.SpecFlow.TestWorkflowPlugins;
using Bellatrix.SpecFlow.Web.TestExecutionExtensions.Browser;

namespace Bellatrix.Web.SpecFlow
{
    public static class AppRegistrationExtensions
    {
        public static BaseApp UseBrowserBehavior(this BaseApp baseApp)
        {
            baseApp.RegisterType<TestWorkflowPlugin, BrowserWorkflowPlugin>(Guid.NewGuid().ToString());

            return baseApp;
        }

        public static BaseApp UseLogExecutionBehavior(this BaseApp baseApp)
        {
            baseApp.RegisterType<TestWorkflowPlugin, LogWorkflowPlugin>(Guid.NewGuid().ToString());

            return baseApp;
        }

        public static BaseApp UseControlLocalOverridesCleanBehavior(this BaseApp baseApp)
        {
            baseApp.RegisterType<TestWorkflowPlugin, ControlsLocalOverridesCleanExtension>(Guid.NewGuid().ToString());

            return baseApp;
        }
    }
}