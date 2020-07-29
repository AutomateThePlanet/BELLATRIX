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
using System.Text;
using System.Threading;
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;
using Bellatrix.Web.Untils;
using Bellatrix.Web.Waits;
using OpenQA.Selenium;

namespace Bellatrix.Web
{
    [DebuggerDisplay("BELLATRIX Element")]
    public partial class Element : IElementVisible, IElementCssClass, IElement, IWebLayoutElement
    {
        public static Func<Element, string, string> OverrideGetAttributeGlobally;
        public static Func<Element, string> OverrideCssClassGlobally;
        public static Func<Element, bool> OverrideIsVisibleGlobally;
        public static Func<Element, bool> OverrideIsPresentGlobally;
        public static Action OverrideScrollToVisibleGlobally;
        public static Action<Element, string, string> OverrideSetAttributeGlobally;

        public static Func<Element, string, string> OverrideGetAttributeLocally;
        public static Func<Element, string> OverrideCssClassLocally;
        public static Func<Element, bool> OverrideIsVisibleLocally;
        public static Func<Element, bool> OverrideIsPresentLocally;
        public static Action OverrideScrollToVisibleLocally;
        public static Action<Element, string, string> OverrideSetAttributeLocally;

        private readonly ElementWaitService _elementWaiter;
        private readonly List<BaseUntil> _untils;
        private IWebElement _wrappedElement;

        public string TagName => WrappedElement.TagName;

        public Element()
        {
            _elementWaiter = new ElementWaitService();
            WrappedDriver = ServicesCollection.Current.Resolve<IWebDriver>();
            _untils = new List<BaseUntil>();
            JavaScriptService = ServicesCollection.Current.Resolve<JavaScriptService>();
            BrowserService = ServicesCollection.Current.Resolve<BrowserService>();
            ElementCreateService = ServicesCollection.Current.Resolve<ElementCreateService>();
        }

        // ReSharper disable All
#pragma warning disable 67
        public static event EventHandler<ElementActionEventArgs> ScrollingToVisible;
        public static event EventHandler<ElementActionEventArgs> ScrolledToVisible;
        public static event EventHandler<ElementActionEventArgs> SettingAttribute;
        public static event EventHandler<ElementActionEventArgs> AttributeSet;
#pragma warning restore 67

        // ReSharper restore All
        public static event EventHandler<ElementActionEventArgs> CreatingElement;
        public static event EventHandler<ElementActionEventArgs> CreatedElement;
        public static event EventHandler<ElementActionEventArgs> CreatingElements;
        public static event EventHandler<ElementActionEventArgs> CreatedElements;
        public static event EventHandler<NativeElementActionEventArgs> ReturningWrappedElement;

