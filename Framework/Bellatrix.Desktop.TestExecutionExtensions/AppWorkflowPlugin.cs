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
using System;
using System.Linq;
using System.Reflection;
using Bellatrix.Desktop.Configuration;
using Bellatrix.TestWorkflowPlugins;

namespace Bellatrix.Desktop.TestExecutionExtensions
{
    public class AppWorkflowPlugin : TestWorkflowPlugin
    {
        protected override void PreTestsArrange(object sender, TestWorkflowPluginEventArgs e)
        {
            if (e.TestClassType.GetCustomAttributes().Any(x => x.GetType().Equals(typeof(AppAttribute)) || x.GetType().IsSubclassOf(typeof(AppAttribute))))
            {
                var appConfiguration = GetCurrentAppConfiguration(e.TestMethodMemberInfo, e.TestClassType, e.Container);

                if (appConfiguration != null)
                {
                    ResolvePreviousAppConfiguration(e.Container);

                    bool shouldRestartApp = ShouldRestartApp(e.Container);

                    if (shouldRestartApp)
                    {
                        RestartApp(e.Container);
                    }
                }
                else
                {
                    e.Container.RegisterInstance(false, "_isAppStartedDuringPreTestsArrange");
                }
            }

            base.PreTestsArrange(sender, e);
        }

        protected override void PreTestInit(object sender, TestWorkflowPluginEventArgs e)
        {
            bool isappStartedDuringPreTestsArrange = e.Container.Resolve<bool>("_isAppStartedDuringPreTestsArrange");
            if (!isappStartedDuringPreTestsArrange)
            {
                var appConfiguration = GetCurrentAppConfiguration(e.TestMethodMemberInfo, e.TestClassType, e.Container);
                if (appConfiguration != null)
                {
                    ResolvePreviousAppConfiguration(e.Container);
                    bool shouldRestartApp = ShouldRestartApp(e.Container);

                    if (shouldRestartApp)
                    {
                        RestartApp(e.Container);
                    }
                }
            }

            e.Container.RegisterInstance(false, "_isAppStartedDuringPreTestsArrange");
            base.PreTestInit(sender, e);
        }

        protected override void PostTestCleanup(object sender, TestWorkflowPluginEventArgs e)
        {
            var appConfiguration = GetCurrentAppConfiguration(e.TestMethodMemberInfo, e.TestClassType, e.Container);

            if (appConfiguration?.AppBehavior == AppBehavior.RestartOnFail && e.TestOutcome.Equals(TestOutcome.Failed))
            {
                ShutdownApp(e.Container);
            }
        }

        private bool ShouldRestartApp(ServicesCollection container)
        {
            bool shouldRestartApp = false;
            var previousTestExecutionEngine = container.Resolve<TestExecutionEngine>();
            var previousAppConfiguration = container.Resolve<AppConfiguration>("_previousAppConfiguration");
            var currentAppConfiguration = container.Resolve<AppConfiguration>("_currentAppConfiguration");
            if (previousTestExecutionEngine == null ||
                currentAppConfiguration.AppBehavior == AppBehavior.RestartEveryTime ||
                previousAppConfiguration.AppBehavior == AppBehavior.NotSet ||
                !previousTestExecutionEngine.IsAppStartedCorrectly)
            {
                shouldRestartApp = true;
            }
            else if (!currentAppConfiguration.Equals(previousAppConfiguration))
            {
                shouldRestartApp = true;
            }

            return shouldRestartApp;
        }

        private void RestartApp(ServicesCollection container)
        {
            var currentAppConfiguration = container.Resolve<AppConfiguration>("_currentAppConfiguration");

            ShutdownApp(container);

            // Register the ExecutionEngine that should be used for the current run. Will be used in the next test as PreviousEngineType.
            var testExecutionEngine = new TestExecutionEngine();
            container.RegisterInstance(testExecutionEngine);

            // Register the app that should be used for the current run. Will be used in the next test as PreviousappType.
            container.RegisterInstance(currentAppConfiguration);

            // Start the current engine
            testExecutionEngine.StartApp(currentAppConfiguration, container);
        }

        private void ShutdownApp(ServicesCollection container)
        {
            // Disposing existing engine call only dispose if in parallel.
            var previousTestExecutionEngine = container.Resolve<TestExecutionEngine>();
            previousTestExecutionEngine?.DisposeAll();

            var appConfiguration = new AppConfiguration();
            container.RegisterInstance(appConfiguration, "_previousAppConfiguration");
            container.UnregisterSingleInstance<TestExecutionEngine>();
        }

        private void ResolvePreviousAppConfiguration(ServicesCollection childContainer)
        {
            var appConfiguration = new AppConfiguration();
            if (childContainer.IsRegistered<AppConfiguration>())
            {
                appConfiguration = childContainer.Resolve<AppConfiguration>();
            }

            childContainer.RegisterInstance(appConfiguration, "_previousAppConfiguration");
        }

        private AppConfiguration GetCurrentAppConfiguration(MemberInfo memberInfo, Type testClassType, ServicesCollection container)
        {
            var appAttribute = GetAppAttribute(memberInfo, testClassType);
            if (appAttribute != null)
            {
                container.RegisterInstance(appAttribute.AppConfiguration, "_currentAppConfiguration");
                return appAttribute.AppConfiguration;
            }
            else
            {
                container.RegisterInstance(default(AppConfiguration), "_currentAppConfiguration");
                return null;
            }
        }

        private AppAttribute GetAppAttribute(MemberInfo memberInfo, Type testClassType)
        {
            if (memberInfo != null)
            {
                var methodappAttribute = memberInfo.GetCustomAttribute<AppAttribute>(true);

                if (methodappAttribute != null)
                {
                    return methodappAttribute;
                }
            }

            var classappAttribute = testClassType.GetCustomAttribute<AppAttribute>(true);
            return classappAttribute;
        }
    }
}
