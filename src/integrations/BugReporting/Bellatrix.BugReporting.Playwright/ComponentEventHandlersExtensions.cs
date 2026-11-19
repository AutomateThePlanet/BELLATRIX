using Bellatrix.BugReporting;
using Bellatrix.Playwright.Controls.EventHandlers;

namespace Bellatrix.Playwright.Extensions.Controls.EventHandlers;

public static class ComponentEventHandlersExtensions
{
    extension(ComponentEventHandlers _)
    {
        public BugReportingContextService BugReportingContextService => ServicesCollection.Current.Resolve<BugReportingContextService>();
    }
}