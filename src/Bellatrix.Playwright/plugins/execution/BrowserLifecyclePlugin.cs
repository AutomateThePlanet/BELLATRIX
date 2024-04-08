// <copyright file="BrowserWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using Bellatrix.Plugins;
using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.Services;
using Bellatrix.Playwright.Settings;
using Bellatrix.Playwright.Proxy;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.plugins.execution.Attributes;

namespace Bellatrix.Playwright.Plugins.Browser;

public class BrowserLifecyclePlugin : Plugin
{
    protected override void PreTestsArrange(object sender, PluginEventArgs e)
    {
        if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.IsCloudRun)
        {
            e.Container.RegisterInstance(false, "_isBrowserStartedDuringPreTestsArrange");
            return;
        }

        // Resolve required data for decision making
        var currentBrowserConfiguration = GetCurrentBrowserConfiguration(e);

        if (currentBrowserConfiguration != null)
        {
            ResolvePreviousBrowserType(e.Container);

            // Decide whether the browser needs to be restarted
            bool shouldRestartBrowser = ShouldRestartBrowser(e.Container);

            if (shouldRestartBrowser)
            {
                RestartBrowser(e.Container);
                e.Container.RegisterInstance(true, "_isBrowserStartedDuringPreTestsArrange");
            }
        }
        else
        {
            e.Container.RegisterInstance(false, "_isBrowserStartedDuringPreTestsArrange");
        }

