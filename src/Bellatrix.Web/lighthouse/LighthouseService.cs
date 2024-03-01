// <copyright file="LighthouseService.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using Bellatrix.Assertions;
using Bellatrix.GoogleLighthouse;
using Bellatrix.Layout;
using Bellatrix.Lighthouse;
using Bellatrix.Utilities;
using Bellatrix.Web;
using Bellatrix.Web.lighthouse;
using Newtonsoft.Json;
using OpenQA.Selenium.Remote;
using RestSharp;

namespace Bellatrix;

public class LighthouseService
{
    public static event EventHandler<LighthouseReportEventArgs> AssertedLighthouseReportEvent;

    public static ThreadLocal<Lighthouse.Root> PerformanceReport { get; set; }

    static LighthouseService()
    {
        PerformanceReport = new ThreadLocal<Lighthouse.Root>();
    }

    public void PerformLighthouseAnalysis(string customArgs = "", bool shouldOverrideDefaultArgs = false)
    {
        var browserService = ServicesCollection.Current.Resolve<BrowserService>();
        string arguments;
        if (shouldOverrideDefaultArgs)
        {
            arguments = $"lighthouse {browserService.Url} --output=json,html,csv --port={WrappedWebDriverCreateService.DebuggerPort} {customArgs}";
        }
        else
        {
            var defaultArgs = GetDefaultLighthouseArgs();
            arguments = $"lighthouse {browserService.Url} --output=json,html,csv --port={WrappedWebDriverCreateService.DebuggerPort} {defaultArgs} {customArgs}";
        }

        var settings = ConfigurationService.GetSection<LighthouseSettings>();

        if (WrappedWebDriverCreateService.BrowserConfiguration.ExecutionType == Web.Enums.ExecutionType.Regular)
        {
            var driverExecutablePath = ExecutionDirectoryResolver.GetDriverExecutablePath();
            ProcessProvider.StartCLIProcessAndWaitToFinish(driverExecutablePath, arguments, false, settings.Timeout, o => Console.WriteLine(o), e => Console.WriteLine(e));

            PerformanceReport.Value = ReadPerformanceReport();
        }
        else if (WrappedWebDriverCreateService.BrowserConfiguration.ExecutionType == Web.Enums.ExecutionType.Grid)
        {
            var app = ServicesCollection.Current.Resolve<App>();
            //app.ApiClient.BaseUrl = ((RemoteWebDriver)browserService.WrappedDriver).Url;
            var request = new RestRequest($"{((RemoteWebDriver)browserService.WrappedDriver).Url}/grid/admin/HubRemoteHostRetrieverServlet/session/{((RemoteWebDriver)browserService.WrappedDriver).SessionId}/", Method.Get);
            var queryResult = app.ApiClient.Execute(request);
            //app.ApiClient.BaseUrl = $"http://{queryResult.Content}";
            request = new RestRequest($"http://{queryResult.Content}/extra/LighthouseServlet", Method.Get);
            request.AddHeader("lighthouse", arguments.Replace("lighthouse ", string.Empty));
            PerformanceReport.Value = app.ApiClient.Get<Lighthouse.Root>(request).Data;
        }
        else
        {
            throw new NotSupportedException("Lighthouse analysis is supported only for regular and grid mode executions.");
        }
    }

