// <copyright file="AllureWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using System.Reflection;
using System.Threading;
using Allure.Commons;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Screenshots;
using Bellatrix.Plugins.Screenshots.Plugins;
using Bellatrix.Plugins.Video.Plugins;
using Newtonsoft.Json.Linq;

namespace Bellatrix.Results.Allure;

public class AllureWorkflowPlugin : Plugin, IScreenshotPlugin, IVideoPlugin
{
    private static ThreadLocal<AllureLifecycle> _allureLifecycle;
    private static ThreadLocal<string> _testContainerId;
    private static ThreadLocal<string> _testResultId;
    private static ThreadLocal<bool> _hasStarted;

    public AllureWorkflowPlugin()
    {
        if (_allureLifecycle == null)
        {
            _allureLifecycle = new ThreadLocal<AllureLifecycle>();
            _testContainerId = new ThreadLocal<string>();
            _testResultId = new ThreadLocal<string>();
            _hasStarted = new ThreadLocal<bool>();
        }

        _allureLifecycle.Value = AllureLifecycle.Instance;
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
        if (File.Exists(e.ScreenshotPath))
        {
            _allureLifecycle.Value.AddAttachment("image on fail", "image/png", e.ScreenshotPath);
        }
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
        if (File.Exists(e.VideoPath))
        {
            _allureLifecycle.Value.AddAttachment("video on fail", "video/mpg", e.VideoPath);
        }
    }

    protected override void TestInitFailed(object sender, PluginEventArgs e)
    {
        StartTestContainer(e.TestFullName);
        StartTestCase(e.TestName, e.TestClassName, e.TestFullName);
        StopTestCase(e.Categories, e.Authors, e.Descriptions, e.TestMethodMemberInfo, e.TestOutcome, e.ConsoleOutputMessage, e.ConsoleOutputStackTrace);
        StopTestContainer();

        base.TestInitFailed(sender, e);
    }

    protected override void PreTestInit(object sender, PluginEventArgs e)
    {
        _hasStarted.Value = false;
        StartTestContainer(e.TestFullName);
        StartTestCase(e.TestName, e.TestClassName, e.TestFullName);
        _hasStarted.Value = true;
        base.PreTestInit(sender, e);
    }

    protected override void PostTestCleanup(object sender, PluginEventArgs e)
    {
        if (_hasStarted.Value)
        {
            StopTestCase(e.Categories, e.Authors, e.Descriptions, e.TestMethodMemberInfo, e.TestOutcome, e.ConsoleOutputMessage, e.ConsoleOutputStackTrace);
            StopTestContainer();
        }

        _hasStarted.Value = false;

        base.PostTestCleanup(sender, e);
    }

    private void StartTestContainer(string fullTestName)
    {
        _testContainerId.Value = Guid.NewGuid().ToString();
        var container = new TestResultContainer
                        {
                            uuid = _testContainerId.Value,
                            name = fullTestName,
                        };

        _allureLifecycle.Value.StartTestContainer(container);
    }

    private void StartTestCase(string testName, string className, string testFullName)
    {
        _testResultId.Value = Guid.NewGuid().ToString();
        var testResult = new TestResult
                         {
                             uuid = _testResultId.Value,
                             name = testName,
                             historyId = testFullName,
                             fullName = testFullName,
                             labels = new List<Label>
                                      {
                                          Label.Thread(),
                                          Label.Host(),
                                          Label.TestClass(className),
                                          Label.TestMethod(testName),
                                          Label.Package(className),
                                      },
                         };
        _allureLifecycle.Value.StartTestCase(_testContainerId.Value, testResult);
    }

