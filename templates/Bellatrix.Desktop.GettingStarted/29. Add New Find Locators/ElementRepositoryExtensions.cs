using Bellatrix.Desktop.Controls.Core;

namespace Bellatrix.Desktop.GettingStarted.ExtensionMethodsLocators;

// 1. To ease the usage of the locator, we need to create extension methods of ComponentCreateService.
// This is everything after that you can use your new locator as it was originally part of Bellatrix.
public static class ElementRepositoryExtensions
{
    public static TComponent CreateByNameStartingWith<TComponent>(this ComponentCreateService repo, string tag)
        where TComponent : Component => repo.Create<TComponent, FindNameStartingWithStrategy>(new FindNameStartingWithStrategy(tag));

    public static ComponentsList<TComponent> CreateAllByNameStartingWith<TComponent>(this ComponentCreateService repo, string tag)
        where TComponent : Component => new ComponentsList<TComponent>(new FindNameStartingWithStrategy(tag), null);
}
