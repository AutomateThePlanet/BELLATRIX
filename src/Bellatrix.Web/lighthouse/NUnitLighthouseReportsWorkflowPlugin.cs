// <copyright file="NUnitResultsWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Linq;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Screenshots;
using Bellatrix.Plugins.Screenshots.Plugins;
using Bellatrix.Utilities;
using Bellatrix.Web;
using NUnit.Framework;

namespace Bellatrix.GoogleLighthouse.NUnit;

public class NUnitLighthouseReportsWorkflowPlugin : Plugin
{
    private static readonly object _lockObject = new object();

    public NUnitLighthouseReportsWorkflowPlugin()
    {
    }

    public TestContext TestContext { get; set; }

    protected override void PostTestCleanup(object sender, PluginEventArgs e)
    {
        var settings = ConfigurationService.GetSection<LighthouseSettings>();
        if (settings != null && settings.IsEnabled && WrappedWebDriverCreateService.BrowserConfiguration.ExecutionType == Web.Enums.ExecutionType.Regular)
        {
            lock (_lockObject)
            {
                var driverExecutablePath = new DirectoryInfo(ExecutionDirectoryResolver.GetDriverExecutablePath());
                var file = driverExecutablePath.GetFiles("*.report.json", SearchOption.AllDirectories).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                if (file != null && file.Exists)
                {
                    TestContext.AddTestAttachment(file.FullName);
                }

                file = driverExecutablePath.GetFiles("*.report.html", SearchOption.AllDirectories).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                if (file != null && file.Exists)
                {
                    TestContext.AddTestAttachment(file.FullName);
                }

                file = driverExecutablePath.GetFiles("*.report.csv", SearchOption.AllDirectories).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                if (file != null && file.Exists)
                {
                    TestContext.AddTestAttachment(file.FullName);
                }
            }
        }
    }
}