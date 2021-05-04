using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS.GettingStarted.ExtensionMethodsLocators
{
    // 1. To ease the usage of the locator, we need to create extension methods of Element.
    // This is everything after that you can use your new locator as it was originally part of Bellatrix.
    public static class ElementCreateExtensions
    {
        public static TComponent CreateByNameStartingWith<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string id)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => element.Create<TComponent, FindNameStartingWithStrategy>(new FindNameStartingWithStrategy(id));

        public static ComponentsList<TComponent, FindNameStartingWithStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByNameStartingWith<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string id)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindNameStartingWithStrategy, IOSDriver<IOSElement>, IOSElement>(new FindNameStartingWithStrategy(id), element.WrappedElement);
    }
}
