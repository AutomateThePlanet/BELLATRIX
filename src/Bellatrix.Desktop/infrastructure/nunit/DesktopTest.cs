// <copyright file="DesktopTest.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Desktop.NUnit;

public abstract class DesktopTest : NUnitBaseTest
{
    private static readonly object _lockObject = new object();
    private static bool _arePluginsAlreadyInitialized;

    public App App => ServicesCollection.Current.FindCollection(TestContext.Test.ClassName).Resolve<App>();

    public override void Configure()
    {
        lock (_lockObject)
        {
            if (!_arePluginsAlreadyInitialized)
            {
                NUnitPluginConfiguration.Add();
                ExecutionTimePlugin.Add();
                VideoRecorderPluginConfiguration.AddNUnit();
                ScreenshotsPluginConfiguration.AddNUnit();
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

                _arePluginsAlreadyInitialized = true;
            }
        }
    }
}