// <copyright file="Element.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Events;
using Bellatrix.Mobile.Locators;
using Bellatrix.Mobile.Services;
using Bellatrix.Mobile.Untils;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile.Core
{
    [DebuggerDisplay("BELLATRIX Element")]
    public partial class Element<TDriver, TDriverElement> : IElement<TDriverElement>
        where TDriver : AppiumDriver<TDriverElement>
        where TDriverElement : AppiumWebElement
    {
        public static Func<Element<TDriver, TDriverElement>, string, string> OverrideGetAttributeGlobally;
        public static Func<Element<TDriver, TDriverElement>, bool> OverrideIsVisibleGlobally;
        public static Func<Element<TDriver, TDriverElement>, bool> OverrideIsPresentGlobally;
        public static Func<Element<TDriver, TDriverElement>, ScrollDirection, bool> OverrideScrollToVisibleGlobally;

        public static Func<Element<TDriver, TDriverElement>, string, string> OverrideGetAttributeLocally;
        public static Func<Element<TDriver, TDriverElement>, bool> OverrideIsVisibleLocally;
        public static Func<Element<TDriver, TDriverElement>, bool> OverrideIsPresentLocally;
        public static Func<Element<TDriver, TDriverElement>, ScrollDirection, bool> OverrideScrollToVisibleLocally;

        private readonly ElementWaitService<TDriver, TDriverElement> _elementWait;
        private readonly List<BaseUntil<TDriver, TDriverElement>> _untils;
        private TDriverElement _wrappedElement;

        public Element()
        {
            _elementWait = new ElementWaitService<TDriver, TDriverElement>();
            WrappedDriver = ServicesCollection.Current.Resolve<TDriver>();
            _untils = new List<BaseUntil<TDriver, TDriverElement>>();
        }

        // ReSharper disable All
        #pragma warning disable 67
        public static event EventHandler<ElementActionEventArgs<TDriverElement>> ScrollingToVisible;
        public static event EventHandler<ElementActionEventArgs<TDriverElement>> ScrolledToVisible;
        #pragma warning restore 67

        // ReSharper restore All
        public static event EventHandler<ElementActionEventArgs<TDriverElement>> CreatingElement;
        public static event EventHandler<ElementActionEventArgs<TDriverElement>> CreatedElement;
        public static event EventHandler<ElementActionEventArgs<TDriverElement>> CreatingElements;
        public static event EventHandler<ElementActionEventArgs<TDriverElement>> CreatedElements;
        public static event EventHandler<NativeElementActionEventArgs<TDriverElement>> ReturningWrappedElement;

        public TDriver WrappedDriver { get; }

        public TDriverElement WrappedElement
        {
            get
            {
                ReturningWrappedElement?.Invoke(this, new NativeElementActionEventArgs<TDriverElement>(GetAndWaitWebDriverElement()));
                return GetAndWaitWebDriverElement(true);
            }
            internal set => _wrappedElement = value;
        }

        public TDriverElement ParentWrappedElement { get; set; }

        public TDriverElement FoundWrappedElement { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public dynamic By { get; internal set; }

        public static void ClearLocalOverrides()
        {
            OverrideGetAttributeLocally = null;
            OverrideIsVisibleLocally = null;
            OverrideIsPresentLocally = null;
            OverrideScrollToVisibleLocally = null;
        }

        public string GetAttribute(string name)
        {
            var action = InitializeFunction<Element<TDriver, TDriverElement>, string, string>(this, OverrideGetAttributeGlobally, OverrideGetAttributeLocally, DefaultGetAttribute);
            return action(name);
        }

        public TElement Create<TElement, TBy>(TBy by)
            where TBy : By<TDriver, TDriverElement>
            where TElement : Element<TDriver, TDriverElement>
        {
            CreatingElement?.Invoke(this, new ElementActionEventArgs<TDriverElement>(this));

            var nativeElement = GetAndWaitWebDriverElement();
            var elementRepository = new ElementRepository();
            var element = elementRepository.CreateElementWithParent<TElement, TBy, TDriver, TDriverElement>(by, nativeElement);

            CreatedElement?.Invoke(this, new ElementActionEventArgs<TDriverElement>(this));

            return element;
        }

        public ElementsList<TElement, TBy, TDriver, TDriverElement> CreateAll<TElement, TBy>(TBy by)
            where TBy : By<TDriver, TDriverElement>
            where TElement : Element<TDriver, TDriverElement>
        {
            CreatingElements?.Invoke(this, new ElementActionEventArgs<TDriverElement>(this));
            TDriverElement nativeElement = null;
            try
            {
                nativeElement = GetWebDriverElement();
            }
            catch (InvalidOperationException)
            {
                // Ignore
            }

            var elementsCollection = new ElementsList<TElement, TBy, TDriver, TDriverElement>(by, nativeElement);

            CreatedElements?.Invoke(this, new ElementActionEventArgs<TDriverElement>(this));

            return elementsCollection;
        }

        public void WaitToBe() => GetAndWaitWebDriverElement(true);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsPresent
        {
            get
            {
                var action = InitializeAction(this, OverrideIsPresentGlobally, OverrideIsPresentLocally, DefaultIsPresent);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsVisible
        {
            get
            {
                var action = InitializeAction(this, OverrideIsVisibleGlobally, OverrideIsVisibleLocally, DefaultIsVisible);
                return action();
            }
        }

        public void ScrollToVisible(ScrollDirection direction)
        {
            var action = InitializeFunction(this, OverrideScrollToVisibleGlobally, OverrideScrollToVisibleLocally, DefaultScrollToVisible);
            action(direction);
        }

        public string ElementName { get; internal set; }

        public string PageName { get; internal set; }

        public Point Location => WrappedElement.Location;

        public Size Size => WrappedElement.Size;

        public void EnsureState(BaseUntil<TDriver, TDriverElement> until) => _untils.Add(until);

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{ElementName}");
            sb.AppendLine($"X = {Location.X}");
            sb.AppendLine($"Y = {Location.Y}");
            sb.AppendLine($"Height = {Size.Height}");
            sb.AppendLine($"Width = {Size.Width}");
            return sb.ToString();
        }

        protected TDriverElement GetAndWaitWebDriverElement(bool shouldRefresh = false)
        {
            if (_wrappedElement == null || shouldRefresh)
            {
                if (_untils.Count == 0 || _untils[0] == null)
                {
                    EnsureState(new UntilExist<TDriver, TDriverElement>());
                }

                try
                {
                    foreach (var until in _untils)
                    {
                        if (until != null)
                        {
                            _elementWait.Wait(this, until);
                        }

                        if (until.GetType().Equals(typeof(UntilNotExist<TDriver, TDriverElement>)))
                        {
                            return _wrappedElement;
                        }
                    }

                    _wrappedElement = GetWebDriverElement(shouldRefresh);
                }
                catch (WebDriverTimeoutException ex)
                {
                    throw new TimeoutException($"The element with Name = {ElementName} Locator {By.Value} was not found on the page or didn't fulfill the specified conditions.", ex);
                }
            }

            _untils.Clear();

            return _wrappedElement;
        }

        private TDriverElement GetWebDriverElement(bool shouldRefresh = false)
        {
            if (!shouldRefresh && _wrappedElement != null)
            {
                return _wrappedElement;
            }

            if (ParentWrappedElement == null && FoundWrappedElement == null)
            {
                return By.FindElement(WrappedDriver);
            }

            if (ParentWrappedElement != null)
            {
                return By.FindElement(ParentWrappedElement);
            }

            if (FoundWrappedElement != null)
            {
                return FoundWrappedElement;
            }

            return _wrappedElement;
        }
    }
}
