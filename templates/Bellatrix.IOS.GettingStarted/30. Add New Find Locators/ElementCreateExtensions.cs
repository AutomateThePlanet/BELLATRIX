using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS.GettingStarted.ExtensionMethodsLocators;

// 1. To ease the usage of the locator, we need to create extension methods of Element.
// This is everything after that you can use your new locator as it was originally part of Bellatrix.
public static class ElementCreateExtensions
{
    public static TComponent CreateByNameStartingWith<TComponent>(this Component<IOSDriver, AppiumElement> element, string id)
        where TComponent : Component<IOSDriver, AppiumElement> => element.Create<TComponent, FindNameStartingWithStrategy>(new FindNameStartingWithStrategy(id));

    public static ComponentsList<TComponent, FindNameStartingWithStrategy, IOSDriver, AppiumElement> CreateAllByNameStartingWith<TComponent>(this Component<IOSDriver, AppiumElement> element, string id)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindNameStartingWithStrategy, IOSDriver, AppiumElement>(new FindNameStartingWithStrategy(id), element.WrappedElement);
}
