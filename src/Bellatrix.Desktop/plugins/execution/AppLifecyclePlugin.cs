// <copyright file="AppWorkflowPlugin.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Bellatrix.Desktop.Configuration;
using Bellatrix.Desktop.Services;
using Bellatrix.KeyVault;
using Bellatrix.Plugins;
using Bellatrix.Utilities;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Desktop.Plugins;

public class AppLifecyclePlugin : Plugin
{
    protected override void PostTestsArrange(object sender, PluginEventArgs e)
    {
        var appConfiguration = GetCurrentAppConfiguration(e.TestMethodMemberInfo, e.TestClassType, e.Container);

        if (appConfiguration != null)
        {
            ResolvePreviousAppConfiguration(e.Container);

            bool shouldRestartApp = ShouldRestartApp(e.Container);

            if (shouldRestartApp)
            {
                RestartApp(e.Container);
                e.Container.RegisterInstance(true, "_isAppStartedDuringPreTestsArrange");
            }
        }
        else
        {
            e.Container.RegisterInstance(false, "_isAppStartedDuringPreTestsArrange");
        }

        base.PostTestsArrange(sender, e);
    }

    protected override void PreTestInit(object sender, PluginEventArgs e)
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

    protected override void PostTestsCleanup(object sender, PluginEventArgs e)
    {
        ShutdownApp(e.Container);
    }

    private bool ShouldRestartApp(ServicesCollection container)
    {
        bool shouldRestartApp = false;
        var previousTestExecutionEngine = container.Resolve<TestExecutionEngine>();
        var previousAppConfiguration = container.Resolve<AppInitializationInfo>("_previousAppConfiguration");
        var currentAppConfiguration = container.Resolve<AppInitializationInfo>("_currentAppConfiguration");
        if (currentAppConfiguration?.Lifecycle == Lifecycle.RestartEveryTime || previousTestExecutionEngine == null || !previousTestExecutionEngine.IsAppStartedCorrectly || !currentAppConfiguration.Equals(previousAppConfiguration))
        {
            shouldRestartApp = true;
        }

        return shouldRestartApp;
    }

    private void RestartApp(ServicesCollection container)
    {
        var currentAppConfiguration = container.Resolve<AppInitializationInfo>("_currentAppConfiguration");

        ShutdownApp(container);

        // Register the ExecutionEngine that should be used for the current run. Will be used in the next test as PreviousEngineType.
        var testExecutionEngine = new TestExecutionEngine();
        ////container.RegisterInstance(testExecutionEngine);

        // Register the app that should be used for the current run. Will be used in the next test as PreviousappType.
        container.RegisterInstance(currentAppConfiguration);

        // Start the current engine
        testExecutionEngine.StartApp(currentAppConfiguration, container);
    }

    private void ShutdownApp(ServicesCollection container)
    {
        DisposeDriverService.Dispose(container);
        var appConfiguration = new AppInitializationInfo();

        // BUG: If we use ReuseIfStarted, there is a new child container for each class and when
        // we initialize a new childcontainer the _previousAppConfiguration is missing and the app
        // is still opened. Probably this won't work in parallel.
        container.RegisterInstance(appConfiguration, "_previousAppConfiguration");
        ////container.UnregisterSingleInstance<TestExecutionEngine>();
    }

    private void ResolvePreviousAppConfiguration(ServicesCollection childContainer)
    {
        var appConfiguration = new AppInitializationInfo();
        if (childContainer.IsRegistered<AppInitializationInfo>())
        {
            appConfiguration = childContainer.Resolve<AppInitializationInfo>();
        }

        childContainer.RegisterInstance(appConfiguration, "_previousAppConfiguration");
    }

    private AppInitializationInfo GetCurrentAppConfiguration(MemberInfo memberInfo, Type testClassType, ServicesCollection container)
    {
        var appAttribute = GetAppAttribute(memberInfo, testClassType);
        if (appAttribute != null)
        {
            container.RegisterInstance(appAttribute.AppConfiguration, "_currentAppConfiguration");
            appAttribute.AppConfiguration.ClassFullName = testClassType.FullName;
            return appAttribute.AppConfiguration;
        }
        else
        {
            Lifecycle currentLifecycle = Parse<Lifecycle>(ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.DefaultLifeCycle);
            Size currentAppSize = default;
            if (!string.IsNullOrEmpty(ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.Resolution))
            {
                currentAppSize = WindowsSizeResolver.GetWindowSize(ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.Resolution);
            }

            var appConfiguration = new AppInitializationInfo
            {
                AppPath = ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.DefaultAppPath,
                Lifecycle = currentLifecycle,
                Size = currentAppSize,
                AppiumOptions = new DesiredCapabilities(),
                ClassFullName = testClassType.FullName,
            };

            InitializeGridOptionsFromConfiguration(appConfiguration.AppiumOptions, testClassType);
            InitializeCustomCodeOptions(appConfiguration.AppiumOptions, testClassType);

            container.RegisterInstance(appConfiguration, "_currentAppConfiguration");
            return appConfiguration;
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

    private TEnum Parse<TEnum>(string value)
        where TEnum : struct
    {
        return (TEnum)Enum.Parse(typeof(TEnum), value.Replace(" ", string.Empty), true);
    }

    private void InitializeCustomCodeOptions(dynamic options, Type testClassType)
    {
        var customCodeOptions = ServicesCollection.Current.Resolve<Dictionary<string, string>>($"caps-{testClassType.FullName}");
        if (customCodeOptions != null && customCodeOptions.Count > 0)
        {
            foreach (var item in customCodeOptions)
            {
                if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value))
                {
                    options.AddAdditionalCapability(item.Key, FormatGridOptions(item.Value, testClassType));
                }
            }
        }
    }

    private void InitializeGridOptionsFromConfiguration(dynamic options, Type testClassType)
    {
        if (ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.Arguments == null)
        {
            return;
        }

        if (ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.Arguments[0].Count > 0)
        {
            foreach (var item in ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.Arguments[0])
            {
                if (!string.IsNullOrEmpty(item.Key) && !string.IsNullOrEmpty(item.Value))
                {
                    options.AddAdditionalCapability(item.Key, FormatGridOptions(item.Value, testClassType));
                }
            }
        }
    }

    private dynamic FormatGridOptions(string option, Type testClassType)
    {
        if (bool.TryParse(option, out bool result))
        {
            return result;
        }
        else if (int.TryParse(option, out int resultNumber))
        {
            return resultNumber;
        }
        else if (option.StartsWith("env_") || option.StartsWith("vault_"))
        {
            return SecretsResolver.GetSecret(() => option);
        }
        else if (double.TryParse(option, out double resultRealNumber))
        {
            return resultRealNumber;
        }
        else if (option.StartsWith("AssemblyFolder", StringComparison.Ordinal))
        {
            var executionFolder = ExecutionDirectoryResolver.GetDriverExecutablePath();
            option = option.Replace("AssemblyFolder", executionFolder);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                option = option.Replace('\\', '/');
            }

            return option;
        }
        else
        {
            var runName = testClassType.Assembly.GetName().Name;
            var timestamp = $"{DateTime.Now:yyyyMMdd.HHmm}";
            return option.Replace("{runName}", timestamp).Replace("{runName}", runName);
        }
    }
}
