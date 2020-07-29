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
using Bellatrix.Desktop;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Desktop.TestExecutionExtensions
{
    [Binding]
    public partial class AppWorkflowPlugin
    {
        [Given(@"I open app")]
        public void GivenIOpenApp()
        {
            if (_currentAppConfiguration != null)
            {
                ResolvePreviousBrowserConfiguration();

                // Decide whether the browser needs to be restarted
                bool shouldRestartApp = ShouldRestartApp();

                if (shouldRestartApp)
                {
                    RestartApp();
                }
            }
        }

        [Given(@"I use app with path (.*)")]
        public void GivenUseApp(string appPath)
        {
            _currentAppConfiguration.AppPath = appPath;
        }

        [Given(@"I restart the app on test fail")]
        public void GivenRestartAppOnFail()
        {
            _currentAppConfiguration.AppBehavior = AppBehavior.RestartOnFail;
        }

        [Given(@"I restart the app every time")]
        public void GivenRestartAppEveryTime()
        {
            _currentAppConfiguration.AppBehavior = AppBehavior.RestartEveryTime;
        }

        [Given(@"I resize the app (.*) px x  (.*) px")]
        public void GivenAppBrowserEveryTime(int width, int height)
        {
            _currentAppConfiguration.Size = new Size(width, height);
        }

        [Given(@"I reuse the browser if started")]
        public void GivenReuseAppIfStarted()
        {
            _currentAppConfiguration.AppBehavior = AppBehavior.ReuseIfStarted;
        }
    }
}
