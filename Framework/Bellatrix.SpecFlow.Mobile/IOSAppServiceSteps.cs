// <copyright file="IOSAppServiceSteps.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.SpecFlow.Mobile;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Mobile.IOS
{
    [Binding]
    public class IOSAppServiceSteps : IOSSteps
    {
        [When(@"I background the iOS app for (.*) seconds")]
        public void WhenIBackgroundApp(int seconds)
        {
            App.AppService.BackgroundApp(seconds);
        }

        [When(@"I close the iOS app")]
        public void WhenICloseApp()
        {
            App.AppService.CloseApp();
        }

        [When(@"I launch the iOS app")]
        public void WhenILaunchApp()
        {
            App.AppService.CloseApp();
        }

        [When(@"I reset the iOS app")]
        public void WhenIResetApp()
        {
            App.AppService.ResetApp();
        }

        [When(@"I install iOS app with path (.*)")]
        public void WhenIInstallApp(string path)
        {
            App.AppService.InstallApp(path);
        }
    }
}
