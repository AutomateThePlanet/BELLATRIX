using Bellatrix.DynamicTestCases;
using Bellatrix.Mobile.EventHandlers.IOS;

namespace Bellatrix.Mobile.IOS;

public static class ComponentEventHandlersExtensions
{
    extension(ComponentEventHandlers _)
    {
        public DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
}