using System.Diagnostics;
using Bellatrix.Playwright.Services.Browser;

namespace Bellatrix.Playwright.Services;
public class JavaScriptService : WebService
{
    public JavaScriptService(WrappedBrowser wrappedBrowser)
    : base(wrappedBrowser)
    {
    }

    public object Execute<TComponent>(string script, TComponent component, params object[] args)
        where TComponent : Component
    {
        try
        {
            return component.WrappedElement.EvaluateAsync(script, args).Result.ToString();
        }
        catch (NullReferenceException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return string.Empty;
        }
    }

    public object Execute(string script)
    {
        try
        {
            return CurrentPage.EvaluateAsync(script).Result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return string.Empty;
        }
    }

    //public string Execute(string frameName, string script)
    //{
    //    WrappedDriver.SwitchTo().Frame(frameName);
    //    var result = (string)Execute(script);
    //    WrappedDriver.SwitchTo().DefaultContent();

    //    return result;
    //}

    public object Execute(string script, params object[] args)
    {
        try
        {
            return CurrentPage.EvaluateAsync(script, args).Result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return string.Empty;
        }
    }

    public string Execute<TComponent>(string script, TComponent component)
        where TComponent : Component
    {
        return Execute(script, component.WrappedElement);
    }

    public string Execute(string script, ILocator nativeLocator)
    {
        try
        {
            return nativeLocator.EvaluateAsync(script).Result.ToString();
        }
        catch (NullReferenceException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return string.Empty;
        }
    }

    public void ExecuteAsync(string script, ILocator nativeLocator)
    {
        try
        {
            nativeLocator.EvaluateAsync(script);
        }
        catch (NullReferenceException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }
}
