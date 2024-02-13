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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Diagnostics;
using OpenQA.Selenium;

namespace Bellatrix.Web;

public class JavaScriptService : WebService
{
    public JavaScriptService(IWebDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public object Execute<TComponent>(string script, TComponent element, params object[] args)
        where TComponent : Component
    {
        try
        {
            var javaScriptExecutor = WrappedDriver as IJavaScriptExecutor;
            var result = javaScriptExecutor?.ExecuteScript(script, element.WrappedElement, args);

            return result;
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
            var javaScriptExecutor = WrappedDriver as IJavaScriptExecutor;
            var result = javaScriptExecutor?.ExecuteScript(script);

            return result?.ToString();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return string.Empty;
        }
    }

    public string Execute(string frameName, string script)
    {
        WrappedDriver.SwitchTo().Frame(frameName);
        var result = (string)Execute(script);
        WrappedDriver.SwitchTo().DefaultContent();

        return result;
    }

    public string Execute(string script, params object[] args)
    {
        try
        {
            var javaScriptExecutor = WrappedDriver as IJavaScriptExecutor;
            var result = javaScriptExecutor?.ExecuteScript(script, args);

            return result?.ToString();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return string.Empty;
        }
    }

    public string Execute<TComponent>(string script, TComponent element)
        where TComponent : Component
    {
        try
        {
            var javaScriptExecutor = WrappedDriver as IJavaScriptExecutor;
            var result = javaScriptExecutor?.ExecuteScript(script, element.WrappedElement);

            return result?.ToString();
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

    public string Execute(string script, IWebElement nativeElement)
    {
        try
        {
            var javaScriptExecutor = WrappedDriver as IJavaScriptExecutor;
            var result = javaScriptExecutor?.ExecuteScript(script, nativeElement);

            return result?.ToString();
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

    public void ExecuteAsync(string script, IWebElement nativeElement)
    {
        try
        {
            var javaScriptExecutor = WrappedDriver as IJavaScriptExecutor;
            javaScriptExecutor?.ExecuteAsyncScript(script, nativeElement);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }
}