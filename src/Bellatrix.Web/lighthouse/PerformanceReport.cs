// <copyright file="PerformanceReport.cs" company="Automate The Planet Ltd.">
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
using Newtonsoft.Json;

namespace Bellatrix.Lighthouse;

public class Credits
{
    [JsonProperty("axe-core")]
    public string AxeCore { get; set; }
}

public class Environment
{
    [JsonProperty("networkUserAgent")]
    public string NetworkUserAgent { get; set; }

    [JsonProperty("hostUserAgent")]
    public string HostUserAgent { get; set; }

    [JsonProperty("benchmarkIndex")]
    public double BenchmarkIndex { get; set; }

    [JsonProperty("credits")]
    public Credits Credits { get; set; }
}

public class Heading
{
    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("itemType")]
    public string ItemType { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("valueType")]
    public string ValueType { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("granularity")]
    public double? Granularity { get; set; }

    [JsonProperty("subItemsHeading")]
    public SubItemsHeading SubItemsHeading { get; set; }

    [JsonProperty("displayUnit")]
    public string DisplayUnit { get; set; }
}

public class Item
{
    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("resolution")]
    public string Resolution { get; set; }

    [JsonProperty("cumulativeLayoutShiftMainFrame")]
    public double CumulativeLayoutShiftMainFrame { get; set; }

    [JsonProperty("totalCumulativeLayoutShift")]
    public double TotalCumulativeLayoutShift { get; set; }

    [JsonProperty("responseTime")]
    public double ResponseTime { get; set; }

    [JsonProperty("reason")]
    public string Reason { get; set; }

    [JsonProperty("failures")]
    public List<string> Failures { get; set; }

    [JsonProperty("isParseFailure")]
    public bool IsParseFailure { get; set; }

    [JsonProperty("parseFailureReason")]
    public string ParseFailureReason { get; set; }

    [JsonProperty("themeColor")]
    public object ThemeColor { get; set; }

    [JsonProperty("group")]
    public string Group { get; set; }

    [JsonProperty("groupLabel")]
    public string GroupLabel { get; set; }

    [JsonProperty("duration")]
    public double Duration { get; set; }

    [JsonProperty("numRequests")]
    public double NumRequests { get; set; }

    [JsonProperty("numScripts")]
    public double NumScripts { get; set; }

    [JsonProperty("numStylesheets")]
    public double NumStylesheets { get; set; }

    [JsonProperty("numFonts")]
    public double NumFonts { get; set; }

    [JsonProperty("numTasks")]
    public double NumTasks { get; set; }

    [JsonProperty("numTasksOver10ms")]
    public double NumTasksOver10ms { get; set; }

    [JsonProperty("numTasksOver25ms")]
    public double NumTasksOver25ms { get; set; }

    [JsonProperty("numTasksOver50ms")]
    public double NumTasksOver50ms { get; set; }

    [JsonProperty("numTasksOver100ms")]
    public double NumTasksOver100ms { get; set; }

    [JsonProperty("numTasksOver500ms")]
    public double NumTasksOver500ms { get; set; }

    [JsonProperty("rtt")]
    public double Rtt { get; set; }

    [JsonProperty("throughput")]
    public double Throughput { get; set; }

    [JsonProperty("maxRtt")]
    public double MaxRtt { get; set; }

    [JsonProperty("maxServerLatency")]
    public double MaxServerLatency { get; set; }

    [JsonProperty("totalByteWeight")]
    public double TotalByteWeight { get; set; }

    [JsonProperty("totalTaskTime")]
    public double TotalTaskTime { get; set; }

    [JsonProperty("mainDocumentTransferSize")]
    public double MainDocumentTransferSize { get; set; }

    [JsonProperty("resourceType")]
    public string ResourceType { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("requestCount")]
    public double RequestCount { get; set; }

    [JsonProperty("transferSize")]
    public double TransferSize { get; set; }

    [JsonProperty("severity")]
    public string Severity { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("totalBytes")]
    public double TotalBytes { get; set; }

    [JsonProperty("wastedBytes")]
    public double WastedBytes { get; set; }

    [JsonProperty("wastedPercent")]
    public double WastedPercent { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("npm")]
    public string Npm { get; set; }

    [JsonProperty("source")]
    public Source Source { get; set; }

    [JsonProperty("selector")]
    public string Selector { get; set; }

    [JsonProperty("coverage")]
    public string Coverage { get; set; }

    [JsonProperty("fontSize")]
    public string FontSize { get; set; }
}

