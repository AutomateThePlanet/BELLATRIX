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
using System.Drawing;
using Bellatrix.Mobile.Enums;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Mobile.TestExecutionExtensions
{
    public partial class AppWorkflowPlugin
    {
        [Given(@"I open Android app with path (.*) in SauceLabs")]
        public void GivenOpenAndroidAppInSauceLabs(string appPath)
        {
            _androidSauceLabsAppConfiguration = new AndroidSauceLabsAppConfiguration
            {
                AppPath = appPath,
                ExecutionType = ExecutionType.SauceLabs,
            };
            _currentAppConfiguration = _androidSauceLabsAppConfiguration;
        }

        [Given(@"I open iOS app with path (.*) in SauceLabs")]
        public void GivenOpenIOSAppInSauceLabs(string appPath)
        {
            _iosSauceLabsAppConfiguration = new IOSSauceLabsAppConfiguration
                                                {
                                                    AppPath = appPath,
                                                    ExecutionType = ExecutionType.SauceLabs,
                                                };
            _currentAppConfiguration = _iosSauceLabsAppConfiguration;
        }
    }
}