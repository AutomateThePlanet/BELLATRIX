using Bellatrix.CognitiveServices;
using Bellatrix.DynamicTestCases;

namespace Bellatrix.Web.GettingStarted;

public static partial class AppExtensions
{
    extension(App app)
    {
        public DynamicTestCasesService TestCases
        {
            get 
            {
                return new DynamicTestCasesService();
            }
        }
    }
}