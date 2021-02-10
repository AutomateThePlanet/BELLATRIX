// <copyright file="ElementsList.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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

namespace Bellatrix.Web
{
    public class ElementsList<TElement> : IEnumerable<TElement>
        where TElement : Element
    {
        private readonly FindStrategy _by;
        private readonly IWebElement _parentElement;
        private readonly List<TElement> _foundElements;
        private readonly bool _shouldCacheFoundElements;
        private List<TElement> _cachedElements;

        public ElementsList(
            FindStrategy by,
            IWebElement parentElement,
            bool shouldCacheFoundElements)
        : this(by, parentElement)
        {
            _shouldCacheFoundElements = shouldCacheFoundElements;
        }

        public ElementsList(
            FindStrategy by,
            IWebElement parentElement)
        {
            _by = by;
            _parentElement = parentElement;
            _foundElements = new List<TElement>();
            WrappedDriver = ServicesCollection.Current.Resolve<IWebDriver>();
        }

        public ElementsList()
        {
            _foundElements = new List<TElement>();
            WrappedDriver = ServicesCollection.Current.Resolve<IWebDriver>();
        }

        public ElementsList(IEnumerable<TElement> nativeElementList)
        {
            _foundElements = new List<TElement>(nativeElementList);
            WrappedDriver = ServicesCollection.Current.Resolve<IWebDriver>();
        }

        public IWebDriver WrappedDriver { get; }

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

                if (_by != null)
                {
                    var nativeElements = WaitWebDriverElements();
                    int index = 0;
                    foreach (var nativeElement in nativeElements)
                    {
                        var elementRepository = new ElementRepository();
                        if (_parentElement != null)
                        {
                            var element =
                                elementRepository.CreateElementWithParent<TElement>(_by, _parentElement, nativeElement, index++, _shouldCacheFoundElements);
                            yield return element;
                        }
                        else
                        {
                            var element =
                                elementRepository.CreateElementThatIsFound<TElement>(_by, nativeElement, _shouldCacheFoundElements);
                            yield return element;
                        }
                    }
                }
            }
        }

        private IEnumerable<IWebElement> WaitWebDriverElements()
        {
            var elementFinder = _parentElement == null
                ? new NativeElementFinderService(WrappedDriver)
                : new NativeElementFinderService(_parentElement);
            var elementWaiter = new ElementWaitService();
            if (_parentElement == null)
            {
                return ConditionalWait(elementFinder);
            }
            else
            {
                var elementRepository = new ElementRepository();
                var parentElement = elementRepository.CreateElementThatIsFound<Element>(_by, _parentElement, true);

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
                   totalRunTimeoutMilliseconds: SettingsService.GetSection<TimeoutSettings>().ElementToExistTimeout,
                   sleepTimeMilliseconds: SettingsService.GetSection<TimeoutSettings>().SleepInterval);

            return elementFinder.FindAll(_by);
        }
    }
}