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
using System.Reflection;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Screenshots;
using Bellatrix.Plugins.Screenshots.Plugins;
using Bellatrix.Utilities;
using Bellatrix.Web;
using Microsoft.TeamFoundation.Common;
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
        // As lighthouse analysis run is possible only if there is LighthouseAnalysisRunAttribute,
        // The only condition that needs to be met is if there is such attribute.
        if (HasLighthouseAttribute(e) && WrappedWebDriverCreateService.BrowserConfiguration.ExecutionType == Web.Enums.ExecutionType.Regular)
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

    /// <summary>
    /// Checks if the test is a lighthouse analysis or not.
    /// </summary>
    private bool HasLighthouseAttribute(PluginEventArgs e)
    {
        // Does it have any attribute of type BrowserAttribute?
        bool testHasAnyAttribute = !e.TestMethodMemberInfo.GetCustomAttributes().Where(x => x is BrowserAttribute).IsNullOrEmpty();


        if (testHasAnyAttribute)
        {
            // Checks if this attribute is for lighthouse analysis
            return e.TestMethodMemberInfo.GetCustomAttribute<LighthouseAnalysisRunAttribute>() != null;
        }
        else
        {
            // Otherwise, checks if the class has this attribute
            return e.TestClassType.GetCustomAttribute<LighthouseAnalysisRunAttribute>() != null;
        }
    }
}