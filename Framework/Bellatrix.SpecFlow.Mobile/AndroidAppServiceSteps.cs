// <copyright file="AndroidAppServiceSteps.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.SpecFlow.Mobile.Android
{
    [Binding]
    public class AndroidAppServiceSteps : AndroidSteps
    {
        [When(@"I background the android app for (.*) seconds")]
        public void WhenIBackgroundApp(int seconds)
        {
            App.AppService.BackgroundApp(seconds);
        }

        [When(@"I close the android app")]
        public void WhenICloseApp()
        {
            App.AppService.CloseApp();
        }

        [When(@"I launch the android app")]
        public void WhenILaunchApp()
        {
            App.AppService.LaunchApp();
        }

        [When(@"I reset the android app")]
        public void WhenIResetApp()
        {
            App.AppService.ResetApp();
        }

        [When(@"I install android app with path (.*)")]
        public void WhenIInstallApp(string path)
        {
            App.AppService.InstallApp(path);
        }

        [When(@"I remove android app with package (.*)")]
        public void WhenIRemoveAppWithPackage(string package)
        {
            App.AppService.RemoveApp(package);
        }

        [When(@"I start activity (.*) from package (.*)")]
        public void WhenIStartActiovity(string activityName, string packageName)
        {
            App.AppService.StartActivity(packageName, activityName);
        }

        [When(@"I start activity (.*) from package (.*) with intent (.*)")]
        public void WhenIStartActiovityWithIntent(string activityName, string packageName, string intentAction)
        {
            App.AppService.StartActivityWithIntent(packageName, activityName, intentAction);
        }
    }
}
