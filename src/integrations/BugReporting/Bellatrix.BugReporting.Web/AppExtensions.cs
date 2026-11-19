using Bellatrix.Web;

namespace Bellatrix.BugReporting.Web;

public static class AppExtensions
{
    extension(App _)
    {
        public BugReportingContextService BugReporting => ServicesCollection.Current.Resolve<BugReportingContextService>();
    }
}