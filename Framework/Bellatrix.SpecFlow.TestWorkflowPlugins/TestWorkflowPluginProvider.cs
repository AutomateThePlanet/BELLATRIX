// <copyright file="TestWorkflowPluginProvider.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.SpecFlow.TestWorkflowPlugins
{
    public class TestWorkflowPluginProvider : ITestWorkflowPluginProvider
    {
        public event EventHandler<TestWorkflowPluginEventArgs> PreBeforeScenarioEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> TestInitFailedEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PostBeforeScenarioEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PreAfterScenarioEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PostAfterScenarioEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> TestCleanupFailedEvent;

        public event EventHandler<Exception> BeforeScenarioFailedEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PreBeforeFeatureActEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PreBeforeFeatureArrangeEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PostBeforeFeatureActEvent;

        public event EventHandler<TestWorkflowPluginEventArgs> PostBeforeFeatureArrangeEvent;

        public void PreBeforeFeatureAct(string featureName, List<string> featureTags)
            => RaiseTestEvent(PreBeforeFeatureActEvent, TestOutcome.Unknown, featureName, featureTags);

        public void PreBeforeFeatureArrange(string featureName, List<string> featureTags)
            => RaiseTestEvent(PreBeforeFeatureArrangeEvent, TestOutcome.Unknown, featureName, featureTags);

        public void PreBeforeScenario(string featureName, string scenarioName, List<string> featureTags, List<string> scenarioTags)
            => RaiseTestEvent(PreBeforeScenarioEvent, TestOutcome.Unknown, featureName, scenarioName, featureTags, scenarioTags);

        public void TestInitFailed(Exception ex, string featureName, string scenarioName, List<string> featureTags, List<string> scenarioTags, string message, string stackTrace)
            => RaiseTestEvent(TestInitFailedEvent, ex, featureName, scenarioName, featureTags, scenarioTags, message, stackTrace);

        public void PostBeforeScenario(string featureName, string scenarioName, List<string> featureTags, List<string> scenarioTags)
            => RaiseTestEvent(PostBeforeScenarioEvent, TestOutcome.Unknown, featureName, scenarioName, featureTags, scenarioTags);

        public void PreAfterScenario(TestOutcome testOutcome, string featureName, string scenarioName, List<string> featureTags, List<string> scenarioTags, string message, string stackTrace)
            => RaiseTestEvent(PreAfterScenarioEvent, testOutcome, featureName, scenarioName, featureTags, scenarioTags, message, stackTrace);

        public void PostAfterScenario(TestOutcome testOutcome, string featureName, string scenarioName, List<string> featureTags, List<string> scenarioTags, string message, string stackTrace)
            => RaiseTestEvent(PostAfterScenarioEvent, testOutcome, featureName, scenarioName, featureTags, scenarioTags, message, stackTrace);

        public void TestCleanupFailed(Exception ex, string featureName, string scenarioName, List<string> featureTags, List<string> scenarioTags, string message, string stackTrace)
            => RaiseTestEvent(TestCleanupFailedEvent, ex, featureName, scenarioName, featureTags, scenarioTags, message, stackTrace);

        public void BeforeFeatureFailed(Exception ex) => BeforeScenarioFailedEvent?.Invoke(this, ex);

        public void PostBeforeFeatureAct(string featureName, List<string> featureTags)
            => RaiseTestEvent(PostBeforeFeatureActEvent, TestOutcome.Unknown, featureName, featureTags);

        public void PostBeforeFeatureArrange(string featureName, List<string> featureTags)
            => RaiseTestEvent(PostBeforeFeatureArrangeEvent, TestOutcome.Unknown, featureName, featureTags);

        private void RaiseTestEvent(
            EventHandler<TestWorkflowPluginEventArgs> eventHandler,
            Exception exception,
            string featureName,
            string scenarioName,
            List<string> featureTags,
            List<string> scenarioTags,
            string message = null,
            string stackTrace = null) =>
            eventHandler?.Invoke(this, new TestWorkflowPluginEventArgs(exception, featureName, scenarioName, message, stackTrace, featureTags, scenarioTags));

        private void RaiseTestEvent(
            EventHandler<TestWorkflowPluginEventArgs> eventHandler,
            TestOutcome testOutcome,
            string featureName,
            string scenarioName,
            List<string> featureTags,
            List<string> scenarioTags,
            string message = null,
            string stackTrace = null) =>
            eventHandler?.Invoke(this, new TestWorkflowPluginEventArgs(testOutcome, featureName, scenarioName, message, stackTrace, featureTags, scenarioTags));

        private void RaiseTestEvent(
            EventHandler<TestWorkflowPluginEventArgs> eventHandler,
            TestOutcome testOutcome,
            string featureName,
            List<string> featureTags,
            string message = null,
            string stackTrace = null) =>
            eventHandler?.Invoke(this, new TestWorkflowPluginEventArgs(testOutcome, featureName, string.Empty, message, stackTrace, featureTags, new List<string>()));
    }
}