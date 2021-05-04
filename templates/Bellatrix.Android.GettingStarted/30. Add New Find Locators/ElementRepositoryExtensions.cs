using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android.GettingStarted.ExtensionMethodsLocators
{
    // 1. To ease the usage of the locator, we need to create extension methods of ElementCreateService.
    // This is everything after that you can use your new locator as it was originally part of Bellatrix.
    public static class ElementRepositoryExtensions
    {
        public static TElement CreateByIdStartingWith<TElement>(this ElementCreateService repo, string id)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, FindIdStartingWithStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdStartingWithStrategy(id));

        public static ComponentsList<TElement, FindIdStartingWithStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByIdStartingWith<TElement>(this ElementCreateService repo, string id)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TElement, FindIdStartingWithStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdStartingWithStrategy(id), null);
    }
}