public class Details
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("headings")]
    public List<Heading> Headings { get; set; }

    [JsonProperty("items")]
    public List<Item> Items { get; set; }

    [JsonProperty("overallSavingsMs")]
    public double OverallSavingsMs { get; set; }

    [JsonProperty("longestChain")]
    public LongestChain LongestChain { get; set; }

    [JsonProperty("debugData")]
    public DebugData DebugData { get; set; }

    [JsonProperty("summary")]
    public Summary Summary { get; set; }

    [JsonProperty("overallSavingsBytes")]
    public double OverallSavingsBytes { get; set; }
}

public class IsOnHttps
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class RedirectsHttp
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class ServiceWorker
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class Viewport
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("warnings")]
    public List<object> Warnings { get; set; }
}

public class FirstContentfulPaint
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }
}

public class LargestContentfulPaint
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }
}

public class FirstMeaningfulPaint
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }
}

public class SpeedIndex
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }
}

public class TotalBlockingTime
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }
}

public class MaxPotentialFid
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }
}

public class CumulativeLayoutShift
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class ErrorsInConsole
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class ServerResponseTime
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class Interactive
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }
}

public class LongestChain
{
    [JsonProperty("duration")]
    public double Duration { get; set; }

    [JsonProperty("length")]
    public double Length { get; set; }

    [JsonProperty("transferSize")]
    public double TransferSize { get; set; }
}

public class CriticalRequestChains
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class Redirects
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class DebugData
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("manifestUrl")]
    public object ManifestUrl { get; set; }

    [JsonProperty("impact")]
    public string Impact { get; set; }

    [JsonProperty("tags")]
    public List<string> Tags { get; set; }

    [JsonProperty("stacks")]
    public List<Stack> Stacks { get; set; }
}

public class InstallableManifest
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("warnings")]
    public List<object> Warnings { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class AppleTouchIcon
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("warnings")]
    public List<object> Warnings { get; set; }
}

public class SplashScreen
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("explanation")]
    public string Explanation { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class ThemedOmnibox
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("explanation")]
    public string Explanation { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class MaskableIcon
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("explanation")]
    public string Explanation { get; set; }
}

public class ContentWidth
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class ImageAspectRatio
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class ImageSizeResponsive
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class PreloadFonts
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class Deprecations
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class MainthreadWorkBreakdown
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class Summary
{
    [JsonProperty("wastedMs")]
    public double WastedMs { get; set; }

    [JsonProperty("wastedBytes")]
    public double WastedBytes { get; set; }
}

public class BootupTime
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class UsesRelPreload
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class UsesRelPreconnect
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("warnings")]
    public List<object> Warnings { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class FontDisplay
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("warnings")]
    public List<object> Warnings { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class Diagnostics
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class NetworkRtt
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class NetworkServerLatency
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class MainThreadTasks
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class PerformanceBudget
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class TimingBudget
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class ResourceSummary
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class SubItemsHeading
{
    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("itemType")]
    public string ItemType { get; set; }

    [JsonProperty("valueType")]
    public string ValueType { get; set; }
}

