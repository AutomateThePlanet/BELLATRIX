using Bellatrix.Web.Controls.EventHandlers;
using Bellatrix.Web.Extensions.Controls.EventHandlers;

namespace Bellatrix.Web;

public static class BugReportingEventHandlers
{
    public static void Add()
    {
        var elementEventHandlers = new List<ComponentEventHandlers>()
        {
            new BugReportingTextFieldEventHandlers(),
            new BugReportingDateEventHandlers(),
            new BugReportingColorEventHandlers(),
            new BugReportingCheckboxEventHandlers(),
            new BugReportingDateTimeLocalEventHandlers(),
            new BugReportingElementEventHandlers(),
            new BugReportingEmailEventHandlers(),
            new BugReportingInputFileEventHandlers(),
            new BugReportingMonthEventHandlers(),
            new BugReportingNumberEventHandlers(),
            new BugReportingPasswordEventHandlers(),
            new BugReportingPhoneEventHandlers(),
            new BugReportingRangeEventHandlers(),
            new BugReportingSearchEventHandlers(),
            new BugReportingSelectEventHandlers(),
            new BugReportingTextAreaEventHandlers(),
            new BugReportingTimeEventHandlers(),
            new BugReportingUrlEventHandlers(),
            new BugReportingWeekEventHandlers(),
        };
        foreach (var elementEventHandler in elementEventHandlers)
        {
            elementEventHandler.SubscribeToAll();
        }
    }
}