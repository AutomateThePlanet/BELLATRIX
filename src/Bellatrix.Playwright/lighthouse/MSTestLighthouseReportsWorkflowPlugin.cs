// <copyright file="MSTestLighthouseReportsWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Plugins;
using Bellatrix.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bellatrix.Playwright.Services;
using Bellatrix.Playwright.Enums;

namespace Bellatrix.GoogleLighthouse.MSTest;

public class MSTestLighthouseReportsWorkflowPlugin : Plugin
{
    private static readonly object _lockObject = new object();

#pragma warning disable MSTEST0005 // Test context property should have valid layout
    public static TestContext TestContext { get; set; }
#pragma warning restore MSTEST0005 // Test context property should have valid layout

    protected override void PostTestCleanup(object sender, PluginEventArgs e)
    {
        var settings = ConfigurationService.GetSection<LighthouseSettings>();
        if (settings.IsEnabled && WrappedBrowserCreateService.BrowserConfiguration.ExecutionType == ExecutionType.Regular)
        {
            lock (_lockObject)
            {
                var driverExecutablePath = new DirectoryInfo(ExecutionDirectoryResolver.GetDriverExecutablePath());
                var file = driverExecutablePath.GetFiles("*.report.json", SearchOption.AllDirectories).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                if (file != null && file.Exists)
                {
                    TestContext?.AddResultFile(file.FullName);
                }

                file = driverExecutablePath.GetFiles("*.report.html", SearchOption.AllDirectories).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                if (file != null && file.Exists)
                {
                    TestContext?.AddResultFile(file.FullName);
                }

                file = driverExecutablePath.GetFiles("*.report.csv", SearchOption.AllDirectories).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                if (file != null && file.Exists)
                {
                    TestContext?.AddResultFile(file.FullName);
                }
            }
        }
    }
}