public class ThirdPartySummary
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class ThirdPartyFacades
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class LargestContentfulPaintElement
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class LayoutShiftElements
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class LongTasks
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class NoUnloadListeners
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class NonCompositedAnimations
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class UnsizedImages
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class ValidSourceMaps
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class PreloadLcpImage
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class CspXss
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class PwaCrossBrowser
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class PwaPageTransitions
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class PwaEachPageHasUrl
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class Accesskeys
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class AriaAllowedAttr
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class AriaCommandName
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class AriaHiddenBody
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class AriaHiddenFocus
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class AriaInputFieldName
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class AriaMeterName
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class AriaProgressbarName
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class AriaRequiredAttr
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class AriaRequiredChildren
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class AriaRequiredParent
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class AriaRoles
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class AriaToggleFieldName
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class AriaTooltipName
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class AriaTreeitemName
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class AriaValidAttrValue
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class AriaValidAttr
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class ButtonName
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class Bypass
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class ColorContrast
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class DefinitionList
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class Dlitem
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class DocumentTitle
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class DuplicateIdActive
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class DuplicateIdAria
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class FormFieldMultipleLabels
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class FrameTitle
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class HeadingOrder
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class HtmlHasLang
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class HtmlLangValid
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class ImageAlt
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class InputImageAlt
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class Label
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class LinkName
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class List
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class Listitem
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class MetaRefresh
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class MetaViewport
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class ObjectAlt
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class Tabindex
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class TdHeadersAttr
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class ThHasDataCells
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class ValidLang
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class VideoCaption
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class CustomControlsLabels
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class CustomControlsRoles
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class FocusTraps
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class FocusableControls
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class InteractiveElementAffordance
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class LogicalTabOrder
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class ManagedFocus
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class OffscreenContentHidden
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class UseLandmarks
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class VisualOrderFollowsDom
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class UsesLongCacheTtl
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class TotalByteWeight
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class OffscreenImages
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("warnings")]
    public List<object> Warnings { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class RenderBlockingResources
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class UnminifiedCss
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class UnminifiedJavascript
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("warnings")]
    public List<object> Warnings { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class UnusedCssRules
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class UnusedJavascript
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class ModernImageFormats
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("warnings")]
    public List<object> Warnings { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class UsesOptimizedImages
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("warnings")]
    public List<object> Warnings { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class UsesTextCompression
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class UsesResponsiveImages
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class EfficientAnimatedContent
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class DuplicatedJavascript
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class LegacyJavascript
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class AppcacheManifest
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class Doctype
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class Charset
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class DomSize
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class ExternalAnchorsUseRelNoopener
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("warnings")]
    public List<object> Warnings { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class GeolocationOnStart
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class InspectorIssues
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class NoDocumentWrite
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class NoVulnerableLibraries
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class Stack
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; }
}

public class JsLibraries
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class NotificationOnStart
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class PasswordInputsCanBePastedInto
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class UsesHttp2
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("numericValue")]
    public double NumericValue { get; set; }

    [JsonProperty("numericUnit")]
    public string NumericUnit { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class UsesPassiveEventListeners
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class MetaDescription
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class HttpStatusCode
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class Source
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}

public class FontSize
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class LinkText
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class CrawlableAnchors
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class IsCrawlable
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class RobotsTxt
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class TapTargets
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("displayValue")]
    public string DisplayValue { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class Hreflang
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class Plugins
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }

    [JsonProperty("details")]
    public Details Details { get; set; }
}

public class Canonical
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class StructuredData
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("score")]
    public object Score { get; set; }

    [JsonProperty("scoreDisplayMode")]
    public string ScoreDisplayMode { get; set; }
}

public class Audits
{
    [JsonProperty("is-on-https")]
    public IsOnHttps IsOnHttps { get; set; }

    [JsonProperty("redirects-http")]
    public RedirectsHttp RedirectsHttp { get; set; }

    [JsonProperty("service-worker")]
    public ServiceWorker ServiceWorker { get; set; }

    [JsonProperty("viewport")]
    public Viewport Viewport { get; set; }

    [JsonProperty("first-contentful-paint")]
    public FirstContentfulPaint FirstContentfulPaint { get; set; }

    [JsonProperty("largest-contentful-paint")]
    public LargestContentfulPaint LargestContentfulPaint { get; set; }

    [JsonProperty("first-meaningful-paint")]
    public FirstMeaningfulPaint FirstMeaningfulPaint { get; set; }

    [JsonProperty("speed-index")]
    public SpeedIndex SpeedIndex { get; set; }

    [JsonProperty("total-blocking-time")]
    public TotalBlockingTime TotalBlockingTime { get; set; }

    [JsonProperty("max-potential-fid")]
    public MaxPotentialFid MaxPotentialFid { get; set; }

