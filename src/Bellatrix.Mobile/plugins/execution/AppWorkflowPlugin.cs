// <copyright file="AppWorkflowPlugin.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using System.Runtime.InteropServices;
using Bellatrix.Mobile.Configuration;
using Bellatrix.Plugins;

namespace Bellatrix.Mobile.Plugins
{
    public class AppWorkflowPlugin : Plugin
    {
        protected override void PreTestsArrange(object sender, PluginEventArgs e)
        {
            if (e.TestClassType.GetCustomAttributes().Any(x => x.GetType().Equals(typeof(AppAttribute)) || x.GetType().IsSubclassOf(typeof(AppAttribute))))
            {
                // Resolve required data for decision making
                var appConfiguration = GetCurrentAppConfiguration(e.TestMethodMemberInfo, e.TestClassType, e.Container);

                if (appConfiguration != null)
                {
                    ResolvePreviousAppConfiguration(e.Container);

                    // Decide whether the app needs to be restarted
                    bool shouldRestartApp = ShouldRestartApp(e.Container);

                    if (shouldRestartApp)
                    {
                        RestartApp(e.Container);
                        e.Container.RegisterInstance(true, "_isAppStartedDuringPreTestsArrange");
                    }
                }
                else
                {
                    e.Container.RegisterInstance(true, "_isAppStartedDuringPreTestsArrange");
                }
            }

            base.PreTestsArrange(sender, e);
        }

