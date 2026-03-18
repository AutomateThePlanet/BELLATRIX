using Bellatrix.Web.Controls.EventHandlers;
using Bellatrix.Web.Extensions.Controls.EventHandlers;

namespace Bellatrix.Web;

public static class DynamicTestCasesEventHandlers
{
    public static void Add()
    {
        var elementEventHandlers = new List<ComponentEventHandlers>()
        {
            new DynamicTestCasesTextFieldEventHandlers(),
            new DynamicTestCasesDateEventHandlers(),
            new DynamicTestCasesColorEventHandlers(),
            new DynamicTestCasesCheckboxEventHandlers(),
            new DynamicTestCasesDateTimeLocalEventHandlers(),
            new DynamicTestCasesElementEventHandlers(),
            new DynamicTestCasesEmailEventHandlers(),
            new DynamicTestCasesInputFileEventHandlers(),
            new DynamicTestCasesMonthEventHandlers(),
            new DynamicTestCasesNumberEventHandlers(),
            new DynamicTestCasesPasswordEventHandlers(),
            new DynamicTestCasesPhoneEventHandlers(),
            new DynamicTestCasesRangeEventHandlers(),
            new DynamicTestCasesSearchEventHandlers(),
            new DynamicTestCasesSelectEventHandlers(),
            new DynamicTestCasesTextAreaEventHandlers(),
            new DynamicTestCasesTimeEventHandlers(),
            new DynamicTestCasesUrlEventHandlers(),
            new DynamicTestCasesWeekEventHandlers(),
        };
        foreach (var elementEventHandler in elementEventHandlers)
        {
            elementEventHandler.SubscribeToAll();
        }
    }
}