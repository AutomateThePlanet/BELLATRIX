﻿// <copyright file="DesktopTest.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Core.logging;
using Bellatrix.LLM.Plugins;

namespace Bellatrix.Desktop.MSTest;

public abstract class DesktopTest : MSTestBaseTest
{
    private static readonly object _lockObject = new object();
    private static bool _arePluginsAlreadyInitialized;

    public App App => ServicesCollection.Current.FindCollection(TestContext.FullyQualifiedTestClassName).Resolve<App>();

    public override void Configure()
    {
        lock (_lockObject)
        {
            if (!_arePluginsAlreadyInitialized)
            {
                MSTestPluginConfiguration.Add();
                ExecutionTimePlugin.Add();
                VideoRecorderPluginConfiguration.AddMSTest();
                ScreenshotsPluginConfiguration.AddMSTest();
                DynamicTestCasesPlugin.Add();
                AllurePlugin.Add();
                BugReportingPlugin.Add();
                DesktopPluginsConfiguration.AddLifecycle();
                DesktopPluginsConfiguration.AddLogExecutionLifecycle();
                DesktopPluginsConfiguration.AddVanillaWebDriverScreenshotsOnFail();
                DesktopPluginsConfiguration.AddElementsBddLogging();
                DesktopPluginsConfiguration.AddDynamicTestCases();
                DesktopPluginsConfiguration.AddBugReporting();
                DesktopPluginsConfiguration.AddValidateExtensionsBddLogging();
                DesktopPluginsConfiguration.AddValidateExtensionsDynamicTestCases();
                DesktopPluginsConfiguration.AddValidateExtensionsBugReporting();
                DesktopPluginsConfiguration.AddLayoutAssertionExtensionsBddLogging();
                DesktopPluginsConfiguration.AddLayoutAssertionExtensionsDynamicTestCases();
                DesktopPluginsConfiguration.AddLayoutAssertionExtensionsBugReporting();
                DesktopPluginsConfiguration.ConfigureLLM();

                SmartFailureAnalysisPlugin.Add();
                LoggerFlushPlugin.Add();

                _arePluginsAlreadyInitialized = true;
            }
        }
    }
}
