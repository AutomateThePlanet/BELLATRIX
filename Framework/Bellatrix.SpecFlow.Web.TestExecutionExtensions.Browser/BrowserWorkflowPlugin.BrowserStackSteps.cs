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
using Bellatrix.SpecFlow.Web.TestExecutionExtensions.Browser.CloudProviders;
using Bellatrix.Web;
using Bellatrix.Web.Enums;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Web.TestExecutionExtensions.Browser
{
    public partial class BrowserWorkflowPlugin
    {
        [Given(@"I open (.*) browser in BrowserStack")]
        public void GivenOpenBrowserInBrowserStack(string browser)
        {
            _browserStackBrowserConfiguration = new BrowserStackBrowserConfiguration
                                             {
                                                 BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), browser),
                                                 ExecutionType = ExecutionType.BrowserStack,
                                             };
            _currentBrowserConfiguration = _browserStackBrowserConfiguration;
        }

        [Given(@"I open (.*) browser (.*) in BrowserStack")]
        public void GivenOpenBrowserInBrowserStack(string browser, string browserVersion)
        {
            _browserStackBrowserConfiguration = new BrowserStackBrowserConfiguration
                                             {
                                                 BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), browser),
                                                 ExecutionType = ExecutionType.BrowserStack,
                                                 BrowserVersion = browserVersion,
                                             };
            _currentBrowserConfiguration = _browserStackBrowserConfiguration;
        }

        [Given(@"I want to run the browser on (.*) OS version")]
        public void GivenRunBrowserOnOSVersionInBrowserStack(string osVersion)
        {
            _browserStackBrowserConfiguration.OSVersion = osVersion;
        }

        [Given(@"I want to use console log type (.*)")]
        public void GivenUseConsoleLogTypeInBrowserStack(BrowserStackConsoleLogType consoleLogType)
        {
            _browserStackBrowserConfiguration.ConsoleLogType = consoleLogType;
        }
    }
}