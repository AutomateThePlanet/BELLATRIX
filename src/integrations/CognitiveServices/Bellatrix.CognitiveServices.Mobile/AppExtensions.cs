using Bellatrix.CognitiveServices;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile;

public static class AppExtensions
{
    extension(App<AndroidDriver, AppiumElement> _)
    {
        public ComputerVision ComputerVision => ServicesCollection.Current.Resolve<ComputerVision>();
        public FormRecognizer FormRecognizer => ServicesCollection.Current.Resolve<FormRecognizer>();
    }
    
    extension(App<IOSDriver, AppiumElement> _)
    {
        public ComputerVision ComputerVision => ServicesCollection.Current.Resolve<ComputerVision>();
        public FormRecognizer FormRecognizer => ServicesCollection.Current.Resolve<FormRecognizer>();
    }
}