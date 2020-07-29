using Bellatrix.Mobile.Controls.IOS;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS.GettingStarted.ExtensionMethodsWaitMethods
{
    public static class UntilElementsExtensions
    {
        // 1. The next and final step is to create an extension method for all UI elements.
        // After UntilHasContent is created, it is important to be passed on to the element’s EnsureState method.
        public static TElementType ToHaveSpecificContent<TElementType>(this TElementType element, string content, int? timeoutInterval = null, int? sleepInterval = null)
         where TElementType : Element
        {
            var until = new UntilHaveSpecificContent<IOSDriver<IOSElement>, IOSElement>(content, timeoutInterval, sleepInterval);
            element.EnsureState(until);
            return element;
        }
    }
}
