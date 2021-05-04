using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android.GettingStarted.ExtensionMethodsLocators
{
    // 1. To ease the usage of the locator, we need to create extension methods of Element.
    // This is everything after that you can use your new locator as it was originally part of Bellatrix.
    public static class ElementCreateExtensions
    {
        public static TElement CreateByIdStartingWith<TElement>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, FindIdStartingWithStrategy>(new FindIdStartingWithStrategy(id));

        public static ComponentsList<TElement, FindIdStartingWithStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByIdStartingWith<TElement>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TElement, FindIdStartingWithStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdStartingWithStrategy(id), element.WrappedElement);
    }
}
