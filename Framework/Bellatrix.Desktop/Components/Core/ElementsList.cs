// <copyright file="ElementsList.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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

namespace Bellatrix.Desktop.Controls.Core
{
    public class ElementsList<TElement> : IEnumerable<TElement>
        where TElement : Element
    {
        private readonly FindStrategy _by;
        private readonly WindowsElement _parentElement;
        private readonly List<TElement> _foundElements;
        private readonly bool _shouldCacheFoundElements;
        private List<TElement> _cachedElements;

        public ElementsList(
            FindStrategy by,
            WindowsElement parentElement,
            bool shouldCacheFoundElements)
        : this(by, parentElement)
        {
            _shouldCacheFoundElements = shouldCacheFoundElements;
        }

        public ElementsList(
            FindStrategy by,
            WindowsElement parentElement)
        {
            _by = by;
            _parentElement = parentElement;
            _foundElements = new List<TElement>();
            WrappedDriver = ServicesCollection.Current.Resolve<WindowsDriver<WindowsElement>>();
        }

        public ElementsList()
        {
            _foundElements = new List<TElement>();
            WrappedDriver = ServicesCollection.Current.Resolve<WindowsDriver<WindowsElement>>();
        }

        public ElementsList(IEnumerable<TElement> nativeElementList)
        {
            _foundElements = new List<TElement>(nativeElementList);
            WrappedDriver = ServicesCollection.Current.Resolve<WindowsDriver<WindowsElement>>();
        }

        public WindowsDriver<WindowsElement> WrappedDriver { get; }

        public TElement this[int i] => GetAndWaitWebDriverElements().ElementAt(i);

        public IEnumerator<TElement> GetEnumerator() => GetAndWaitWebDriverElements().GetEnumerator();

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

        public void ForEach(Action<TElement> action)
        {
            foreach (var tableRow in this)
            {
                action(tableRow);
            }
        }

        public void Add(TElement element)
        {
            _foundElements.Add(element);
        }

        private IEnumerable<TElement> GetAndWaitWebDriverElements()
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

            IEnumerable<TElement> GetAndWaitNativeElements()
            {
                foreach (var foundElement in _foundElements)
                {
                    yield return foundElement;
                }

                if (_parentElement != null)
                {
                    var elementRepository = new ElementRepository();
                    foreach (var nativeElement in _by?.FindAllElements(_parentElement))
                    {
                        var element =
                               elementRepository.CreateElementThatIsFound<TElement>(_by, (WindowsElement)nativeElement);
                        yield return element;
                    }
                }
                else
                {
                    var elementRepository = new ElementRepository();
                    foreach (var nativeElement in _by?.FindAllElements(WrappedDriver))
                    {
                        var element =
                              elementRepository.CreateElementThatIsFound<TElement>(_by, nativeElement);
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
                       var elements = _parentElement == null ? _by.FindAllElements(WrappedDriver) : _by.FindAllElements(_parentElement);
                       return elements.Any();
                   },
                   totalRunTimeoutMilliseconds: ConfigurationService.GetSection<DesktopSettings>().ElementToExistTimeout,
                   sleepTimeMilliseconds: ConfigurationService.GetSection<DesktopSettings>().SleepInterval);
            var elements = _parentElement == null ? _by.FindAllElements(WrappedDriver) : _by.FindAllElements(_parentElement);

            return elements;
        }
    }
}