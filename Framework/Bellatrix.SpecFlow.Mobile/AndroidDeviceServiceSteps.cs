// <copyright file="AndroidDeviceServiceSteps.cs" company="Automate The Planet Ltd.">
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
    public class AndroidDeviceServiceSteps : AndroidSteps
    {
        [When(@"I rotate the android device landscape")]
        public void WhenIRotateLandscape()
        {
            App.DeviceService.Rotate(OpenQA.Selenium.ScreenOrientation.Landscape);
        }

        [When(@"I rotate the android device portrait")]
        public void WhenIRotatePortrait()
        {
            App.DeviceService.Rotate(OpenQA.Selenium.ScreenOrientation.Portrait);
        }

        [When(@"I change the android device orientation to landscape")]
        public void WhenIChangeOrientationToLandscape()
        {
            App.DeviceService.Orientation = OpenQA.Selenium.ScreenOrientation.Landscape;
        }

        [When(@"I change the android device orientation to portrait")]
        public void WhenIChangeOrientationToPortrait()
        {
            App.DeviceService.Orientation = OpenQA.Selenium.ScreenOrientation.Portrait;
        }

        [When(@"I change the connection type to airplane mode")]
        public void WhenIChangeConnectionTypeToAirplaneMode()
        {
            App.DeviceService.ConnectionType = OpenQA.Selenium.Appium.Android.ConnectionType.AirplaneMode;
        }

        [When(@"I change the connection type to all network on")]
        public void WhenIChangeConnectionTypeToAllNetworkOn()
        {
            App.DeviceService.ConnectionType = OpenQA.Selenium.Appium.Android.ConnectionType.AllNetworkOn;
        }

        [When(@"I change the connection type to data only")]
        public void WhenIChangeConnectionTypeToDataOnly()
        {
            App.DeviceService.ConnectionType = OpenQA.Selenium.Appium.Android.ConnectionType.DataOnly;
        }

        [When(@"I change the connection type to none")]
        public void WhenIChangeConnectionTypeToNone()
        {
            App.DeviceService.ConnectionType = OpenQA.Selenium.Appium.Android.ConnectionType.None;
        }

        [When(@"I change the connection type to wifi only")]
        public void WhenIChangeConnectionTypeToWifiOnly()
        {
            App.DeviceService.ConnectionType = OpenQA.Selenium.Appium.Android.ConnectionType.WifiOnly;
        }

        [When(@"I lock the device")]
        public void WhenILockDevice()
        {
            App.DeviceService.Lock();
        }

        [When(@"I unlock the device")]
        public void WhenIUnLockDevice()
        {
            App.DeviceService.Unlock();
        }

        [When(@"I turn on the location service")]
        public void WhenITurnOnLocationService()
        {
            App.DeviceService.TurnOnLocationService();
        }

        [When(@"I open notifications")]
        public void WhenIOpenNotifications()
        {
            App.DeviceService.OpenNotifications();
        }

        [When(@"I set setting (.*) = (.*)")]
        public void WhenISetSetting(string name, string value)
        {
            App.DeviceService.SetSetting(name, value);
        }
    }
}
