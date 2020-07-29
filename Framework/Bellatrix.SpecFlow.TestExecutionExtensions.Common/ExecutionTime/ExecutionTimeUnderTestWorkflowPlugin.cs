// <copyright file="ExecutionTimeUnderTestWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.SpecFlow.TestExecutionExtensions.Common
{
    // The tag should be in the format executiontimeunder-12-seconds - no matter the casing.
    public class ExecutionTimeUnderTestWorkflowPlugin : TestWorkflowPlugin
    {
        private static readonly Dictionary<string, DateTime> _testsExecutionTimes = new Dictionary<string, DateTime>();

        protected override void PreBeforeScenario(object sender, TestWorkflowPluginEventArgs e)
        {
            TimeSpan executionTimeout = GetExecutionTimeout(e.FeatureTags, e.ScenarioTags);
            if (executionTimeout != TimeSpan.MaxValue)
            {
                DateTime startTime = DateTime.Now;
                if (!_testsExecutionTimes.ContainsKey(e.TestFullName))
                {
                    _testsExecutionTimes.Add(e.TestFullName, startTime);
                }
                else
                {
                    _testsExecutionTimes[e.TestFullName] = startTime;
                }
            }
        }

        protected override void PostAfterScenario(object sender, TestWorkflowPluginEventArgs e)
        {
            TimeSpan executionTimeout = GetExecutionTimeout(e.FeatureTags, e.ScenarioTags);
            if (executionTimeout != TimeSpan.MaxValue)
            {
                DateTime endTime = DateTime.Now;
                if (_testsExecutionTimes.ContainsKey(e.TestFullName))
                {
                    var startTime = _testsExecutionTimes[e.TestFullName];
                    var totalExecutionTime = endTime - startTime;
                    _testsExecutionTimes.Remove(e.TestFullName);
                    if (totalExecutionTime > executionTimeout)
                    {
                        throw new ExecutionTimeoutException($"The test {e.TestFullName} was executed for {totalExecutionTime}. The specified limit was {executionTimeout}.");
                    }
                }
            }
        }

        private TimeSpan GetExecutionTimeout(List<string> featureTags, List<string> scenarioTags)
        {
            TimeSpan scenarioTimeout = GetTimeoutInfoByTags(featureTags);
            TimeSpan featureTimeout = GetTimeoutInfoByTags(scenarioTags);

            if (scenarioTimeout != TimeSpan.MaxValue)
            {
                return scenarioTimeout;
            }

            if (featureTimeout != TimeSpan.MaxValue)
            {
                return featureTimeout;
            }

            return TimeSpan.MaxValue;
        }

        private TimeSpan GetTimeoutInfoByTags(List<string> tags)
        {
            if (tags == null)
            {
                throw new ArgumentNullException();
            }

            string timeoutTagText = tags.FirstOrDefault(x => x.ToLower().StartsWith("executiontimeunder", StringComparison.Ordinal));
            var timeoutResult = TimeSpan.MaxValue;
            if (string.IsNullOrEmpty(timeoutTagText))
            {
                string[] parts = timeoutTagText?.Split('-');
                if (parts?.Length >= 2)
                {
                    bool isTimeoutSet = int.TryParse(parts[1], out int timeout);
                    if (isTimeoutSet)
                    {
                        switch (parts[3].ToLower())
                        {
                            case "milliseconds":
                                timeoutResult = TimeSpan.FromMilliseconds(timeout);
                                break;
                            case "minutes":
                                timeoutResult = TimeSpan.FromMilliseconds(timeout);
                                break;
                            case "seconds":
                            default:
                                timeoutResult = TimeSpan.FromSeconds(timeout);
                                break;
                        }
                    }
                }
            }

            return timeoutResult;
        }
    }
}