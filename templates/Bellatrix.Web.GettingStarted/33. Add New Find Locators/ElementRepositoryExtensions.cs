namespace Bellatrix.Web.GettingStarted.ExtensionMethodsLocators;

// 1. To ease the usage of the locator, we need to create extension methods of ComponentCreateService.
// This is everything after that you can use your new locator as it was originally part of Bellatrix.
public static class ElementRepositoryExtensions
{
    public static TComponent CreateByIdStartingWith<TComponent>(this ComponentCreateService repository, string idPrefix, bool shouldCache = false)
        where TComponent : Component => repository.Create<TComponent, FindIdStartingWithStrategy>(new FindIdStartingWithStrategy(idPrefix), shouldCache);

    public static ComponentsList<TComponent> CreateAllByIdStartingWith<TComponent>(this ComponentCreateService repository, string idPrefix)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdStartingWithStrategy(idPrefix), null);
}