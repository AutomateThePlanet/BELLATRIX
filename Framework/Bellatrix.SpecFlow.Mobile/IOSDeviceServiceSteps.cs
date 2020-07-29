// <copyright file="IOSDeviceServiceSteps.cs" company="Automate The Planet Ltd.">
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
    public class IOSDeviceServiceSteps : IOSSteps
    {
        [When(@"I rotate the iOS device landscape")]
        public void WhenIRotateLandscape()
        {
            App.DeviceService.Rotate(OpenQA.Selenium.ScreenOrientation.Landscape);
        }

        [When(@"I rotate the iOS device portrait")]
        public void WhenIRotatePortrait()
        {
            App.DeviceService.Rotate(OpenQA.Selenium.ScreenOrientation.Portrait);
        }

        [When(@"I change the iOS device orientation to landscape")]
        public void WhenIChangeOrientationToLandscape()
        {
            App.DeviceService.Orientation = OpenQA.Selenium.ScreenOrientation.Landscape;
        }

        [When(@"I change the iOS device orientation to portrait")]
        public void WhenIChangeOrientationToPortrait()
        {
            App.DeviceService.Orientation = OpenQA.Selenium.ScreenOrientation.Portrait;
        }

        [When(@"I lock the iOS device for (.*) seconds")]
        public void WhenILockDevice(int seconds)
        {
            App.DeviceService.Lock(seconds);
        }

        [When(@"I shake the iOS device")]
        public void WhenIShakeDevice()
        {
            App.DeviceService.ShakeDevice();
        }
    }
}
