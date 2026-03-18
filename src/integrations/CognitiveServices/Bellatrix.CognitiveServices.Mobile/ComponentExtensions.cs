using Bellatrix.CognitiveServices;
using Bellatrix.CognitiveServices.services;
using Bellatrix.Mobile.Core;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile;

public static class ComponentExtensions
{
    extension(Component<AndroidDriver, AppiumElement> component)
    {
        public AssertedFormPage AIAnalyze()
        {
            string currentComponentScreenshot = component.TakeScreenshot();
            var formRecognizer = ServicesCollection.Current.Resolve<FormRecognizer>();
            var analyzedComponent = formRecognizer.Analyze(currentComponentScreenshot);
            return analyzedComponent;
        }
    }
    
    extension(Component<IOSDriver, AppiumElement> component)
    {
        public AssertedFormPage AIAnalyze()
        {
            string currentComponentScreenshot = component.TakeScreenshot();
            var formRecognizer = ServicesCollection.Current.Resolve<FormRecognizer>();
            var analyzedComponent = formRecognizer.Analyze(currentComponentScreenshot);
            return analyzedComponent;
        }
    }
}