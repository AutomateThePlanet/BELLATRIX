using Bellatrix.Web;

namespace Bellatrix.DynamicTestCases.Web;

public static class AppExtensions
{
    extension(App _)
    {
        public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
}