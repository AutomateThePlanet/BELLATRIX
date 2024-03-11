// <copyright file="JavaScriptService.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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
