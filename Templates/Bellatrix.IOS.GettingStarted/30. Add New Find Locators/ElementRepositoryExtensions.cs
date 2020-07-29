using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS.GettingStarted.ExtensionMethodsLocators
{
    // 1. To ease the usage of the locator, we need to create extension methods of ElementCreateService.
    // This is everything after that you can use your new locator as it was originally part of Bellatrix.
    public static class ElementRepositoryExtensions
    {
        public static TElement CreateByNameStartingWith<TElement>(this ElementCreateService repo, string id)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, ByNameStartingWith, IOSDriver<IOSElement>, IOSElement>(new ByNameStartingWith(id));

        public static ElementsList<TElement, ByNameStartingWith, IOSDriver<IOSElement>, IOSElement> CreateAllByNameStartingWith<TElement>(this ElementCreateService repo, string id)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByNameStartingWith, IOSDriver<IOSElement>, IOSElement>(new ByNameStartingWith(id), null);
    }
}
