using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS.GettingStarted.ExtensionMethodsLocators
{
    // 1. To ease the usage of the locator, we need to create extension methods of Element.
    // This is everything after that you can use your new locator as it was originally part of Bellatrix.
    public static class ElementCreateExtensions
    {
        public static TElement CreateByNameStartingWith<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string id)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, FindNameStartingWithStrategy>(new FindNameStartingWithStrategy(id));

        public static ElementsList<TElement, FindNameStartingWithStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByNameStartingWith<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string id)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindNameStartingWithStrategy, IOSDriver<IOSElement>, IOSElement>(new FindNameStartingWithStrategy(id), element.WrappedElement);
    }
}
