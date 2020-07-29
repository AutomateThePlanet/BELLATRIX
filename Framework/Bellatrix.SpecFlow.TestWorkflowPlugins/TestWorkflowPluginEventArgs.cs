// <copyright file="TestWorkflowPluginEventArgs.cs" company="Automate The Planet Ltd.">
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
using System.Reflection;

namespace Bellatrix.SpecFlow.TestWorkflowPlugins
{
    public class TestWorkflowPluginEventArgs : EventArgs
    {
        public TestWorkflowPluginEventArgs()
        {
        }

        public TestWorkflowPluginEventArgs(string featureName,
            string scenarioName,
            string consoleOutputMessage,
            string consoleOutputStackTrace,
            List<string> featureTags,
            List<string> scenarioTags)
        {
            FeatureName = featureName;
            ScenarioName = scenarioName;
            ConsoleOutputMessage = consoleOutputMessage;
            ConsoleOutputStackTrace = consoleOutputStackTrace;
            FeatureTags = featureTags;
            ScenarioTags = scenarioTags;
            TestFullName = $"{FeatureName}.{ScenarioName}";
        }

        public TestWorkflowPluginEventArgs(
            TestOutcome testOutcome,
            string featureName,
            string scenarioName,
            string consoleOutputMessage,
            string consoleOutputStackTrace,
            List<string> featureTags,
            List<string> scenarioTags)
            : this(featureName, scenarioName, consoleOutputMessage, consoleOutputStackTrace, featureTags, scenarioTags)
            => TestOutcome = testOutcome;

        public TestWorkflowPluginEventArgs(
            Exception exception,
            string featureName,
            string scenarioName,
            string consoleOutputMessage,
            string consoleOutputStackTrace,
            List<string> featureTags,
            List<string> scenarioTags)
            : this(featureName, scenarioName, consoleOutputMessage, consoleOutputStackTrace, featureTags, scenarioTags)
            => Exception = exception;

        public Exception Exception { get; }

        public TestOutcome TestOutcome { get; }

        public string FeatureName { get; }

        public string ScenarioName { get; }

        public string TestFullName { get; }

        public IServicesCollection Container { get; set; }

        public string ConsoleOutputMessage { get; }

        public string ConsoleOutputStackTrace { get; }

        public List<string> FeatureTags { get; }

        public List<string> ScenarioTags { get; }
    }
}