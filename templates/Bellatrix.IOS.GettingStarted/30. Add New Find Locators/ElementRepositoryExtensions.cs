using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS.GettingStarted.ExtensionMethodsLocators;

// 1. To ease the usage of the locator, we need to create extension methods of ComponentCreateService.
// This is everything after that you can use your new locator as it was originally part of Bellatrix.
public static class ElementRepositoryExtensions
{
    public static TComponent CreateByNameStartingWith<TComponent>(this ComponentCreateService repo, string id)
        where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => repo.Create<TComponent, FindNameStartingWithStrategy, IOSDriver<IOSElement>, IOSElement>(new FindNameStartingWithStrategy(id));

    public static ComponentsList<TComponent, FindNameStartingWithStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByNameStartingWith<TComponent>(this ComponentCreateService repo, string id)
        where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindNameStartingWithStrategy, IOSDriver<IOSElement>, IOSElement>(new FindNameStartingWithStrategy(id), null);
}
