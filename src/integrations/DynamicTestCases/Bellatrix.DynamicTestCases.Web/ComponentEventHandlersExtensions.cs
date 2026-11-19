using Bellatrix.DynamicTestCases;
using Bellatrix.Web.Controls.EventHandlers;

namespace Bellatrix.Web.Extensions.Controls.EventHandlers;

public static class ComponentEventHandlersExtensions
{
    extension(ComponentEventHandlers _)
    {
        public DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
}