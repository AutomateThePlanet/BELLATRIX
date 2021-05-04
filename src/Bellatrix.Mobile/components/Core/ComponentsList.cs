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
using Bellatrix.Mobile.Core;
using Bellatrix.Mobile.Locators;
using Bellatrix.Mobile.Services;
using Bellatrix.Mobile.Untils;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.Controls.Core
{
    public class ComponentsList<TElement, TBy, TDriver, TDriverElement> : IEnumerable<TElement>
        where TBy : FindStrategy<TDriver, TDriverElement>
        where TElement : Component<TDriver, TDriverElement>
        where TDriver : AppiumDriver<TDriverElement>
        where TDriverElement : AppiumWebElement
    {
        private readonly TBy _by;
        private readonly TDriverElement _mobileElement;

        public ComponentsList(TBy by, TDriverElement mobileElement)
        {
            _by = by;
            _mobileElement = mobileElement;
            WrappedDriver = ServicesCollection.Current.Resolve<TDriver>();
        }

        public TDriver WrappedDriver { get; }

        public TElement this[int i] => GetAndWaitWebDriverElements().ElementAt(i);

        public IEnumerator<TElement> GetEnumerator() => GetAndWaitWebDriverElements().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count() => GetAndWaitWebDriverElements().Count();

        private IEnumerable<TElement> GetAndWaitWebDriverElements()
        {
            var elementWaiter = new ElementWaitService<TDriver, TDriverElement>();
            elementWaiter.WaitInternal(_by, new WaitToExistStrategy<TDriver, TDriverElement>());

            var nativeElements = _mobileElement == null
                ? _by.FindAllElements(WrappedDriver)
                : _by.FindAllElements(_mobileElement);

            foreach (var nativeElement in nativeElements)
            {
                var elementRepository = new ElementRepository();
                var wrappedDriver = ServicesCollection.Current.Resolve<TDriver>();
                TDriverElement currentNativeElement;
                if (wrappedDriver is AndroidDriver<AndroidElement>)
                {
                    currentNativeElement = (TDriverElement)Activator.CreateInstance(typeof(AndroidElement), wrappedDriver, nativeElement.Id);
                }
                else
                {
                    currentNativeElement = (TDriverElement)Activator.CreateInstance(typeof(IOSElement), wrappedDriver, nativeElement.Id);
                }

                yield return elementRepository.CreateElementThatIsFound<TElement, TBy, TDriver, TDriverElement>(_by, currentNativeElement);
            }
        }
    }
}