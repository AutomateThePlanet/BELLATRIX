// <copyright file="BrowserServiceSteps.cs" company="Automate The Planet Ltd.">
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
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Web
{
    [Binding]
    public class BrowserServiceSteps : WebSteps
    {
        [When(@"I click browser's back button")]
        public void WhenIClickBackBrowserButton()
        {
            App.BrowserService.Back();
        }

        [When(@"I click browser's forward button")]
        public void WhenIClickForwardBrowserButton()
        {
            App.BrowserService.Forward();
        }

        [When(@"I click browser's refresh button")]
        [When(@"I refresh the browser")]
        [When(@"I refresh the page")]
        public void WhenIClickRefreshBrowserButton()
        {
            App.BrowserService.Refresh();
        }

        [When(@"I wait until the browser is ready")]
        public void WhenIWaitForBrowserToBeReady()
        {
            App.BrowserService.WaitUntilReady();
        }

        [When(@"I wait for all AJAX requests to finish")]
        public void WhenIWaitForAjaxRequests()
        {
            App.BrowserService.WaitForAjax();
        }

        [When(@"I wait for all Angular requests to finish")]
        public void WhenIWaitForAngularRequests()
        {
            App.BrowserService.WaitForAngular();
        }

        [When(@"I maximize the browser")]
        public void WhenIMaximizeBrowser()
        {
            App.BrowserService.Maximize();
        }
    }
}
