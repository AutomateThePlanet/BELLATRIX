using System;
using System.Linq;
using Bellatrix.Web.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Web.Tests
{
    [Binding]
    [TestClass]
    public class TestsInitialize : WebHooks
    {
        [BeforeTestRun(Order = 1)]
        public static void PreBeforeTestRun()
        {
            var app = new Bellatrix.Web.App();
            app.UseExceptionLogger();
            app.UseMsTestSettings();
            app.UseLogger();
            app.UseControlDataHandlers();
            app.UseBrowserBehavior();
            app.UseLogExecutionBehavior();
            app.UseControlLocalOverridesCleanBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseFullPageScreenshotsOnFail();

            InitializeTestExecutionBehaviorObservers(TestExecutionProvider);

            // Software machine automation module helps you to install the required software to the developer's machine
            // such as a specific version of the browsers, browser extensions, and any other required software.
            // You can configure it from BELLATRIX configuration file testFrameworkSettings.json
            //  "machineAutomationSettings": {
            //      "isEnabled": "true",
            //      "packagesToBeInstalled": [ "googlechrome", "firefox --version=65.0.2", "opera" ]
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

        [AfterTestRun(Order = 1)]
        public static void PreAfterTestRun()
        {
            var app = ServicesCollection.Current.Resolve<Bellatrix.Web.App>();
            app?.Dispose();
        }

        [BeforeFeature(Order = 1)]
        public static void PreBeforeFeatureArrange(FeatureContext featureContext)
        {
            try
            {
                TestExecutionProvider.PreBeforeFeatureArrange(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeFeatureFailed(ex);
                throw;
            }
        }

        [BeforeFeature(Order = 100)]
        public static void PostBeforeFeatureArrange(FeatureContext featureContext)
        {
            try
            {
                TestExecutionProvider.PostBeforeFeatureArrange(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeFeatureFailed(ex);
                throw;
            }
        }

        [BeforeFeature(Order = 101)]
        public static void PreBeforeFeatureAct(FeatureContext featureContext)
        {
            try
            {
                TestExecutionProvider.PreBeforeFeatureAct(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeFeatureFailed(ex);
                throw;
            }
        }

        [BeforeFeature(Order = 200)]
        public static void PostBeforeFeatureAct(FeatureContext featureContext)
        {
            try
            {
                TestExecutionProvider.PostBeforeFeatureAct(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeFeatureFailed(ex);
                throw;
            }
        }
    }
}
