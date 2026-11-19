using Bellatrix.BugReporting;
using Bellatrix.Mobile.EventHandlers.Android;

namespace Bellatrix.Mobile.Android;

public static class ComponentEventHandlerExtensions
{
    extension(ComponentEventHandlers _)
    {
        public BugReportingContextService BugReportingContextService => ServicesCollection.Current.Resolve<BugReportingContextService>();
    }
}