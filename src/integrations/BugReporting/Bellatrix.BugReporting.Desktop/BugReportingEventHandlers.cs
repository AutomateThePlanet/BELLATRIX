using Bellatrix.Desktop.BddLogging;
using Bellatrix.Desktop.BugReporting;
using Bellatrix.Desktop.EventHandlers;

namespace Bellatrix.Desktop;

public static class BugReportingEventHandlers
{
    public static void Add()
    {
        var elementEventHandlers = new List<ComponentEventHandlers>()
        {
            new BugReportingCheckboxEventHandlers(),
            new BugReportingComboBoxEventHandlers(),
            new BugReportingDateEventHandlers(),
            new BugReportingComponentEventHandlers(),
            new BugReportingPasswordEventHandlers(),
            new BugReportingTextAreaEventHandlers(),
            new BugReportingTextFieldEventHandlers(),
            new BugReportingTimeEventHandlers(),
        };
        foreach (var elementEventHandler in elementEventHandlers)
        {
            elementEventHandler.SubscribeToAll();
        }
    }
}