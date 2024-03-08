namespace Bellatrix.Playwright.Services;
public class ComponentCreateService
{
    public TComponent Create<TComponent, TBy>(TBy by, bool shouldCacheElement)
        where TBy : FindStrategy
        where TComponent : Component
    {
        return ServicesCollection.Current.Resolve<ComponentRepository>().CreateComponentThatIsFound<TComponent>(by, null, shouldCacheElement);
    }

    public ComponentsList<TComponent> CreateAll<TComponent, TBy>(TBy by, bool shouldCacheElements)
        where TBy : FindStrategy
        where TComponent : Component
    {
        return new ComponentsList<TComponent>(by, null, shouldCacheElements);
    }
}
