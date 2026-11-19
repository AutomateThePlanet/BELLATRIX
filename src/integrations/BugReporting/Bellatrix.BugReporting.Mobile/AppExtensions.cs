using Bellatrix.BugReporting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile;

public static class AppExtensions
{
    extension(App<AndroidDriver, AppiumElement> _)
    {
        public BugReportingContextService BugReporting => ServicesCollection.Current.Resolve<BugReportingContextService>();
    }

    extension(App<IOSDriver, AppiumElement> _)
    {
        public BugReportingContextService BugReporting => ServicesCollection.Current.Resolve<BugReportingContextService>();
    }
}