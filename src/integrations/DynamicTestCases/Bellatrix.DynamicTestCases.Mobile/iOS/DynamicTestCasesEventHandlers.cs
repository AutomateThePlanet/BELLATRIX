using Bellatrix.Mobile.EventHandlers.IOS;

namespace Bellatrix.Mobile.IOS;

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
            new DynamicTestCasesNumberEventHandlers(),
            new DynamicTestCasesSeekBarEventHandlers(),
        };
        foreach (var elementEventHandler in elementEventHandlers)
        {
            elementEventHandler.SubscribeToAll();
        }
    }
}