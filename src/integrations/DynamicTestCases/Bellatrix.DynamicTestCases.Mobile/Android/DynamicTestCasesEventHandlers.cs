using Bellatrix.Mobile.EventHandlers.Android;

namespace Bellatrix.Mobile.Android;

public static class DynamicTestCasesEventHandlers
{
    public static void Add()
    {
        var elementEventHandlers = new List<ComponentEventHandlers>
        {
            new DynamicTestCasesButtonEventHandlers(),
            new DynamicTestCasesRadioButtonEventHandlers(),
            new DynamicTestCasesCheckboxEventHandlers(),
            new DynamicTestCasesToggleButtonEventHandlers(),
            new DynamicTestCasesTextFieldEventHandlers(),
            new DynamicTestCasesComboBoxEventHandlers(),
            new DynamicTestCasesPasswordEventHandlers(),
            new DynamicTestCasesImageButtonEventHandlers(),
            new DynamicTestCasesSwitchEventHandlers(),
            new DynamicTestCasesNumberEventHandlers(),
            new DynamicTestCasesSeekBarEventHandlers(),
        };
        foreach (var elementEventHandler in elementEventHandlers)
        {
            elementEventHandler.SubscribeToAll();
        }
    }
}