using Bellatrix.DynamicTestCases;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile;

public static class AppExtensions
{
    extension(App<AndroidDriver, AppiumElement> _)
    {
        public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
    
    extension(App<IOSDriver, AppiumElement> _)
    {
        public DynamicTestCasesService TestCases => ServicesCollection.Current.Resolve<DynamicTestCasesService>();
    }
}