    private void StopTestCase(List<string> categories, List<string> authors, List<string> descriptions, MemberInfo memberInfo, TestOutcome testOutcome, string consoleMessage, string stackTrace)
    {
        ApplyCategoryAttributes(categories);
        ApplyAuthorsAttributes(authors);
        ApplyDescriptionAttributes(descriptions);
        ApplyAllureSeverityAttributes(memberInfo);
        ApplyAllureFeatureAttributes(memberInfo);
        ApplyAllureIssueAttributes(memberInfo);
        ApplyAllureStoryAttributes(memberInfo);
        ApplyAllureTagAttributes(memberInfo);
        ApplyAllureTmsAttributes(memberInfo);
        ApplyAllureLinkAttributes(memberInfo);
        ApplyAllureSuiteAttributes(memberInfo);
        ApplyAllureSubSuiteAttributes(memberInfo);
        ApplyAllureParentSuiteAttributes(memberInfo);
        ApplyAllureEpicAttributes(memberInfo);
        UpdateAllureTestResults(consoleMessage, stackTrace);

        _allureLifecycle.Value.StopTestCase(testCase => testCase.status = GetAllureTestResultOutput(testOutcome, consoleMessage));
        _allureLifecycle.Value.WriteTestCase(_testResultId.Value);
    }

    private void UpdateAllureTestResults(string consoleMessage, string stackTrace)
    {
        string message = consoleMessage;
        if (!string.IsNullOrEmpty(stackTrace))
        {
            message = $"{message}{Environment.NewLine}{new string('-', 10)}{stackTrace}";
        }

        Debug.WriteLine($"Allure test results message: {Environment.NewLine}{message}");
        _allureLifecycle.Value.UpdateTestCase(
            x => x.statusDetails = new StatusDetails
                                   {
                                       message = message,
                                       trace = stackTrace,
                                   });
    }

    private void ApplyCategoryAttributes(List<string> categories)
    {
        foreach (var category in categories)
        {
            _allureLifecycle.Value.UpdateTestCase(x => x.labels.Add(Label.Tag(category)));
        }
    }

    private void ApplyAuthorsAttributes(List<string> authors)
    {
        foreach (var author in authors)
        {
            _allureLifecycle.Value.UpdateTestCase(x => x.labels.Add(Label.Owner(author)));
        }
    }

    private void ApplyDescriptionAttributes(List<string> descriptions)
    {
        foreach (var description in descriptions)
        {
            _allureLifecycle.Value.UpdateTestCase(x => x.description += $"{description}\n");
        }
    }

    private void ApplyAllureEpicAttributes(MemberInfo memberInfo)
    {
        var allEpicAttributes = GetAllAttributes<AllureEpicAttribute>(memberInfo);
        foreach (var epicAttribute in allEpicAttributes)
        {
            _allureLifecycle.Value.UpdateTestCase(x => x.labels.Add(Label.Epic(epicAttribute.Epic)));
        }
    }

    private void ApplyAllureParentSuiteAttributes(MemberInfo memberInfo)
    {
        var allParentSuiteAttributes = GetAllAttributes<AllureParentSuiteAttribute>(memberInfo);
        foreach (var parentSuiteAttribute in allParentSuiteAttributes)
        {
            _allureLifecycle.Value.UpdateTestCase(x => x.labels.Add(Label.ParentSuite(parentSuiteAttribute.ParentSuite)));
        }
    }

    private void ApplyAllureSubSuiteAttributes(MemberInfo memberInfo)
    {
        var allSubSuiteAttributes = GetAllAttributes<AllureSubSuiteAttribute>(memberInfo);
        foreach (var subSuiteAttribute in allSubSuiteAttributes)
        {
            _allureLifecycle.Value.UpdateTestCase(x => x.labels.Add(Label.SubSuite(subSuiteAttribute.SubSuite)));
        }
    }

    private void ApplyAllureSuiteAttributes(MemberInfo memberInfo)
    {
        var allSuiteAttributes = GetAllAttributes<AllureSuiteAttribute>(memberInfo);
        foreach (var suiteAttribute in allSuiteAttributes)
        {
            _allureLifecycle.Value.UpdateTestCase(x => x.labels.Add(Label.Suite(suiteAttribute.Suite)));
        }
    }

    private void ApplyAllureLinkAttributes(MemberInfo memberInfo)
    {
        var allLinkAttributes = GetAllAttributes<AllureLinkAttribute>(memberInfo);
        foreach (var linkAttribute in allLinkAttributes)
        {
            _allureLifecycle.Value.UpdateTestCase(x => x.links.Add(linkAttribute.Link));
        }
    }

