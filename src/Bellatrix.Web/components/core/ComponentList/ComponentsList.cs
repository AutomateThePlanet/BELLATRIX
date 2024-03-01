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
using Bellatrix.Utilities;
using Bellatrix.Web.Waits;
using OpenQA.Selenium;

namespace Bellatrix.Web;

public class ComponentsList<TComponent> : IEnumerable<TComponent>
    where TComponent : Component
{
    private readonly FindStrategy _by;
    private readonly IWebElement _parenTComponent;
    private readonly List<TComponent> _foundElements;
    private readonly bool _shouldCacheFoundElements;
    private List<TComponent> _cachedElements;

    public ComponentsList(
        FindStrategy by,
        IWebElement parenTComponent,
        bool shouldCacheFoundElements)
    : this(by, parenTComponent)
    {
        _shouldCacheFoundElements = shouldCacheFoundElements;
    }

    public ComponentsList(
        FindStrategy by,
        IWebElement parenTComponent)
    {
        _by = by;
        _parenTComponent = parenTComponent;
        _foundElements = new List<TComponent>();
        WrappedDriver = ServicesCollection.Current.Resolve<IWebDriver>();
    }

    public ComponentsList()
    {
        _foundElements = new List<TComponent>();
        WrappedDriver = ServicesCollection.Current.Resolve<IWebDriver>();
    }

    public ComponentsList(IEnumerable<TComponent> nativeElementList)
    {
        _foundElements = new List<TComponent>(nativeElementList);
        WrappedDriver = ServicesCollection.Current.Resolve<IWebDriver>();
    }

    public IWebDriver WrappedDriver { get; }

    public TComponent this[int i]
    {
        get
        {
            try
            {
                return GetAndWaitWebDriverElements().ElementAt(i);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new Exception($"Component at position {i} was not found.", ex);
            }
        }
    }

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

            if (_by != null)
            {
                var nativeElements = WaitWebDriverElements();
                int index = 0;
                foreach (var nativeElement in nativeElements)
                {
                    var elementRepository = new ComponentRepository();
                    if (_parenTComponent != null)
                    {
                        var element =
                            elementRepository.CreateComponentWithParent<TComponent>(_by, _parenTComponent, nativeElement, index++, _shouldCacheFoundElements);
                        yield return element;
                    }
                    else
                    {
                        var element =
                            elementRepository.CreateComponentThatIsFound<TComponent>(_by, nativeElement, _shouldCacheFoundElements);
                        yield return element;
                    }
                }
            }
        }
    }

    private IEnumerable<IWebElement> WaitWebDriverElements()
    {
        var elementFinder = _parenTComponent == null
            ? new NativeElementFinderService(WrappedDriver)
            : new NativeElementFinderService(_parenTComponent);
        var elementWaiter = new ComponentWaitService();
        if (_parenTComponent == null)
        {
            return ConditionalWait(elementFinder);
        }
        else
        {
            var elementRepository = new ComponentRepository();
            var parenTComponent = elementRepository.CreateComponentThatIsFound<Component>(_by, _parenTComponent, true);

            return ConditionalWait(elementFinder);
        }
    }

    // TODO: This is a technical debt, for the case with _by that is not null and we want to verify the non-existance of elements
    public IEnumerable<IWebElement> ConditionalWait(NativeElementFinderService elementFinder)
    {
        Bellatrix.Utilities.Wait.ForConditionUntilTimeout(
               () =>
               {
                   return elementFinder.FindAll(_by).Any();
               },
               totalRunTimeoutMilliseconds: ConfigurationService.GetSection<WebSettings>().TimeoutSettings.ElementToExistTimeout,
               sleepTimeMilliseconds: ConfigurationService.GetSection<WebSettings>().TimeoutSettings.SleepInterval);

        return elementFinder.FindAll(_by);
    }

    public void AddRange(List<TComponent> currentFilteredCells)
    {
        _foundElements.AddRange(currentFilteredCells);
    }
}