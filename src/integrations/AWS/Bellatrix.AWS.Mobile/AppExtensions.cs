using Bellatrix.Mobile;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.AWS.Mobile;

public static class AppExtensions
{
    extension(App<AndroidDriver, AppiumElement> _)
    {
        public AWSServicesFactory AWS => ServicesCollection.Current.Resolve<AWSServicesFactory>();
    }
    
    extension(App<IOSDriver, AppiumElement> _)
    {
        public AWSServicesFactory AWS => ServicesCollection.Current.Resolve<AWSServicesFactory>();
    }
}