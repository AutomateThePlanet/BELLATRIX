// <copyright file="LoadTestingWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.TestWorkflowPlugins;
using Bellatrix.Web.TestExecutionExtensions.LoadTesting;

namespace Bellatrix.Web.LoadTesting
{
    public class LoadTestingWorkflowPlugin : TestWorkflowPlugin
    {
        protected override void PreTestInit(object sender, TestWorkflowPluginEventArgs e)
        {
            var loadTestingAttribute = GetOverridenAttribute<LoadTestAttribute>(e.TestMethodMemberInfo);
            var loadTestingWorkflowPluginContext = new LoadTestingWorkflowPluginContext();

            if (loadTestingAttribute == null)
            {
                loadTestingWorkflowPluginContext.IsLoadTestingEnabled = false;
            }
            else
            {
                loadTestingWorkflowPluginContext.IsLoadTestingEnabled = ConfigurationService.Instance.GetLoadTestingSettings().IsEnabled;
                loadTestingWorkflowPluginContext.ShouldFilterByHost = loadTestingAttribute.ShouldRecordHostRequestsOnly;
                loadTestingWorkflowPluginContext.FilterHost = loadTestingAttribute.Host;
            }

            loadTestingWorkflowPluginContext.CurrentTestName = e.TestFullName;

            e.Container.RegisterInstance(loadTestingWorkflowPluginContext);
            loadTestingWorkflowPluginContext.PreTestInit();

            base.PreTestInit(sender, e);
        }

        protected override void PostTestCleanup(object sender, TestWorkflowPluginEventArgs e)
        {
            var loadTestingWorkflowPluginContext = e.Container.Resolve<LoadTestingWorkflowPluginContext>();
            loadTestingWorkflowPluginContext?.PostTestCleanup();
            base.PostTestCleanup(sender, e);
        }
    }
}