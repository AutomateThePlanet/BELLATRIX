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
using Bellatrix.Mobile;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Mobile.TestExecutionExtensions
{
    public partial class AppWorkflowPlugin
    {
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

        [Given(@"I use device with name (.*)")]
        public void GivenUseDeviceWithName(string deviceName)
        {
            _currentAppConfiguration.DeviceName = deviceName;
        }

        [Given(@"I reuse the app if started")]
        public void GivenReuseAppIfStarted()
        {
            _currentAppConfiguration.AppBehavior = AppBehavior.ReuseIfStarted;
        }

        [Given(@"I use Android version (.*)")]
        public void GivenAndroidPlatformVersion(string platformVersion)
        {
            _currentAppConfiguration.PlatformVersion = platformVersion;
            _currentAppConfiguration.MobileOSType = MobileOSType.Android;
            _currentAppConfiguration.PlatformName = "Android";
        }

        [Given(@"I use iOS version (.*)")]
        public void GivenIOSPlatformVersion(string platformVersion)
        {
            _currentAppConfiguration.PlatformVersion = platformVersion;
            _currentAppConfiguration.MobileOSType = MobileOSType.IOS;
            _currentAppConfiguration.PlatformName = "iOS";
        }

        [Given(@"I use browser (.*)")]
        public void GivenUseBrowserName(string browserName)
        {
            _currentAppConfiguration.BrowserName = browserName;
            _androidBrowserStackAppConfiguration.BrowserName = browserName;
            _androidCrossBrowserTestingAppConfiguration.BrowserName = browserName;
            _androidSauceLabsAppConfiguration.BrowserName = browserName;
            _iosBrowserStackAppConfiguration.BrowserName = browserName;
            _iosCrossBrowserTestingAppConfiguration.BrowserName = browserName;
            _iosSauceLabsAppConfiguration.BrowserName = browserName;
        }

        [Given(@"I use app activity (.*)")]
        public void GivenUseAppActivity(string appActivity)
        {
            _currentAppConfiguration.AppActivity = appActivity;
            _androidBrowserStackAppConfiguration.AppActivity = appActivity;
            _androidCrossBrowserTestingAppConfiguration.AppActivity = appActivity;
            _androidSauceLabsAppConfiguration.AppActivity = appActivity;
        }

        [Given(@"I use app package (.*)")]
        public void GivenUseAppPackage(string appPackage)
        {
            _currentAppConfiguration.AppPackage = appPackage;
            _androidBrowserStackAppConfiguration.AppPackage = appPackage;
            _androidCrossBrowserTestingAppConfiguration.AppPackage = appPackage;
            _androidSauceLabsAppConfiguration.AppPackage = appPackage;
        }

        [Given(@"I run the app on Android (.*)")]
        public void GivenRunAppOnAndroidVersion(string platformVersion)
        {
            _androidSauceLabsAppConfiguration.PlatformVersion = platformVersion;
            _androidCrossBrowserTestingAppConfiguration.PlatformVersion = platformVersion;
            _androidBrowserStackAppConfiguration.PlatformVersion = platformVersion;
        }

        [Given(@"I run the app on iOS (.*)")]
        public void GivenRunAppOnIOSVersion(string platformVersion)
        {
            _iosSauceLabsAppConfiguration.PlatformVersion = platformVersion;
            _iosCrossBrowserTestingAppConfiguration.PlatformVersion = platformVersion;
            _iosBrowserStackAppConfiguration.PlatformVersion = platformVersion;
        }

        [Given(@"I want to record a video of the execution")]
        public void GivenRecordVideo()
        {
            _iosSauceLabsAppConfiguration.RecordVideo = true;
            _iosCrossBrowserTestingAppConfiguration.RecordVideo = true;
            _iosBrowserStackAppConfiguration.CaptureVideo = true;
            _androidSauceLabsAppConfiguration.RecordVideo = true;
            _androidCrossBrowserTestingAppConfiguration.RecordVideo = true;
            _androidBrowserStackAppConfiguration.CaptureVideo = true;
        }

        [Given(@"I want to record screenshots of the execution")]
        public void GivenRecordScreenshots()
        {
            _iosSauceLabsAppConfiguration.RecordScreenshots = true;
            _androidSauceLabsAppConfiguration.RecordScreenshots = true;
        }

        [Given(@"I want to user screen resolution (.*) px x (.*) px")]
        public void GivenRecordScreenshots(int width, int height)
        {
            string resolution = new Size(width, height).ToString();
            _iosSauceLabsAppConfiguration.ScreenResolution = resolution;
            _iosCrossBrowserTestingAppConfiguration.ScreenResolution = resolution;
            _iosBrowserStackAppConfiguration.ScreenResolution = resolution;
            _androidSauceLabsAppConfiguration.ScreenResolution = resolution;
            _androidCrossBrowserTestingAppConfiguration.ScreenResolution = resolution;
            _androidBrowserStackAppConfiguration.ScreenResolution = resolution;
        }

        [Given(@"I want to capture a network logs of the execution")]
        public void GivenCaptureNetworkLogs()
        {
            _iosCrossBrowserTestingAppConfiguration.RecordNetwork = true;
            _iosBrowserStackAppConfiguration.CaptureNetworkLogs = true;
            _androidCrossBrowserTestingAppConfiguration.RecordNetwork = true;
            _androidBrowserStackAppConfiguration.CaptureNetworkLogs = true;
        }

        [Given(@"I want to set build = (.*)")]
        public void GivenSetBuild(string build)
        {
            _iosCrossBrowserTestingAppConfiguration.Build = build;
            _iosBrowserStackAppConfiguration.Build = build;
            _androidCrossBrowserTestingAppConfiguration.Build = build;
            _androidBrowserStackAppConfiguration.Build = build;
        }
    }
}