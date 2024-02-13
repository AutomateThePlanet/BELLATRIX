// <copyright file="Constants.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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
namespace Bellatrix.Mobile.IOS.Tests;

public class Constants
{
    public const string IOSAppBundleId = @"com.xamarin.standardcontrols";

    // from https://emm.how/t/ios-11-list-of-default-apps-and-bundle-id-s/465
    public const string AppleCalendarBundleId = @"com.apple.mobilecal";
    public const string IOSNativeAppPath = @"AssemblyFolder/Demos/TestApp.app.zip";
    public const string IOSDefaultDeviceName = "iPhone 6";
    public const string IOSDefaultVersion = "11.3";
}