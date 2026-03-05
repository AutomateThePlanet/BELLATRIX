using Bellatrix.BugReporting;
using Bellatrix.Mobile.EventHandlers.IOS;

namespace Bellatrix.Mobile.IOS;

public static class ComponentEventHandlerExtensions
{
    extension(ComponentEventHandlers _)
    {
        public BugReportingContextService BugReportingContextService => ServicesCollection.Current.Resolve<BugReportingContextService>();
    }
}