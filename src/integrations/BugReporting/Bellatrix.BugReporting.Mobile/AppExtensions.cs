using Bellatrix.BugReporting;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile;

public static class AppExtensions
{
    extension(App<AppiumDriver, AppiumElement> _)
    {
        public BugReportingContextService BugReporting => ServicesCollection.Current.Resolve<BugReportingContextService>();
    }
}