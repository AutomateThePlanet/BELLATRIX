﻿// <copyright file="IOSTest.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Android;
using Bellatrix.Mobile.IOS;

namespace Bellatrix.Mobile.NUnit;

public abstract class IOSTest : NUnitBaseTest
{
    private static readonly object _lockObject = new object();
    private static bool _arePluginsAlreadyInitialized;

    public IOSApp App => ServicesCollection.Current.FindCollection(TestContext.Test.ClassName).Resolve<IOSApp>();

    public override void Configure()
    {
        lock (_lockObject)
        {
            if (!_arePluginsAlreadyInitialized)
            {
                NUnitPluginConfiguration.Add();
                ExecutionTimePlugin.Add();
                DynamicTestCasesPlugin.Add();
                AllurePlugin.Add();
                BugReportingPlugin.Add();
                VideoRecorderPluginConfiguration.AddNUnit();
                ScreenshotsPluginConfiguration.AddNUnit();
                IOSPluginsConfiguration.AddIOSDriverScreenshotsOnFail();
                IOSPluginsConfiguration.AddElementsBddLogging();
                IOSPluginsConfiguration.AddDynamicTestCases();
                IOSPluginsConfiguration.AddBugReporting();
                IOSPluginsConfiguration.AddValidateExtensionsBddLogging();
                IOSPluginsConfiguration.AddValidateExtensionsDynamicTestCases();
                IOSPluginsConfiguration.AddValidateExtensionsBugReporting();
                IOSPluginsConfiguration.AddLayoutAssertionExtensionsBddLogging();
                IOSPluginsConfiguration.AddLayoutAssertionExtensionsDynamicTestCases();
                IOSPluginsConfiguration.AddLayoutAssertionExtensionsBugReporting();
                IOSPluginsConfiguration.AddLifecycle();
                IOSPluginsConfiguration.AddLogExecutionLifecycle();

                IOSPluginsConfiguration.ConfigureLLM();
                SmartFailureAnalysisPlugin.Add();
                LoggerFlushPlugin.Add();

                _arePluginsAlreadyInitialized = true;
            }
        }
    }
}