    public void AssertFirstMeaningfulPaintScoreMoreThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.FirstMeaningfulPaint.Score;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue > expected, $"{PerformanceReport.Value.Audits.FirstMeaningfulPaint.Title} should be > {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Audits.FirstMeaningfulPaint.Score.ToString(), PerformanceReport.Value.Audits.FirstMeaningfulPaint.Title));
    }

    public void AssertFirstContentfulPaintScoreLessThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.FirstContentfulPaint.Score;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue < expected, $"{PerformanceReport.Value.Audits.FirstContentfulPaint.Title} should be < {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Audits.FirstContentfulPaint.Score.ToString(), PerformanceReport.Value.Audits.FirstContentfulPaint.Title));
    }

    public void AssertSpeedIndexScoreLessThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.SpeedIndex.Score;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue < expected, $"{PerformanceReport.Value.Audits.SpeedIndex.Title} should be < {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Audits.SpeedIndex.Score.ToString(), PerformanceReport.Value.Audits.SpeedIndex.Title));
    }

    public void AssertLargestContentfulPaintScoreLessThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.LargestContentfulPaint.Score;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue < expected, $"{PerformanceReport.Value.Audits.LargestContentfulPaint.Title} should be < {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Audits.LargestContentfulPaint.Score.ToString(), PerformanceReport.Value.Audits.LargestContentfulPaint.Title));
    }

    public void AssertInteractiveScoreLessThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.Interactive.Score;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue < expected, $"{PerformanceReport.Value.Audits.Interactive.Title} should be < {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Audits.Interactive.Score.ToString(), PerformanceReport.Value.Audits.Interactive.Title));
    }

    public void AssertTotalBlockingTimeLessThan(double expected)
    {
        double actualValue = double.Parse(PerformanceReport.Value.Audits.TotalBlockingTime.DisplayValue);
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue < expected, $"{PerformanceReport.Value.Audits.TotalBlockingTime.Title} should be < {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Audits.TotalBlockingTime.DisplayValue, PerformanceReport.Value.Audits.TotalBlockingTime.Title));
    }

    public void AssertCumulativeLayoutShiftScoreLessThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.CumulativeLayoutShift.Score;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue < expected, $"{PerformanceReport.Value.Audits.CumulativeLayoutShift.Title} should be < {expected} but was {actualValue}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Audits.CumulativeLayoutShift.Score.ToString(), PerformanceReport.Value.Audits.CumulativeLayoutShift.Title));
    }

    public void AssertRedirectScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.Redirects.NumericValue;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue < expected, $"{PerformanceReport.Value.Audits.Redirects.Title} should be > {expected} but was {PerformanceReport.Value.Audits.Redirects.NumericValue}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Audits.Redirects.Score.ToString(), PerformanceReport.Value.Audits.Redirects.Title));
    }

    public void AsserJavaExecutionTimeScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Audits.BootupTime.NumericValue;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue < expected, $"{PerformanceReport.Value.Audits.BootupTime.Title} should be > {expected} but was {PerformanceReport.Value.Audits.BootupTime.NumericValue}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Audits.BootupTime.Score.ToString(), PerformanceReport.Value.Audits.BootupTime.Title));
    }

    public void AsserSEOScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Categories.Seo.Score;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue > expected, $"{PerformanceReport.Value.Categories.Seo.Title} should be > {expected} but was {PerformanceReport.Value.Categories.Seo.Score}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Categories.Seo.Score.ToString(), PerformanceReport.Value.Categories.Seo.Title));
    }

    public void AsserBestPracticesScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Categories.BestPractices.Score;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue > expected, $"{PerformanceReport.Value.Categories.BestPractices.Title} should be > {expected} but was {PerformanceReport.Value.Categories.BestPractices.Score}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Categories.BestPractices.Score.ToString(), PerformanceReport.Value.Categories.BestPractices.Title));
    }

    public void AssertPWAScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Categories.Pwa.Score;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue > expected, $"{PerformanceReport.Value.Categories.Pwa.Title} should be > {expected} but was {PerformanceReport.Value.Categories.Pwa.Score}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Categories.Pwa.Score.ToString(), PerformanceReport.Value.Categories.Pwa.Title));
    }

    public void AssertAccessibilityScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Categories.Accessibility.Score;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue > expected, $"{PerformanceReport.Value.Categories.Accessibility.Title} should be > {expected} but was {PerformanceReport.Value.Categories.Accessibility.Score}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Categories.Accessibility.Score.ToString(), PerformanceReport.Value.Categories.Accessibility.Title));
    }

    public void AssertPerformanceScoreAboveThan(double expected)
    {
        double actualValue = PerformanceReport.Value.Categories.Performance.Score;
        Assert.IsTrue<LighthouseAssertFailedException>(actualValue > expected, $"{PerformanceReport.Value.Categories.Performance.Title} should be > {expected} but was {PerformanceReport.Value.Categories.Performance.Score}");
        AssertedLighthouseReportEvent?.Invoke(this, new LighthouseReportEventArgs(expected.ToString(), PerformanceReport.Value.Categories.Performance.Score.ToString(), PerformanceReport.Value.Categories.Performance.Title));
    }

    public MetricPreciseValidationBuilder AssertMetric(Expression<Func<Lighthouse.Root, object>> expression)
    {
        string metricName = TypePropertiesNameResolver.GetMemberName(expression);
        Func<Lighthouse.Root, object> compiledExpression = expression.Compile();
        dynamic actualValue = compiledExpression(PerformanceReport.Value);

        return new MetricPreciseValidationBuilder(actualValue, metricName);
    }

    private string GetDefaultLighthouseArgs()
    {
        var settings = ConfigurationService.GetSection<LighthouseSettings>();
        var defaultArgs = new StringBuilder();
        foreach (var item in settings.Arguments[0])
        {
            defaultArgs.Append($" --{item.Key}");
            if (!string.IsNullOrEmpty(item.Value))
            {
                defaultArgs.Append($"={item.Value}");
            }
        }

        return defaultArgs.ToString();
    }

    private Lighthouse.Root ReadPerformanceReport()
    {
        var driverExecutablePath = ExecutionDirectoryResolver.GetDriverExecutablePath();
        var directoryInfo = new DirectoryInfo(driverExecutablePath);
        string pattern = "*.report.json";
        var file = directoryInfo.GetFiles(pattern, SearchOption.AllDirectories).OrderByDescending(f => f.LastWriteTime).First();
        string fileContent = File.ReadAllText(file.FullName);
        return JsonConvert.DeserializeObject<Lighthouse.Root>(fileContent);
    }
}
