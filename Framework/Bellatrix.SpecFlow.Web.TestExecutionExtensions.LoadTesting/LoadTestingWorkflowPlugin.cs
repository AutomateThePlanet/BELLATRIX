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
using System;
using System.Collections.Generic;
using System.Linq;
using Bellatrix.SpecFlow.TestWorkflowPlugins;
using Bellatrix.Web.TestExecutionExtensions.LoadTesting;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Web.LoadTesting
{
    [Binding]
    public class LoadTestingWorkflowPlugin : TestWorkflowPlugin
    {
        private readonly LoadTestingWorkflowPluginContext _loadTestingWorkflowPluginContext;

        public LoadTestingWorkflowPlugin() => _loadTestingWorkflowPluginContext = ServicesCollection.Current.Resolve<LoadTestingWorkflowPluginContext>();

        [Given(@"I should filter request by host")]
        public void GivenShouldFilterRequestByHost()
        {
            _loadTestingWorkflowPluginContext.ShouldFilterByHost = true;
        }

        [Given(@"I use filter host (.*)")]
        public void GivenUseFilterHost(string filterHost)
        {
            _loadTestingWorkflowPluginContext.FilterHost = filterHost;
        }

        protected override void PreBeforeScenario(object sender, TestWorkflowPluginEventArgs e)
        {
            InitializeLoadTesting(e.FeatureTags, e.ScenarioTags);

            _loadTestingWorkflowPluginContext.CurrentTestName = e.TestFullName;

            _loadTestingWorkflowPluginContext.PreTestInit();

            base.PreBeforeScenario(sender, e);
        }

        protected override void PostAfterScenario(object sender, TestWorkflowPluginEventArgs e)
        {
            _loadTestingWorkflowPluginContext.PostTestCleanup();
            base.PostAfterScenario(sender, e);
        }

        private void InitializeLoadTesting(List<string> featureTags, List<string> scenarioTags)
        {
            bool isLoadTest = false;
            bool featureShouldTakeScreenshot = GetIsLoadTestByTags(featureTags);
            bool scenarioShouldTakeScreenshot = GetIsLoadTestByTags(scenarioTags);
            bool isEnabled = ConfigurationService.Instance.GetLoadTestingSettings().IsEnabled;
            _loadTestingWorkflowPluginContext.IsLoadTestingEnabled = isEnabled && (featureShouldTakeScreenshot || scenarioShouldTakeScreenshot);
        }

        private bool GetIsLoadTestByTags(List<string> tags)
        {
            if (tags == null)
            {
                throw new ArgumentNullException();
            }

            bool isLoadTest = tags.Any(x => x.ToLower().Equals("loadtest", StringComparison.Ordinal));
            return isLoadTest;
        }
    }
}