        public IWebDriver WrappedDriver { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IWebElement WrappedElement
        {
            get
            {
                var element = GetAndWaitWebDriverElement(ShouldCacheElement);
                ReturningWrappedElement?.Invoke(this, new NativeElementActionEventArgs(element));
                if (element == null)
                {
                    throw new NullReferenceException($"The element with locator {By.Value} was not found. Probably you should change the locator or wait for a specific condition.");
                }

                return element;
            }
            set => _wrappedElement = value;
        }

        public IWebElement ParentWrappedElement { get; set; }
        public int ElementIndex { get; set; }
        internal bool ShouldCacheElement { get; set; }

        protected readonly JavaScriptService JavaScriptService;
        protected readonly BrowserService BrowserService;
        protected readonly ElementCreateService ElementCreateService;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public dynamic By { get; internal set; }

        public static void ClearLocalOverrides()
        {
            OverrideGetAttributeLocally = null;
            OverrideCssClassLocally = null;
            OverrideIsVisibleLocally = null;
            OverrideIsPresentLocally = null;
            OverrideScrollToVisibleLocally = null;
            OverrideSetAttributeLocally = null;
        }

        public string GetTitle() => string.IsNullOrEmpty(GetAttribute("title")) ? null : GetAttribute("title");

        public string GetTabIndex() => string.IsNullOrEmpty(GetAttribute("tabindex")) ? null : GetAttribute("tabindex");

        public string GetAccessKey() => string.IsNullOrEmpty(GetAttribute("accesskey")) ? null : GetAttribute("accesskey");

        public string GetStyle() => string.IsNullOrEmpty(GetAttribute("style")) ? null : GetAttribute("style");

        public string GetDir() => string.IsNullOrEmpty(GetAttribute("dir")) ? null : GetAttribute("dir");

        public string GetLang() => string.IsNullOrEmpty(GetAttribute("lang")) ? null : GetAttribute("lang");

        public dynamic Create<TBy>(TBy by, Type newElementType)
            where TBy : By
        {
            CreatingElement?.Invoke(this, new ElementActionEventArgs(this));

            Debug.WriteLine(by.Value);
            var elementRepository = new ElementRepository();
            var element = elementRepository.CreateElementWithParent(by, WrappedElement, newElementType, ShouldCacheElement);

            CreatedElement?.Invoke(this, new ElementActionEventArgs(this));

            return element;
        }

        public TElement Create<TElement, TBy>(TBy by, bool shouldCacheElement = false)
            where TBy : By
            where TElement : Element
        {
            CreatingElement?.Invoke(this, new ElementActionEventArgs(this));

            Debug.WriteLine(by.Value);
            var elementRepository = new ElementRepository();
            var element = elementRepository.CreateElementWithParent<TElement>(by, WrappedElement, null, 0, shouldCacheElement);

            CreatedElement?.Invoke(this, new ElementActionEventArgs(this));

            return element;
        }

        public ElementsList<TElement> CreateAll<TElement, TBy>(TBy by)
            where TBy : By
            where TElement : Element
        {
            CreatingElements?.Invoke(this, new ElementActionEventArgs(this));

            var elementsCollection = new ElementsList<TElement>(by, WrappedElement, ShouldCacheElement);

            CreatedElements?.Invoke(this, new ElementActionEventArgs(this));

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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string CssClass
        {
            get
            {
                var action = InitializeAction(this, OverrideCssClassGlobally, OverrideCssClassLocally, DefaultCssClass);
                return action();
            }
        }

        public string GetAttribute(string name)
        {
            var action = InitializeFunction(this, OverrideGetAttributeGlobally, OverrideGetAttributeLocally, DefaultGetAttribute);
            return action(name);
        }

        public void ScrollToVisible()
        {
            var action = InitializeAction(OverrideScrollToVisibleGlobally, OverrideScrollToVisibleLocally, DefaultScrollToVisible);
            action();
        }

        public void SetAttribute(string name, string value)
        {
            var action = InitializeAction(this, OverrideSetAttributeGlobally, OverrideSetAttributeLocally, DefaultSetAttribute);
            action(name, value);
        }

        public string ElementName { get; internal set; }

        public string PageName { get; internal set; }

        public virtual Type ElementType => GetType();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Type LocatorType => By.GetType();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string LocatorValue => By.Value;

        public void EnsureState(BaseUntil until) => _untils.Add(until);

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

        private IWebElement GetAndWaitWebDriverElement(bool shouldCacheElement = false)
        {
            if (_wrappedElement != null && shouldCacheElement)
            {
                return _wrappedElement;
            }

            if (_untils.Count == 0 || _untils[0] == null)
            {
                EnsureState(Until.To.Exists());
            }

            try
            {
                foreach (var until in _untils)
                {
                    if (until != null)
                    {
                        _elementWaiter.Wait(this, until);
                    }

                    if (until != null && until.GetType() == typeof(UntilNotExist))
                    {
                        return _wrappedElement;
                    }
                }

                _wrappedElement = GetWebDriverElement(shouldCacheElement);

                ScrollToMakeElementVisible();
                if (ConfigurationService.Instance.GetWebSettings().ShouldWaitUntilReadyOnElementFound)
                {
                    BrowserService.WaitUntilReady();
                }

                if (ConfigurationService.Instance.GetWebSettings().ShouldWaitForAngular)
                {
                    BrowserService.WaitForAngular();
                }

                AddArtificialDelay();

                _untils.Clear();
            }
            catch (WebDriverTimeoutException)
            {
                throw new TimeoutException($"\n\nThe element: \n Name: '{ElementName}', \n Locator: '{LocatorType.Name} = {LocatorValue}', \n Type: '{ElementType.Name}' \nWas not found on the page or didn't fulfill the specified conditions.\n\n");
            }

            return _wrappedElement;
        }

        private void AddArtificialDelay()
        {
            if (WrappedWebDriverCreateService.BrowserSettings.ArtificialDelayBeforeAction != 0)
            {
                Thread.Sleep(WrappedWebDriverCreateService.BrowserSettings.ArtificialDelayBeforeAction);
            }
        }

        private void ScrollToMakeElementVisible()
        {
            // By default scroll down to make the element visible.
            if (WrappedWebDriverCreateService.BrowserConfiguration.ShouldAutomaticallyScrollToVisible)
            {
                DefaultScrollToVisible(false);
            }
        }

        private IWebElement GetWebDriverElement(bool shouldCacheElement = false)
        {
            if (_wrappedElement != null && shouldCacheElement)
            {
                return _wrappedElement;
            }

            if (ParentWrappedElement == null && _wrappedElement == null)
            {
                var nativeElementFinderService = new NativeElementFinderService(WrappedDriver);
                return nativeElementFinderService.FindAll(By)[ElementIndex];
            }

            if (ParentWrappedElement != null)
            {
                var nativeElementFinderService = new NativeElementFinderService(ParentWrappedElement);
                return nativeElementFinderService.FindAll(By)[ElementIndex];
            }

            return _wrappedElement;
        }
    }
}