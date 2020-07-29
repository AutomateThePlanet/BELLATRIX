// <copyright file="NavigationServiceSteps.cs" company="Automate The Planet Ltd.">
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
    public class NavigationServiceSteps : WebSteps
    {
        [When(@"I navigate to URL (.*)")]
        [When(@"I navigate to URL = (.*)")]
        public void WhenINavigateToUrl(string url)
        {
            App.NavigationService.Navigate(url);
        }

        [When(@"I navigate to local page (.*)")]
        [When(@"I navigate to local page = (.*)")]
        public void WhenINavigateToLocalPage(string page)
        {
            App.NavigationService.NavigateToLocalPage(page);
        }

        [When(@"I wait for partial URL (.*)")]
        [When(@"I wait for partial URL = (.*)")]
        public void WhenIWaitForPartualUrl(string url)
        {
            App.NavigationService.WaitForPartialUrl(url);
        }
    }
}
