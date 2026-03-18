using Bellatrix.DynamicTestCases;
using Bellatrix.Mobile.EventHandlers.Android;

namespace Bellatrix.Mobile.Android;

public static class ComponentEventHandlersExtensions
{
    extension(ComponentEventHandlers _)
    {
        public DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
}