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
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Bellatrix.Mobile;
using Bellatrix.Mobile.Configuration;
using Bellatrix.SpecFlow.TestWorkflowPlugins;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Mobile.TestExecutionExtensions
{
    public partial class AppWorkflowPlugin : TestWorkflowPlugin
    {
        private AppConfiguration _currentAppConfiguration;
        private AppConfiguration _previousAppConfiguration;
        private AndroidSauceLabsAppConfiguration _androidSauceLabsAppConfiguration;
        private AndroidCrossBrowserTestingAppConfiguration _androidCrossBrowserTestingAppConfiguration;
        private AndroidBrowserStackAppConfiguration _androidBrowserStackAppConfiguration;
        private IOSSauceLabsAppConfiguration _iosSauceLabsAppConfiguration;
        private IOSCrossBrowserTestingAppConfiguration _iosCrossBrowserTestingAppConfiguration;
        private IOSBrowserStackAppConfiguration _iosBrowserStackAppConfiguration;

        public AppWorkflowPlugin()
        {
            InitializeAppConfigurations();
        }

        [Given(@"I open app")]
        public void OpenApp()
        {
            // Resolve required data for decision making
            if (_currentAppConfiguration != null)
            {
                ResolvePreviousAppConfiguration();

                // Decide whether the browser needs to be restarted
                bool shouldRestartApp = ShouldRestartApp();

                if (shouldRestartApp)
                {
                    RestartApp();
                }
            }
        }

        protected override void PostAfterScenario(object sender, TestWorkflowPluginEventArgs e)
        {
            if (_currentAppConfiguration?.AppBehavior == AppBehavior.RestartOnFail && !e.TestOutcome.Equals(TestOutcome.Passed))
            {
                RestartApp();
            }
        }

        private void InitializeAppConfigurations()
        {
            _currentAppConfiguration = new AppConfiguration(DetermineOS());
            _androidSauceLabsAppConfiguration = new AndroidSauceLabsAppConfiguration
                                                {
                                                    OSPlatform = DetermineOS(),
                                                };
            _androidCrossBrowserTestingAppConfiguration = new AndroidCrossBrowserTestingAppConfiguration
                                                          {
                                                              OSPlatform = DetermineOS(),
                                                          };
            _androidBrowserStackAppConfiguration = new AndroidBrowserStackAppConfiguration
                                                   {
                                                       OSPlatform = DetermineOS(),
                                                   };
            _iosSauceLabsAppConfiguration = new IOSSauceLabsAppConfiguration
                                            {
                                                OSPlatform = DetermineOS(),
                                            };
            _iosCrossBrowserTestingAppConfiguration = new IOSCrossBrowserTestingAppConfiguration
                                                      {
                                                          OSPlatform = DetermineOS(),
                                                      };
            _iosBrowserStackAppConfiguration = new IOSBrowserStackAppConfiguration
                                               {
                                                   OSPlatform = DetermineOS(),
                                               };
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
            var previousTestExecutionEngine = ServicesCollection.Current.Resolve<Bellatrix.Mobile.TestExecutionEngine>();
            previousTestExecutionEngine.Dispose(ServicesCollection.Current);
            ServicesCollection.Current.UnregisterSingleInstance<TestExecutionEngine>();

            // Register the ExecutionEngine that should be used for the current run. Will be used in the next test as PreviousEngineType.
            var testExecutionEngine = new TestExecutionEngine();
            ServicesCollection.Current.RegisterInstance(testExecutionEngine);

            string classFullName = DetermineTestClassFullName();
            _androidBrowserStackAppConfiguration.InitializeAppiumOptions(classFullName);
            _androidCrossBrowserTestingAppConfiguration.InitializeAppiumOptions(classFullName);
            _androidSauceLabsAppConfiguration.InitializeAppiumOptions(classFullName);
            _iosBrowserStackAppConfiguration.InitializeAppiumOptions(classFullName);
            _iosCrossBrowserTestingAppConfiguration.InitializeAppiumOptions(classFullName);
            _iosSauceLabsAppConfiguration.InitializeAppiumOptions(classFullName);

            // Register the Browser type that should be used for the current run. Will be used in the next test as PreviousBrowserType.
            ServicesCollection.Current.RegisterInstance(_currentAppConfiguration);

            // Start the current engine with current browser type.
            testExecutionEngine.StartApp(_currentAppConfiguration, ServicesCollection.Current);
        }

        private void ResolvePreviousAppConfiguration()
        {
            var browserConfiguration = new AppConfiguration();
            if (ServicesCollection.Current.IsRegistered<AppConfiguration>())
            {
                browserConfiguration = ServicesCollection.Current.Resolve<AppConfiguration>();
            }

            _previousAppConfiguration = browserConfiguration;
        }

        private string DetermineTestClassFullName()
        {
            string classFullName = string.Empty;
            var callStackTrace = new StackTrace();
            var currentAssembly = GetType().Assembly;

            foreach (var frame in callStackTrace.GetFrames())
            {
                var frameMethodInfo = frame.GetMethod() as MethodInfo;
                if (!frameMethodInfo?.ReflectedType?.Assembly.Equals(currentAssembly) == true && (frameMethodInfo.Name.Equals("ScenarioInitialize") || frameMethodInfo.Name.Equals("ScenarioStart")))
                {
                    classFullName = frameMethodInfo.DeclaringType.FullName;
                    break;
                }
            }

            return classFullName;
        }

        private OS DetermineOS()
        {
            var result = OS.Windows;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                result = OS.OSX;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                result = OS.Linux;
            }

            return result;
        }
    }
}
