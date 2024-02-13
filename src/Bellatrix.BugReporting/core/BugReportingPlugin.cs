// <copyright file="BugReportingPlugin.cs" company="Automate The Planet Ltd.">
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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Bellatrix.BugReporting.AzureDevOps;
using Bellatrix.BugReporting.Contracts;
using Bellatrix.BugReporting.Jira;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Screenshots;
using Bellatrix.Plugins.Screenshots.Plugins;
using Bellatrix.Plugins.Video.Plugins;

namespace Bellatrix.BugReporting.Core;

public class BugReportingPlugin : Plugin, IScreenshotPlugin, IVideoPlugin
{
    private static List<string> _filesToBeAttached;
    private readonly IBugReportingService _bugReportingService;
    private readonly BugReportingContextService _bugReportingContextService;

    public BugReportingPlugin(IBugReportingService bugReportingService, BugReportingContextService bugReportingContextService)
    {
        _bugReportingService = bugReportingService;
        _bugReportingContextService = bugReportingContextService;
        _filesToBeAttached = new List<string>();
    }

    protected override void PreTestInit(object sender, PluginEventArgs e)
    {
        if (!IsBugReportingEnabled())
        {
            return;
        }

        base.PreTestInit(sender, e);
        InitializeTestCase(e);
    }

    protected override void PostTestCleanup(object sender, PluginEventArgs e)
    {
        if (!IsBugReportingEnabled())
        {
            return;
        }

        base.PostTestCleanup(sender, e);

        if ((e.TestOutcome == TestOutcome.Failed || e.TestOutcome == TestOutcome.Error) && _bugReportingContextService?.Context != null)
        {
            _bugReportingService.LogBug(_bugReportingContextService.Context.Value, e.Exception.ToString(), _filesToBeAttached);
        }

        _bugReportingContextService?.ResetContext();
    }

    public void SubscribeScreenshotPlugin(IScreenshotPluginProvider provider)
    {
        provider.ScreenshotGeneratedEvent += ScreenshotGenerated;
    }

    public void UnsubscribeScreenshotPlugin(IScreenshotPluginProvider provider)
    {
        provider.ScreenshotGeneratedEvent -= ScreenshotGenerated;
    }

    public void ScreenshotGenerated(object sender, ScreenshotPluginEventArgs e)
    {
        _filesToBeAttached.Add(e.ScreenshotPath);
    }

    public void SubscribeVideoPlugin(IVideoPluginProvider provider)
    {
        provider.VideoGeneratedEvent += VideoGenerated;
    }

    public void UnsubscribeVideoPlugin(IVideoPluginProvider provider)
    {
        provider.VideoGeneratedEvent -= VideoGenerated;
    }

    public void VideoGenerated(object sender, VideoPluginEventArgs e)
    {
        _filesToBeAttached.Add(e.VideoPath);
    }

    private void InitializeTestCase(PluginEventArgs args)
    {
        if (args.TestMethodMemberInfo == null)
        {
            return;
        }

        _filesToBeAttached = new List<string>();
        _bugReportingContextService.Context.Value = new BugReportingContext();
        _bugReportingContextService.Context.Value.TestCaseName = TestNameToDesciption(args.TestName);
        _bugReportingContextService.Context.Value.TestFullName = $"{args.TestMethodMemberInfo.DeclaringType.Name}.{args.TestName}";
        _bugReportingContextService.Context.Value.TestProjectName = args.TestMethodMemberInfo.DeclaringType.FullName;
    }

    private string TestNameToDesciption(string name)
    {
        var returnStr = name;
        for (var i = 1; i < name.Length; i++)
        {
            var letter = name.Substring(i, 1);

            if (letter.GetHashCode() != letter.ToLower().GetHashCode())
            {
                returnStr = returnStr.Replace(letter, $" {letter.ToLower()}");
            }
        }

        returnStr = returnStr.Replace("_", string.Empty).Trim();
        returnStr = returnStr.First().ToString().ToUpper() + returnStr.Substring(1);
        return returnStr;
    }

    private bool IsBugReportingEnabled()
    {
        return ConfigurationService.GetSection<AzureDevOpsBugReportingSettings>().IsEnabled ||
            ConfigurationService.GetSection<JiraBugReportingSettings>().IsEnabled;
    }
}