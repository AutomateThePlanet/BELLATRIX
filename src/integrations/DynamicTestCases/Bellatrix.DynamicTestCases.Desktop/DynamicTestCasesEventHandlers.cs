using Bellatrix.Desktop.DynamicTestCases;
using Bellatrix.Desktop.EventHandlers;

namespace Bellatrix.Desktop;

public static class DynamicTestCasesEventHandlers
{
    public static void Add()
    {
        var elementEventHandlers = new List<ComponentEventHandlers>()
        {
            new DynamicTestCasesCheckboxEventHandlers(),
            new DynamicTestCasesComboBoxEventHandlers(),
            new DynamicTestCasesDateEventHandlers(),
            new DynamicTestCasesComponentEventHandlers(),
            new DynamicTestCasesPasswordEventHandlers(),
            new DynamicTestCasesTextAreaEventHandlers(),
            new DynamicTestCasesTextFieldEventHandlers(),
            new DynamicTestCasesTimeEventHandlers(),
        };
        foreach (var elementEventHandler in elementEventHandlers)
        {
            elementEventHandler.SubscribeToAll();
        }
    }
}