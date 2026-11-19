using Bellatrix.Desktop.EventHandlers;
using Bellatrix.DynamicTestCases;

namespace Bellatrix.Desktop.EventHandlers;

public static class ComponentEventHandlersExtensions
{
    extension(ComponentEventHandlers _)
    {
        public DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
}