    private void ApplyAllureTmsAttributes(MemberInfo memberInfo)
    {
        var allTmsAttributes = GetAllAttributes<AllureTmsAttribute>(memberInfo);
        foreach (var tmsAttribute in allTmsAttributes)
        {
            _allureLifecycle.Value.UpdateTestCase(x => x.links.Add(tmsAttribute.TmsLink));
        }
    }

    private void ApplyAllureTagAttributes(MemberInfo memberInfo)
    {
        var allTagsAttributes = GetAllAttributes<AllureTagAttribute>(memberInfo);
        foreach (var tagAttribute in allTagsAttributes)
        {
            foreach (var tag in tagAttribute.Tags)
            {
                _allureLifecycle.Value.UpdateTestCase(x => x.labels.Add(Label.Tag(tag)));
            }
        }
    }

    private void ApplyAllureStoryAttributes(MemberInfo memberInfo)
    {
        var allStoryAttributes = GetAllAttributes<AllureStoryAttribute>(memberInfo);
        foreach (var storyAttribute in allStoryAttributes)
        {
            foreach (var story in storyAttribute.Stories)
            {
                _allureLifecycle.Value.UpdateTestCase(x => x.labels.Add(Label.Story(story)));
            }
        }
    }

    private void ApplyAllureIssueAttributes(MemberInfo memberInfo)
    {
        var allIssueAttributes = GetAllAttributes<AllureIssueAttribute>(memberInfo);
        foreach (var issueAttribute in allIssueAttributes)
        {
            _allureLifecycle.Value.UpdateTestCase(x => x.links.Add(issueAttribute.IssueLink));
        }
    }

    private void ApplyAllureFeatureAttributes(MemberInfo memberInfo)
    {
        var allFeatureAttributes = GetAllAttributes<AllureFeatureAttribute>(memberInfo);
        foreach (var featureAttribute in allFeatureAttributes)
        {
            foreach (var feature in featureAttribute.Features)
            {
                _allureLifecycle.Value.UpdateTestCase(x => x.labels.Add(Label.Feature(feature)));
            }
        }
    }

    private void ApplyAllureSeverityAttributes(MemberInfo memberInfo)
    {
        var allSeverityAttribute = GetOverridenAttribute<AllureSeverityAttribute>(memberInfo);
        if (allSeverityAttribute != null)
        {
            _allureLifecycle.Value.UpdateTestCase(x => x.labels.Add(Label.Severity(allSeverityAttribute.Severity)));
        }
    }

    private void StopTestContainer()
    {
        _allureLifecycle.Value.UpdateTestContainer(_testContainerId.Value,
            cont =>
            {
            });
        _allureLifecycle.Value.StopTestContainer(_testContainerId.Value);
        _allureLifecycle.Value.WriteTestContainer(_testContainerId.Value);
    }

    private Status GetAllureTestResultOutput(TestOutcome testOutcome, string consoleMessage)
    {
        if (testOutcome != TestOutcome.Passed)
        {
            var allureConfiguration = JObject.Parse(_allureLifecycle.Value.JsonConfiguration);
            var allureSection = allureConfiguration["allure"];
            try
            {
                var config = allureSection?.ToObject<AllureExtendedConfiguration>();
                if (config?.BrokenTestData != null)
                {
                    foreach (var word in config.BrokenTestData)
                    {
                        if (consoleMessage.Contains(word))
                        {
                            return Status.broken;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Ignored
            }

            switch (testOutcome)
            {
                case TestOutcome.Inconclusive:
                    return Status.broken;
                case TestOutcome.Aborted:
                    return Status.skipped;
                case TestOutcome.Passed:
                    return Status.passed;
                case TestOutcome.Error:
                    return Status.broken;
                case TestOutcome.Failed:
                    return Status.failed;
                default:
                    return Status.none;
            }
        }

        return Status.passed;
    }
}