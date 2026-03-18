using Bellatrix.DynamicTestCases;
using Bellatrix.Playwright.Controls.EventHandlers;

namespace Bellatrix.Playwright.Extensions.Controls.EventHandlers;

public static class ComponentEventHandlersExtensions
{
    extension(ComponentEventHandlers _)
    {
        public DynamicTestCasesService DynamicTestCasesService => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
}