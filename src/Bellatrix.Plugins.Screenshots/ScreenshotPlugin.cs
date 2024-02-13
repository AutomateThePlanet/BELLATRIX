// <copyright file="ScreenshotWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Screenshots.Contracts;
using Bellatrix.Plugins.Screenshots.Plugins;
using Serilog;

namespace Bellatrix.Plugins.Screenshots;

public class ScreenshotPlugin : Plugin
{
    private readonly IScreenshotEngine _screenshotEngine;
    private readonly IScreenshotOutputProvider _screenshotOutputProvider;
    private readonly IScreenshotPluginProvider _screenshotPluginProvider;
    private bool _isEnabled;

    public ScreenshotPlugin(
        IScreenshotEngine screenshotEngine,
        IScreenshotOutputProvider screenshotOutputProvider,
        IScreenshotPluginProvider screenshotPluginProvider)
    {
        _isEnabled = ConfigurationService.GetSection<ScreenshotsSettings>().IsEnabled;
        _screenshotEngine = screenshotEngine;
        _screenshotOutputProvider = screenshotOutputProvider;
        _screenshotPluginProvider = screenshotPluginProvider;
        InitializeScreenshotProviderObservers();
    }

    protected override void PreTestCleanup(object sender, PluginEventArgs e)
    {
        try
        {
            if (_isEnabled && e.TestOutcome != TestOutcome.Passed)
            {
                TakeScreenshotAndSaveAsFile(e);
            }
        }
        catch (Exception ex)
        {
            Logger.LogWarning("Failed to take screenshot due to error: " + ex.Message);

            // Ignore since it is failing often because of bugs in Remote driver for Chrome
        }

        base.PreTestCleanup(sender, e);
    }

    protected override void TestCleanupFailed(object sender, PluginEventArgs e)
    {
        TakeScreenshotAndSaveAsFile(e);

        base.TestCleanupFailed(sender, e);
    }

    private void TakeScreenshotAndSaveAsFile(PluginEventArgs e)
    {
        var screenshotSaveDir = _screenshotOutputProvider.GetOutputFolder();
        var screenshotFileName = _screenshotOutputProvider.GetUniqueFileName(e.TestName);
        string image = _screenshotEngine.TakeScreenshot(e.Container);
        string imagePath = Path.Combine(screenshotSaveDir, screenshotFileName);
        File.WriteAllBytes(imagePath, Convert.FromBase64String(image));
        _screenshotPluginProvider.ScreenshotGenerated(e, imagePath);
    }

    private void InitializeScreenshotProviderObservers()
    {
        var observers = ServicesCollection.Current.ResolveAll<IScreenshotPlugin>();
        foreach (var observer in observers)
        {
            observer.SubscribeScreenshotPlugin(_screenshotPluginProvider);
        }
    }
}