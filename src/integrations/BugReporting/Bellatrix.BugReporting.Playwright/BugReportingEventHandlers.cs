using Bellatrix.Playwright.Controls.EventHandlers;
using Bellatrix.Playwright.Extensions.Controls.EventHandlers;

namespace Bellatrix.Playwright;

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
            new BugReportingNumberEventHandlers(),
            new BugReportingMultipleSelectEventHandlers(),
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