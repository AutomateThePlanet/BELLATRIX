using Bellatrix.Playwright;

namespace Bellatrix.BugReporting.Playwright;

public static class AppExtensions
{
    extension(App _)
    {
        public BugReportingContextService BugReporting => ServicesCollection.Current.Resolve<BugReportingContextService>();
    }
}