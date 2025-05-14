using Bellatrix.Plugins.Screenshots.Contracts;
using Bellatrix.Plugins;
using Bellatrix.Web.LLM.services;
using Bellatrix.Web.LLM.Extensions;
using Bellatrix.Plugins.Screenshots.Plugins;
using Bellatrix.Plugins.Screenshots;
using System;
using System.Threading;
using Bellatrix.Web.LLM.Plugins;

namespace Bellatrix.Web.LLM.plugins;
public class SmartFailureAnalysisPlugin : Plugin, IScreenshotPlugin
{
    private readonly IScreenshotOutputProvider _screenshotOutputProvider;
    private readonly BrowserService _browserService;
    private static ThreadLocal<string> _screenshotPath = new ThreadLocal<string>();


    public SmartFailureAnalysisPlugin()
    {
        _screenshotOutputProvider = ServicesCollection.Current.Resolve<IScreenshotOutputProvider>();
        _browserService = ServicesCollection.Current.Resolve<BrowserService>();
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
            var summary = _browserService.GetPageSummaryJson();
            SmartFailureAnalyzer.SaveTestPass(e.TestFullName, log, summary);
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
            var pageSummary = _browserService.GetPageSummaryJson(); // live page state

            diagnosis = SmartFailureAnalyzer.Diagnose(
                e.TestFullName,
                e.Exception?.ToString() ?? "No exception captured.",
                log,
                pageSummary,
                _screenshotPath.Value);

          
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
