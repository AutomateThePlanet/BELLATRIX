﻿// <copyright file="AppServiceTests.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
using System.IO;
using Bellatrix.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BA = Bellatrix.Assertions;

namespace Bellatrix.Mobile.Android.Tests;

[TestClass]
[Android(Constants.AndroidNativeAppPath,
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".view.Controls1",
    Lifecycle.ReuseIfStarted)]
[AllureSuite("Services")]
[AllureFeature("AppService")]
public class AppServiceTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void TestBackgroundApp()
    {
        App.AppService.BackgroundApp(1);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void TestResetApp()
    {
        App.AppService.ResetApp();
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void InstallAppInstalledTrue_When_AppIsInstalled()
    {
        Assert.IsTrue(App.AppService.IsAppInstalled(Constants.AndroidNativeAppId));
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void InstallAppInstalledFalse_When_AppIsUninstalled()
    {
        string appPath = Path.Combine(ProcessProvider.GetExecutingAssemblyFolder(), "Demos\\ApiDemos.apk");
        App.AppService.InstallApp(appPath);

        App.AppService.RemoveApp(Constants.AndroidNativeAppId);

        Assert.IsFalse(App.AppService.IsAppInstalled(Constants.AndroidNativeAppId));

        App.AppService.InstallApp(appPath);
        Assert.IsTrue(App.AppService.IsAppInstalled(Constants.AndroidNativeAppId));
    }
}