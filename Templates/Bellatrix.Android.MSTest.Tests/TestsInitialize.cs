﻿using Bellatrix.Mobile.Android;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.MSTest.Tests
{
    [TestClass]
    public class TestsInitialize : AndroidTest
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            var app = new AndroidApp();

            app.UseExceptionLogger();
            app.UseMsTestSettings();
            app.UseAppBehavior();
            app.UseLogExecutionBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseAndroidDriverScreenshotsOnFail();
            app.UseElementsBddLogging();
            app.UseValidateExtensionsBddLogging();
            app.UseLayoutAssertionExtensionsBddLogging();
            app.StartAppiumLocalService();
            app.Initialize();

            // Software machine automation module helps you to install the required software to the developer's machine
            // such as a specific version of the browsers, browser extensions, and any other required software.
            // You can configure it from BELLATRIX configuration file testFrameworkSettings.json
            //  "machineAutomationSettings": {
            //      "isEnabled": "true",
            //      "packagesToBeInstalled": [ "goog", "firefox --version=65.0.2", "opera" ]
            //  }
            //
            // You need to specify the packages to be installed in the packagesToBeInstalled array. You can search for packages in the
            // public community repository- https://chocolatey.org/
            //
            // To use the service you need to start Visual Studio in Administrative Mode. The service supports currently only Windows.
            // In the future BELLATRIX releases we will support OSX and Linux as well.
            //
            // To use the machine automation setup- install Bellatrix.MachineAutomation NuGet package.
            // SoftwareAutomationService.InstallRequiredSoftware();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            var app = ServicesCollection.Current.Resolve<AndroidApp>();
            app?.Dispose();
            app?.StopAppiumLocalService();
        }
    }
}