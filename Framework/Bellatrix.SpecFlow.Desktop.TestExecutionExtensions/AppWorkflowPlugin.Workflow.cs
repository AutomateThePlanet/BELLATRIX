// <copyright file="AppWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop;
using Bellatrix.Desktop.Configuration;
using Bellatrix.SpecFlow.TestWorkflowPlugins;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Desktop.TestExecutionExtensions
{
    public partial class AppWorkflowPlugin : TestWorkflowPlugin
    {
        private readonly AppConfiguration _currentAppConfiguration;
        private AppConfiguration _previousAppConfiguration;

        public AppWorkflowPlugin() => _currentAppConfiguration = new AppConfiguration();

        protected override void PostAfterScenario(object sender, TestWorkflowPluginEventArgs e)
        {
            if (_currentAppConfiguration?.AppBehavior == AppBehavior.RestartOnFail && !e.TestOutcome.Equals(TestOutcome.Passed))
            {
                RestartApp();
            }

            base.PostAfterScenario(sender, e);
        }

        private bool ShouldRestartApp()
        {
            bool shouldRestartBrowser = false;
            var previousTestExecutionEngine = ServicesCollection.Current.Resolve<TestExecutionEngine>();

            if (_currentAppConfiguration.AppBehavior == AppBehavior.RestartEveryTime ||
                string.IsNullOrEmpty(_previousAppConfiguration.AppPath) ||
                !previousTestExecutionEngine.IsAppStartedCorrectly)
            {
                shouldRestartBrowser = true;
            }
            else if (!_currentAppConfiguration.Equals(_previousAppConfiguration))
            {
                shouldRestartBrowser = true;
            }

            return shouldRestartBrowser;
        }

        private void RestartApp()
        {
            // Disposing existing engine
            var previousTestExecutionEngine = ServicesCollection.Current.Resolve<TestExecutionEngine>();
            previousTestExecutionEngine.Dispose(ServicesCollection.Current);
            ServicesCollection.Current.UnregisterSingleInstance<TestExecutionEngine>();

            // Register the ExecutionEngine that should be used for the current run. Will be used in the next test as PreviousEngineType.
            var testExecutionEngine = new TestExecutionEngine();
            ServicesCollection.Current.RegisterInstance(testExecutionEngine);

            // Register the Browser type that should be used for the current run. Will be used in the next test as PreviousBrowserType.
            ServicesCollection.Current.RegisterInstance(_currentAppConfiguration);

            // Start the current engine with current browser type.
            testExecutionEngine.StartApp(_currentAppConfiguration, ServicesCollection.Current);
        }

        private void ResolvePreviousBrowserConfiguration()
        {
            var browserConfiguration = new AppConfiguration();
            if (ServicesCollection.Current.IsRegistered<AppConfiguration>())
            {
                browserConfiguration = ServicesCollection.Current.Resolve<AppConfiguration>();
            }

            _previousAppConfiguration = browserConfiguration;
        }
    }
}
