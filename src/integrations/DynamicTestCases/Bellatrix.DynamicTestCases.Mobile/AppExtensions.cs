using Bellatrix.DynamicTestCases;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile;

public static class AppExtensions
{
    extension(App<AppiumDriver, AppiumElement> _)
    {
        public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
}