using System;
using System.Diagnostics;
using System.Reflection;

namespace Bellatrix.Playwright.Services;
public class ComponentRepository
{
    public dynamic CreateComponentWithParent(FindStrategy by, ILocator parenTComponent, Type newElementType, bool shouldCacheElement)
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        dynamic element = Activator.CreateInstance(newElementType);
        element.By = by;
        element.ParentWrappedElement = parenTComponent;
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;
        element.ShouldCacheElement = shouldCacheElement;

        return element;
    }

    public TComponentType CreateComponentWithParent<TComponentType>(FindStrategy by, ILocator parenTComponent, ILocator foundElement, int elementsIndex, bool shouldCacheElement)
        where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponentType>();
        element.By = by;
        element.ParentWrappedElement = parenTComponent;
        element.WrappedElement = foundElement;
        element.ElementIndex = elementsIndex;
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;
        element.ShouldCacheElement = shouldCacheElement;

        return element;
    }

    public dynamic CreateComponentThatIsFound(FindStrategy by, ILocator webElement, Type newElementType, bool shouldCacheElement)
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        dynamic element = Activator.CreateInstance(newElementType);
        element.By = by;
        element.WrappedElement = webElement;
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;
        element.ShouldCacheElement = shouldCacheElement;

        return element;
    }

    public TComponentType CreateComponentThatIsFound<TComponentType>(FindStrategy by, ILocator webElement, bool shouldCacheElement)
        where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponentType>();
        element.By = by;
        element.WrappedElement = webElement;
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;
        element.ShouldCacheElement = shouldCacheElement;

        return element;
    }

    private void DetermineComponentAttributes(out string elementName, out string pageName)
    {
        elementName = string.Empty;
        pageName = string.Empty;
        try
        {
            var callStackTrace = new StackTrace();
            var currentAssembly = GetType().Assembly;

            foreach (var frame in callStackTrace.GetFrames())
            {
                var frameMethodInfo = frame.GetMethod() as MethodInfo;
                if (!frameMethodInfo?.ReflectedType?.Assembly.Equals(currentAssembly) == true &&
                    !frameMethodInfo.IsStatic &&
                    frameMethodInfo.ReturnType.IsSubclassOf(typeof(Component)))
                {
                    elementName = frame.GetMethod().Name.Replace("get_", string.Empty);
                    if (frameMethodInfo.ReflectedType.IsSubclassOf(typeof(WebPage)))
                    {
                        pageName = frameMethodInfo.ReflectedType.Name;
                    }

                    break;
                }
            }
        }
        catch (Exception e)
        {
            e.PrintStackTrace();
        }
    }
}
