using Bellatrix.Playwright;

namespace Bellatrix.DynamicTestCases.Web;

public static class PageExtensions
{
    extension(Page _)
    {
        public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
}