// <copyright file="SmartFailureAnalysisPlugin.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
// <note>This file is part of an academic research project exploring autonomous test agents using LLMs and Semantic Kernel.
// The architecture and agent logic are original contributions by Anton Angelov, forming the foundation for a PhD dissertation.
// Please cite or credit appropriately if reusing in academic or commercial work.</note>

using Bellatrix.Plugins.Screenshots.Contracts;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Screenshots.Plugins;
using Bellatrix.Plugins.Screenshots;

namespace Bellatrix.LLM.Plugins;
public class SmartFailureAnalysisPlugin : Plugin, IScreenshotPlugin
{
    private readonly IScreenshotOutputProvider _screenshotOutputProvider;
    private readonly IViewSnapshotProvider _viewSnapshotProvider;
    private static ThreadLocal<string> _screenshotPath = new ThreadLocal<string>();


    public SmartFailureAnalysisPlugin()
    {
        _screenshotOutputProvider = ServicesCollection.Current.Resolve<IScreenshotOutputProvider>();
        _viewSnapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
    }

    public static void Add()
    {
        ServicesCollection.Current.RegisterType<Plugin, SmartFailureAnalysisPlugin>(Guid.NewGuid().ToString());
        ServicesCollection.Current.RegisterType<IScreenshotPlugin, SmartFailureAnalysisPlugin>(Guid.NewGuid().ToString());
    }

    public void SubscribeScreenshotPlugin(IScreenshotPluginProvider provider)
    {
        provider.ScreenshotGeneratedEvent += ScreenshotGenerated;
    }

    public void UnsubscribeScreenshotPlugin(IScreenshotPluginProvider provider)
    {
        provider.ScreenshotGeneratedEvent -= ScreenshotGenerated;
    }

    public void ScreenshotGenerated(object sender, ScreenshotPluginEventArgs args)
    {
        if (!string.IsNullOrEmpty(args.ScreenshotPath))
        {
            _screenshotPath.Value = args.ScreenshotPath;
        }
    }

    protected override void PreTestCleanup(object sender, PluginEventArgs e)
    {
        if (e.TestOutcome == TestOutcome.Passed)
        {
            var log = Logger.GetLogs();
            var snapshot = _viewSnapshotProvider.GetCurrentViewSnapshot();
            SmartFailureAnalyzer.SaveTestPass(e.TestFullName, log, snapshot);
        }
        else
        {
            RunFailureAnalysisAsync(e);
        }
    }

    private void RunFailureAnalysisAsync(PluginEventArgs e)
    {
        string diagnosis = string.Empty;
        try
        {
            var log = Logger.GetLogs(); // flushless BDD log
            var snapshot = _viewSnapshotProvider.GetCurrentViewSnapshot(); // live page state

            diagnosis = SmartFailureAnalyzer.Diagnose(
                e.TestFullName,
                e.Exception?.ToString() ?? "No exception captured.",
                log,
                snapshot,
                _screenshotPath.Value ?? string.Empty);

          
        }
        catch (Exception ex)
        {
            Logger.LogError($"❌ SmartFailureAnalysisPlugin failed: {ex.Message}");
        }

        // Split diagnosis summary from body (assumes skill returns both)
        var (analysisSummary, extended) = SmartFailureAnalyzer.ExtractSummaryAndBody(diagnosis);

        Logger.LogWarning($"""

🧠 AI-Driven Root Cause Summary:
{diagnosis}
""");

//        Logger.LogWarning($"""

//🧠 AI-Driven Root Cause Summary:
//{analysisSummary}

//🧩 Extended Analysis:
//{extended}
//""");
    }
}
