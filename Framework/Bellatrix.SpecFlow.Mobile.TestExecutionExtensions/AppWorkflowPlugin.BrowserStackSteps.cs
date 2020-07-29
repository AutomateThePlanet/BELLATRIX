// <copyright file="AppWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile;
using Bellatrix.Mobile.Enums;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Mobile.TestExecutionExtensions
{
    [Binding]
    public partial class AppWorkflowPlugin
    {
        [Given(@"I open Android app with path (.*) in BrowserStack")]
        public void GivenOpenAndroidAppInBrowserStack(string appPath)
        {
            _androidBrowserStackAppConfiguration = new AndroidBrowserStackAppConfiguration
                                                {
                                                    AppPath = appPath,
                                                    ExecutionType = ExecutionType.BrowserStack,
                                                };
            _currentAppConfiguration = _androidBrowserStackAppConfiguration;
        }

        [Given(@"I open iOS app with path (.*) in BrowserStack")]
        public void GivenOpenIOSAppInBrowserStack(string appPath)
        {
            _iosBrowserStackAppConfiguration = new IOSBrowserStackAppConfiguration
                                            {
                                                AppPath = appPath,
                                                ExecutionType = ExecutionType.BrowserStack,
                                            };
            _currentAppConfiguration = _iosBrowserStackAppConfiguration;
        }

        [Given(@"I want to use console log type (.*)")]
        public void GivenUseConsoleLogTypeInBrowserStack(BrowserStackConsoleLogType consoleLogType)
        {
            _androidBrowserStackAppConfiguration.ConsoleLogType = consoleLogType;
            _iosBrowserStackAppConfiguration.ConsoleLogType = consoleLogType;
        }
    }
}