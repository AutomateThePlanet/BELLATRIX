using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android.GettingStarted.ExtensionMethodsLocators;

// 1. To ease the usage of the locator, we need to create extension methods of ComponentCreateService.
// This is everything after that you can use your new locator as it was originally part of Bellatrix.
public static class ElementRepositoryExtensions
{
    public static TComponent CreateByIdStartingWith<TComponent>(this ComponentCreateService repo, string id)
        where TComponent : Component<AndroidDriver, AppiumElement> => repo.Create<TComponent, FindIdStartingWithStrategy, AndroidDriver, AppiumElement>(new FindIdStartingWithStrategy(id));

    public static ComponentsList<TComponent, FindIdStartingWithStrategy, AndroidDriver, AppiumElement> CreateAllByIdStartingWith<TComponent>(this ComponentCreateService repo, string id)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindIdStartingWithStrategy, AndroidDriver, AppiumElement>(new FindIdStartingWithStrategy(id), null);
}