    [JsonProperty("cumulative-layout-shift")]
    public CumulativeLayoutShift CumulativeLayoutShift { get; set; }

    [JsonProperty("errors-in-console")]
    public ErrorsInConsole ErrorsInConsole { get; set; }

    [JsonProperty("server-response-time")]
    public ServerResponseTime ServerResponseTime { get; set; }

    [JsonProperty("interactive")]
    public Interactive Interactive { get; set; }

    [JsonProperty("critical-request-chains")]
    public CriticalRequestChains CriticalRequestChains { get; set; }

    [JsonProperty("redirects")]
    public Redirects Redirects { get; set; }

    [JsonProperty("installable-manifest")]
    public InstallableManifest InstallableManifest { get; set; }

    [JsonProperty("apple-touch-icon")]
    public AppleTouchIcon AppleTouchIcon { get; set; }

    [JsonProperty("splash-screen")]
    public SplashScreen SplashScreen { get; set; }

    [JsonProperty("themed-omnibox")]
    public ThemedOmnibox ThemedOmnibox { get; set; }

    [JsonProperty("maskable-icon")]
    public MaskableIcon MaskableIcon { get; set; }

    [JsonProperty("content-width")]
    public ContentWidth ContentWidth { get; set; }

    [JsonProperty("image-aspect-ratio")]
    public ImageAspectRatio ImageAspectRatio { get; set; }

    [JsonProperty("image-size-responsive")]
    public ImageSizeResponsive ImageSizeResponsive { get; set; }

    [JsonProperty("preload-fonts")]
    public PreloadFonts PreloadFonts { get; set; }

    [JsonProperty("deprecations")]
    public Deprecations Deprecations { get; set; }

    [JsonProperty("mainthread-work-breakdown")]
    public MainthreadWorkBreakdown MainthreadWorkBreakdown { get; set; }

    [JsonProperty("bootup-time")]
    public BootupTime BootupTime { get; set; }

    [JsonProperty("uses-rel-preload")]
    public UsesRelPreload UsesRelPreload { get; set; }

    [JsonProperty("uses-rel-preconnect")]
    public UsesRelPreconnect UsesRelPreconnect { get; set; }

    [JsonProperty("font-display")]
    public FontDisplay FontDisplay { get; set; }

    [JsonProperty("diagnostics")]
    public Diagnostics Diagnostics { get; set; }

    [JsonProperty("network-rtt")]
    public NetworkRtt NetworkRtt { get; set; }

    [JsonProperty("network-server-latency")]
    public NetworkServerLatency NetworkServerLatency { get; set; }

    [JsonProperty("main-thread-tasks")]
    public MainThreadTasks MainThreadTasks { get; set; }

    [JsonProperty("performance-budget")]
    public PerformanceBudget PerformanceBudget { get; set; }

    [JsonProperty("timing-budget")]
    public TimingBudget TimingBudget { get; set; }

    [JsonProperty("resource-summary")]
    public ResourceSummary ResourceSummary { get; set; }

    [JsonProperty("third-party-summary")]
    public ThirdPartySummary ThirdPartySummary { get; set; }

    [JsonProperty("third-party-facades")]
    public ThirdPartyFacades ThirdPartyFacades { get; set; }

    [JsonProperty("largest-contentful-paint-element")]
    public LargestContentfulPaintElement LargestContentfulPaintElement { get; set; }

    [JsonProperty("layout-shift-elements")]
    public LayoutShiftElements LayoutShiftElements { get; set; }

    [JsonProperty("long-tasks")]
    public LongTasks LongTasks { get; set; }

    [JsonProperty("no-unload-listeners")]
    public NoUnloadListeners NoUnloadListeners { get; set; }

    [JsonProperty("non-composited-animations")]
    public NonCompositedAnimations NonCompositedAnimations { get; set; }

    [JsonProperty("unsized-images")]
    public UnsizedImages UnsizedImages { get; set; }

    [JsonProperty("valid-source-maps")]
    public ValidSourceMaps ValidSourceMaps { get; set; }

