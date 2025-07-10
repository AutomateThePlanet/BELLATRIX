// <copyright file="JavaScriptService.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using System.Diagnostics;
using System.Text.Json;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.SyncPlaywright;

namespace Bellatrix.Playwright.Services;
public class JavaScriptService : WebService
{
    public JavaScriptService(WrappedBrowser wrappedBrowser)
    : base(wrappedBrowser)
    {
    }

    public TReturn Execute<TReturn, TComponent>(string script, TComponent component, params object[] args)
        where TComponent : Component
    {
        try
        {
            return component.Evaluate<TReturn>(script, args);
        }
        catch (NullReferenceException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return default;
        }
    }

    public TResult Execute<TResult>(string script)
    {
        try
        {
            return CurrentPage.Evaluate<TResult>(script);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.ToString());
            return default;
        }
    }

    public TResult Execute<TResult>(string script, params object[] args)
    {
        try
        {
            return CurrentPage.Evaluate<TResult>(script);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return default;
        }
    }

    //public string Execute(string frameName, string script)
    //{
    //    WrappedDriver.SwitchTo().Frame(frameName);
    //    var result = (string)Execute(script);
    //    WrappedDriver.SwitchTo().DefaultContent();

    //    return result;
    //}

    public JsonElement? Execute(string script, params object[] args)
    {
        try
        {
            return CurrentPage.Evaluate(script, args);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return null;
        }
    }

    public string Execute<TComponent>(string script, TComponent component)
        where TComponent : Component
    {
        return Execute<string>(script, component);
    }

    public string Execute(string script, WebElement nativeLocator)
    {
        try
        {
            return nativeLocator.Evaluate<string>(script);
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

    public T Execute<T>(string script, WebElement nativeLocator)
    {
        try
        {
            return nativeLocator.Evaluate<T>(script);
        }
        catch (NullReferenceException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return default(T);
        }
    }

    public JsonElement? Execute(string script, WebElement webElement, params object[] args)
    {
        try
        {
            return webElement.Evaluate(script, args);
        }
        catch (NullReferenceException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return null;
        }
    }

    public void ExecuteAsync(string script, WebElement webElement)
    {
        try
        {
            webElement.WrappedLocator.EvaluateAsync(script);
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
