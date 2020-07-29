// <copyright file="BrowserWorkflowPlugin.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using System.Drawing;
using Bellatrix.Web;
using Bellatrix.Web.Enums;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Web.TestExecutionExtensions.Browser
{
    public partial class BrowserWorkflowPlugin
    {
        [Given(@"I use (.*) browser")]
        public void GivenUseBrowser(string browser)
        {
            _currentBrowserConfiguration.BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), browser);
            _currentBrowserConfiguration.ExecutionType = ExecutionType.Regular;
        }

        [Given(@"I use (.*) browser on Windows")]
        public void GivenOpenBrowserWindows(string browser)
        {
            if (_currentPlatform.Equals(OS.Windows))
            {
                _currentBrowserConfiguration.BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), browser);
            }
        }

        [Given(@"I use (.*) browser on OSX")]
        public void GivenOpenBrowserOSX(string browser)
        {
            if (_currentPlatform.Equals(OS.OSX))
            {
                _currentBrowserConfiguration.BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), browser);
            }
        }

        [Given(@"I restart the browser on test fail")]
        public void GivenRestartBrowserOnFail()
        {
            _currentBrowserConfiguration.BrowserBehavior = BrowserBehavior.RestartOnFail;
        }

        [Given(@"I restart the browser every time")]
        public void GivenRestartBrowserEveryTime()
        {
            _currentBrowserConfiguration.BrowserBehavior = BrowserBehavior.RestartEveryTime;
        }

        [Given(@"I reuse the browser if started")]
        public void GivenReuseBrowserIfStarted()
        {
            _currentBrowserConfiguration.BrowserBehavior = BrowserBehavior.ReuseIfStarted;
        }

        [Given(@"I capture HTTP traffic")]
        public void GivenCaptureHttpTraffic()
        {
            _currentBrowserConfiguration.ShouldCaptureHttpTraffic = true;
        }

        [Given(@"I want to run the browser on (.*) platform")]
        public void GivenRunBrowserOnOperatingSystem(string platform)
        {
            _crossBrowserTestingBrowserConfiguration.Platform = platform;
            _sauceLabsBrowserConfiguration.Platform = platform;
            _browserStackBrowserConfiguration.OperatingSystem = platform;
        }

        [Given(@"I want to record a video of the execution")]
        public void GivenRecordVideo()
        {
            _crossBrowserTestingBrowserConfiguration.RecordVideo = true;
            _sauceLabsBrowserConfiguration.RecordVideo = true;
            _browserStackBrowserConfiguration.CaptureVideo = true;
        }

        [Given(@"I want to capture a network logs of the execution")]
        public void GivenCaptureNetworkLogs()
        {
            _crossBrowserTestingBrowserConfiguration.RecordNetwork = true;
            _browserStackBrowserConfiguration.CaptureNetworkLogs = true;
        }

        [Given(@"I want to set build = (.*)")]
        public void GivenSetBuild(string build)
        {
            _crossBrowserTestingBrowserConfiguration.Build = build;
            _browserStackBrowserConfiguration.Build = build;
        }

        [Given(@"I resize the browser (.*) px x (.*) px")]
        public void GivenResizeBrowserToResolution(int width, int height)
        {
            string resolution = new Size(width, height).ToString();
            _sauceLabsBrowserConfiguration.ScreenResolution = resolution;
            _sauceLabsBrowserConfiguration.ScreenResolution = resolution;
            _browserStackBrowserConfiguration.ScreenResolution = resolution;
        }
    }
}