using Bellatrix.Playwright;

namespace Bellatrix.DynamicTestCases.Playwright;

public static class AppExtensions
{
    extension(App _)
    {
        public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
}