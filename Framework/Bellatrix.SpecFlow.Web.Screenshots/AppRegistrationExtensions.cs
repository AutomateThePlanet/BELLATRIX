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
using Bellatrix.SpecFlow.TestExecutionExtensions.Screenshots;
using Bellatrix.SpecFlow.TestWorkflowPlugins;
using Bellatrix.TestExecutionExtensions.Screenshots;
using Bellatrix.TestExecutionExtensions.Screenshots.Contracts;
using Bellatrix.Web.Screenshots;

namespace Bellatrix.Web.SpecFlow
{
    public static class AppRegistrationExtensions
    {
        public static BaseApp UseFullPageScreenshotsOnFail(this BaseApp baseApp)
        {
            baseApp.RegisterType<IScreenshotEngine, FullPageScreenshotEngine>();
            baseApp.RegisterType<IScreenshotOutputProvider, ScreenshotOutputProvider>();
            baseApp.RegisterType<Bellatrix.SpecFlow.TestExecutionExtensions.Screenshots.IScreenshotPluginProvider, Bellatrix.SpecFlow.TestExecutionExtensions.Screenshots.ScreenshotPluginProvider>();
            baseApp.RegisterType<TestWorkflowPlugin, Bellatrix.SpecFlow.TestExecutionExtensions.Screenshots.ScreenshotWorkflowPlugin>(Guid.NewGuid().ToString());

            return baseApp;
        }
    }
}
