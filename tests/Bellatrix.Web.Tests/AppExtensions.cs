using Bellatrix.DynamicTestCases;

namespace Bellatrix.Web.GettingStarted;

public static partial class AppExtensions
{
    extension(App app)
    {
        public static DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
}