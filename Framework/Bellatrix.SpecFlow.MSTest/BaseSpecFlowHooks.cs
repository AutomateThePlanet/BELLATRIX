// <copyright file="SpecFlowHooks.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Linq;
using System.Threading;
using Bellatrix.SpecFlow.TestWorkflowPlugins;
using Bellatrix.Trace;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.MSTest
{
    [Binding]
    public class BaseSpecFlowHooks
    {
        protected static readonly TestWorkflowPluginProvider TestExecutionProvider;
        protected static ThreadLocal<Exception> ThrownException;
        private StringWriter _stringWriter = new StringWriter();

        static BaseSpecFlowHooks()
        {
            TestExecutionProvider = new TestWorkflowPluginProvider();
            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                if (eventArgs.Exception.Source != "System.Private.CoreLib")
                {
                    if (ThrownException == null)
                    {
                        ThrownException = new ThreadLocal<Exception>(() => eventArgs.Exception);
                    }
                    else
                    {
                        ThrownException.Value = eventArgs.Exception;
                    }
                }
            };
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Telemetry.Instance.Flush();
        }

        protected static bool IsDebugRun()
        {
#if DEBUG
            bool isDebug = true;
#else
            bool isDebug = false;
#endif

            return isDebug;
        }

        protected static void InitializeTestExecutionBehaviorObservers(TestWorkflowPluginProvider testExecutionProvider)
        {
            var observers = ServicesCollection.Current.ResolveAll<TestWorkflowPlugin>();
            foreach (var observer in observers)
            {
                observer.Subscribe(testExecutionProvider);
            }
        }

        [BeforeScenario(Order = 1)]
        public void PreBeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);

            try
            {
                TestExecutionProvider.PreBeforeScenario(featureContext.FeatureInfo.Title, scenarioContext.ScenarioInfo.Title, featureContext.FeatureInfo.Tags.ToList(), scenarioContext.ScenarioInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeFeatureFailed(ex);
                throw;
            }
        }

        [BeforeScenario(Order = 100)]
        public void PostBeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);

            try
            {
                TestExecutionProvider.PostBeforeScenario(featureContext.FeatureInfo.Title, scenarioContext.ScenarioInfo.Title, featureContext.FeatureInfo.Tags.ToList(), scenarioContext.ScenarioInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeFeatureFailed(ex);
                throw;
            }
        }

        [AfterScenario(Order = 1)]
        public void PreAfterScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);

            try
            {
                TestOutcome testOutcome = GetTestOutcomeFromScenarioExecutionStatus(scenarioContext.ScenarioExecutionStatus);
                string consoleOutput = _stringWriter.ToString();
                string stackTrace = ThrownException?.Value?.ToString();
                TestExecutionProvider.PreAfterScenario(testOutcome, featureContext.FeatureInfo.Title, scenarioContext.ScenarioInfo.Title, featureContext.FeatureInfo.Tags.ToList(), scenarioContext.ScenarioInfo.Tags.ToList(), consoleOutput, stackTrace);
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeFeatureFailed(ex);
                throw;
            }
        }

        [AfterScenario(Order = 100)]
        public void PostAfterScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);

            try
            {
                TestOutcome testOutcome = GetTestOutcomeFromScenarioExecutionStatus(scenarioContext.ScenarioExecutionStatus);
                string consoleOutput = _stringWriter.ToString();
                string stackTrace = ThrownException?.Value?.ToString();
                TestExecutionProvider.PostAfterScenario(testOutcome, featureContext.FeatureInfo.Title, scenarioContext.ScenarioInfo.Title, featureContext.FeatureInfo.Tags.ToList(), scenarioContext.ScenarioInfo.Tags.ToList(), consoleOutput, stackTrace);
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeFeatureFailed(ex);
                throw;
            }
        }

        private TestOutcome GetTestOutcomeFromScenarioExecutionStatus(ScenarioExecutionStatus scenarioExecutionStatus)
        {
            if (scenarioExecutionStatus == ScenarioExecutionStatus.OK)
            {
                return TestOutcome.Passed;
            }
            else if (scenarioExecutionStatus == ScenarioExecutionStatus.TestError)
            {
                return TestOutcome.Failed;
            }

            return TestOutcome.Unknown;
        }
    }
}