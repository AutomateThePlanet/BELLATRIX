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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bellatrix.Desktop.Services;
using Bellatrix.Desktop.Untils;
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop.Controls.Core
{
    public class ElementsList<TElement> : IEnumerable<TElement>
        where TElement : Element
    {
        private readonly Locators.FindStrategy _by;
        private readonly WindowsElement _windowsElement;

        public ElementsList(
            Locators.FindStrategy by,
            WindowsElement windowsElement)
        {
            _by = by;
            _windowsElement = windowsElement;
            WrappedDriver = ServicesCollection.Current.Resolve<WindowsDriver<WindowsElement>>();
        }

        public WindowsDriver<WindowsElement> WrappedDriver { get; }

        public TElement this[int i] => GetAndWaitWebDriverElements().ElementAt(i);

        public IEnumerator<TElement> GetEnumerator() => GetAndWaitWebDriverElements().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count() => GetAndWaitWebDriverElements().Count();

        private IEnumerable<TElement> GetAndWaitWebDriverElements()
        {
            var elementWaiter = new ElementWaitService();
            elementWaiter.WaitInternal(_by, Wait.To.Exists());

            var nativeElements = _windowsElement == null
                ? _by.FindAllElements(WrappedDriver)
                : _by.FindAllElements(_windowsElement);

            foreach (var nativeElement in nativeElements)
            {
                var elementRepository = new ElementRepository();
                yield return elementRepository.CreateElementThatIsFound<TElement>(_by, new WindowsElement(WrappedDriver, nativeElement.Id));
            }
        }
    }
}