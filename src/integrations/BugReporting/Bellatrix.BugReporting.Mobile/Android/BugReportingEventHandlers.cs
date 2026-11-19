using Bellatrix.Mobile.EventHandlers.Android;

namespace Bellatrix.Mobile.Android;

public static class BugReportingEventHandlers
{
    public static void Add()
    {
        var elementEventHandlers = new List<ComponentEventHandlers>
        {
            new BugReportingButtonEventHandlers(),
            new BugReportingRadioButtonEventHandlers(),
            new BugReportingCheckboxEventHandlers(),
            new BugReportingToggleButtonEventHandlers(),
            new BugReportingTextFieldEventHandlers(),
            new BugReportingComboBoxEventHandlers(),
            new BugReportingPasswordEventHandlers(),
            new BugReportingImageButtonEventHandlers(),
            new BugReportingNumberEventHandlers(),
            new BugReportingSeekBarEventHandlers(),
        };
        foreach (var elementEventHandler in elementEventHandlers)
        {
            elementEventHandler.SubscribeToAll();
        }
    }
}