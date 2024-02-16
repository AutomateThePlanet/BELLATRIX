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
using Bellatrix.Mobile.Core;
using Bellatrix.Mobile.Locators;
using Bellatrix.Mobile.Services;
using Bellatrix.Mobile.Untils;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.Controls.Core;

public class ComponentsList<TComponent, TBy, TDriver, TDriverElement> : IEnumerable<TComponent>
    where TBy : FindStrategy<TDriver, TDriverElement>
    where TComponent : Component<TDriver, TDriverElement>
    where TDriver : AppiumDriver
    where TDriverElement : AppiumElement
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

    public TComponent this[int i] => GetAndWaitWebDriverElements().ElementAt(i);

    public IEnumerator<TComponent> GetEnumerator() => GetAndWaitWebDriverElements().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count() => GetAndWaitWebDriverElements().Count();

    private IEnumerable<TComponent> GetAndWaitWebDriverElements()
    {
        var elementWaiter = new ComponentWaitService<TDriver, TDriverElement>();
        elementWaiter.WaitInternal(_by, new WaitToExistStrategy<TDriver, TDriverElement>());

        var nativeElements = _mobileElement == null
            ? _by.FindAllElements(WrappedDriver)
            : _by.FindAllElements(_mobileElement);

        foreach (var nativeElement in nativeElements)
        {
            var elementRepository = new ComponentRepository();
            var wrappedDriver = ServicesCollection.Current.Resolve<TDriver>();
            TDriverElement currentNativeElement;
            if (wrappedDriver is AndroidDriver)
            {
                currentNativeElement = (TDriverElement)Activator.CreateInstance(typeof(AppiumElement), wrappedDriver, nativeElement.Id);
            }
            else
            {
                currentNativeElement = (TDriverElement)Activator.CreateInstance(typeof(AppiumElement), wrappedDriver, nativeElement.Id);
            }

            yield return elementRepository.CreateComponentThatIsFound<TComponent, TBy, TDriver, TDriverElement>(_by, currentNativeElement);
        }
    }
}