    [JsonProperty("preload-lcp-image")]
    public PreloadLcpImage PreloadLcpImage { get; set; }

    [JsonProperty("csp-xss")]
    public CspXss CspXss { get; set; }

    [JsonProperty("pwa-cross-browser")]
    public PwaCrossBrowser PwaCrossBrowser { get; set; }

    [JsonProperty("pwa-page-transitions")]
    public PwaPageTransitions PwaPageTransitions { get; set; }

    [JsonProperty("pwa-each-page-has-url")]
    public PwaEachPageHasUrl PwaEachPageHasUrl { get; set; }

    [JsonProperty("accesskeys")]
    public Accesskeys Accesskeys { get; set; }

    [JsonProperty("aria-allowed-attr")]
    public AriaAllowedAttr AriaAllowedAttr { get; set; }

    [JsonProperty("aria-command-name")]
    public AriaCommandName AriaCommandName { get; set; }

    [JsonProperty("aria-hidden-body")]
    public AriaHiddenBody AriaHiddenBody { get; set; }

    [JsonProperty("aria-hidden-focus")]
    public AriaHiddenFocus AriaHiddenFocus { get; set; }

    [JsonProperty("aria-input-field-name")]
    public AriaInputFieldName AriaInputFieldName { get; set; }

    [JsonProperty("aria-meter-name")]
    public AriaMeterName AriaMeterName { get; set; }

    [JsonProperty("aria-progressbar-name")]
    public AriaProgressbarName AriaProgressbarName { get; set; }

    [JsonProperty("aria-required-attr")]
    public AriaRequiredAttr AriaRequiredAttr { get; set; }

    [JsonProperty("aria-required-children")]
    public AriaRequiredChildren AriaRequiredChildren { get; set; }

    [JsonProperty("aria-required-parent")]
    public AriaRequiredParent AriaRequiredParent { get; set; }

    [JsonProperty("aria-roles")]
    public AriaRoles AriaRoles { get; set; }

    [JsonProperty("aria-toggle-field-name")]
    public AriaToggleFieldName AriaToggleFieldName { get; set; }

    [JsonProperty("aria-tooltip-name")]
    public AriaTooltipName AriaTooltipName { get; set; }

    [JsonProperty("aria-treeitem-name")]
    public AriaTreeitemName AriaTreeitemName { get; set; }

    [JsonProperty("aria-valid-attr-value")]
    public AriaValidAttrValue AriaValidAttrValue { get; set; }

    [JsonProperty("aria-valid-attr")]
    public AriaValidAttr AriaValidAttr { get; set; }

    [JsonProperty("button-name")]
    public ButtonName ButtonName { get; set; }

    [JsonProperty("bypass")]
    public Bypass Bypass { get; set; }

    [JsonProperty("color-contrast")]
    public ColorContrast ColorContrast { get; set; }

    [JsonProperty("definition-list")]
    public DefinitionList DefinitionList { get; set; }

    [JsonProperty("dlitem")]
    public Dlitem Dlitem { get; set; }

    [JsonProperty("document-title")]
    public DocumentTitle DocumentTitle { get; set; }

    [JsonProperty("duplicate-id-active")]
    public DuplicateIdActive DuplicateIdActive { get; set; }

    [JsonProperty("duplicate-id-aria")]
    public DuplicateIdAria DuplicateIdAria { get; set; }

    [JsonProperty("form-field-multiple-labels")]
    public FormFieldMultipleLabels FormFieldMultipleLabels { get; set; }

    [JsonProperty("frame-title")]
    public FrameTitle FrameTitle { get; set; }

    [JsonProperty("heading-order")]
    public HeadingOrder HeadingOrder { get; set; }

    [JsonProperty("html-has-lang")]
    public HtmlHasLang HtmlHasLang { get; set; }

    [JsonProperty("html-lang-valid")]
    public HtmlLangValid HtmlLangValid { get; set; }

    [JsonProperty("image-alt")]
    public ImageAlt ImageAlt { get; set; }

    [JsonProperty("input-image-alt")]
    public InputImageAlt InputImageAlt { get; set; }

    [JsonProperty("label")]
    public Label Label { get; set; }

