using Bellatrix.Desktop.EventHandlers;
using Bellatrix.BugReporting;

namespace Bellatrix.Desktop.EventHandlers;

public static class ComponentEventHandlersExtensions
{
    extension(ComponentEventHandlers _)
    {
        public BugReportingContextService BugReportingContextService => ServicesCollection.Current.Resolve<BugReportingContextService>();
    }
}