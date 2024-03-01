// <copyright file="JavaScriptBy.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace Bellatrix.Web.Locators;

/// <summary>
/// Provides a mechanism by which to find elements within a document using JavaScript.
/// </summary>
public class SeleniumJavaScriptBy : OpenQA.Selenium.By
{
    private readonly string _script;
    private readonly object[] _args;

    public SeleniumJavaScriptBy(string script, params object[] args)
    {
        _script = script;
        _args = args;
        Description = "Protractor.JavaScriptBy";
    }

    public object[] AdditionalScriptArguments { get; set; }

    public override IWebElement FindElement(ISearchContext context)
    {
        ReadOnlyCollection<IWebElement> elements = FindElements(context);
        if (elements.Count == 0)
        {
            throw new NoSuchElementException($"Unable to locate element: {{ {Description} }}.");
        }

        return elements[0];
    }

    public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
    {
        object[] scriptArgs = _args;
        if (AdditionalScriptArguments != null && AdditionalScriptArguments.Length > 0)
        {
            scriptArgs = new object[_args.Length + AdditionalScriptArguments.Length];
            _args.CopyTo(scriptArgs, 0);
            AdditionalScriptArguments.CopyTo(scriptArgs, _args.Length);
        }

        var jsExecutor = context as IJavaScriptExecutor;
        if (jsExecutor == null)
        {
            var wrapsDriver = context as IWrapsDriver;
            if (wrapsDriver != null)
            {
                jsExecutor = wrapsDriver.WrappedDriver as IJavaScriptExecutor;
            }
        }

        if (jsExecutor == null)
        {
            throw new NotSupportedException("Could not get an IJavaScriptExecutor instance from the context.");
        }

        var elements = jsExecutor.ExecuteScript(_script, scriptArgs) as ReadOnlyCollection<IWebElement>;
        if (elements == null)
        {
            elements = new ReadOnlyCollection<IWebElement>(new List<IWebElement>(0));
        }

        return elements;
    }
}