    [JsonProperty("link-name")]
    public LinkName LinkName { get; set; }

    [JsonProperty("list")]
    public List List { get; set; }

    [JsonProperty("listitem")]
    public Listitem Listitem { get; set; }

    [JsonProperty("meta-refresh")]
    public MetaRefresh MetaRefresh { get; set; }

    [JsonProperty("meta-viewport")]
    public MetaViewport MetaViewport { get; set; }

    [JsonProperty("object-alt")]
    public ObjectAlt ObjectAlt { get; set; }

    [JsonProperty("tabindex")]
    public Tabindex Tabindex { get; set; }

    [JsonProperty("td-headers-attr")]
    public TdHeadersAttr TdHeadersAttr { get; set; }

    [JsonProperty("th-has-data-cells")]
    public ThHasDataCells ThHasDataCells { get; set; }

    [JsonProperty("valid-lang")]
    public ValidLang ValidLang { get; set; }

    [JsonProperty("video-caption")]
    public VideoCaption VideoCaption { get; set; }

    [JsonProperty("custom-controls-labels")]
    public CustomControlsLabels CustomControlsLabels { get; set; }

    [JsonProperty("custom-controls-roles")]
    public CustomControlsRoles CustomControlsRoles { get; set; }

    [JsonProperty("focus-traps")]
    public FocusTraps FocusTraps { get; set; }

    [JsonProperty("focusable-controls")]
    public FocusableControls FocusableControls { get; set; }

    [JsonProperty("interactive-element-affordance")]
    public InteractiveElementAffordance InteractiveElementAffordance { get; set; }

    [JsonProperty("logical-tab-order")]
    public LogicalTabOrder LogicalTabOrder { get; set; }

    [JsonProperty("managed-focus")]
    public ManagedFocus ManagedFocus { get; set; }

    [JsonProperty("offscreen-content-hidden")]
    public OffscreenContentHidden OffscreenContentHidden { get; set; }

    [JsonProperty("use-landmarks")]
    public UseLandmarks UseLandmarks { get; set; }

    [JsonProperty("visual-order-follows-dom")]
    public VisualOrderFollowsDom VisualOrderFollowsDom { get; set; }

    [JsonProperty("uses-long-cache-ttl")]
    public UsesLongCacheTtl UsesLongCacheTtl { get; set; }

    [JsonProperty("total-byte-weight")]
    public TotalByteWeight TotalByteWeight { get; set; }

    [JsonProperty("offscreen-images")]
    public OffscreenImages OffscreenImages { get; set; }

    [JsonProperty("render-blocking-resources")]
    public RenderBlockingResources RenderBlockingResources { get; set; }

    [JsonProperty("unminified-css")]
    public UnminifiedCss UnminifiedCss { get; set; }

    [JsonProperty("unminified-javascript")]
    public UnminifiedJavascript UnminifiedJavascript { get; set; }

    [JsonProperty("unused-css-rules")]
    public UnusedCssRules UnusedCssRules { get; set; }

    [JsonProperty("unused-javascript")]
    public UnusedJavascript UnusedJavascript { get; set; }

    [JsonProperty("modern-image-formats")]
    public ModernImageFormats ModernImageFormats { get; set; }

    [JsonProperty("uses-optimized-images")]
    public UsesOptimizedImages UsesOptimizedImages { get; set; }

    [JsonProperty("uses-text-compression")]
    public UsesTextCompression UsesTextCompression { get; set; }

    [JsonProperty("uses-responsive-images")]
    public UsesResponsiveImages UsesResponsiveImages { get; set; }

    [JsonProperty("efficient-animated-content")]
    public EfficientAnimatedContent EfficientAnimatedContent { get; set; }

    [JsonProperty("duplicated-javascript")]
    public DuplicatedJavascript DuplicatedJavascript { get; set; }

    [JsonProperty("legacy-javascript")]
    public LegacyJavascript LegacyJavascript { get; set; }

    [JsonProperty("appcache-manifest")]
    public AppcacheManifest AppcacheManifest { get; set; }

    [JsonProperty("doctype")]
    public Doctype Doctype { get; set; }