        protected override void PreTestInit(object sender, PluginEventArgs e)
        {
            bool isAppStartedDuringPreTestsArrange = e.Container.Resolve<bool>("_isAppStartedDuringPreTestsArrange");
            if (!isAppStartedDuringPreTestsArrange)
            {
                // Resolve required data for decision making
                var appConfiguration = GetCurrentAppConfiguration(e.TestMethodMemberInfo, e.TestClassType, e.Container);
                if (appConfiguration != null)
                {
                    ResolvePreviousAppConfiguration(e.Container);

                    // Decide whether the app needs to be restarted
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

        protected override void PostTestCleanup(object sender, PluginEventArgs e)
        {
            var appConfiguration = GetCurrentAppConfiguration(e.TestMethodMemberInfo, e.TestClassType, e.Container);

            if (appConfiguration?.Lifecycle == Lifecycle.RestartEveryTime || (appConfiguration?.Lifecycle == Lifecycle.RestartOnFail && !e.TestOutcome.Equals(TestOutcome.Passed)))
            {
                ShutdownApp(e.Container);
                e.Container.RegisterInstance(false, "_isAppStartedDuringPreTestsArrange");
            }
        }

        private bool ShouldRestartApp(ServicesCollection container)
        {
            bool shouldRestartApp = false;
            var previousTestExecutionEngine = container.Resolve<TestExecutionEngine>();
            var previousAppConfiguration = container.Resolve<AppConfiguration>("_previousAppConfiguration");
            var currentAppConfiguration = container.Resolve<AppConfiguration>("_currentAppConfiguration");
            if (previousTestExecutionEngine == null || !previousTestExecutionEngine.IsAppStartedCorrectly || !currentAppConfiguration.Equals(previousAppConfiguration))
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
            var currentAppConfiguration = container.Resolve<AppConfiguration>("_currentAppConfiguration");

            ShutdownApp(container);

            // Register the ExecutionEngine that should be used for the current run. Will be used in the next test as PreviousEngineType.
            var testExecutionEngine = new TestExecutionEngine();

            // Register the app that should be used for the current run. Will be used in the next test as PreviousappType.
            container.RegisterInstance(currentAppConfiguration);

            // Start the current engine
            testExecutionEngine.StartApp(currentAppConfiguration, container);
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
            var currentAppConfiguration = default(AppConfiguration);
            var androidAttribute = GetAppAttribute<AndroidAttribute>(memberInfo, testClassType);
            var iosAttribute = GetAppAttribute<IOSAttribute>(memberInfo, testClassType);
            var androidWebAttribute = GetAppAttribute<AndroidWebAttribute>(memberInfo, testClassType);
            var androidSauceLabsAttribute = GetAppAttribute<AndroidSauceLabsAttribute>(memberInfo, testClassType);
            var androidCrossappTestingAttribute = GetAppAttribute<AndroidCrossBrowserTestingAttribute>(memberInfo, testClassType);
            var androidappStackAttribute = GetAppAttribute<AndroidBrowserStackAttribute>(memberInfo, testClassType);

            var iosWebAttribute = GetAppAttribute<IOSWebAttribute>(memberInfo, testClassType);
            var iosSauceLabsAttribute = GetAppAttribute<IOSSauceLabsAttribute>(memberInfo, testClassType);
            var iosCrossappTestingAttribute = GetAppAttribute<IOSCrossBrowserTestingAttribute>(memberInfo, testClassType);
            var iosappStackAttribute = GetAppAttribute<IOSBrowserStackAttribute>(memberInfo, testClassType);

            if (androidAttribute != null && iosAttribute != null)
            {
                throw new ArgumentException("You need to specify only single platform attribute - Android or IOS.");
            }
            else if (androidAttribute != null)
            {
                currentAppConfiguration = androidAttribute.AppConfiguration;
            }
            else if (androidWebAttribute != null)
            {
                currentAppConfiguration = androidWebAttribute.AppConfiguration;
            }
            else if (androidSauceLabsAttribute != null)
            {
                currentAppConfiguration = androidSauceLabsAttribute.AppConfiguration;
                currentAppConfiguration.AppiumOptions = androidSauceLabsAttribute.CreateAppiumOptions(memberInfo, testClassType);
            }
            else if (androidCrossappTestingAttribute != null)
            {
                currentAppConfiguration = androidCrossappTestingAttribute.AppConfiguration;
                currentAppConfiguration.AppiumOptions = androidCrossappTestingAttribute.CreateAppiumOptions(memberInfo, testClassType);
            }
            else if (androidappStackAttribute != null)
            {
                currentAppConfiguration = androidappStackAttribute.AppConfiguration;
                currentAppConfiguration.AppiumOptions = androidappStackAttribute.CreateAppiumOptions(memberInfo, testClassType);
            }
            else if (iosAttribute != null)
            {
                currentAppConfiguration = iosAttribute.AppConfiguration;
            }
            else if (iosWebAttribute != null)
            {
                currentAppConfiguration = iosWebAttribute.AppConfiguration;
            }
            else if (iosSauceLabsAttribute != null)
            {
                currentAppConfiguration = iosSauceLabsAttribute.AppConfiguration;
                currentAppConfiguration.AppiumOptions = iosSauceLabsAttribute.CreateAppiumOptions(memberInfo, testClassType);
            }
            else if (iosCrossappTestingAttribute != null)
            {
                currentAppConfiguration = iosCrossappTestingAttribute.AppConfiguration;
                currentAppConfiguration.AppiumOptions = iosCrossappTestingAttribute.CreateAppiumOptions(memberInfo, testClassType);
            }
            else if (iosappStackAttribute != null)
            {
                currentAppConfiguration = iosappStackAttribute.AppConfiguration;
                currentAppConfiguration.AppiumOptions = iosappStackAttribute.CreateAppiumOptions(memberInfo, testClassType);
            }
            else
            {
                throw new ArgumentException("To run BELLATRIX mobile tests you need to add Android or IOS attributes over your test class.");
            }

            container.RegisterInstance(currentAppConfiguration, "_currentAppConfiguration");
            return currentAppConfiguration;
        }

        private TAppAttribute GetAppAttribute<TAppAttribute>(MemberInfo memberInfo, Type testClassType)
            where TAppAttribute : AppAttribute
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException();
            }

            var currentPlatform = DetermineOS();

            var methodappAttribute = memberInfo.GetCustomAttributes<TAppAttribute>(true).FirstOrDefault(x => x.AppConfiguration.OSPlatform.Equals(currentPlatform));
            var classappAttribute = testClassType.GetCustomAttributes<TAppAttribute>(true).FirstOrDefault(x => x.AppConfiguration.OSPlatform.Equals(currentPlatform));

            int appMethodsAttributesCount = memberInfo.GetCustomAttributes<TAppAttribute>(true).Count();
            int appClassAttributesCount = testClassType.GetCustomAttributes<TAppAttribute>(true).Count();

            if (appMethodsAttributesCount == 1 && methodappAttribute != null)
            {
                methodappAttribute.AppConfiguration.OSPlatform = currentPlatform;
            }

            if (appClassAttributesCount == 1 && classappAttribute != null)
            {
                classappAttribute.AppConfiguration.OSPlatform = currentPlatform;
            }

            if (methodappAttribute != null)
            {
                return methodappAttribute;
            }
            else
            {
                return classappAttribute;
            }
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
