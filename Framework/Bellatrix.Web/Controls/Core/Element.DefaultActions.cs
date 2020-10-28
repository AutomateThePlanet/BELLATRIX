// <copyright file="Element.DefaultActions.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using Bellatrix.Web.Events;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Web
{
    public partial class Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Point Location => WrappedElement.Location;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Size Size => WrappedElement.Size;

        public string GetCssValue(string propertyName) => WrappedElement.GetCssValue(propertyName);

        protected virtual void DefaultSetAttribute(Element element, string attributeName, string attributeValue)
        {
            SettingAttribute?.Invoke(this, new ElementActionEventArgs(element));

            JavaScriptService.Execute(
                $"arguments[0].setAttribute('{attributeName}', '{attributeValue}');",
                element);

            AttributeSet?.Invoke(this, new ElementActionEventArgs(element));
        }

        protected virtual bool DefaultIsPresent(Element element)
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

        protected virtual bool DefaultIsVisible(Element element)
        {
            if (IsPresent)
            {
                return WrappedElement.Displayed;
            }

            return false;
        }

        protected virtual string DefaultGetAttribute(Element element, string name)
        {
            var result = WrappedElement.GetAttribute(name);
            return result;
        }

        protected virtual string DefaultCssClass(Element element) => string.IsNullOrEmpty(WrappedElement.GetAttribute("class")) ? null : WrappedElement.GetAttribute("class");

        protected virtual void DefaultScrollToVisible()
        {
            DefaultScrollToVisible(true);
        }

        internal void DefaultClick<TElement>(
            TElement element,
            EventHandler<ElementActionEventArgs> clicking,
            EventHandler<ElementActionEventArgs> clicked)
            where TElement : Element
        {
            clicking?.Invoke(this, new ElementActionEventArgs(element));

            element.ToExists().ToBeClickable().WaitToBe();
            JavaScriptService.Execute("arguments[0].focus();arguments[0].click();", element);

            clicked?.Invoke(this, new ElementActionEventArgs(element));
        }

        internal void DefaultFocus(Element element)
        {
            Focusing?.Invoke(this, new ElementActionEventArgs(element));

            var remoteElement = (RemoteWebElement)element.ToBeClickable().WrappedElement;

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

            Focused?.Invoke(this, new ElementActionEventArgs(element));
        }

        internal void DefaultFocusJavaScript<TElement>(TElement element, EventHandler<ElementActionEventArgs> focusing, EventHandler<ElementActionEventArgs> focused)
            where TElement : Element
        {
            focusing?.Invoke(this, new ElementActionEventArgs(element));

            if (WrappedWebDriverCreateService.BrowserConfiguration.BrowserType != BrowserType.Opera)
            {
                JavaScriptService.Execute("arguments[0].focus();", element);
            }
            else
            {
                element.ToBeClickable().WrappedElement.SendKeys(string.Empty);
            }

            focused?.Invoke(this, new ElementActionEventArgs(element));
        }

        internal void DefaultHover<TElement>(TElement element, EventHandler<ElementActionEventArgs> hovering, EventHandler<ElementActionEventArgs> hovered)
            where TElement : Element
        {
            hovering?.Invoke(this, new ElementActionEventArgs(element));

            JavaScriptService.Execute("arguments[0].onmouseover();", element);

            hovered?.Invoke(this, new ElementActionEventArgs(element));
        }

        internal string DefaultInnerText<TElement>(TElement element)
            where TElement : Element
            => WrappedElement.Text.Replace("\r\n", string.Empty);

        internal void DefaultSetValue<TElement>(TElement element, EventHandler<ElementActionEventArgs> gettingValue, EventHandler<ElementActionEventArgs> gotValue, string value)
            where TElement : Element
        {
            gettingValue?.Invoke(this, new ElementActionEventArgs(element, value));
            SetAttribute("value", value);
            gotValue?.Invoke(this, new ElementActionEventArgs(element, value));
        }

        internal string DefaultGetValue<TElement>(TElement element)
            where TElement : Element
        => WrappedElement.GetAttribute("value");

        internal int? DefaultGetMaxLength<TElement>(TElement element)
            where TElement : Element
        {
            int? result = string.IsNullOrEmpty(GetAttribute("maxlength")) ? null : (int?)int.Parse(GetAttribute("maxlength"));
            if (result != null && (result == 2147483647 || result == -1))
            {
                result = null;
            }

            return result;
        }

        internal int? DefaultGetMinLength<TElement>(TElement element)
            where TElement : Element
        {
            int? result = string.IsNullOrEmpty(GetAttribute("minlength")) ? null : (int?)int.Parse(GetAttribute("minlength"));

            if (result != null && result == -1)
            {
                result = null;
            }

            return result;
        }

        internal int? DefaultGetSize<TElement>(TElement element)
            where TElement : Element
            => string.IsNullOrEmpty(GetAttribute("size")) ? null : (int?)int.Parse(GetAttribute("size"));

        internal int? DefaultGetHeight<TElement>(TElement element)
            where TElement : Element
            => string.IsNullOrEmpty(GetAttribute("height")) ? null : (int?)int.Parse(GetAttribute("height"));

        internal int? DefaultGetWidth<TElement>(TElement element)
            where TElement : Element
            => string.IsNullOrEmpty(GetAttribute("width")) ? null : (int?)int.Parse(GetAttribute("width"));

        internal string DefaultInnerHtml<TElement>(TElement element)
            where TElement : Element
            => WrappedElement.GetAttribute("innerHTML");

        internal string DefaultGetFor<TElement>(TElement output)
            where TElement : Element
            => string.IsNullOrEmpty(output.GetAttribute("for")) ? null : output.GetAttribute("for");

        protected bool DefaultIsDisabled<TElement>(TElement element)
            where TElement : Element
        {
            string valueAttr = WrappedElement.GetAttribute("disabled");
            return valueAttr == "true";
        }

        internal string DefaultGetText<TElement>(TElement element)
            where TElement : Element
            => WrappedElement.Text;

        internal int? DefaultGetMin<TElement>(TElement element)
            where TElement : Element
            => string.IsNullOrEmpty(GetAttribute("min")) ? null : (int?)int.Parse(GetAttribute("min"));

        internal int? DefaultGetMax<TElement>(TElement element)
            where TElement : Element
            => string.IsNullOrEmpty(GetAttribute("max")) ? null : (int?)int.Parse(GetAttribute("max"));

        internal string DefaultGetMinAsString<TElement>(TElement element)
            where TElement : Element
            => string.IsNullOrEmpty(GetAttribute("min")) ? null : GetAttribute("min");

        internal string DefaultGetMaxAsString<TElement>(TElement element)
            where TElement : Element
            => string.IsNullOrEmpty(GetAttribute("max")) ? null : GetAttribute("max");

        internal int? DefaultGetStep<TElement>(TElement element)
            where TElement : Element
            => string.IsNullOrEmpty(GetAttribute("step")) ? null : (int?)int.Parse(GetAttribute("step"));

        internal string DefaultGetPlaceholder<TElement>(TElement element)
            where TElement : Element
            => string.IsNullOrEmpty(GetAttribute("placeholder")) ? null : GetAttribute("placeholder");

        internal bool DefaultGetAutoComplete<TElement>(TElement element)
            where TElement : Element
            => GetAttribute("autocomplete") == "on";

        internal bool DefaultGetReadonly<TElement>(TElement element)
            where TElement : Element
            => !string.IsNullOrEmpty(GetAttribute("readonly"));

        internal bool DefaultGetRequired<TElement>(TElement element)
            where TElement : Element
            => !string.IsNullOrEmpty(GetAttribute("required"));

        internal string DefaultGetList<TElement>(TElement element) => string.IsNullOrEmpty(GetAttribute("list")) ? null : GetAttribute("list");

        internal void DefaultSetText<TElement>(TElement element, EventHandler<ElementActionEventArgs> settingValue, EventHandler<ElementActionEventArgs> valueSet, string value)
            where TElement : Element
        {
            settingValue?.Invoke(this, new ElementActionEventArgs(element, value));

            // HACK: (Anton: 06.09.2018): InternetExplorer SendKeys is typing everything twice.
            if (!WrappedWebDriverCreateService.BrowserConfiguration.BrowserType.Equals(BrowserType.InternetExplorer))
            {
                Thread.Sleep(50);

                // (Anton: 11.09.2019): We do this for optimization purposes so that we don't make two WebDriver calls.
                var wrappedElement = WrappedElement;
                try
                {
                    wrappedElement.Clear();
                }
                catch
                {
                    // ignore
                }

                Thread.Sleep(50);

                wrappedElement.SendKeys(value);
            }
            else
            {
                SetAttribute("value", value);
            }

            valueSet?.Invoke(this, new ElementActionEventArgs(element, value));
        }

        private void DefaultScrollToVisible(bool shouldWait = true)
        {
            ScrollingToVisible?.Invoke(this, new ElementActionEventArgs(this));
            try
            {
                var wrappedElement = _wrappedElement ?? WrappedElement;
                JavaScriptService.Execute("arguments[0].scrollIntoView(true);", wrappedElement);
                if (shouldWait)
                {
                    Thread.Sleep(500);
                    this.ToExists().WaitToBe();
                }
            }
            catch (ElementNotInteractableException)
            {
                // ignore
            }

            ScrolledToVisible?.Invoke(this, new ElementActionEventArgs(this));
        }
    }
}