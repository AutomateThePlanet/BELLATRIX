// <copyright file="ElementsList.cs" company="Automate The Planet Ltd.">
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bellatrix.Desktop.Configuration;
using Bellatrix.Desktop.Locators;
using Bellatrix.Desktop.Services;
using Bellatrix.Desktop.Untils;
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop.Controls.Core;

public class ComponentsList<TComponent> : IEnumerable<TComponent>
    where TComponent : Component
{
    private readonly FindStrategy _by;
    private readonly WindowsElement _parenTComponent;
    private readonly List<TComponent> _foundElements;
    private readonly bool _shouldCacheFoundElements;
    private List<TComponent> _cachedElements;

    public ComponentsList(
        FindStrategy by,
        WindowsElement parenTComponent,
        bool shouldCacheFoundElements)
    : this(by, parenTComponent)
    {
        _shouldCacheFoundElements = shouldCacheFoundElements;
    }

    public ComponentsList(
        FindStrategy by,
        WindowsElement parenTComponent)
    {
        _by = by;
        _parenTComponent = parenTComponent;
        _foundElements = new List<TComponent>();
        WrappedDriver = ServicesCollection.Current.Resolve<WindowsDriver<WindowsElement>>();
    }

    public ComponentsList()
    {
        _foundElements = new List<TComponent>();
        WrappedDriver = ServicesCollection.Current.Resolve<WindowsDriver<WindowsElement>>();
    }

    public ComponentsList(IEnumerable<TComponent> nativeElementList)
    {
        _foundElements = new List<TComponent>(nativeElementList);
        WrappedDriver = ServicesCollection.Current.Resolve<WindowsDriver<WindowsElement>>();
    }

    public WindowsDriver<WindowsElement> WrappedDriver { get; }

    public TComponent this[int i] => GetAndWaitWebDriverElements().ElementAt(i);

    public IEnumerator<TComponent> GetEnumerator() => GetAndWaitWebDriverElements().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count()
    {
        if (_by == null)
        {
            return _foundElements.Count;
        }

        var nativeElements = WaitWebDriverElements();
        return nativeElements.Count();
    }

    public void ForEach(Action<TComponent> action)
    {
        foreach (var tableRow in this)
        {
            action(tableRow);
        }
    }

    public void Add(TComponent element)
    {
        _foundElements.Add(element);
    }

    private IEnumerable<TComponent> GetAndWaitWebDriverElements()
    {
        if (_shouldCacheFoundElements && _cachedElements == null)
        {
            _cachedElements = GetAndWaitNativeElements().ToList();
        }

        if (_shouldCacheFoundElements && _cachedElements != null)
        {
            foreach (var element in _cachedElements)
            {
                yield return element;
            }
        }
        else
        {
            foreach (var element in GetAndWaitNativeElements())
            {
                yield return element;
            }
        }

        IEnumerable<TComponent> GetAndWaitNativeElements()
        {
            foreach (var foundElement in _foundElements)
            {
                yield return foundElement;
            }

            if (_parenTComponent != null)
            {
                var elementRepository = new ComponentsRepository();
                foreach (var nativeElement in _by?.FindAllElements(_parenTComponent))
                {
                    var element =
                           elementRepository.CreateComponentThatIsFound<TComponent>(_by, (WindowsElement)nativeElement);
                    yield return element;
                }
            }
            else
            {
                var elementRepository = new ComponentsRepository();
                foreach (var nativeElement in _by?.FindAllElements(WrappedDriver))
                {
                    var element =
                          elementRepository.CreateComponentThatIsFound<TComponent>(_by, nativeElement);
                    yield return element;
                }
            }
        }
    }

    // TODO: This is a technical debt, for the case with _by that is not null and we want to verify the non-existance of elements
    public dynamic WaitWebDriverElements()
    {
        Utilities.Wait.ForConditionUntilTimeout(
               () =>
               {
                   var elements = _parenTComponent == null ? _by.FindAllElements(WrappedDriver) : _by.FindAllElements(_parenTComponent);
                   return elements.Any();
               },
               totalRunTimeoutMilliseconds: ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.ElementToExistTimeout,
               sleepTimeMilliseconds: ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.SleepInterval);
        var elements = _parenTComponent == null ? _by.FindAllElements(WrappedDriver) : _by.FindAllElements(_parenTComponent);

        return elements;
    }
}