    [JsonProperty("charset")]
    public Charset Charset { get; set; }

    [JsonProperty("dom-size")]
    public DomSize DomSize { get; set; }

    [JsonProperty("external-anchors-use-rel-noopener")]
    public ExternalAnchorsUseRelNoopener ExternalAnchorsUseRelNoopener { get; set; }

    [JsonProperty("geolocation-on-start")]
    public GeolocationOnStart GeolocationOnStart { get; set; }

    [JsonProperty("inspector-issues")]
    public InspectorIssues InspectorIssues { get; set; }

    [JsonProperty("no-document-write")]
    public NoDocumentWrite NoDocumentWrite { get; set; }

    [JsonProperty("no-vulnerable-libraries")]
    public NoVulnerableLibraries NoVulnerableLibraries { get; set; }

    [JsonProperty("js-libraries")]
    public JsLibraries JsLibraries { get; set; }

    [JsonProperty("notification-on-start")]
    public NotificationOnStart NotificationOnStart { get; set; }

    [JsonProperty("password-inputs-can-be-pasted-into")]
    public PasswordInputsCanBePastedInto PasswordInputsCanBePastedInto { get; set; }

    [JsonProperty("uses-http2")]
    public UsesHttp2 UsesHttp2 { get; set; }

    [JsonProperty("uses-passive-event-listeners")]
    public UsesPassiveEventListeners UsesPassiveEventListeners { get; set; }

    [JsonProperty("meta-description")]
    public MetaDescription MetaDescription { get; set; }

    [JsonProperty("http-status-code")]
    public HttpStatusCode HttpStatusCode { get; set; }

    [JsonProperty("font-size")]
    public FontSize FontSize { get; set; }

    [JsonProperty("link-text")]
    public LinkText LinkText { get; set; }

    [JsonProperty("crawlable-anchors")]
    public CrawlableAnchors CrawlableAnchors { get; set; }

    [JsonProperty("is-crawlable")]
    public IsCrawlable IsCrawlable { get; set; }

    [JsonProperty("robots-txt")]
    public RobotsTxt RobotsTxt { get; set; }

    [JsonProperty("tap-targets")]
    public TapTargets TapTargets { get; set; }

    [JsonProperty("hreflang")]
    public Hreflang Hreflang { get; set; }

    [JsonProperty("plugins")]
    public Plugins Plugins { get; set; }

    [JsonProperty("canonical")]
    public Canonical Canonical { get; set; }

    [JsonProperty("structured-data")]
    public StructuredData StructuredData { get; set; }
}

public class Performance
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }
}

public class Accessibility
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("manualDescription")]
    public string ManualDescription { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }
}

public class BestPractices
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }
}

public class Seo
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("manualDescription")]
    public string ManualDescription { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }
}

public class Pwa
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("manualDescription")]
    public string ManualDescription { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("score")]
    public double Score { get; set; }
}

public class Categories
{
    [JsonProperty("performance")]
    public Performance Performance { get; set; }

    [JsonProperty("accessibility")]
    public Accessibility Accessibility { get; set; }

    [JsonProperty("best-practices")]
    public BestPractices BestPractices { get; set; }

    [JsonProperty("seo")]
    public Seo Seo { get; set; }

    [JsonProperty("pwa")]
    public Pwa Pwa { get; set; }
}

public class Root
{
    [JsonProperty("userAgent")]
    public string UserAgent { get; set; }

    [JsonProperty("environment")]
    public Environment Environment { get; set; }

    [JsonProperty("lighthouseVersion")]
    public string LighthouseVersion { get; set; }

    [JsonProperty("fetchTime")]
    public DateTime FetchTime { get; set; }

    [JsonProperty("requestedUrl")]
    public string RequestedUrl { get; set; }

    [JsonProperty("finalUrl")]
    public string FinalUrl { get; set; }

    [JsonProperty("runWarnings")]
    public List<object> RunWarnings { get; set; }

    [JsonProperty("audits")]
    public Audits Audits { get; set; }

    [JsonProperty("categories")]
    public Categories Categories { get; set; }
}