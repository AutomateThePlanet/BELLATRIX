using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android.GettingStarted.ExtensionMethodsLocators;

// 1. To ease the usage of the locator, we need to create extension methods of Element.
// This is everything after that you can use your new locator as it was originally part of Bellatrix.
public static class ElementCreateExtensions
{
    public static TComponent CreateByIdStartingWith<TComponent>(this Component<AndroidDriver, AppiumElement> element, string id)
        where TComponent : Component<AndroidDriver, AppiumElement> => element.Create<TComponent, FindIdStartingWithStrategy>(new FindIdStartingWithStrategy(id));

    public static ComponentsList<TComponent, FindIdStartingWithStrategy, AndroidDriver, AppiumElement> CreateAllByIdStartingWith<TComponent>(this Component<AndroidDriver, AppiumElement> element, string id)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindIdStartingWithStrategy, AndroidDriver, AppiumElement>(new FindIdStartingWithStrategy(id), element.WrappedElement);
}
