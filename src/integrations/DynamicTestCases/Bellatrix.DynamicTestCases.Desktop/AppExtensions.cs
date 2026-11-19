using Bellatrix.Desktop;

namespace Bellatrix.DynamicTestCases.Desktop;

public static class AppExtensions
{
    extension(App _)
    {
        public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
}