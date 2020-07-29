// <copyright file="TestWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.SpecFlow.TestWorkflowPlugins
{
    [Serializable]
    public class TestWorkflowPlugin
    {
        public void Subscribe(ITestWorkflowPluginProvider provider)
        {
            provider.PreBeforeScenarioEvent += PreBeforeScenario;
            provider.TestInitFailedEvent += TestInitFailed;
            provider.PostBeforeScenarioEvent += PostBeforeScenario;
            provider.PreAfterScenarioEvent += PreAfterScenario;
            provider.PostAfterScenarioEvent += PostAfterScenario;
            provider.TestCleanupFailedEvent += TestCleanupFailed;
            provider.BeforeScenarioFailedEvent += BeforeScenarioFailed;
            provider.PreBeforeFeatureArrangeEvent += PreBeforeFeatureArrange;
            provider.PreBeforeFeatureActEvent += PreBeforeFeatureAct;
            provider.PostBeforeFeatureActEvent += PostBeforeFeatureAct;
            provider.PostBeforeFeatureArrangeEvent += PostBeforeFeatureArrange;
        }

        public void Unsubscribe(ITestWorkflowPluginProvider provider)
        {
            provider.PreBeforeScenarioEvent -= PreBeforeScenario;
            provider.TestInitFailedEvent -= TestInitFailed;
            provider.PostBeforeScenarioEvent -= PostBeforeScenario;
            provider.PreAfterScenarioEvent -= PreAfterScenario;
            provider.PostAfterScenarioEvent -= PostAfterScenario;
            provider.TestCleanupFailedEvent -= TestCleanupFailed;
            provider.BeforeScenarioFailedEvent -= BeforeScenarioFailed;
            provider.PreBeforeFeatureArrangeEvent -= PreBeforeFeatureArrange;
            provider.PreBeforeFeatureActEvent -= PreBeforeFeatureAct;
            provider.PostBeforeFeatureActEvent -= PostBeforeFeatureAct;
            provider.PostBeforeFeatureArrangeEvent -= PostBeforeFeatureArrange;
        }

        protected virtual void BeforeScenarioFailed(object sender, Exception ex)
        {
        }

        protected virtual void PreBeforeScenario(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void TestInitFailed(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PostBeforeScenario(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PreAfterScenario(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PostAfterScenario(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void TestCleanupFailed(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PreBeforeFeatureAct(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PreBeforeFeatureArrange(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PostBeforeFeatureAct(object sender, TestWorkflowPluginEventArgs e)
        {
        }

        protected virtual void PostBeforeFeatureArrange(object sender, TestWorkflowPluginEventArgs e)
        {
        }
    }
}