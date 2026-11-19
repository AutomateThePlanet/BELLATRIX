using Bellatrix.BugReporting;
using Bellatrix.Web.Controls.EventHandlers;

namespace Bellatrix.Web.Extensions.Controls.EventHandlers;

public static class ComponentEventHandlersExtensions
{
    extension(ComponentEventHandlers _)
    {
        public BugReportingContextService BugReportingContextService => ServicesCollection.Current.Resolve<BugReportingContextService>();
    }
}