        base.PreTestsArrange(sender, e);
    }

    protected override void PreTestInit(object sender, PluginEventArgs e)
    {
        bool isBrowserStartedDuringPreTestsArrange = e.Container.Resolve<bool>("_isBrowserStartedDuringPreTestsArrange");
        if (!isBrowserStartedDuringPreTestsArrange)
        {
            // Resolve required data for decision making
            var currentBrowserConfiguration = GetCurrentBrowserConfiguration(e);
            if (currentBrowserConfiguration != null)
            {
                ResolvePreviousBrowserType(e.Container);

                // Decide whether the browser needs to be restarted
                bool shouldRestartBrowser = ShouldRestartBrowser(e.Container);

                if (shouldRestartBrowser)
                {
                    RestartBrowser(e.Container);
                }
            }
        }

        e.Container.RegisterInstance(false, "_isBrowserStartedDuringPreTestsArrange");
        base.PreTestInit(sender, e);
    }

    protected override void PostTestCleanup(object sender, PluginEventArgs e)
    {
        var currentBrowserConfiguration = GetCurrentBrowserConfiguration(e);
        if (currentBrowserConfiguration != null)
        {
            if (currentBrowserConfiguration.ShouldCaptureHttpTraffic)
            {
                var proxyService = e.Container.Resolve<ProxyService>();
                if (proxyService != null)
                {
                    proxyService.RequestsHistory.Clear();
                    proxyService.ResponsesHistory.Clear();
                }
            }

            if (currentBrowserConfiguration.BrowserBehavior == Lifecycle.RestartEveryTime || (currentBrowserConfiguration.BrowserBehavior == Lifecycle.RestartOnFail && !e.TestOutcome.Equals(TestOutcome.Passed)))
            {
                ShutdownBrowser(e.Container);
                e.Container.RegisterInstance(false, "_isAppStartedDuringPreTestsArrange");
            }
            
            if (currentBrowserConfiguration.BrowserBehavior == Lifecycle.ReuseIfStarted)
            {
                WrappedBrowserCreateService.InitializeBrowserContextAndPage(e.Container.Resolve<WrappedBrowser>());
            }
        }
    }

    private bool ShouldRestartBrowser(ServicesCollection container)
    {
        if (ConfigurationService.GetSection<WebSettings>().ExecutionSettings.IsCloudRun)
        {
            return true;
        }

        bool shouldRestartBrowser = false;
        var previousTestExecutionEngine = container.Resolve<TestExecutionEngine>();
        var previousBrowserConfiguration = container.Resolve<BrowserConfiguration>("_previousBrowserConfiguration");
        var currentBrowserConfiguration = container.Resolve<BrowserConfiguration>("_currentBrowserConfiguration");
        if (previousTestExecutionEngine == null || !previousTestExecutionEngine.IsBrowserStartedCorrectly || !currentBrowserConfiguration.Equals(previousBrowserConfiguration))
        {
            shouldRestartBrowser = true;
        }

        return shouldRestartBrowser;
    }

    private void RestartBrowser(ServicesCollection container)
    {
        var currentBrowserConfiguration = container.Resolve<BrowserConfiguration>("_currentBrowserConfiguration");

        ShutdownBrowser(container);

        // Register the ExecutionEngine that should be used for the current run. Will be used in the next test as PreviousEngineType.
        var testExecutionEngine = new TestExecutionEngine();
        container.RegisterInstance(testExecutionEngine);

        // Register the Browser type that should be used for the current run. Will be used in the next test as PreviousBrowserType.
        container.RegisterInstance(currentBrowserConfiguration);

        // Start the current engine with current browser type.
        testExecutionEngine.StartBrowser(currentBrowserConfiguration, container);
    }

    private void ShutdownBrowser(ServicesCollection container)
    {
        // Disposing existing engine call only dispose if in parallel.
        var previousTestExecutionEngine = container.Resolve<TestExecutionEngine>();
        previousTestExecutionEngine?.DisposeAll();
        
        bool shouldScrollToVisible = ConfigurationService.GetSection<WebSettings>().ShouldAutomaticallyScrollToVisible;
        var browserConfiguration = new BrowserConfiguration(BrowserTypes.NotSet, false, false, shouldScrollToVisible);
        container.RegisterInstance(browserConfiguration, "_previousBrowserConfiguration");
        container.UnregisterSingleInstance<TestExecutionEngine>();
    }

    private void ResolvePreviousBrowserType(ServicesCollection container)
    {
        bool shouldScrollToVisible = ConfigurationService.GetSection<WebSettings>().ShouldAutomaticallyScrollToVisible;
        var browserConfiguration = new BrowserConfiguration(BrowserTypes.NotSet, false, false, shouldScrollToVisible);
        if (container.IsRegistered<BrowserConfiguration>())
        {
            browserConfiguration = container.Resolve<BrowserConfiguration>();
        }

        container.RegisterInstance(browserConfiguration, "_previousBrowserConfiguration");
    }

    private BrowserConfiguration GetCurrentBrowserConfiguration(Bellatrix.Plugins.PluginEventArgs e)
    {
        var browserAttribute = GetBrowserAttribute(e.TestMethodMemberInfo, e.TestClassType);
        string fullClassName = e.TestClassType.FullName;

        if (browserAttribute != null)
        {
            BrowserTypes currentBrowserType = browserAttribute.Browser;

            Lifecycle currentLifecycle = browserAttribute.Lifecycle;
            bool shouldCaptureHttpTraffic = browserAttribute.ShouldCaptureHttpTraffic;
            bool shouldDisableJavaScript = browserAttribute.ShouldDisableJavaScript;
            bool shouldAutomaticallyScrollToVisible = browserAttribute.ShouldAutomaticallyScrollToVisible;
            Size currentBrowserSize = browserAttribute.Size;
            ExecutionType executionType = browserAttribute.ExecutionType;
            var options = (browserAttribute as IBrowserOptionsAttribute)?.CreateOptions(e.TestMethodMemberInfo, e.TestClassType) ?? null;

            var browserConfiguration = new BrowserConfiguration(executionType, currentLifecycle, currentBrowserType, currentBrowserSize, fullClassName, shouldCaptureHttpTraffic, shouldDisableJavaScript, shouldAutomaticallyScrollToVisible, options);
            e.Container.RegisterInstance(browserConfiguration, "_currentBrowserConfiguration");

            return browserConfiguration;
        }
        else
        {
            BrowserTypes currentBrowserType = Parse<BrowserTypes>(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.DefaultBrowser);

            if (e.Arguments != null & e.Arguments.Any())
            {
                if (e.Arguments[0] is BrowserTypes)
                {
                    currentBrowserType = (BrowserTypes)e.Arguments[0];
                }
            }

            Lifecycle currentLifecycle = Parse<Lifecycle>(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.DefaultLifeCycle);

            Size currentBrowserSize = default;
            if (!string.IsNullOrEmpty(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Resolution))
            {
                currentBrowserSize = WindowsSizeResolver.GetWindowSize(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Resolution);
            }

            ExecutionType executionType = Parse<ExecutionType>(ConfigurationService.GetSection<WebSettings>().ExecutionSettings.ExecutionType);
            bool shouldCaptureHttpTraffic = ConfigurationService.GetSection<WebSettings>().ShouldCaptureHttpTraffic;
            bool shouldAutomaticallyScrollToVisible = ConfigurationService.GetSection<WebSettings>().ShouldAutomaticallyScrollToVisible;

            string testName = e.TestFullName != null ? e.TestFullName.Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace(",", string.Empty).Replace("\"", string.Empty) : e.TestClassType.FullName;

            var browserConfiguration = new BrowserConfiguration(executionType, currentLifecycle, currentBrowserType, currentBrowserSize, fullClassName, shouldCaptureHttpTraffic: false, shouldDisableJavaScript: false, shouldAutomaticallyScrollToVisible);
            e.Container.RegisterInstance(browserConfiguration, "_currentBrowserConfiguration");

            return browserConfiguration;
        }
    }

    private TEnum Parse<TEnum>(string value)
     where TEnum : struct
    {
        return (TEnum)Enum.Parse(typeof(TEnum), value.Replace(" ", string.Empty), true);
    }

    private BrowserAttribute GetBrowserAttribute(MemberInfo memberInfo, Type testClassType)
    {
        var currentPlatform = DetermineOS();

        var methodBrowserAttribute = memberInfo?.GetCustomAttributes<BrowserAttribute>(true).FirstOrDefault(x => x.OS.Equals(currentPlatform));
        var classBrowserAttribute = testClassType.GetCustomAttributes<BrowserAttribute>(true).FirstOrDefault(x => x.OS.Equals(currentPlatform));

        int? appMethodsAttributesCount = memberInfo?.GetCustomAttributes<BrowserAttribute>(true).Count();
        int appClassAttributesCount = testClassType.GetCustomAttributes<BrowserAttribute>(true).Count();

        if (methodBrowserAttribute != null && appMethodsAttributesCount == 1)
        {
            methodBrowserAttribute.OS = currentPlatform;
        }

        if (appClassAttributesCount == 1 && classBrowserAttribute != null)
        {
            classBrowserAttribute.OS = currentPlatform;
        }

        if (methodBrowserAttribute != null)
        {
            return methodBrowserAttribute;
        }
        else
        {
            return classBrowserAttribute;
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