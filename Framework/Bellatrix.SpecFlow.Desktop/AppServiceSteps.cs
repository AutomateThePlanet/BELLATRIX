// <copyright file="AppServiceSteps.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.SpecFlow.Desktop
{
    [Binding]
    public class AppServiceSteps : DesktopSteps
    {
        [When(@"I click app's back button")]
        public void WhenIClickBackAppButton()
        {
            App.AppService.Back();
        }

        [When(@"I click app's forward button")]
        public void WhenIClickForwardAppButton()
        {
            App.AppService.Forward();
        }

        [When(@"I maximize the app")]
        public void WhenIMaximizeApp()
        {
            App.AppService.Maximize();
        }
    }
}
