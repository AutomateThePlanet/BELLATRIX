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
using Bellatrix.Web.Configuration;
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;
using Bellatrix.Web.Untils;
using Bellatrix.Web.Waits;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Web
{
    [DebuggerDisplay("BELLATRIX Element")]
    public partial class Element : IElementVisible, IElementCssClass, IElement, IWebLayoutElement
    {
        public static event EventHandler<ElementActionEventArgs> Focusing;
        public static event EventHandler<ElementActionEventArgs> Focused;

        private readonly ElementWaitService _elementWaiter;
        private readonly List<WaitStrategy> _untils;
        private IWebElement _wrappedElement;

        public string TagName => WrappedElement.TagName;

        public Element()
        {
            _elementWaiter = new ElementWaitService();
            WrappedDriver = ServicesCollection.Current.Resolve<IWebDriver>();
            _untils = new List<WaitStrategy>();
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

        public string GetTitle() => string.IsNullOrEmpty(GetAttribute("title")) ? null : GetAttribute("title");

        public string GetTabIndex() => string.IsNullOrEmpty(GetAttribute("tabindex")) ? null : GetAttribute("tabindex");

        public string GetAccessKey() => string.IsNullOrEmpty(GetAttribute("accesskey")) ? null : GetAttribute("accesskey");

        public string GetStyle() => string.IsNullOrEmpty(GetAttribute("style")) ? null : GetAttribute("style");

        public string GetDir() => string.IsNullOrEmpty(GetAttribute("dir")) ? null : GetAttribute("dir");

        public string GetLang() => string.IsNullOrEmpty(GetAttribute("lang")) ? null : GetAttribute("lang");

        public dynamic Create<TBy>(TBy by, Type newElementType)
            where TBy : FindStrategy
        {
            CreatingElement?.Invoke(this, new ElementActionEventArgs(this));

            var elementRepository = new ElementRepository();
            var element = elementRepository.CreateElementWithParent(by, WrappedElement, newElementType, ShouldCacheElement);

            CreatedElement?.Invoke(this, new ElementActionEventArgs(this));

            return element;
        }

        public TElement Create<TElement, TBy>(TBy by, bool shouldCacheElement = false)
            where TBy : FindStrategy
            where TElement : Element
        {
            CreatingElement?.Invoke(this, new ElementActionEventArgs(this));

            var elementRepository = new ElementRepository();
            var element = elementRepository.CreateElementWithParent<TElement>(by, WrappedElement, null, 0, shouldCacheElement);

            CreatedElement?.Invoke(this, new ElementActionEventArgs(this));

            return element;
        }

        public ElementsList<TElement> CreateAll<TElement, TBy>(TBy by)
            where TBy : FindStrategy
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
                try
                {
                    return GetAndWaitWebDriverElement(false) != null;
                }
                catch
                {
                    return false;
                }
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsVisible
        {
            get
            {
                if (IsPresent)
                {
                    return WrappedElement.Displayed;
                }

                return false;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string CssClass
        {
            get
            {
                return GetClassAttribute();
            }
        }

        public string GetAttribute(string name)
        {
            return WrappedElement.GetAttribute(name);
        }

        public void ScrollToVisible()
        {
            ScrollToVisible(true);
        }

        public void SetAttribute(string name, string value)
        {
            SettingAttribute?.Invoke(this, new ElementActionEventArgs(this));

            JavaScriptService.Execute(
                $"arguments[0].setAttribute('{name}', '{name}');", this);

            AttributeSet?.Invoke(this, new ElementActionEventArgs(this));
        }

        public void Focus()
        {
            Focusing?.Invoke(this, new ElementActionEventArgs(this));

            var remoteElement = (RemoteWebElement)this.ToBeClickable().WrappedElement;

            // ReSharper disable once UnusedVariable
            if (WrappedWebDriverCreateService.BrowserConfiguration.BrowserType != BrowserType.Opera)
            {
                JavaScriptService.Execute("window.focus();");
                JavaScriptService.Execute("arguments[0].focus();", WrappedElement);
            }
            else
            {
                var hack = remoteElement.LocationOnScreenOnceScrolledIntoView;
                remoteElement.SendKeys(Keys.Space);
            }

            Focused?.Invoke(this, new ElementActionEventArgs(this));
        }

        public string ElementName { get; internal set; }

        public string PageName { get; internal set; }

        public virtual Type ElementType => GetType();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Type LocatorType => By.GetType();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string LocatorValue => By.Value;

        public void EnsureState(WaitStrategy until)
        {
            _untils.Add(until);
        }

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
                EnsureState(Wait.To.Exists());
            }

            try
            {
                foreach (var until in _untils)
                {
                    if (until != null)
                    {
                        _elementWaiter.Wait(this, until);
                    }

                    if (until != null && until.GetType() == typeof(WaitNotToExistStrategy))
                    {
                        return _wrappedElement;
                    }
                }

                _wrappedElement = GetWebDriverElement(shouldCacheElement);

                ScrollToMakeElementVisible();
                if (ConfigurationService.GetSection<WebSettings>().ShouldWaitUntilReadyOnElementFound)
                {
                    BrowserService.WaitUntilReady();
                }

                if (ConfigurationService.GetSection<WebSettings>().ShouldWaitForAngular)
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
                ScrollToVisible(false);
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